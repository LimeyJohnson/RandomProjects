--------------------------------------------------------------------------
-- |
-- Module      :  RWSE
-- Copyright   :  (c) 2006-2007 Martin Grabmueller
-- License     :  GPL
-- 
-- Maintainer  :  magr@cs.tu-berlin.de
-- Stability   :  provisional
-- Portability :  portable
--
-- Combined reader-writer-state-error monad transformer.
--
-- RWSE is a monad which combines the reader monad (for environment 
-- passing), the writer monad (for output), the state monad (for keeping
-- mutable state) and the error monad (for throwing and catching 
-- exceptions) into one single monad transformer.  This monad is 
-- parameterized by an inner monad.  If this inner monad is IO, even
-- input/output is possible by using the lift operation.

-- Changes (Jaakko Järvi):

-- * Changed the definition of the local function to correspond
--   to the behavior of the Reader monad
-- * Added modify from the WriterMonad

--------------------------------------------------------------------------
module RWSE(
            -- * The monad, and running it
            RWSE, runRWSE,
            -- * Reading
            ask, local,
            -- * Writing
            tell, listen,
            -- * State
            get, gets, put, modify,
            -- * Error
            throwError, catchError,
            -- * Lifting
            lift
            ) where

data RWSE err env state out inner a = RWSE ((env, state, [out]) -> 
				       inner (Either err a, state, [out]))

instance Monad inner => Monad (RWSE err env state out inner) where
    return a = RWSE (\ (e, s, o) -> return (Right a, s, o))
    RWSE p >>= f = RWSE (\ (e, s, o) -> 
 		   do (res, s', o') <- p (e, s, o)
 		      case res of
 		        Left er -> return (Left er, s', o')
 		        Right a -> 
 		          do let RWSE f' = f a
		             f' (e, s', o'))

	
runRWSE :: Monad inner => env -> state -> RWSE err env state out inner a ->
	inner (Either err a, state, [out])
runRWSE e s (RWSE f) =
    do (res, s', out) <- f (e, s, []) 
       return (res, s', reverse out)

throwError :: Monad inner => err -> RWSE err env state out inner a
throwError msg = RWSE (\ (e, s, o) -> return (Left msg, s, o))

catchError :: Monad inner => RWSE err env state out inner a ->
              (err -> RWSE err env state out inner a) -> 
              RWSE err env state out inner a
catchError (RWSE f) handle =
    RWSE (\ (e, s, o) -> do res <- f (e, s, o)
                            case res of
                              (Left msg, s', o') -> 
                                  do let RWSE f' = handle msg
                                     f' (e, s', o')
                              (Right res', s', o') ->
                                  return (Right res', s', o'))

get :: Monad inner => RWSE err env state out inner state
get = RWSE (\ (e, s, o) -> return (Right s, s, o))

gets :: Monad inner => (state -> a) -> RWSE err env state out inner a
gets f = do 
  s <- get 
  return (f s) 

put :: Monad inner => state -> RWSE err env state out inner ()
put s' = RWSE (\ (e, s, o) -> return (Right (), s', o))

modify :: Monad inner => (state -> state) -> RWSE err env state out inner ()
modify f = do
  s <- get
  put (f s)

ask :: Monad inner => RWSE err env state out inner env
ask = RWSE (\ (e, s, o) -> return (Right e, s, o))

-- local :: Monad inner => env -> RWSE err env state out inner a ->
-- 	 RWSE err env state out inner a
-- local e' (RWSE f) = RWSE (\ (e, s, o) -> f (e', s, o))

local :: Monad inner => (env -> env) -> RWSE err env state out inner a ->
	 RWSE err env state out inner a
local efunc (RWSE f) = RWSE (\ (e, s, o) -> f ((efunc e), s, o))

tell :: Monad inner => out -> RWSE err env state out inner ()
tell o' = RWSE (\ (e, s, o) -> return (Right (), s, o' : o))

listen :: Monad inner => RWSE err env state out inner [out]
listen = RWSE (\ (e, s, o) -> return (Right (reverse o), s, o))

lift :: Monad inner => inner a -> RWSE err env state out inner a
lift p = RWSE (\ (e, s, o) -> do a <- p
	                         return (Right a, s, o))

test :: IO ()
test = do (res, state, out) <- runRWSE 1 3 test0
	  case res of
	    Left err -> putStrLn $ "error: " ++ show err
	    Right res' -> putStrLn $ "success: " ++ show res'
	  mapM_ print out

test0 :: RWSE String Integer Integer String IO Char 
test0 = do i <- ask
	   s <- get
	   put 8
	   tell "lala"
	   lift $ putStrLn $ "env: " ++ show i
	   lift $ putStrLn $ "state: " ++ show s
	   s <- get
	   tell "banana"
	   lift $ putStrLn $ "state: " ++ show s
	   local (const 2) (do i <- ask
		               lift $ putStrLn $ "env: " ++ show i)
	   o <- listen
	   lift $ mapM_ print o
           c <- catchError test1 (\ (m:msg) -> return m)
           lift $ putStrLn $ "c: " ++ [c]
	   tell "gugu"
	   --throwError "foo"
	   tell "bumbum"
	   return 'a'

test1 :: RWSE String Integer Integer String IO Char 
test1 = do tell "aaa"
           throwError "Bla!"
           tell "bbb"
           return 'b'
