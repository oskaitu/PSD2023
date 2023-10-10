# Exercises 
## 5.1 

* F# 

**Using higher order**

``` f#
- let merge a b = List.append a b |> List.sort
- ;;
val lst1: int list = [3; 5; 12]
val lst2: int list = [2; 3; 4; 7]
val merge: a: 'a list -> b: 'a list -> 'a list when 'a: comparison

> merge lst1 lst2;;
val it: int list = [2; 3; 3; 4; 5; 7; 12]
```

**Using match case** 

``` f#
let merge (x:int list) (y:int list) = 
-    let rec aux x y acc =
-         match x, y with
-         | [], [] -> List.sort(acc)
-         | x::xs, [] -> aux xs [] (x::acc)
-         | [], y::ys -> aux [] ys (y::acc)
-         | x::xs, y::ys -> aux xs ys (x::y::acc)
-     aux x y [];;
val merge: x: int list -> y: int list -> int list

merge [1;2;3;] [2;4;6;];;;;

val it: int list = [1; 2; 2; 3; 4; 6]
```

* Java 

``` java
import java.util.Arrays;

public class smallo {

    static int[] merge(int[] xs, int[] ys)
{
  int mergedLists[] = new int[xs.length+ys.length];
  for (int i = 0; i < xs.length; i++) {
    mergedLists[i] = xs[i];
  }
  for (int i = 0; i < ys.length; i++) {
    mergedLists[xs.length+i] = ys[i];
  }
  Arrays.sort(mergedLists);
  return mergedLists;
}

public static void main(String[] args){

    int[] xs = { 3, 5, 12 };
    int[] ys = { 2, 3, 4, 7 };
    int res[] = merge(xs, ys);
    for (int i : res) {
      System.out.print(i + " ");  
    }
}
}

```

## 5.7

``` F#

let rec typ (e : tyexpr) (env : typ env) : typ =
    match e with
    | CstI i -> TypI
    | CstB b -> TypB
    | Var x  -> lookup env x 
    | Prim(ope, e1, e2) -> 
      let t1 = typ e1 env
      let t2 = typ e2 env
      match (ope, t1, t2) with
      | ("*", TypI, TypI) -> TypI
      | ("+", TypI, TypI) -> TypI
      | ("-", TypI, TypI) -> TypI
      | ("=", TypI, TypI) -> TypB
      | ("<", TypI, TypI) -> TypB
      | ("&", TypB, TypB) -> TypB
      | _   -> failwith "unknown op, or type error"
    | Let(x, eRhs, letBody) -> 
      let xTyp = typ eRhs env
      let letBodyEnv = (x, xTyp) :: env 
      typ letBody letBodyEnv
    | If(e1, e2, e3) -> 
      match typ e1 env with
      | TypB -> let t2 = typ e2 env
                let t3 = typ e3 env
                if t2 = t3 then t2
                else failwith "If: branch types differ"
      | _    -> failwith "If: condition not boolean"
    | Letfun(f, x, xTyp, fBody, rTyp, letBody) -> 
      let fTyp = TypF(xTyp, rTyp) 
      let fBodyEnv = (x, xTyp) :: (f, fTyp) :: env
      let letBodyEnv = (f, fTyp) :: env
      if typ fBody fBodyEnv = rTyp
      then typ letBody letBodyEnv
      else failwith ("Letfun: return type in " + f)
    | Call(Var f, eArg) -> 
      match lookup env f with
      | TypF(xTyp, rTyp) ->
        if typ eArg env = xTyp then rTyp
        else failwith "Call: wrong argument type"
      | _ -> failwith "Call: unknown function"
    | Call(_, eArg) -> failwith "Call: illegal function in call"
    | ListExpr(e, t) -> //This is added 
      let check a = (typ a env) = t 
      if (List.forall check e) then TypL t else failwith "type error in list"

//testcases 

//if all types are the same 

> let testShouldPass:tyexpr = ListExpr([CstI 5; CstI 10], TypI);;
val testShouldPass : tyexpr = ListExpr ([CstI 5; CstI 10], TypI)

> typeCheck testShouldPass;;
val it : typ = TypL TypI

//if they are not

> let testshouldFail:tyexpr = ListExpr([CstI 5;CstB false], TypI);;
val testshouldFail : tyexpr = ListExpr ([CstI 5; CstB false], TypI)

> typeCheck testshouldFail;;
System.Exception: type error in list

```


