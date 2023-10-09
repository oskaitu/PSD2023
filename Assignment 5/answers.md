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


## 6.3
## 6.4
## 6.5