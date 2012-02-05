module CliteParser where

import CliteSyntax
import CliteLexer 
import Prelude 
import Monad

-------------------------------------------------------------------
-- The primitive parser combinators follow those of the paper:   --
--                                                               --
--     Daan Leijen, Erik Meijer                                  --
--                                                               --
--     Parsec: Direct Style Monadic Parser Combinators           --
--     For The Real World                                        --
-------------------------------------------------------------------

--------------------------------------
-- Parser type                      --
--------------------------------------

newtype Parser a = P (ParseState -> Consumed a)

-- The top-level parser reply is either "Consumed" or "Empty",
-- depending on whether the parser consumed any input or not This is
-- used for restricting lookahead when trying different parses.  The
-- following table shows how the "consumption" flag is being updated
-- by bind

-- p        | q        | (p >>= q) 
------------------------------
-- Empty    | Empty    | Empty 
-- Empty    | Consumed | Consumed 
-- Consumed | Empty    | Consumed 
-- Consumed | Consumed | Consumed 

data Consumed a = Consumed (Reply a) 
                | Empty (Reply a) 

-- Reply is the the result of a parse
--  a is typically AST
--  ParseState maintains the parse state: current position and 
--  the list of Tokens yet to parse.
--  Message is explained below.
data Reply a = Ok a ParseState Message 
             | Error Message 


-- ParseState is remaining tokens to be parsed, and the current position
data ParseState = ParseState [Token] Pos

-- First String is a string representation of the unexpected token
-- List of strings are string representations of productions that would 
-- be possible
data Message = Message Pos String [String]

----------------------------------------
-- Parse position in the token stream --
----------------------------------------

getPos :: ParseState -> Pos
getPos (ParseState _ p) = p

nextPos :: Pos -> Token -> Pos
nextPos p (TokPos newp t) = newp
nextPos p _ = p


---------------------------
-- Access parsed program --
---------------------------

isParseOk :: Consumed a -> Bool
isParseOk (Consumed (Ok _ _ _)) = True
isParseOk (Empty (Ok _ _ _)) = True
isParseOk _ = False

getResult :: Consumed a -> a
getResult (Consumed (Ok x _ _)) = x
getResult (Empty (Ok x _ _)) = x

getMessage :: Consumed a -> Message
getMessage (Consumed (Error msg)) = msg
getMessage (Empty (Error msg)) = msg

-----------------------------------
-- Showing parser error messages --
-----------------------------------

showParseMessage :: Message -> String
showParseMessage (Message p s ls) 
    = show p ++ ": parse error: " ++ 
      "\nunexpected \"" ++ s ++ "\"\n" ++
      "expecting one of " ++ foldr1 (\x y -> x ++ ", " ++ y) ls ++ "\n"



------------------
-- Parser monad --
------------------

instance Monad Parser where
    return v = P (\state ->
                      Empty (Ok v state (Message dummyPos [] [])))

    p >>= f = bind p f


bind :: Parser a -> (a -> Parser b) -> Parser b
bind p f = P (\state -> case (parse p state) of
                       Empty reply1 -> case (reply1) of
                                         Ok x st _ -> (parse (f x) st)
                                         Error msg -> Empty (Error msg)

                          -- Consumed results are wrapped into "Consumed" 
                          -- immediately, which delays applying
                          -- the parser (f x) until it is really needed, if ever
                       Consumed reply1 -> 
                           Consumed
                           (case (reply1) of
                              Ok x st _ -> case (parse (f x) st) of
                                             Consumed reply2 -> reply2
                                             Empty reply2 -> reply2
                              Error msg -> Error msg
                           )
            )



-------------------
-- Invoke parser --
-------------------

--  parse inp with parser P p
parse :: Parser a -> ParseState -> Consumed a
parse (P p) inp = p inp

-----------------------
-- Choice combinator --
-----------------------

