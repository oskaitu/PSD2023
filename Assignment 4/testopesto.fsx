#r "nuget: FsLexYacc.Runtime"
#load "./Fun/Absyn.fs"
#load "./Fun/Fun.fs"
#load "./Fun/FunPar.fs"
#load "./Fun/FunLex.fs"
#load "./Fun/Parse.fs"
#load "./Fun/ParseAndRun.fs"

open Absyn
open Fun
open Parse


let ex5 =
    fromString
        @"let ge2 x = 1 < x
              in let fib n = if ge2(n) then fib(n-1) + fib(n-2) else 1
                 in fib 25 
                 end
              end"
