
type expr = 
  | CstI of int
  | Var of string
  | Let of string * expr * expr
  | Prim of string * expr * expr;;

let e1 = Let("z", CstI 17, Prim("+", Var "z", Var "z"));;


let rec getindex vs x = 
    match vs with 
    | []    -> failwith "Variable not found"
    | y::yr -> if x=y then 0 else 1 + getindex yr x;;


type sinstr =
  | SCstI of int                        (* push integer           *)
  | SVar of int                         (* push variable from env *)
  | SAdd                                (* pop args, push sum     *)
  | SSub                                (* pop args, push diff.   *)
  | SMul                                (* pop args, push product *)
  | SPop                                (* pop value/unbind var   *)
  | SSwap;;                             (* exchange top and next  *)
 
 type stackvalue =
  | Value                               (* A computed value *)
  | Bound of string;;                   (* A bound variable *)

 let rec scomp (e : expr) (cenv : stackvalue list) : sinstr list =
    match e with
    | CstI i -> [SCstI i]
    | Var x  -> [SVar (getindex cenv (Bound x))]
    | Let(x, erhs, ebody) -> 
          scomp erhs cenv @ scomp ebody (Bound x :: cenv) @ [SSwap; SPop]
    | Prim("+", e1, e2) -> 
          scomp e1 cenv @ scomp e2 (Value :: cenv) @ [SAdd] 
    | Prim("-", e1, e2) -> 
          scomp e1 cenv @ scomp e2 (Value :: cenv) @ [SSub] 
    | Prim("*", e1, e2) -> 
          scomp e1 cenv @ scomp e2 (Value :: cenv) @ [SMul] 
    | Prim _ -> failwith "scomp: unknown operator";;


(* Ex 2.4 - assemble to integers *)
(* SCST = 0, SVAR = 1, SADD = 2, SSUB = 3, SMUL = 4, SPOP = 5, SSWAP = 6; *)
let sinstrToInt (list: sinstr list) =
    let rec aux sl il =
        match sl with
        | [] -> List.rev il
        | SCstI x :: xs -> aux xs ( x :: 0 :: il)
        | SVar x :: xs -> aux xs (x :: 1 :: il)
        | SAdd :: xs -> aux xs (2 :: il)
        | SSub :: xs -> aux xs (3 :: il)
        | SMul :: xs -> aux xs (4 :: il)
        | SPop :: xs -> aux xs (5 :: il)
        | SSwap :: xs -> aux xs (6 :: il)
    aux list []

let assemble instrs = sinstrToInt instrs

let list = scomp e1 [];;

(* Output the integers in list inss to the text file called fname: *)

let intsToFile (inss : int list) (fname : string) = 
    let text = String.concat " " (List.map string inss)
    System.IO.File.WriteAllText(fname, text);;

intsToFile (assemble list)"is1.txt"
