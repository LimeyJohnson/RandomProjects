module CliteLexer 
    (
     lexer, 
     Token(..),
     tokToString,
     Pos,
     newPos,
     dummyPos,
     Positioned(..)
    ) where

import Char (isSpace, isAlpha, isDigit, isUpper, isLower, isAlphaNum)
import Position

--------------------------
-- Data type for tokens --
--------------------------

data Token
    = TokEmpty
    | TokIdent String
    | TokIntLit Int
    | TokFloatLit Float
    | TokCharLit Char

-- keywords
    | TokBool | TokChar | TokElse | TokFalse | TokFloat 
    | TokIf | TokInt | TokMain | TokTrue | TokWhile

-- operators
    | TokOr | TokAnd | TokEqual | TokNotEqual | TokLessThanOrEqual 
    | TokGreaterThanOrEqual | TokLessThan | TokGreaterThan 
    | TokAssign | TokPlus | TokMinus | TokTimes | TokDivides | TokRemainder 
    | TokNot | TokOpenBracket
    | TokCloseBracket 
 
-- separators
    | TokSemicolon | TokComma | TokOpenCurly | TokCloseCurly 
    | TokOpenParen | TokCloseParen 

-- special
    | TokPos Pos Token
      deriving (Eq)


----------------------------------
-- Dealing with token positions --
----------------------------------

instance Positioned Token where
    pos (TokPos p _) = p 
    pos _ = dummyPos

-- Any token may be wrapped into TokPos, unwrap removes such wrappings
--    unwrap :: Token -> Token
    unwrap (TokPos _ tok) = unwrap tok
    unwrap t = t


----------------------------
-- From tokens to strings --
----------------------------

instance Show Token where
  show = tokToString . unwrap
 
-- convert a reserved word or sequence of symbols to a Token
tokToString :: Token -> String
-- operators
tokToString TokOr = "||" 
tokToString TokAnd = "&&" 
tokToString TokEqual = "==" 
tokToString TokNotEqual = "!=" 
tokToString TokLessThanOrEqual = "<=" 
tokToString TokGreaterThanOrEqual = ">="
tokToString TokLessThan = "<" 
tokToString TokGreaterThan = ">"
tokToString TokAssign = "="
tokToString TokPlus = "+" 
tokToString TokMinus = "-"
tokToString TokTimes = "*"
tokToString TokDivides = "/"
tokToString TokRemainder = "%" 
tokToString TokNot = "!" 
tokToString TokOpenBracket = "["
tokToString TokCloseBracket = "]"
-- separators
tokToString TokSemicolon = ";" 
tokToString TokComma = "," 
tokToString TokOpenCurly = "{"
tokToString TokCloseCurly = "}"
tokToString TokOpenParen = "("
tokToString TokCloseParen = ")"
-- keywords
tokToString TokBool = "bool"
tokToString TokChar = "char"
tokToString TokElse = "else"
tokToString TokFalse = "false"
tokToString TokFloat = "float"
tokToString TokIf = "if"
tokToString TokInt = "int"
tokToString TokMain = "main"
tokToString TokTrue = "true"
tokToString TokWhile = "while"

tokToString (TokIdent s) = s
tokToString (TokIntLit i) = show i
tokToString (TokCharLit c) = show c
tokToString (TokFloatLit f) = show f

tokToString TokEmpty = "<empty token>"
tokToString (TokPos p tok) = "<pos token: " ++ show p ++ ":" ++ 
                                 tokToString tok ++ ">"



----------------------------
-- From strings to tokens --
----------------------------

-- convert a tokToString word or sequence of symbols to a Token
reserved :: String -> Token
-- operators
reserved "||" = TokOr
reserved "&&" = TokAnd
reserved "==" = TokEqual
reserved "!=" = TokNotEqual
reserved "<=" = TokLessThanOrEqual
reserved ">=" = TokGreaterThanOrEqual
reserved "<" = TokLessThan
reserved ">" = TokGreaterThan
reserved "=" = TokAssign
reserved "+" = TokPlus
reserved "-" = TokMinus
reserved "*" = TokTimes
reserved "/" = TokDivides
reserved "%" = TokRemainder
reserved "!" = TokNot
reserved "[" = TokOpenBracket
reserved "]" = TokCloseBracket
-- separators
reserved ";" = TokSemicolon
reserved "," = TokComma
reserved "{" = TokOpenCurly
reserved "}" = TokCloseCurly
reserved "(" = TokOpenParen
reserved ")" = TokCloseParen
-- keywords
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

-- helper functions for detecting tokens
isOp1 :: String -> Bool
isOp1 x = elem x (words "= < > = + - * / % ! [ ]")

isOp2 :: String -> Bool
isOp2 x = elem x (words "|| && == != <= >= <= >= == !=")

isSep1 :: String -> Bool
isSep1 s = elem s (words "; , { } ( )")

isKeyword :: String -> Bool
isKeyword s = elem s (words "bool char else false float if int main true while")

isComment :: String -> Bool
isComment = (==) "//"

-- identifiers characters
isIdentifierChar :: Char -> Bool
isIdentifierChar x = 
    isAlpha x || isDigit x || elem x "_$"

------------------------------------------------------------------------------
-- lexer 
------------------------------------------------------------------------------

lexer :: String -> Pos -> [Token]
lexer str pos = 
     case str of
--       '\n':xs -> lexer xs (updatePosChar pos '\n') -- next case covers this
       x:xs | isSpace x -> lexer xs (updatePosChar pos x)
       x:xs | isAlpha x -> identifierOrKeyword [x] xs pos
       x:xs | isDigit x -> numericLiteral [x] xs pos
       x:xs | x == '\'' -> charLiteral xs pos
       x:xs | isSep1 [x] -> TokPos pos (reserved [x]) : 
                            lexer xs (incSourceCol pos 1)                                
       x:y:xs | isComment [x, y] -> comment xs (incSourceCol pos 2)
       x:y:xs | isOp2 [x, y] -> 
                  let newpos = incSourceCol pos 2
                  in TokPos pos (reserved [x, y]) : lexer xs newpos
       x:xs | isOp1 [x] -> 
                  let newpos = incSourceCol pos 1 
                  in TokPos pos (reserved [x]) : lexer xs newpos
       x:_ -> error (show pos ++ ": Lexer error: unknown character " ++ [x]) 
       [] -> []
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
             '.':xs -> floatLiteral (lit ++ ".") xs pos
             x:xs | isAlpha x -> error (show pos ++ ": Lexer error: invalid character " ++ [x] ++ " in numeric literal" ) 
             xs -> let newp = updatePosString lit pos
                       newtok = TokIntLit (read lit::Int)
                   in TokPos pos newtok : lexer xs newp
       floatLiteral lit str pos = 
           case str of 
             x:xs | isDigit x -> floatLiteral (lit ++ [x]) xs pos
             x:xs | isAlpha x -> error (show pos ++ ": Lexer error: invalid character " ++ [x] ++ " in numeric literal" ) 
             xs -> let newp = updatePosString lit pos
                       newtok = TokFloatLit (read lit::Float)
                   in TokPos pos newtok : lexer xs newp
       charLiteral str pos =
           case str of 
             x:'\'':xs -> let newp = incSourceCol pos 3
                              newtok = TokCharLit x
                          in TokPos pos newtok : lexer xs newp
             '\\':'n':'\'':xs -> let newp = incSourceCol pos 4
                                     newtok = TokCharLit '\n'
                                 in TokPos pos newtok : lexer xs newp
       comment str pos = 
           lexer ((drop 1 . dropWhile (/= '\n')) str) (updatePosChar pos '\n')