## 6.1

**p1**
``` F# 

> run (fromString @"let add x = let f y = x+y in f end
in add 2 5 end");;
val it : HigherFun.value = Int 7

```

**p2**
``` F# 

> run (fromString @"let add x = let f y = x+y in f end
in let addtwo = add 2
in addtwo 5 end
end");;
val it : HigherFun.value = Int 7

```

**p3**
``` F# 

> run (fromString @"let add x = let f y = x+y in f end
in let addtwo = add 2
in let x = 77 in addtwo 5 end
end
end");;
val it : HigherFun.value = Int 7

```

the scope of x = 77 is local to that call
and we dont utilize x in this case, we just declare it.
Were we do change the code to use x it would be like below 

``` F# 

> run (fromString @"let add x = let f y = x+y in f end
in let addtwo = add 2
in let x = 77 in addtwo x end
end
end");;
val it : HigherFun.value = Int 79

```


**p4**
``` F# 

> run (fromString @"let add x = let f y = x+y in f end
in add 2 end");;
val it : HigherFun.value =
  Closure
    ("f", "y", Prim ("+", Var "x", Var "y"),
     [("x", Int 2);
      ("add",
       Closure
         ("add", "x", Letfun ("f", "y", Prim ("+", Var "x", Var "y"), Var "f"),
          []))])
>

```
This just returns the closure of adding two to something, it could be used like this: 

``` F# 
open Absyn;;

> let add2 = run (fromString @"let add x = let f y = x+y in f end in add 2 end");;
val add2 : HigherFun.value =
  Closure
    ("f", "y", Prim ("+", Var "x", Var "y"),
     [("x", Int 2);
      ("add",
       Closure
         ("add", "x", Letfun ("f", "y", Prim ("+", Var "x", Var "y"), Var "f"),
          []))])

> let res = eval(Call(Var "add2", CstI 5)) [("add2",add2)];;
val res : HigherFun.value = Int 7

```

Which looks very much like any other higher-order function from f# since we can call the closure with an argument and get the result for any input instead of having it hard-coded like in p1-p3


## 6.2

**Absyn**

``` F#

type expr = 
  | CstI of int
  | CstB of bool
  | Var of string
  | Let of string * expr * expr
  | Prim of string * expr * expr
  | If of expr * expr * expr
  | Letfun of string * string * expr * expr    (* (f, x, fBody, letBody) *)
  | Call of expr * expr
  | Fun of string * expr // this is added 

```

**fsl**

``` F#

let keyword s =
    match s with
    | "fun"  -> FUN (* new *)
    | "else"  -> ELSE 
    | "end"   -> END
    | "false" -> CSTBOOL false
    | "if"    -> IF
    | "in"    -> IN
    | "let"   -> LET
    | "not"   -> NOT
    | "then"  -> THEN
    | "true"  -> CSTBOOL true
    | _       -> NAME s
}

rule Token = parse
  | [' ' '\t' '\r'] { Token lexbuf }
  | '\n'            { lexbuf.EndPos <- lexbuf.EndPos.NextLine; Token lexbuf }
  | ['0'-'9']+      { CSTINT (System.Int32.Parse (lexemeAsString lexbuf)) }
  | ['a'-'z''A'-'Z']['a'-'z''A'-'Z''0'-'9']*
                    { keyword (lexemeAsString lexbuf) }
  | "(*"            { commentStart := lexbuf.StartPos;
                      commentDepth := 1; 
                      SkipComment lexbuf; Token lexbuf }
  | "->"             { ARROW }                 
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
  | eof             { EOF }
  | _               { failwith "Lexer error: illegal symbol" }

```

**fsy**

