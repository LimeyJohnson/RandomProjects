module Position ( Pos
                , newPos
                , dummyPos
                , incSourceLine
                , incSourceCol
                , setSourceLine
                , setSourceCol
                , updatePosChar
                , updatePosString
                , Positioned(..)
                ) 
where

-- a class of types that have source position information embedded
class Positioned a where
  pos :: a -> Pos
  unwrap :: a -> a 
  unwrap = id

-- Data type to represent token positions in source files
data Pos = Pos { posline :: Int, poscol :: Int, posfile :: String }
              deriving Eq

instance Show Pos where
  show p = (posfile p) ++ ":" ++ (show (posline p)) ++ ":" ++ (show (poscol p))

newPos :: String -> Int -> Int -> Pos
newPos s l c = Pos { posline = l, poscol = c, posfile = s }

dummyPos = Pos { posline = 0, poscol = 0, posfile = "<No location info>" }

incSourceLine :: Pos -> Int -> Pos
incSourceLine pos n = pos { posline = (posline pos) + n, poscol = 0 }

incSourceCol :: Pos -> Int -> Pos
incSourceCol pos n = pos { poscol = (poscol pos) + n } 

setSourceLine :: Pos -> Int -> Pos
setSourceLine pos n = pos { posline = n }

setSourceCol :: Pos -> Int -> Pos
setSourceCol pos n = pos { poscol = n } 

updatePosChar :: Pos -> Char -> Pos 
updatePosChar pos '\n' = setSourceCol (incSourceLine pos 1) 0
updatePosChar pos c = incSourceCol pos 1

updatePosString :: String -> Pos -> Pos
updatePosString s pos = foldl updatePosChar pos s











        