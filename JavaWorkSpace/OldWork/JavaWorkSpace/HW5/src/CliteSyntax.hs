module CliteSyntax 
    (
     AST(..),
     Program(..),
     Type(..),
     Name,
     tokenToType,
     isArrayType,
     getVarName,
     commaSep,
     Value(..),
     isUndefined
    ) where 

import CliteLexer (Token(..))
import Position
import Array    

-------------------------
-- Data type for types --
-------------------------

data Type = TyInt 
          | TyChar
          | TyFloat 
          | TyBool
          | TyArray Type Int
          | TyFunc Type [Type]
          | TyIntersect [Type]
          | TyVoid
            deriving (Eq)

isArrayType (TyArray _ _ ) = True
isArrayType _ = False

--isGoodArray (TyArray _ TyInt) = True
--isGoodArray _ = False
instance Show Type where
    show TyInt = "int"
    show TyChar = "char"    
    show TyFloat = "float"
    show TyBool = "bool"
    show TyVoid = "void"
    show (TyArray ty i) = (show ty) ++ "[" ++ (show i) ++ "]"
    show (TyFunc ty tys) = (show ty) ++ "(" ++ commaSep (map show tys) ++ ")"
    show (TyIntersect tys) = "Intersection (" ++ commaSep (map show tys) ++ ")"


tokenToType :: Token -> Type
tokenToType TokInt = TyInt
tokenToType TokChar = TyChar
tokenToType TokFloat = TyFloat
tokenToType TokBool = TyBool


-----------------------------
-- Data type for AST nodes --
-----------------------------

type Name = String

data AST 
    = ASTPos Pos AST
    | ASTVariableDecl Name Type
    | ASTBlock [AST]
    | ASTSkip
    | ASTConditional AST AST AST -- if then else
    | ASTLoop AST AST  -- cond body
    | ASTAssignment AST AST 
    | ASTVariableRef Name
    | ASTArrayRef Name AST -- a[expr]
    | ASTLiteral Value
    | ASTAnd AST AST
    | ASTOr AST AST
    | ASTFuncCall String Int {- arity -} [AST] 
    | ASTCast Type AST
    | ASTType Type

getVarName (ASTVariableRef name) = name
getVarName (ASTArrayRef name _) = name
getVarName (ASTPos _ n) = getVarName n
  
-- if a node is wrapped inside an ASTPos, get rid of the wrapping                 

instance Show AST where
  show = pp 0

commaSep :: [String] -> String
commaSep [] = []
commaSep ls = foldr1 (\x y -> x ++ ", " ++ y) ls

pp :: Int -> AST -> String
pp n (ASTPos pos node) = pp n node
pp _ (ASTVariableRef s) = s
pp n (ASTArrayRef s e) = s ++ "[" ++ (pp n e) ++ "]"
pp n (ASTVariableDecl v ty) = indent n ++ v ++ ":" ++ (show ty) ++ ";"
pp _ (ASTLiteral v) = show v
pp _ (ASTAnd a b) = "(" ++ (show a) ++ "&&" ++ (show b) ++ ")"
pp _ (ASTOr a b) = "(" ++ (show a) ++ "||" ++ (show b) ++ ")"
pp n (ASTFuncCall s arity params) = s ++ "(" ++ commaSep (map (pp n) params) ++ ")"
pp n ASTSkip = ";"
pp n (ASTBlock stmts) = indent n ++ "{" ++ concatMap (pp (n+2)) stmts ++ indent n ++ "}"
pp n (ASTConditional c t e) = indent n ++ "if (" ++ pp n c ++ ")" ++
                              pp n t ++ 
                              indent n ++ "else" ++ 
                              pp n e
pp n (ASTLoop c b) = indent n ++ "while (" ++ pp n c ++ ")" ++
                     pp n b 
pp n (ASTAssignment l r) =  indent n ++ (pp n l) ++ " = " ++ (pp n r) ++ ";"
pp n (ASTCast ty e) =  (show ty) ++ "(" ++ (pp n e) ++ ")"
pp n (ASTType ty) =  show ty
    
indent :: Int -> String
indent n = "\n" ++ replicate n ' '

instance Positioned AST where
  pos (ASTPos p ast) = p
  pos t = error $ "ICE: no position in AST node " ++ (show t)

  unwrap (ASTPos p n) = unwrap n
  unwrap n = n


------------
-- Values --
------------

data Value = VInt Int | VFloat Float | VChar Char | VBool Bool | VVoid |
             VArray (Array Int Value) | VFunc ([Value] -> Value) | VIOFunc ([Value] -> IO Value) | VUndefined

instance Show Value where show = showVal

showVal (VInt i) = show i
showVal (VFloat f) = show f
showVal (VChar c) = show c
showVal (VBool b) = case b of 
                      True -> "true"
                      False -> "false"
showVal VVoid = "void"
showVal (VArray vals) = show vals
showVal (VFunc _) = "function"
showVal (VIOFunc _) = "IO function"
showVal (VUndefined) = "undefined"

-- not sure if casts can be made to work with this def of undefined?

isUndefined (VUndefined) = True
isUndefined _ = False

---------------------------------------------------
-- Data type for representing the parsed program --
---------------------------------------------------

data Program = Program [AST] -- Program is just a list of declarations and statments

instance Show Program where
  show (Program stmts) = 
      "int main {" ++
      (concatMap (pp 2) stmts) ++
     "\n}\n"

