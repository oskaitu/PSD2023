// micro-C example 1
void main(int n) {
    int arr[4];
    arr[0] = 1;
    arr[1] = 1;
    arr[2] = 1;
    arr[3] = 2;   

    int i;
    for(i = 0; i < 4; ++i){
        ++arr[i];
        print arr[i];
        println;
    }
}
