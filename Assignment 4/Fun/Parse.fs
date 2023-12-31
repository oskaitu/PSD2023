(* Lexing and parsing of micro-ML programs using fslex and fsyacc *)

module Parse

open System
open System.IO
open System.Text
open (*Microsoft.*)FSharp.Text.Lexing
open Absyn

(* Plain parsing from a string, with poor error reporting *)

let fromString (str : string) : expr =
    let lexbuf = (*Lexing. insert if using old PowerPack *)LexBuffer<char>.FromString(str)
    try 
      FunPar.Main FunLex.Token lexbuf
    with 
      | exn -> let pos = lexbuf.EndPos 
               failwithf "%s near line %d, column %d\n" 
                  (exn.Message) (pos.Line+1) pos.Column
             
(* Parsing from a file *)

let fromFile (filename : string) =
    use reader = new StreamReader(filename)
    let lexbuf = (* Lexing. insert if using old PowerPack *) LexBuffer<char>.FromTextReader reader
    try 
      FunPar.Main FunLex.Token lexbuf
    with 
      | exn -> let pos = lexbuf.EndPos 
               failwithf "%s in file %s near line %d, column %d\n" 
                  (exn.Message) filename (pos.Line+1) pos.Column

(* Exercise it *)

let e1 = fromString "5+7";;
let e2 = fromString "let f x = x + 7 in f 2 end";;

(* Examples in concrete syntax *)

let ex1 = fromString 
            @"let f1 x = x + 1 in f1 12 end";;

(* Example: factorial *)

let ex2 = fromString 
            @"let fac x = if x=0 then 1 else x * fac(x - 1)
              in fac n end";;

(* Example: deep recursion to check for constant-space tail recursion *)

let ex3 = fromString 
            @"let deep x = if x=0 then 1 else deep(x-1) 
              in deep count end";;
    
(* Example: static scope (result 14) or dynamic scope (result 25) *)

let ex4 = fromString 
            @"let y = 11
              in let f x = x + y
                 in let y = 22 in f 3 end 
                 end
              end";;

(* Example: two function definitions: a comparison and Fibonacci *)

let ex5 = fromString
            @"let ge2 x = 1 < x
              in let fib n = if ge2(n) then fib(n-1) + fib(n-2) else 1
                 in fib 25 
                 end
              end";;
        
let ex6 = fromString 
              "let sum n = 
                if n=0 then n
                 else n + sum(n-1)
                   in sum 1000 
                   end";; 

let ex7 = fromString 
              "let pow n = 
                      if n=1 then n
                          else 3 * pow(n-1)
                            in pow (8+1) end";; 


let ex8 = fromString 
              "let pow n = 
                      if n=1 then n
                          else 3 * pow(n-1)
                            in let powa p = 
                            if p=0 then 0 
                            else (pow p) + powa (p-1)                            
                            in powa (11+1)
                            end end";; 


let ex9 = fromString 
              "let pow n = n * n * n * n * n * n * n *n  
                         in let powa p = 
                          if p=0 then p 
                            else (pow p) + powa (p-1)
                            in powa (10) 
                            end end";;


let ex10 = fromString 
                    "let max2 a b = if a<b then b else a
                              in let max3 abc= max2 a (max2 b c)
                                in max3 25 6 62 end
                              end"



