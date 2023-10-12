# 7.1
Because the program only have the main function there is only 1 declaration, proceeded by two statements, the while loop on the right and the println on the left.
![](attachments/7.1.jpg)

# 7.2

## (i)

Implementation of arrsum

```c
int *sump;

void main(){
    int array[4];
    array[0] = 7;
    array[1] = 13;
    array[2] = 9;
    array[3] = 8;

    int n;
    n = 4;
    
    arrsum(n, array, sump);
    print *sump;
    println;
}
void arrsum(int n, int arr[], int *sump ){
    int i;
    i = 0;
    int sum;
    sum = 0;

    while(i < n){
        sum = sum + arr[i];
        i = i + 1;
    }
    *sump = sum;
    
}
```
To run it we had to add that main takes an integer to get the result, otherwise it just gave us <fun:Invoke@3236>
```c
run (fromFile "ex72i.c") [4];;
37 
val it : Interp.store =
  map
    [(-1, 37); (0, -1); (1, 4); (2, 7); (3, 13); (4, 9); (5, 8); (6, 2);
     (7, 4); ...]
```

# 7.3

# 7.4

# 7.5
