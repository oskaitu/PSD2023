# PLC: 8.1, 8.3, 8.4, 8.5 and 8.6

# 8.1

### (i)
```fsharp
> compile "ex11";;
val it : Machine.instr list =
  [LDARGS; CALL (1, "L1"); STOP; Label "L1"; INCSP 1; INCSP 1; INCSP 100;
   GETSP; CSTI 99; SUB; INCSP 100; GETSP; CSTI 99; SUB; INCSP 100; GETSP;   
   CSTI 99; SUB; INCSP 100; GETSP; CSTI 99; SUB; GETBP; CSTI 2; ADD; CSTI 1;
   STI; INCSP -1; GOTO "L3"; Label "L2"; GETBP; CSTI 103; ADD; LDI; GETBP;  
   CSTI 2; ADD; LDI; ADD; CSTI 0; STI; INCSP -1; GETBP; CSTI 2; ADD; GETBP; 
   CSTI 2; ADD; LDI; CSTI 1; ADD; STI; INCSP -1; INCSP 0; Label "L3"; GETBP;
   CSTI 2; ADD; LDI; GETBP; CSTI 0; ADD; LDI; SWAP; LT; NOT; IFNZRO "L2";   
   GETBP; CSTI 2; ADD; CSTI 1; STI; INCSP -1; GOTO "L5"; Label "L4"; GETBP; 
   CSTI 204; ADD; LDI; GETBP; CSTI 2; ADD; LDI; ADD; GETBP; CSTI 305; ADD; LDI;
   GETBP; CSTI 2; ADD; LDI; ADD; CSTI 0; STI; STI; INCSP -1; GETBP; CSTI 2;
   ADD; ...]

```
### (ii)

ex3.c
```fsharp
> compileToFile (fromFile "ex3.c") "ex3.out"
;;
val it : Machine.instr list =
  [LDARGS; CALL (1, "L1"); STOP; Label "L1"; INCSP 1; GETBP; CSTI 1; ADD;
   CSTI 0; STI; INCSP -1; GOTO "L3"; Label "L2"; GETBP; CSTI 1; ADD; LDI;
   PRINTI; INCSP -1; GETBP; CSTI 1; ADD; GETBP; CSTI 1; ADD; LDI; CSTI 1; ADD;
   STI; INCSP -1; INCSP 0; Label "L3"; GETBP; CSTI 1; ADD; LDI; GETBP; CSTI 0;
   ADD; LDI; LT; IFNZRO "L2"; INCSP -1; RET 0]
```
ex5.c
```fsharp
> compileToFile (fromFile "ex5.c") "ex5.out"
;;
val it : Machine.instr list =
  [LDARGS; CALL (1, "L1"); STOP; Label "L1"; INCSP 1; GETBP; CSTI 1; ADD;
   GETBP; CSTI 0; ADD; LDI; STI; INCSP -1; INCSP 1; GETBP; CSTI 0; ADD; LDI;
   GETBP; CSTI 2; ADD; CALL (2, "L2"); INCSP -1; GETBP; CSTI 2; ADD; LDI;
   PRINTI; INCSP -1; INCSP -1; GETBP; CSTI 1; ADD; LDI; PRINTI; INCSP -1;
   INCSP -1; RET 0; Label "L2"; GETBP; CSTI 1; ADD; LDI; GETBP; CSTI 0; ADD;
   LDI; GETBP; CSTI 0; ADD; LDI; MUL; STI; INCSP -1; INCSP 0; RET 1]
```

