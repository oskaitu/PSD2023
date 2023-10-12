
int *sump;

void main(int n){
    int array[4];
    array[0] = 7;
    array[1] = 13;
    array[2] = 9;
    array[3] = 8;
    
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