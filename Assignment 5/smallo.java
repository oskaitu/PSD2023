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