-- If p consumes input, return p's result whether Ok or Error
-- if p does not consume input and if q consumes input, return q's result
-- if q does not consume input, combine messages of p and q
(<|>) :: Parser a -> Parser a -> Parser a 
p <|> q = P (\state -> 
                 case (parse p state) of 
                   Empty (Error msg1) 
                     -> case (parse q state) of 
                          Empty (Error msg2) 
                              -> mergeError msg1 msg2 
                          Empty (Ok x inp msg2) 
                              -> mergeOk x inp msg1 msg2 
                          consumed 
                              -> consumed 
                   Empty (Ok x inp msg1) 
                     -> case (parse q state) of 
                          Empty (Error msg2) 
                              -> mergeOk x inp msg1 msg2 
                          Empty (Ok _ _ msg2) 
                              -> mergeOk x inp msg1 msg2 
                          consumed 
                              -> consumed 
                   consumed -> consumed)
             where
               mergeOk x inp msg1 msg2 
                   = Empty (Ok x inp (merge msg1 msg2)) 
               mergeError msg1 msg2 
                   = Empty (Error (merge msg1 msg2)) 
               merge (Message pos inp exp1) (Message _ _ exp2) 
                   = Message pos inp (exp1 ++ exp2) 


---------------------------------------------------------------------------
-- Combinator to add labels for productions to be used in error messages --
---------------------------------------------------------------------------

-- bind a string describing a production to be used as in the error message
-- in a failed parse 
-- if exp == "a", error message will look something like "Expecting a"
(<?>) :: Parser a -> String -> Parser a
p <?> exp
  = P (\state ->
           case (parse p state) of 
             Empty (Error msg) -> Empty (Error (expect msg exp))
             Empty (Ok x st msg) -> Empty (Ok x st (expect msg exp))
             other -> other)
       where
         expect (Message pos inp _) exp = Message pos inp [exp]

--------------------
-- Useful parsers --
--------------------

many :: Parser a -> Parser [a]
many p = many1 p <|> return []

many1 :: Parser a -> Parser [a]
many1 p = p >>= \v ->
          many p >>= \vs ->
          return (v:vs)



-------------------------------------------------------------------------------
-- Clite parsers                                                             --
--                                                                           --
-- We assume that tokens may or may not be wrapped within TokPos constructor --
-- The unwrap function unwraps a token until something other                 --
-- than TokPos is returned                                                   --
-------------------------------------------------------------------------------


satisfy :: (Token -> Bool) -> Parser Token
satisfy test 
    = P (\(ParseState input pos) ->
             case (input) of
               (c:cs) | test c 
                   -> let newPos = nextPos pos c
                          newState = ParseState cs newPos
                      in seq newPos -- evaluate newPos strictly
                          (Consumed
                           (Ok c newState 
                               (Message newPos [] [])))
               (c:cs) -> let newPos = nextPos pos c 
--                              note that we deviate from Leijen's original 
--                              handling of position. We want to show the position of the token that fails,
--                              so we get the position of the first unsuccesful token
                         in Empty (Error (Message newPos (show c) []))
               [] -> Empty (Error (Message pos "end of input" [])))
                                
                                 

commaSep1 :: Parser AST -> Parser [AST]
commaSep1 p = p >>= \v -> 
              many (aToken TokComma >> p) >>= \vs ->
              return (v:vs)

aToken :: Token -> Parser Token
aToken tok = (satisfy (\t -> (unwrap t) == tok)) <?> tokToString tok

program :: Parser Program
program = do
    aToken TokInt 
    aToken TokMain
    aToken TokOpenParen
    aToken TokCloseParen
    aToken TokOpenCurly
    stmts <- many statementOrDeclaration
    aToken TokCloseCurly
    return $ Program (concat stmts)


declaration :: Parser [AST]
declaration = do
  tyNode <- typeId
  let (ASTType ty) = unwrap tyNode
  decl <- commaSep1 (declarator ty)
  aToken TokSemicolon
  return decl

declarator :: Type -> Parser AST
declarator ty = do
  tok <- identifier
  let var = tokToString (unwrap tok)
  (
   do aToken TokOpenBracket
      tok2 <- satisfy (isIntLit . unwrap) <?> "int literal"
      let i = case unwrap tok2 of TokIntLit i -> i
      aToken TokCloseBracket
      return $ ASTPos (pos tok) $ ASTVariableDecl var (TyArray ty i)
    ) <|> (return $ ASTPos (pos tok) $ ASTVariableDecl var ty)
      where 
        isIntLit (TokIntLit _) = True
        isIntLit _ = False

