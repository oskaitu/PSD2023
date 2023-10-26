

void main(int n){
    int array[4];
    array[0] = 7;
    array[1] = 13;
    array[2] = 9;
    array[3] = 8;    
    int *sump;

    arrsum(n, array, sump);
    print *sump;
    println;
}
void arrsum(int n, int arr[], int *sump ){
    int i;
    int sum;
    sum = 0;
    for (i = 0; i<n; i = i + 1)
    {
        sum = sum + arr[i];
    }
    *sump = sum;
}