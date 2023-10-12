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
    i = 0;

    while(i < n){
        arr[i] = i * i;
        i = i + 1;
    }
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