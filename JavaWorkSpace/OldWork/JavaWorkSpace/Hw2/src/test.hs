module CliteLexer (newPos) where

import Char (isSpace, isAlpha, isDigit, isUpper, isLower, isAlphaNum, digitToInt)
data Pos = Pos FN Row Col
type FN = String
type Row = Int
type Col = Int
instance Show Pos where show(Pos a b c) = "<"++a++" ("++show b++", "++show c++")>" 
newPos :: String -> Int -> Int -> Pos
newPos s a b = Pos s a b

incSourceLine :: Pos -> Int -> Pos
incSourceLine (Pos a b _) n = Pos a (b+n) 0

incSourceCol :: Pos -> Int -> Pos
incSourceCol (Pos a b c) n = Pos a b (c+n)

setSourceLine :: Pos -> Int -> Pos
setSourceLine (Pos a _ c) n = Pos a n c

start :: String -> Int
start a = testInt 1 (reverse a)

stringToInt :: Int -> String -> Int
stringToInt a [x] = (digitToInt x)*a
stringToInt a (x:xs) = (digitToInt x)*a + testInt (a*10) xs 