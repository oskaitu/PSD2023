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

let ex9 = fromString 
              "let pow n = n * n * n * n * n * n * n *n  
                         in let powa p = 
                          if p=0 then p 
                            else (pow p) + powa (p-1)
                            in powa (10) 
                            end end";;
val ex9 : Absyn.expr =
  Letfun
    ("pow", "n",
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
                    Var "n"), Var "n"), Var "n"), Var "n"), Var "n"),
     Letfun
       ("powa", "p",
        If
          (Prim ("=", Var "p", CstI 0), Var "p",
           Prim
             ("+", Call (Var "pow", Var "p"),
              Call (Var "powa", Prim ("-", Var "p", CstI 1)))),
        Call (Var "powa", CstI 10)))

> run ex9;;
val it : int = 167731333

```





## Exercise 4.3

* Absyn

``` Fsharp

type expr = 
  | CstI of int
  | CstB of bool
  | Var of string
  | Let of string * expr * expr
  | Prim of string * expr * expr
  | If of expr * expr * expr
  | Letfun of string * string list * expr * expr (* (f, x, fBody, letBody) *)
  | Call of expr * expr list

```

**Fun.fs** 

* Closure 


``` Fsharp

type value = 
  | Int of int
  | Closure of string * string list * expr * value env (* (f, x, fBody, fDeclEnv) *)

```

* Eval 

``` Fsharp

let rec eval (e: expr) (env: value env) : int =
    match e with
    | CstI i -> i
    | CstB b -> if b then 1 else 0
    | Var x ->
        match lookup env x with
        | Int i -> i
        | _ -> failwith "eval Var"
    | Prim(ope, e1, e2) ->
        let i1 = eval e1 env
        let i2 = eval e2 env

        match ope with
        | "*" -> i1 * i2
        | "+" -> i1 + i2
        | "-" -> i1 - i2
        | "=" -> if i1 = i2 then 1 else 0
        | "<" -> if i1 < i2 then 1 else 0
        | _ -> failwith ("unknown primitive " + ope)
    | Let(x, eRhs, letBody) ->
        let xVal = Int(eval eRhs env)
        let bodyEnv = (x, xVal) :: env
        eval letBody bodyEnv
    | If(e1, e2, e3) ->
        let b = eval e1 env
        if b <> 0 then eval e2 env else eval e3 env
    | Letfun(f, paramos, fBody, letBody) ->
        let bodyEnv = (f, Closure(f, paramos, fBody, env)) :: env
        eval letBody bodyEnv
    | Call(Var f, eArg) ->
        let fClosure = lookup env f

        match fClosure with
        | Closure(f, paramosdos, fBody, fDeclEnv) ->
            let xVal = List.fold (fun acc los -> Int(eval los env) :: acc) [] eArg // Vi skal nu folde over alle values, i stedet for den ene som den tog før
            let argOs = List.zip paramosdos xVal // Kombinerer nu de to lister ved hjælp af zip funktionen
            let fBodyEnv = argOs @ (f, fClosure) :: fDeclEnv // skal nu merges med de andre envs
            eval fBody fBodyEnv
        | _ -> failwith "eval Call: not a function"
    | Call _ -> failwith "eval Call: not first-order function"


```

* Example of new version with lists

``` F#

let ex1 = Letfun("f1", ["x"], Prim("+", Var "x", CstI 1), Call(Var "f1", [CstI 12]));;

```

## Exercise 4.4

* Funpar

``` F#

Names1: 
    NAME                                { [$1]     }
  | NAME Names1                         { $1 :: $2 }
;

AtExpr:
    Const                               { $1                     }
  | NAME                                { Var $1                 }
  | LET NAME EQ Expr IN Expr END        { Let($2, $4, $6)        }
  | LET NAME Names1 EQ Expr IN Expr END { Letfun($2, $3, $5, $7) }
  | LPAR Expr RPAR                      { $2                     }
;

ListExpressions:
  AtExpr                                { [$1]                    }
  | ListExpressions AtExpr              { $2 :: $1                }

AppExpr:
  AtExpr ListExpressions                { Call($1,$2)            }
;

```

*Example of running pow

``` F#

> let hotdog = fromString "let pow x n = if n=0 then 1 else x * pow x (n-1) in pow 3 8 end" 

val hotdog : expr =
  Letfun
    ("pow", ["x"; "n"],
     If
       (Prim ("=", Var "n", CstI 0), CstI 1,
        Prim
          ("*", Var "x",
           Call (Var "pow", [Prim ("-", Var "n", CstI 1); Var "x"]))),
     Call (Var "pow", [CstI 8; CstI 3]))

> run hotdog;;
val it : int = 6561

```


## Exercise 4.5

* Funpar 

``` F#


%token ELSE END FALSE IF IN LET NOT THEN TRUE AND OR
%token PLUS MINUS TIMES DIV MOD
%token EQ NE GT LT GE LE
%token LPAR RPAR 
%token EOF

%left OR  /* lowest precedence  */
%left AND
%left ELSE              
%left EQ NE 
%left GT LT GE LE
%left PLUS MINUS
%left TIMES DIV MOD 
%nonassoc NOT           /* highest precedence  */

  | Expr AND   Expr                     { If($1,$3, CstB false ) }
  | Expr OR    Expr                     { If($1, CstB true, $3  )}
;

```

*  FunLex

``` F#

rule Token = parse
  | [' ' '\t' '\r'] { Token lexbuf }
  | '\n'            { lexbuf.EndPos <- lexbuf.EndPos.NextLine; Token lexbuf }
  | ['0'-'9']+      { CSTINT (System.Int32.Parse (lexemeAsString lexbuf)) }
  | ['a'-'z''A'-'Z']['a'-'z''A'-'Z''0'-'9']*
                    { keyword (lexemeAsString lexbuf) }
  | "(*"            { commentStart := lexbuf.StartPos;
                      commentDepth := 1; 
                      SkipComment lexbuf; Token lexbuf }
  | '='             { EQ }
  | "<>"            { NE }
  | '>'             { GT }
  | '<'             { LT }
  | ">="            { GE }
  | "<="            { LE }
  | '+'             { PLUS }                     
  | '-'             { MINUS }                     
  | '*'             { TIMES }                     
  | '/'             { DIV }                     
  | '%'             { MOD }
  | '('             { LPAR }
  | ')'             { RPAR }
  | "&&"            { AND } 
  | "||"            { OR  }
  | eof             { EOF }
  | _               { failwith "Lexer error: illegal symbol" }

```


* Examples

``` F#
(*&& should be false*)

> let hotdog = fromString"true && false";;  
val hotdog : Absyn.expr = If (CstB true, CstB false, CstB false)

> run hotdog;;
val it : int = 0


(*|| should be true*)

> let gothotdog = fromString "false || true";;
val gothotdog : Absyn.expr = If (CstB false, CstB true, CstB true)

> run gothotdog;;
val it : int = 1



```