typeId :: Parser AST
typeId = do { tyTok <- (aToken TokInt <|>
                        aToken TokChar <|> 
                        aToken TokFloat <|>
                        aToken TokBool)
            ; return $ ASTPos (pos tyTok) $ ASTType (tokenToType (unwrap tyTok))
            } <?> "type"

statementOrDeclaration :: Parser [AST]
statementOrDeclaration = (many1 statement <|> declaration)


statement :: Parser AST
statement = ifStatement <|> whileStatement <|> block <|> skip <|> assignmentOrCall
                       
ifStatement :: Parser AST
ifStatement = do
  start <- aToken TokIf 
  aToken TokOpenParen
  cond <- expression
  aToken TokCloseParen
  thenp <- statement
  elsep <- ((aToken TokElse >> statement) <|> return ASTSkip) 
  return $ ASTPos (pos start) $ ASTConditional cond thenp elsep

whileStatement :: Parser AST
whileStatement = do
  start <- aToken TokWhile
  aToken TokOpenParen
  cond <- expression
  aToken TokCloseParen
  body <- statement
  return $ ASTPos (pos start) $ ASTLoop cond body

block :: Parser AST
block = do
  start <- aToken TokOpenCurly
  stmts <- many statementOrDeclaration 
  aToken TokCloseCurly
  return $ ASTPos (pos start) $ ASTBlock (concat stmts)

skip :: Parser AST
skip = do t <- aToken TokSemicolon 
          return $ ASTPos (pos t) ASTSkip
              

assignmentOrCall :: Parser AST
assignmentOrCall = do
  tok <- identifier
  ( (arrayRefCont tok >>= assignmentCont) <|> 
    assignmentCont (ASTPos (pos tok) $ ASTVariableRef (tokToString (unwrap tok))) <|>
    do { res <- callCont tok ; aToken TokSemicolon ; return res })
        
-- AssignmentCont ::= VariableRef = Expression ;
assignmentCont :: AST -> Parser AST
assignmentCont lh = do
  aToken TokAssign
  rh <- expression
  aToken TokSemicolon
  return $ ASTPos (pos lh) $ ASTAssignment lh rh

callCont :: Token -> Parser AST
callCont tok = do
  aToken TokOpenParen -- only 1 arg funcs supported for now
  e <- expression
  aToken TokCloseParen
  return $ ASTPos (pos tok) $ ASTFuncCall (tokToString (unwrap tok)) 1 [e] 


-- Expression ::= Conjunction { || Conjunction }
expression :: Parser AST
expression =  conjunction  >>= \lh ->
              (
               aToken TokOr >>= \op ->
               expression >>= \rh ->
               return $ ASTPos (pos op) $ ASTOr lh rh
              ) <|> return lh

-- Conjunction ::= Equality { && Equality }
conjunction :: Parser AST
conjunction =  equality  >>= \lh ->
              (
               aToken TokAnd >>= \op ->
               conjunction >>= \rh ->
               return $ ASTPos (pos op) $ ASTAnd lh rh
              ) <|> return lh

-- Equality ::= Relation { EquOp Relation }
-- EquOp ::= == | !=
equality :: Parser AST
equality =  relation  >>= \lh ->
              (
               (aToken TokEqual <|> aToken TokNotEqual) >>= \op ->
               equality >>= \rh ->
               return $ ASTPos (pos op) $ ASTFuncCall
                          (tokToString (unwrap op)) 2 [lh, rh]
              ) <|> return lh

-- Relation ::= Addition { RelOp Addition }
-- EquOp ::= <= | < | >= | >
relation :: Parser AST
relation =  addition  >>= \lh ->
              (
               (aToken TokLessThanOrEqual <|> 
                aToken TokLessThan <|>
                aToken TokGreaterThanOrEqual <|>
                aToken TokGreaterThan) >>= \op ->
               relation >>= \rh ->
               return $ ASTPos (pos op) $ ASTFuncCall
                          (tokToString (unwrap op)) 2 [lh, rh]
              ) <|> return lh