Just example for ex5.c..
|Numeric| Symbolic | Micro-c |
|--------|---------|----------|
| 24 | LDARGS | -|
| 19 | CALL 1 5 | setup program
| 25 | STOP | -|
| 15 | INCSP 1 | int r |
| 13 | GETBP | -|
| 0  | CSTI 1 | push pointer to r|
| 1  | ADD |-|
| 13 | GETBP |-|
| 0  | CSTI 0 | push pointer to n|
| 1  | ADD |-|
| 11 | LDI | get top of stack|
| 12 | STI | store to address (r = n)|
| 15 | INCSP -1 | -|
| 15 | INCSP 1 | int r|
| 13 | GETBP |-|
| 0  | CSTI 0 | push pointer to n |
| 1  | ADD |-|
| 11 | LDI |-|
| 13 | GETBP |-|
| 0  | CSTI 2 |-|
| 1  | ADD |-|
| 19 | CALL 2 57 | call square |
| 15 | INCSP -1 |-|
| 13 | GETBP |-|
| 0  | CSTI 2 |-|
| 1  | ADD |-|
| 11 | LDI |-|
| 22 | PRINTI | print r|
| 15 | INCSP -1 |-|
| 15 | INCSP -1 |-|
| 13 | GETBP |-|
| 0  | CSTI 1 |-|
| 1  | ADD |-|
| 11 | LDI |-|
| 22 | PRINTI | print r|
| 15 | INCSP -1 |-|
| 15 | INCSP -1 |-|
| 21 | RET 0 | end main|
| 13 | GETBP |-|
| 0  | CSTI 1 | get pointer to *rp|
| 1  | ADD |-|
| 11 | LDI |-|
| 13 | GETBP |-|
| 0  | CSTI 0 | get i|
| 1  | ADD |-|
| 11 | LDI |-|
| 13 | GETBP |-|
| 0  | CSTI 0 | get i again|
| 1  | ADD |-|
| 11 | LDI |-|
| 3  | MUL |-|
| 12 | STI | end *rp = i * i|
| 15 | INCSP -1 |-|
| 15 | INCSP 0 |-|
| 21 | RET 1 | return square|


The nested scope is addressed by declaring int r again so it's actually in the inside scope.

It happens here (| 15 | INCSP 1 | int r|)