``` F# 

%left ELSE              /* lowest precedence  */
%left EQ NE  
%left GT LT GE LE
%left PLUS MINUS
%left TIMES DIV MOD    /* highest precedence  */
%nonassoc NOT
%nonassoc ARROW FUN //todo check if this is correct


AtExpr:
    Const                               { $1                     }
  | NAME                                { Var $1                 }
  | FUN NAME ARROW Expr                 { Fun($2, $4)            } 
  | LET NAME EQ Expr IN Expr END        { Let($2, $4, $6)        }
  | LET NAME NAME EQ Expr IN Expr END   { Letfun($2, $3, $5, $7) }
  | LPAR Expr RPAR                      { $2                     }
;

```

**HigherFun**

``` F# 

type value = 
  | Int of int
  | Closure of string * string * expr * value env       (* (f, x, fBody, fDeclEnv) *)
  | Clos of string * expr * value env (* (x,body,declEnv) *)


let rec eval (e : expr) (env : value env) : value =
    match e with
    | CstI i -> Int i
    | CstB b -> Int (if b then 1 else 0)
    | Var x  -> lookup env x
    | Prim(ope, e1, e2) -> 
      let v1 = eval e1 env
      let v2 = eval e2 env
      match (ope, v1, v2) with
      | ("*", Int i1, Int i2) -> Int (i1 * i2)
      | ("+", Int i1, Int i2) -> Int (i1 + i2)
      | ("-", Int i1, Int i2) -> Int (i1 - i2)
      | ("=", Int i1, Int i2) -> Int (if i1 = i2 then 1 else 0)
      | ("<", Int i1, Int i2) -> Int (if i1 < i2 then 1 else 0)
      |  _ -> failwith "unknown primitive or wrong type"
    | Let(x, eRhs, letBody) -> 
      let xVal = eval eRhs env
      let letEnv = (x, xVal) :: env 
      eval letBody letEnv
    | If(e1, e2, e3) -> 
      match eval e1 env with
      | Int 0 -> eval e3 env
      | Int _ -> eval e2 env
      | _     -> failwith "eval If"
    | Letfun(f, x, fBody, letBody) -> 
      let bodyEnv = (f, Closure(f, x, fBody, env)) :: env
      eval letBody bodyEnv
    | Call(eFun, eArg) -> 
      let fClosure = eval eFun env  (* Different from Fun.fs - to enable first class functions *)
      match fClosure with
      | Closure (f, x, fBody, fDeclEnv) ->
        let xVal = eval eArg env
        let fBodyEnv = (x, xVal) :: (f, fClosure) :: fDeclEnv
        in eval fBody fBodyEnv
      | _ -> failwith "eval Call: not a function"
    | Fun(x, body) -> Clos(x, body, env)

```

**Test** 

``` F# 

> fromString @"fun x -> 2*x";;  
val it : Absyn.expr = Fun ("x", Prim ("*", CstI 2, Var "x"))

> fromString @"let y = 22 in fun z -> z+y end";;  
val it : Absyn.expr =
  Let ("y", CstI 22, Fun ("z", Prim ("+", Var "z", Var "y")))

> run (fromString @"let y = 22 in fun z -> z+y end");;
val it : HigherFun.value =
  Clos ("z", Prim ("+", Var "z", Var "y"), [("y", Int 22)])

> run (fromString @"fun x -> 2*x");;
val it : HigherFun.value = Clos ("x", Prim ("*", CstI 2, Var "x"), [])

```

## 6.3

We already added this to verify 6.2 since we thought we were supposed to do that. 
here are the tests. 

``` F#

> fromString @"let add x = fun y -> x+y
in add 2 5 end";;
val it : Absyn.expr =
  Letfun
    ("add", "x", Fun ("y", Prim ("+", Var "x", Var "y")),
     Call (Call (Var "add", CstI 2), CstI 5))

> fromString @"let add = fun x -> fun y -> x+y
in add 2 5 end";;
val it : Absyn.expr =
  Let
    ("add", Fun ("x", Fun ("y", Prim ("+", Var "x", Var "y"))),
     Call (Call (Var "add", CstI 2), CstI 5))


```

## 6.4
## 6.5