int *sump;

void main(int n){
    int array[20];
    squares(n, array);
    arrsum(n, array, sump);

    print *sump;
    println;

}

void squares(int n, int arr[]){
    int i;
    for (i=0; i<n; i = i + 1)
    {
        arr[i] = i * i;
        i = i + 1;
    }
}

void arrsum(int n, int arr[], int *sump ){
    int i;
    int sum;
    sum = 0;
    for (i = 0; i < n; i = i + 1)
    {
        sum = sum + arr[i];
    }
    *sump = sum;
    
}