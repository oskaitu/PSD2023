(* Programming language concepts for software developers, 2010-08-28 *)

// Done:1.1, 1.2, 1.4, 2.1, 2.2, 2.3
// To do: PLC: (optionally also 2.6).

(* Evaluating simple expressions with variables *)

module Intro2

(* Association lists map object language variables to their values *)

let env = [ ("a", 3); ("c", 78); ("baf", 666); ("b", 111) ]

let emptyenv = [] (* the empty environment *)

let rec lookup env x =
    match env with
    | [] -> failwith (x + " not found")
    | (y, v) :: r -> if x = y then v else lookup r x

let cvalue = lookup env "c"


(* Object language expressions with variables *)

type expr =
    | CstI of int
    | Var of string
    | Prim of string * expr * expr
    | If of expr * expr * expr

let e1 = CstI 17

let e2 = Prim("+", CstI 3, Var "a")

let e3 = Prim("+", Prim("*", Var "b", CstI 9), Var "a")


(* Evaluation within an environment *)
// exercise 1.1.1
let rec eval e (env: (string * int) list) : int =
    match e with
    | CstI i -> i
    | Var x -> lookup env x
    | Prim("+", e1, e2) -> eval e1 env + eval e2 env
    | Prim("*", e1, e2) -> eval e1 env * eval e2 env
    | Prim("-", e1, e2) -> eval e1 env - eval e2 env
    | Prim("max", e1, e2) ->
        let e1 = eval e1 env
        let e2 = eval e2 env
        if (e1 < e2) then e2 else e1
    | Prim("min", e1, e2) ->
        let e1 = eval e1 env
        let e2 = eval e2 env
        if (e1 < e2) then e1 else e2
    | Prim("==", e1, e2) -> if (eval e1 env = eval e2 env) then 1 else 0
    | Prim _ -> failwith "unknown primitive"

// exercise 1.1.2

let e2v1 = eval e2 env
let e2v2 = eval e2 [ ("a", 314) ]
let e3v = eval e3 env

let mye1 = Prim("*", CstI 2, CstI 0)

let evaluate = eval mye1 env

// exercise 1.1.3

let rec eval2 e (env: (string * int) list) : int =
    match e with
    | CstI i -> i
    | Var x -> lookup env x
    | Prim(ope, e1, e2) ->
        let i1 = eval2 e1 env
        let i2 = eval2 e2 env

        match ope with
        | "+" -> i1 + i2
        | "-" -> i1 - i2
        | "*" -> i1 * i2
        | "max" -> if (i1 < i2) then i2 else i1
        | "min" -> if (i1 < i2) then i1 else i2
        | "==" -> if (i1 = i2) then 1 else 0
        | _ -> failwith "unknown primitive"
    | If(e1, e2, e3) ->
        if (eval e1 env) <> 0 then
            (eval2 e2 env)
        else
            (eval2 e3 env)

type aexp =
    | CstI of int
    | Var of string
    | Add of aexp * aexp
    | Mul of aexp * aexp
    | Sub of aexp * aexp

// Write the representation of the expressions

// exercise 1.2.2

// v − (w + z)
let w1 = Sub(Var "v", Add(Var "w", Var "z"))
// 2 ∗ (v − (w + z))
let w2 = Mul(CstI 2, Sub(Var "v", Add(Var "w", Var "z")))
// x + y + z + v.
let w3 = Add(Var "x", (Add(Var "y", (Add(Var "z", Var "v")))))

// 1.2.3
let rec fmt (e: aexp) =
    match e with
    | CstI x -> x.ToString()
    | Var x -> x
    | Add(x, y) -> "(" + fmt x + " + " + fmt y + ")"
    | Mul(x, y) -> "(" + fmt x + " * " + fmt y + ")"
    | Sub(x, y) -> "(" + fmt x + " - " + fmt y + ")"

let _ = (printf "%s \n" (fmt w1))
let _ = (printf "%s \n" (fmt w2))
let _ = (printf "%s \n" (fmt w3))

// exercise 1.2.4
let rec simplify (e: aexp) =
    match e with
    | CstI x -> CstI x
    | Var x -> Var x
    | Add(e1, e2) ->
        let simp1 = simplify e1
        let simp2 = simplify e2

        match simp1, simp2 with
        | (e, CstI 0)
        | (CstI 0, e) -> e
        | (_, _) -> Add(e1, e2)
    | Mul(e1, e2) ->
        let simp1 = simplify e1
        let simp2 = simplify e2

        match simp1, simp2 with
        | (_, CstI 0)
        | (CstI 0, _) -> CstI 0
        | (e, CstI 1)
        | (CstI 1, e) -> e
        | (_, _) -> Mul(e1, e2)
    | Sub(e1, e2) when e2 = CstI 0 ->
        let simp1 = simplify e1
        let simp2 = simplify e2

        match simp1, simp2 with
        | (e, CstI 0) -> e
        | (e1, e2) when e1 = e2 -> CstI 0
        | (_, _) -> Sub(e1, e2)

// exercise 1.2.5

let rec diff e var =
    match e with
    | CstI _ -> CstI 0
    | Var x when x = var -> CstI 0
    | Var _ -> CstI 1
    | Add(e1, e2) -> Add(diff e1 var, diff e2 var)
    | Sub(e1, e2) -> Sub(diff e1 var, diff e2 var)
    | Mul(e1, e2) -> Add(Mul(diff e1 var, e2), Mul(e1, diff e2 var))