This is the same as before, but we see the 4 represented as input. 
Not gonna go through the whole thing again.
```java
[ ]{0: LDARGS}
[ 4 ]{1: CALL 1 5}
[ 4 -999 4 ]{5: INCSP 1}
[ 4 -999 4 0 ]{7: GETBP}
[ 4 -999 4 0 2 ]{8: CSTI 1}
[ 4 -999 4 0 2 1 ]{10: ADD}
[ 4 -999 4 0 3 ]{11: CSTI 0}
[ 4 -999 4 0 3 0 ]{13: STI}
[ 4 -999 4 0 0 ]{14: INCSP -1}
[ 4 -999 4 0 ]{16: GOTO 43}
[ 4 -999 4 0 ]{43: GETBP}
[ 4 -999 4 0 2 ]{44: CSTI 1}
[ 4 -999 4 0 2 1 ]{46: ADD}
[ 4 -999 4 0 3 ]{47: LDI}
[ 4 -999 4 0 0 ]{48: GETBP}
[ 4 -999 4 0 0 2 ]{49: CSTI 0}
[ 4 -999 4 0 0 2 0 ]{51: ADD}
[ 4 -999 4 0 0 2 ]{52: LDI}
[ 4 -999 4 0 0 4 ]{53: LT}
[ 4 -999 4 0 1 ]{54: IFNZRO 18}
[ 4 -999 4 0 ]{18: GETBP}
[ 4 -999 4 0 2 ]{19: CSTI 1}
[ 4 -999 4 0 2 1 ]{21: ADD}
[ 4 -999 4 0 3 ]{22: LDI}
[ 4 -999 4 0 0 ]{23: PRINTI}
0 [ 4 -999 4 0 0 ]{24: INCSP -1}
[ 4 -999 4 0 ]{26: GETBP}
[ 4 -999 4 0 2 ]{27: CSTI 1}
[ 4 -999 4 0 2 1 ]{29: ADD}
[ 4 -999 4 0 3 ]{30: GETBP}
[ 4 -999 4 0 3 2 ]{31: CSTI 1}
[ 4 -999 4 0 3 2 1 ]{33: ADD}
[ 4 -999 4 0 3 3 ]{34: LDI}
[ 4 -999 4 0 3 0 ]{35: CSTI 1}
[ 4 -999 4 0 3 0 1 ]{37: ADD}
[ 4 -999 4 0 3 1 ]{38: STI}
[ 4 -999 4 1 1 ]{39: INCSP -1}
[ 4 -999 4 1 ]{41: INCSP 0}
[ 4 -999 4 1 ]{43: GETBP}
[ 4 -999 4 1 2 ]{44: CSTI 1}
[ 4 -999 4 1 2 1 ]{46: ADD}
[ 4 -999 4 1 3 ]{47: LDI}
[ 4 -999 4 1 1 ]{48: GETBP}
[ 4 -999 4 1 1 2 ]{49: CSTI 0}
[ 4 -999 4 1 1 2 0 ]{51: ADD}
[ 4 -999 4 1 1 2 ]{52: LDI}
[ 4 -999 4 1 1 4 ]{53: LT}
[ 4 -999 4 1 1 ]{54: IFNZRO 18}
[ 4 -999 4 1 ]{18: GETBP}
[ 4 -999 4 1 2 ]{19: CSTI 1}
[ 4 -999 4 1 2 1 ]{21: ADD}
[ 4 -999 4 1 3 ]{22: LDI}
[ 4 -999 4 1 1 ]{23: PRINTI}
1 [ 4 -999 4 1 1 ]{24: INCSP -1}
[ 4 -999 4 1 ]{26: GETBP}
[ 4 -999 4 1 2 ]{27: CSTI 1}
[ 4 -999 4 1 2 1 ]{29: ADD}
[ 4 -999 4 1 3 ]{30: GETBP}
[ 4 -999 4 1 3 2 ]{31: CSTI 1}
[ 4 -999 4 1 3 2 1 ]{33: ADD}
[ 4 -999 4 1 3 3 ]{34: LDI}
[ 4 -999 4 1 3 1 ]{35: CSTI 1}
[ 4 -999 4 1 3 1 1 ]{37: ADD}
[ 4 -999 4 1 3 2 ]{38: STI}
[ 4 -999 4 2 2 ]{39: INCSP -1}
[ 4 -999 4 2 ]{41: INCSP 0}
[ 4 -999 4 2 ]{43: GETBP}
[ 4 -999 4 2 2 ]{44: CSTI 1}
[ 4 -999 4 2 2 1 ]{46: ADD}
[ 4 -999 4 2 3 ]{47: LDI}
[ 4 -999 4 2 2 ]{48: GETBP}
[ 4 -999 4 2 2 2 ]{49: CSTI 0}
[ 4 -999 4 2 2 2 0 ]{51: ADD}
[ 4 -999 4 2 2 2 ]{52: LDI}
[ 4 -999 4 2 2 4 ]{53: LT}
[ 4 -999 4 2 1 ]{54: IFNZRO 18}
[ 4 -999 4 2 ]{18: GETBP}
[ 4 -999 4 2 2 ]{19: CSTI 1}
[ 4 -999 4 2 2 1 ]{21: ADD}
[ 4 -999 4 2 3 ]{22: LDI}
[ 4 -999 4 2 2 ]{23: PRINTI}
2 [ 4 -999 4 2 2 ]{24: INCSP -1}
[ 4 -999 4 2 ]{26: GETBP}
[ 4 -999 4 2 2 ]{27: CSTI 1}
[ 4 -999 4 2 2 1 ]{29: ADD}
[ 4 -999 4 2 3 ]{30: GETBP}
[ 4 -999 4 2 3 2 ]{31: CSTI 1}
[ 4 -999 4 2 3 2 1 ]{33: ADD}
[ 4 -999 4 2 3 3 ]{34: LDI}
[ 4 -999 4 2 3 2 ]{35: CSTI 1}
[ 4 -999 4 2 3 2 1 ]{37: ADD}
[ 4 -999 4 2 3 3 ]{38: STI}
[ 4 -999 4 3 3 ]{39: INCSP -1}
[ 4 -999 4 3 ]{41: INCSP 0}
[ 4 -999 4 3 ]{43: GETBP}
[ 4 -999 4 3 2 ]{44: CSTI 1}
[ 4 -999 4 3 2 1 ]{46: ADD}
[ 4 -999 4 3 3 ]{47: LDI}
[ 4 -999 4 3 3 ]{48: GETBP}
[ 4 -999 4 3 3 2 ]{49: CSTI 0}
[ 4 -999 4 3 3 2 0 ]{51: ADD}
[ 4 -999 4 3 3 2 ]{52: LDI}
[ 4 -999 4 3 3 4 ]{53: LT}
[ 4 -999 4 3 1 ]{54: IFNZRO 18}
[ 4 -999 4 3 ]{18: GETBP}
[ 4 -999 4 3 2 ]{19: CSTI 1}
[ 4 -999 4 3 2 1 ]{21: ADD}
[ 4 -999 4 3 3 ]{22: LDI}
[ 4 -999 4 3 3 ]{23: PRINTI}
3 [ 4 -999 4 3 3 ]{24: INCSP -1}
[ 4 -999 4 3 ]{26: GETBP}
[ 4 -999 4 3 2 ]{27: CSTI 1}
[ 4 -999 4 3 2 1 ]{29: ADD}
[ 4 -999 4 3 3 ]{30: GETBP}
[ 4 -999 4 3 3 2 ]{31: CSTI 1}
[ 4 -999 4 3 3 2 1 ]{33: ADD}
[ 4 -999 4 3 3 3 ]{34: LDI}
[ 4 -999 4 3 3 3 ]{35: CSTI 1}
[ 4 -999 4 3 3 3 1 ]{37: ADD}
[ 4 -999 4 3 3 4 ]{38: STI}
[ 4 -999 4 4 4 ]{39: INCSP -1}
[ 4 -999 4 4 ]{41: INCSP 0}
[ 4 -999 4 4 ]{43: GETBP}
[ 4 -999 4 4 2 ]{44: CSTI 1}
[ 4 -999 4 4 2 1 ]{46: ADD}
[ 4 -999 4 4 3 ]{47: LDI}
[ 4 -999 4 4 4 ]{48: GETBP}
[ 4 -999 4 4 4 2 ]{49: CSTI 0}
[ 4 -999 4 4 4 2 0 ]{51: ADD}
[ 4 -999 4 4 4 2 ]{52: LDI}
[ 4 -999 4 4 4 4 ]{53: LT}
[ 4 -999 4 4 0 ]{54: IFNZRO 18}
[ 4 -999 4 4 ]{56: INCSP -1}
[ 4 -999 4 ]{58: RET 0}
[ 4 ]{4: STOP}
```