-- Addition ::= Term { AddOp Term }
-- AddOp ::= + | -
addition :: Parser AST
addition =  term >>= \lh ->
              (
               (aToken TokPlus <|> aToken TokMinus) >>= \op ->
               addition >>= \rh ->
               return $ ASTPos (pos op) $ ASTFuncCall
                          (tokToString (unwrap op)) 2 [lh, rh]
              ) <|> return lh

-- Term ::= Factor { MulOp Factor }
-- MulOp ::= * | / | %
term :: Parser AST
term =  factor >>= \lh ->  
              (
               (aToken TokTimes <|> 
                aToken TokDivides <|> 
                aToken TokRemainder) >>= \op ->
               factor >>= \rh ->
               return $ ASTPos (pos op) $ ASTFuncCall 
                          (tokToString (unwrap op)) 2 [lh, rh]
              ) <|> return lh


-- Factor ::= [ UnaryOp ] Primary
-- UnaryOp ::= - | !
factor :: Parser AST
factor =  do unop <- (aToken TokMinus) <|> (aToken TokPlus) <|> (aToken TokNot) <|> return TokEmpty
             fac <- primary
             if unop == TokEmpty
              then return fac
              else 
                return $ ASTPos (pos unop) $ ASTFuncCall
                           (tokToString (unwrap unop)) 1 [fac]

-- Primary ::= Identifier | Literal | (Expression)                   
primary :: Parser AST
primary = variableRefOrCall <|> 
          literal <|>
          parenExpression <|>
          cast

parenExpression :: Parser AST
parenExpression = do { aToken TokOpenParen
                     ; e <- expression 
                     ; aToken TokCloseParen
                     ; return e 
                     } 

cast :: Parser AST
cast = do tyNode <- typeId
          let (ASTType ty) = unwrap tyNode
          aToken TokOpenParen
          e <- expression
          aToken TokCloseParen
          return $ ASTPos (pos tyNode) $ ASTCast ty e

arrayRefCont :: Token -> Parser AST
arrayRefCont tok = do 
  aToken TokOpenBracket
  e <- expression
  aToken TokCloseBracket
  return $ ASTPos (pos tok) $ ASTArrayRef (tokToString (unwrap tok)) e

variableRefOrCall :: Parser AST
variableRefOrCall = do 
  tok <- identifier
  let var = tokToString (unwrap tok)
  ( callCont tok <|> 
    arrayRefCont tok <|> 
    (return $ ASTPos (pos tok) $ ASTVariableRef (tokToString (unwrap tok))) )


identifier :: Parser Token
identifier = satisfy (isIdent . unwrap) <?> "identifier"
    where 
      isIdent (TokIdent s) = True
      isIdent _ = False


literal :: Parser AST
literal = intLiteral <|> floatLiteral <|> charLiteral <|> boolLiteral

intLiteral :: Parser AST          
intLiteral = do { tok <- satisfy (isIntLit . unwrap)
                ; let i = case unwrap tok of TokIntLit i -> i
                ; return $ ASTPos (pos tok) $ ASTLiteral (VInt i)
                } 
             <?> "int-literal"
    where 
      isIntLit (TokIntLit _) = True
      isIntLit _ = False

floatLiteral :: Parser AST
floatLiteral = do { tok <- satisfy (isFloatLit . unwrap)
                  ; let i = case unwrap tok of TokFloatLit i -> i
                  ; return $ ASTPos (pos tok) $ ASTLiteral (VFloat i)
                  } 
               <?> "float-literal"
    where 
      isFloatLit (TokFloatLit _) = True
      isFloatLit _ = False

charLiteral :: Parser AST
charLiteral = do { tok <- satisfy (isCharLit . unwrap)
                 ; let i = case unwrap tok of TokCharLit i -> i
                 ; return $ ASTPos (pos tok) $ ASTLiteral (VChar i)
                 } 
              <?> "char-literal"
    where 
      isCharLit (TokCharLit _) = True
      isCharLit _ = False

boolLiteral :: Parser AST
boolLiteral = do { tok <- (aToken TokFalse <|> aToken TokTrue)
                 ; let b = case (unwrap tok) of
                             TokFalse -> False
                             TokTrue -> True
                 ; return $ ASTPos (pos tok) $ ASTLiteral (VBool b)
                 }



