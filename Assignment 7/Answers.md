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

```fsharp
[
LDARGS; 
CALL (1, "L1"); 
STOP; 
Label "L1"; 
INCSP 1; 
GETBP; 
CSTI 1; 
ADD;
GETBP; 
CSTI 0; 
ADD; 
LDI; 
STI; 
INCSP -1; 
INCSP 1;
GETBP; 
CSTI 0; 
ADD; 
LDI;
GETBP; 
CSTI 2; 
ADD; 
CALL (2, "L2"); 
INCSP -1; 
GETBP; 
CSTI 2; 
ADD; 
LDI;
PRINTI; 
INCSP -1; 
INCSP -1; 
GETBP; 
CSTI 1; 
ADD; 
LDI; 
PRINTI; 
INCSP -1;
INCSP -1; 
RET 0; 
Label "L2"; 
GETBP; 
CSTI 1; 
ADD; 
LDI; 
GETBP; 
CSTI 0; 
ADD;
LDI; 
GETBP; 
CSTI 0; 
ADD; 
LDI; 
MUL; 
STI; 
INCSP -1; 
INCSP 0; 
RET 1]
```



### (iii)
```fsharp
```

# 8.3

# 8.4

# 8.5

# 8.6