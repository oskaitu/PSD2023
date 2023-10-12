
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
    i = 0;

    int freq[4];

    while( i < max){
        freq[i] = 0;
        i = i + 1;
    }
   

    histogram(n, arr, max, freq);

    int i;
    i = 0;
    while(i < max){
        print freq[i];
        println;
        i = i + 1;
    }


}

void histogram(int n, int ns[], int max, int freq[]){
    int i;
    i = 0;
    int hotdog;

    while(i < n){
        hotdog = ns[i];
        freq[hotdog] = freq[hotdog] + 1;
        i = i + 1;
    }
}