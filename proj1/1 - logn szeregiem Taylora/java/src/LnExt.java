import java.util.ArrayList;
import java.util.Collections;

public class LnExt extends Ln {

    ArrayList<Double> partialSumsNN;
    ArrayList<Double> partialSumsNR;
    ArrayList<Double> partialSumsSN;
    ArrayList<Double> partialSumsSR;

    public LnExt(int n, double x) {
        super(n, x);
        partialSumsNN = countPartialSum(this.naiveValues);
        Collections.reverse(this.naiveValues);
        partialSumsNR = countPartialSum(this.naiveValues);
        Collections.reverse(partialSumsNR);


        partialSumsSN = countPartialSum(this.smartValues);
        Collections.reverse(this.smartValues);
        partialSumsSR = countPartialSum(this.smartValues);
        Collections.reverse(partialSumsSR);

    }

    private ArrayList<Double> countPartialSum(ArrayList<Double> in){
        ArrayList<Double> out = new ArrayList<>();

        out.add(in.get(0));

        for(int i = 1; i<this.N; i++){
            out.add(    out.get(i-1) + in.get(i)    );
        }

        return out;
    }
}
