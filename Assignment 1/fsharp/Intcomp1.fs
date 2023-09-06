(* Programming language concepts for software developers, 2012-02-17 *)

(* Evaluation, checking, and compilation of object language expressions *)
(* Stack machines for expression evaluation                             *)

(* Object language expressions with variable bindings and nested scope *)

module Intcomp1

type expr =
    | CstI of int
    | Var of string
    | Let of (string * expr) list * expr // string * expr * expr
    | Prim of string * expr * expr

(* ---------------------------------------------------------------------- *)

(* Evaluation of expressions with variables and bindings *)

let rec lookup env x =
    match env with
    | [] -> failwith (x + " not found")
    | (y, v) :: r -> if x = y then v else lookup r x

let rec eval e (env: (string * int) list) : int =
    match e with
    | CstI i -> i
    | Var(x: string) -> lookup env x
    | Let(letEnvs, expresso) ->
        let rec aux (letsgo: (string * expr) list) (acc: (string * int) list) =
            match letsgo with
            | [] -> acc
            | (key, value) :: xs -> (key, (eval value env)) :: aux xs acc

        let env1 = aux letEnvs []
        eval expresso env1
    | Prim("+", e1, e2) -> eval e1 env + eval e2 env
    | Prim("*", e1, e2) -> eval e1 env * eval e2 env
    | Prim("-", e1, e2) -> eval e1 env - eval e2 env
    | Prim _ -> failwith "unknown primitive"


let rec mem x vs =
    match vs with
    | [] -> false
    | v :: vr -> x = v || mem x vr

(* Free variables *)

let rec union (xs, ys) =
    match xs with
    | [] -> ys
    | x :: xr -> if mem x ys then union (xr, ys) else x :: union (xr, ys)

(* minus xs ys  is the set of all elements in xs but not in ys *)

let rec minus (xs, ys) =
    match xs with
    | [] -> []
    | x :: xr -> if mem x ys then minus (xr, ys) else x :: minus (xr, ys)

(* Find all variables that occur free in expression e *)

// exercse 2.2
let rec freevars e : string list =
    match e with
    | CstI i -> []
    | Var x -> [ x ]
    | Let([ (k, v) ], expresso) -> union (freevars v, minus (freevars expresso, [ k ]))
    | Let((k, v) :: xs, expresso) -> union (freevars v, minus (freevars (Let(xs, expresso)), [ k ]))
    | Prim(ope, e1, e2) -> union (freevars e1, freevars e2)

(* ---------------------------------------------------------------------- *)

(* Compilation to target expressions with numerical indexes instead of
   symbolic variable names.  *)

type texpr = (* target expressions *)
    | TCstI of int
    | TVar of int (* index into runtime environment *)
    | TLet of texpr * texpr (* erhs and ebody                 *)
    | TPrim of string * texpr * texpr


(* Map variable name to variable index at compile-time *)

let rec getindex vs x =
    match vs with
    | [] -> failwith "Variable not found"
    | y :: yr -> if x = y then 0 else 1 + getindex yr x

(* Compiling from expr to texpr *)

let rec tcomp (e: expr) (cenv: string list) : texpr =
    match e with
    | CstI i -> TCstI i
    | Var x -> TVar(getindex cenv x)
    | Let((k, v) :: xs, ebody) ->
        let cenv1 = k :: cenv
        TLet(tcomp (Let(xs, v)) cenv, tcomp (Let(xs, ebody)) cenv1)
    | Prim(ope, e1, e2) -> TPrim(ope, tcomp e1 cenv, tcomp e2 cenv)