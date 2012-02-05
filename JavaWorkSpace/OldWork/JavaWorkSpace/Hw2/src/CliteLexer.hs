--Andrew Johnson
module CliteLexer (lexer, newPos) where

import Char (isSpace, isAlpha, isDigit, isUpper, isLower, isAlphaNum, digitToInt)

-- The above are likely useful in your implementation

--------------------------
-- Data type for tokens --
--------------------------

data Token
    = TokIdent String
    | TokIntLit Int
    | TokFloatLit Float
    | TokCharLit Char
-- keywords
    | TokBool | TokChar | TokElse | TokFalse | TokFloat 
    | TokIf | TokInt | TokMain | TokTrue | TokWhile

-- operators
    | TokOr | TokAnd | TokEqual | TokNotEqual | TokLessThanOrEqual 
    | TokGreaterThanOrEqual | TokLessThan | TokGreaterThan 
    | TokAssign | TokPlus | TokMinus | TokTimes | TokDivides | TokReminder 
    | TokNot | TokOpenBracket
    | TokCloseBracket 
 
-- separators
    | TokSemicolon | TokComma | TokOpenCurly | TokCloseCurly 
    | TokOpenParen | TokCloseParen 

-- special
    | TokPos Pos Token |TokFillIn
      deriving Show


-------------------------------------------
-- Helper functions for detecting tokens --
-------------------------------------------

-- convert a reserved word or sequence of symbols to a Token
reserved :: String -> Token
reserved ")" = TokCloseParen
reserved "(" = TokOpenParen
reserved "}" = TokCloseCurly
reserved "{" = TokOpenCurly
reserved "," = TokComma
reserved ";" = TokSemicolon
reserved "]" = TokCloseBracket
reserved "[" = TokOpenBracket
reserved "!" = TokNot
reserved "%" = TokReminder
reserved "/" = TokDivides
reserved "*" = TokTimes
reserved "-" = TokMinus
reserved "+" = TokPlus
reserved "=" = TokAssign
reserved ">" = TokGreaterThan
reserved "<" = TokLessThan
reserved ">=" = TokGreaterThanOrEqual
reserved "<=" = TokLessThanOrEqual
reserved "||" = TokOr
reserved "&&" = TokAnd
reserved "==" = TokEqual
reserved "!=" = TokNotEqual
reserved "bool" = TokBool
reserved "char" = TokChar
reserved "else" = TokElse
reserved "false" = TokFalse
reserved "float" = TokFloat
reserved "if" = TokIf
reserved "int" = TokInt
reserved "main" = TokMain
reserved "true" = TokTrue
reserved "while" = TokWhile


-- isOp1 s is True if x is one of: = < > = + - * / ! [ ] %
isOp1 :: String -> Bool
isOp1 (x:xs) = x=='<'||x=='='||x=='>'||x=='+'||x=='-'||x=='*'||x=='/'||x=='!'||x=='['||x==']'||x=='%'
                    

-- FILL IN

-- isOp2 s is True if x is one of: || && == != <= >= <= >= == !=
isOp2 :: String -> Bool
isOp2 (x:y:xs) = (x =='|'&&y=='|')||(x =='&'&&y=='&')||(x =='='&&y=='=')||(x =='!'&&y=='=')||(x =='<'&&y=='=')||(x =='>'&&y=='=') 
				

-- isSep1 x is True if x is one of: ; , { } ( )
isSep1 :: String -> Bool 
isSep1 (x:xs) = x==';' || x==',' || x=='{'||x=='}'||x=='('||x==')' 
				

-- isKeyword s is True if s is one of: bool char else false float if int main true while
isKeyword :: String -> Bool
isKeyword xs = xs=="bool" ||xs=="char" ||xs=="else" ||xs=="false" ||xs=="float" ||xs=="if" ||xs=="int" ||xs=="main" ||xs=="true" ||xs=="while" 

-- isComment s is true if s is "//"
isComment :: String -> Bool
isComment xs = xs=="//" 

-- identifiers characters
isIdentifierChar :: Char -> Bool
isIdentifierChar x = isAlpha x || isDigit x || elem x "_"

---------------------
-- Token positions --
---------------------

-- Data type to represent token positions in source files
data Pos = Pos FN Row Col
type FN = String
type Row = Int
type Col = Int
instance Show Pos where show(Pos a b c) = "<"++a++" ("++show b++", "++show c++")>" 



--Construct a new position value from filename, row, and column
newPos :: String -> Int -> Int -> Pos
newPos s a b = Pos s a b


-- Increment source line by n, reset column to zero
incSourceLine :: Pos -> Int -> Pos
incSourceLine (Pos a b _) n = Pos a (b+n) 0

-- Increment source column by n
incSourceCol :: Pos -> Int -> Pos
incSourceCol (Pos a b c) n = Pos a b (c+n)

--Construct a new Pos value obtained by changing the source line in pos to n
setSourceLine :: Pos -> Int -> Pos
setSourceLine (Pos a _ c) n = Pos a n c

-- Construct a new Pos value obtained by changing the source column in pos to n
setSourceCol :: Pos -> Int -> Pos
setSourceCol (Pos a b _) n = Pos a b n

-- Update the position pos according to the character c read from the input
updatePosChar :: Pos -> Char -> Pos 
updatePosChar (Pos a b c) '\n' = Pos a (b+1) c
updatePosChar (Pos a b c) d = Pos a b (c+1)

-- Update the position pos according to the string S read from the input
updatePosString :: String -> Pos -> Pos
updatePosString d (Pos a b c) = foldl updatePosChar (Pos a b c) d


-----------
-- Lexer --
-----------

lexer :: String -> Pos -> [Token]
lexer str pos = 
     case str of
     x:xs | isSpace x -> lexer xs (updatePosChar pos x)
     x:xs | isAlpha x -> identifierOrKeyword [x] xs pos
     x:xs | isDigit x -> numericLiteral [x] xs pos
     x:xs | isSep1 [x] -> TokPos pos (reserved [x]) :
     					  lexer xs (incSourceCol pos 1)                                
     x:y:xs | isComment [x, y] ->  comment xs (incSourceCol pos 2)
     x:y:xs | isOp2 [x, y] -> 
     			let newpos = incSourceCol pos 2
     			in TokPos pos (reserved [x,y]) : lexer xs newpos
  				
     x:xs | isOp1 [x] ->
     			let newpos = incSourceCol pos 1
     			in TokPos pos (reserved [x]) : lexer xs newpos
     x:_ -> error ("Lexer error: unnknown character " ++ [x]++ " at position: " ++ (show pos)) 
     []->[]
    where
       identifierOrKeyword :: String -> String -> Pos -> [Token]
       identifierOrKeyword id str pos = 
           case str of 
             x:xs | isIdentifierChar x -> identifierOrKeyword (id ++ [x]) xs pos
             xs -> let newp = updatePosString id pos
                       newtok = if isKeyword id
                                then reserved id
                                else TokIdent id 
                   in TokPos pos newtok : lexer xs newp
       numericLiteral lit str pos = 
           case str of 
             x:xs | isDigit x -> numericLiteral (lit ++ [x]) xs pos
             xs -> let newp = updatePosString lit pos
                       newtok = TokIntLit (read lit::Int)
                   in TokPos pos newtok : lexer xs newp
     
       charLiteral str pos =
           case str of 
             x:'\'':xs -> let newp = incSourceCol pos 3
                              newtok = TokCharLit x
                          in TokPos pos newtok : lexer xs newp
       comment str pos = 
           lexer (dropWhile (/= '\n') str) (updatePosChar pos '\n')