# 8.3
Comp.fs
```fsharp
and cExpr (e : expr) (varEnv : varEnv) (funEnv : funEnv) : instr list = 
    match e with
    | Access acc     -> cAccess acc varEnv funEnv @ [LDI] 
    | Assign(acc, e) -> cAccess acc varEnv funEnv @ cExpr e varEnv funEnv @ [STI]
    | CstI i         -> [CSTI i]
    | Addr acc       -> cAccess acc varEnv funEnv
    | Prim1(ope, e1) ->
      cExpr e1 varEnv funEnv
      @ (match ope with
         | "!"      -> [NOT]
         | "printi" -> [PRINTI]
         | "printc" -> [PRINTC]
         | _        -> raise (Failure "unknown primitive 1"))
    | Prim2(ope, e1, e2) ->
      cExpr e1 varEnv funEnv
      @ cExpr e2 varEnv funEnv
      @ (match ope with
         | "*"   -> [MUL]
         | "+"   -> [ADD]
         | "-"   -> [SUB]
         | "/"   -> [DIV]
         | "%"   -> [MOD]
         | "=="  -> [EQ]
         | "!="  -> [EQ; NOT]
         | "<"   -> [LT]
         | ">="  -> [LT; NOT]
         | ">"   -> [SWAP; LT]
         | "<="  -> [SWAP; LT; NOT]
         | _     -> raise (Failure "unknown primitive 2"))
    | Andalso(e1, e2) ->
      let labend   = newLabel()
      let labfalse = newLabel()
      cExpr e1 varEnv funEnv
      @ [IFZERO labfalse]
      @ cExpr e2 varEnv funEnv
      @ [GOTO labend; Label labfalse; CSTI 0; Label labend]            
    | Orelse(e1, e2) -> 
      let labend  = newLabel()
      let labtrue = newLabel()
      cExpr e1 varEnv funEnv
      @ [IFNZRO labtrue]
      @ cExpr e2 varEnv funEnv
      @ [GOTO labend; Label labtrue; CSTI 1; Label labend]
    | Call(f, es) -> callfun f es varEnv funEnv
    | PreInc acc -> 
      cAccess acc varEnv funEnv @ [DUP; LDI; CSTI 1; ADD; STI]
    | PreDec acc -> 
      cAccess acc varEnv funEnv @ [DUP; LDI; CSTI 1; SUB; STI]
```
To test the function
```fsharp
void main(int n) { 
    int i; 
    i=0;   
    ++i;
    print i; 
    
  }
```

