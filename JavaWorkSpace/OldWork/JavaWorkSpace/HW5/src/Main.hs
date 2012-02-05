module Main (main) where

import CliteSyntax
import CliteLexer
import CliteParser
import CliteTypechecker

import System (getArgs, exitFailure)

processFile :: String -> IO ()
processFile filename =  do 
  s <- readFile filename

  let firstColumn = 0
      firstLine = 1
      firstPos = newPos filename firstLine firstColumn

      tokens = lexer s firstPos
      firstTokPos = if (null tokens) then dummyPos else pos $ head tokens
      result = parse program (ParseState tokens firstTokPos)


  -- if parsing fails, show the error messages and abort
  (Program stmts) <- 
      if (isParseOk result) 
      then return $ getResult result 
      else putStr (showParseMessage (getMessage result)) >> exitFailure

  let (errors, stmts') = checkProgram (stmts)
 
  --  show type errors if any
  sequence_  (map putStrLn errors)

  return ()
               
main :: IO ()
main = do
  filename <- getArgs  -- get the command line arguments
  processFile (filename!!0)
