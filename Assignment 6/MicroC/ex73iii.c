
void main(int x){
    int arr[7];
    arr[0] = 1;
    arr[1] = 2;
    arr[2] = 1;
    arr[3] = 1;
    arr[4] = 1;
    arr[5] = 2;
    arr[6] = 0;

    int n;
    n = 7;

    int max;
    max = 4;

    int i;
    int freq[4];
    for ( i = 0; i < max; i = i + 1)
    {
        freq[i] = 0;
    }
   

    histogram(n, arr, max, freq);

    int i;
    for (i = 0; i < max; i = i + 1)
    {
        print freq[i];
        println;
    }


}

void histogram(int n, int ns[], int max, int freq[]){
    int i;
    
    int hotdog;
    for(i = 0;i < n;i = i + 1)
    {
        hotdog = ns[i];
        freq[hotdog] = freq[hotdog] + 1;
    }
}