Run the file
```fsharp
> compileToFile (fromFile "testPreIncDec.c") "testPreIncDec.out";;
val it : Machine.instr list =
  [LDARGS; CALL (1, "L1"); STOP; Label "L1"; INCSP 1; GETBP; CSTI 1; ADD;
   CSTI 0; STI; INCSP -1; GETBP; CSTI 1; ADD; DUP; LDI; CSTI 1; ADD; STI;
   INCSP -1; GETBP; CSTI 1; ADD; LDI; PRINTI; INCSP -1; INCSP -1; RET 0]
```



# 8.4

```fsharp
> compileToFile (fromFile "ex8.c") "ex8.out";;
val it : Machine.instr list =
  [LDARGS; CALL (0, "L1"); STOP; Label "L1"; INCSP 1; GETBP; CSTI 0; ADD;
   CSTI 20000000; STI; INCSP -1; GOTO "L3"; Label "L2"; GETBP; CSTI 0; ADD;
   GETBP; CSTI 0; ADD; LDI; CSTI 1; SUB; STI; INCSP -1; INCSP 0; Label "L3";
   GETBP; CSTI 0; ADD; LDI; IFNZRO "L2"; INCSP -1; RET -1]
```

```fsharp
LDARGS;
CALL (0, "L1");
STOP;
Label "L1";
INCSP 1;
GETBP;
CSTI 0;
ADD;
CSTI 20000000;
STI;
INCSP -1;
GOTO "L3";
Label "L2";
GETBP;
CSTI 0;
ADD;
GETBP;
CSTI 0;
ADD;
LDI;
CSTI 1;
SUB;
STI;
INCSP -1;
INCSP 0;
Label "L3";
GETBP;
CSTI 0;
ADD;
LDI;
IFNZRO "L2";
INCSP -1;
RET -1;


Ran 0.57 seconds
```

```numeric
0 20000000 16 7 0 1 2 9 18 4 25
Ran 0.126 seconds
```

Prog1 is more efficient because it doesn't need to 
- Setup main.
- Uses a lot less instructions
- Avoids declaring i 
- Avoids storing 20m in i 
- it uses divide instead of subtracting, making the "calculation" a lot faster.












We can see that there are a lot of redundant jumps, so the program can be optimized more.
```bash
24 
19 1 5
25
15 1
13 = Done initalizing main
0 1
1
0 1889
12 = init y and saved 1889
15 -1 = shrink stack
16 95 = jump to #1
13 = #2 = setting up if state
0 1
1
13
0 1
1
11
0 1
1
12
15 -1
13
0 1
1
11 = If statement setup done, load everything
0 4 = logic if statment if y%4
5
0 0
6
17 77 = Jump to #3 if it's true IFZERO 
13 ---- this is the start of && (y%100.....)
0 1
1
11
0 100 = push 100
5
0 0
6 = check EQ MOD
8
18 73 = jump if not zero #5 
13
0 1
1
11
0 400
5
0 0
6 = Check y% 400 EQ 0
16 75 = Jump if true #7
0 1 = #5
16 79 <- #7 = Jump to #6 
0 0 <- #3
17 91 = <- #6, Jump to #4 if also zero 
13 = get y
0 1
1
11
22 = print y
15 -1 
16 93
15 0 <- #4
15 0 
13 <- #1
0 1
1
11
13
0 0
1
11
7 = if y < n 
18 18 = jump to #2, IFNZRO 
15 -1 = shrink
21 0 = return
```

# 8.5



# 8.6