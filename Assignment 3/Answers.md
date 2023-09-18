# Answers

## 3.3
1. A -> Expr EOF
2. F -> Let NAME EQ Expr In Expr End EOF
3. G -> Let NAME EQ Expr In Expr TIMES Expr End EOF
4. C -> Let NAME EQ Expr In Expr TIMES CSTI End EOF
5. H -> Let NAME EQ Expr In Expr PLUS Expr TIMES CSTI End EOF
6. C -> Let NAME EQ Expr In Expr PLUS CSTI TIMES CSTI End EOF
7. B -> Let NAME EQ Expr In NAME PLUS CSTI TIMES CSTI End EOF
8. E -> Let NAME EQ LPAR Expr RPAR In NAME PLUS CSTI TIMES CSTI End EOF
9. C -> Let NAME EQ LPAR CSTI RPAR In NAME PLUS CSTI TIMES CSTI End EOF

## 3.4
![tree](tree.png)  
## 3.5

> fromString "2 + 3 * 4";;
val it : Absyn.expr = Prim ("+", CstI 2, Prim ("*", CstI 3, CstI 4))

fromString "1+2* 3";; 
val it : Absyn.expr = Prim ("+", CstI 1, Prim ("*", CstI 2, CstI 3))

> fromString "1-2- 3";;
val it : Absyn.expr = Prim ("-", Prim ("-", CstI 1, CstI 2), CstI 3)

> fromString "1 + -2";;
val it : Absyn.expr = Prim ("+", CstI 1, CstI -2)

> fromString "x++";;
System.Exception: parse error near line 1, column 3

> fromString "1 + 1.2";;
System.Exception: Lexer error: illegal symbol near line 1, column 6

> fromString "1 + ";;
System.Exception: parse error near line 1, column 4

> fromString "let z = 17) in z+2*3 end";;
System.Exception: parse error near line 1, column 11

> fromString "let in = (17) in z+2*3 end";;
System.Exception: parse error near line 1, column 6

> fromString "1 + let x=5 in let y=7+x in y+y end + x end";;
val it : Absyn.expr =
  Prim
    ("+", CstI 1,
     Let
       ("x", CstI 5,
        Prim
          ("+",
           Let
             ("y", Prim ("+", CstI 7, Var "x"), Prim ("+", Var "y", Var "y")),
           Var "x")))

## 3.6

## 3.7
