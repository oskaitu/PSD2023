# Answers 


## Exercise 4.1

>    let res = run (Prim("+", CstI 5, CstI 7));;
> 
    val res : int = 12

>    let e1 = fromString "5+7";;
> 
    val e1 : Absyn.expr = Prim ("+", CstI 5, CstI 7)

>    let e2 = fromString "let y = 7 in y + 2 end";;
> 
    val e2 : Absyn.expr = Let ("y", CstI 7, Prim ("+", Var "y", CstI 2))

>    let e3 = fromString "let f x = x + 7 in f 2 end";;
> 
    val e3 : Absyn.expr =
    Letfun ("f", "x", Prim ("+", Var "x", CstI 7), Call (Var "f", CstI 2))

>    run (fromString "5+7");;
> 
    val it : int = 12

>    run (fromString "let y = 7 in y + 2 end");;
> 
    val it : int = 9

>    run (fromString "let f x = x + 7 in f 2 end");;
> 
    val it : int = 9

## Exercise 4.2

* Sums 

``` Fsharp
> let ex6 = fromString"let sum n = if n=0 then n else n + sum(n-1) in sum 1000 end";;
val ex6 : Absyn.expr =
  Letfun
    ("sum", "n",
     If
       (Prim ("=", Var "n", CstI 0), Var "n",
        Prim ("+", Var "n", Call (Var "sum", Prim ("-", Var "n", CstI 1)))),
     Call (Var "sum", CstI 1000))

> run ex6;;
val it : int = 500500

```

* $3^8$

``` Fsharp

val ex7 : Absyn.expr =
  Letfun
    ("pow", "n",
     If
       (Prim ("=", Var "n", CstI 1), Var "n",
        Prim ("*", CstI 3, Call (Var "pow", Prim ("-", Var "n", CstI 1)))),
     Call (Var "pow", Prim ("+", CstI 8, CstI 1)))

> run ex7;;
val it : int = 6561

```

* $3^{11}+...$

``` Fsharp

> let ex8 = fromString 
              "let pow n =
                      if n=1 then n
                          else 3 * pow(n-1)
                            in let powa p = 
                            if p=0 then 0 
                            else (pow p) + powa (p-1)
                            in powa (11+1)
                            end end";; 
val ex8 : Absyn.expr =
  Letfun
    ("pow", "n",
     If
       (Prim ("=", Var "n", CstI 1), Var "n",
        Prim ("*", CstI 3, Call (Var "pow", Prim ("-", Var "n", CstI 1)))),
     Letfun
       ("powa", "p",
        If
          (Prim ("=", Var "p", CstI 0), CstI 0,
           Prim
             ("+", Call (Var "pow", Var "p"),
              Call (Var "powa", Prim ("-", Var "p", CstI 1)))),
        Call (Var "powa", Prim ("+", CstI 11, CstI 1))))

> run ex8;;
val it : int = 265720

```

* $1^{8}+2^8...$

``` Fsharp

> let ex9 = fromString 
              "let pow n =
                      if n=1 then n
                        else n * n * n * n * n * n * n *n  
                         in let powa p = 
                          if p=0 then 0 
                            else (pow p) + powa (p-1)
                            in powa (10) 
                            end end";;
val ex9 : Absyn.expr =
  Letfun
    ("pow", "n",
     If
       (Prim ("=", Var "n", CstI 1), Var "n",
        Prim
          ("*",
           Prim
             ("*",
              Prim
                ("*",
                 Prim
                   ("*",
                    Prim
                      ("*", Prim ("*", Prim ("*", Var "n", Var "n"), Var "n"),
                       Var "n"), Var "n"), Var "n"), Var "n"), Var "n")),
     Letfun
       ("powa", "p",
        If
          (Prim ("=", Var "p", CstI 0), CstI 0,
           Prim
             ("+", Call (Var "pow", Var "p"),
              Call (Var "powa", Prim ("-", Var "p", CstI 1)))),
        Call (Var "powa", CstI 10)))

> run ex9;;
val it : int = 167731333

```





## Exercise 4.3


## Exercise 4.4


## Exercise 4.5