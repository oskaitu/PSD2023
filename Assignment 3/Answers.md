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

## 3.6

## 3.7
