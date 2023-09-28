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
## 6.1
## 6.2
## 6.3
## 6.4
## 6.5