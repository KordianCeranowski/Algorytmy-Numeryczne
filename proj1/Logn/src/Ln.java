//Kordian Ceranowski
import java.io.FileWriter;
import java.util.ArrayList;
import java.util.Collections;


public class Ln {

    private ArrayList<Double> naiveValues; //generated using taylor
    private ArrayList<Double> smartValues; //generated by multiplying last value

    ArrayList<Double> sumsNaiveNormal;
    ArrayList<Double> sumsSmartNormal;
    ArrayList<Double> sumsNaiveReversed;
    ArrayList<Double> sumsSmartReversed;

    ArrayList<Double> errNaiveNormal;
    ArrayList<Double> errSmartNormal;
    ArrayList<Double> errNaiveReversed;
    ArrayList<Double> errSmartReversed;

    private ArrayList<Double> errNaiveNormalShort;
    private ArrayList<Double> errSmartNormalShort;
    private ArrayList<Double> errNaiveReversedShort;
    private ArrayList<Double> errSmartReversedShort;

    int nForNaiveNormalPrecision;
    int nForSmartNormalPrecision;
    int nForNaiveReversedPrecision;
    int nForSmartReversedPrecision;

    private double x;
    private  double N;
    private  double trueLn;

    //szereg McLaurina przewiduje dokładność dla wartości -1<x<=1 dla ln=(1+x)
    // wyraz to (((-1)^(n+1))/n) * x^n

    public Ln(double x, int N) {

        System.out.println(" --------- Ln(" + x + ") --------- ");

        this.x = x;
        this.N = N;
        trueLn = java.lang.Math.log(x);
        long startTime = System.nanoTime();

        System.out.println("Wyliczam wyrazy");
        naiveValues = fillArrayNaive();
        smartValues = fillArraySmart();
        System.out.println(((System.nanoTime() - startTime)/1000000 + "ms")); startTime = System.nanoTime();

        System.out.println("Sumuję");
        sumsNaiveNormal = fillSumsNormal(naiveValues);
        sumsSmartNormal = fillSumsNormal(smartValues);
        sumsNaiveReversed = fillSumsReversed(naiveValues);
        sumsSmartReversed = fillSumsReversed(smartValues);
        System.out.println(((System.nanoTime() - startTime)/1000000 + "ms")); startTime = System.nanoTime();

        System.out.println("Wyliczam błędy");
        errNaiveNormal = getError(sumsNaiveNormal);
        errSmartNormal = getError(sumsSmartNormal);
        errNaiveReversed = getError(sumsNaiveReversed);
        errSmartReversed = getError(sumsSmartReversed);
        System.out.println(((System.nanoTime() - startTime)/1000000 + "ms")); startTime = System.nanoTime();

        System.out.println("Uśredniam dane");
        errNaiveNormalShort = fillShortenArray(errNaiveNormal);
        errSmartNormalShort = fillShortenArray(errSmartNormal);
        errNaiveReversedShort = fillShortenArray(errNaiveReversed);
        errSmartReversedShort = fillShortenArray(errSmartReversed);
        System.out.println(((System.nanoTime() - startTime)/1000000 + "ms")); startTime = System.nanoTime();

        System.out.println("Oblczam wymaganą precyzję");
        nForNaiveNormalPrecision = getPrecision(errNaiveNormal);
        nForSmartNormalPrecision = getPrecision(errSmartNormal);
        nForNaiveReversedPrecision = getPrecision(errNaiveReversed);
        nForSmartReversedPrecision = getPrecision(errSmartReversed);
        System.out.println(((System.nanoTime() - startTime)/1000000 + "ms")); startTime = System.nanoTime();

    }

    private ArrayList<Double> fillArrayNaive(){
        double x = this.x-1.0d;
        ArrayList<Double> naiveValues = new ArrayList<>();
        for(int n = 1; n <= N; n++){
            naiveValues.add((MyMath.powForMinusOne(n+1.0d)/n) * MyMath.power(x, n));
        }
        return naiveValues;
    }
    private ArrayList<Double> fillArraySmart(){
        double x = this.x-1;
        ArrayList<Double> smartValues = new ArrayList<>();
        double tempValue = x;
        for(double n=1; n<=N; n++){
            //różnica między kolejnymi wyrazami to -nx/(n+1)
            smartValues.add(tempValue);
            tempValue *= (-n*x/(n+1.0));
        }
        return smartValues;
    }

    private ArrayList<Double> fillSumsNormal(ArrayList<Double> in){
        ArrayList<Double> out = new ArrayList<>();
        double tempSum = 0;
        for(double val : in){
            tempSum += val;
            out.add(tempSum);
        }
        return out;
    }
    private ArrayList<Double> fillSumsReversed(ArrayList<Double> in){
        ArrayList<Double> out = new ArrayList<>();
        double sum = 0;
        for(int i = in.size()-1; i >= 0; i--){
            sum += in.get(i);
        }
        out.add(sum);

        for(int i = 1; i < in.size(); i++){
            sum -= in.get(in.size() - i);
            out.add(sum);
        }
        Collections.reverse(out);
        return out;
    }

    private ArrayList<Double> getError(ArrayList<Double> in){
        ArrayList<Double> out = new ArrayList<>();
        for (Double val : in){
            out.add(Math.abs(this.trueLn - val));
        }
        return out;
    }

    public ArrayList<Double> fillShortenArray(ArrayList<Double> in){
        ArrayList<Double> out = new ArrayList<>();
        double sum = 0;
        for(int i=0; i<in.size(); i++){
            sum += in.get(i);
            if(i % Const.CLUSTER_SIZE == (Const.CLUSTER_SIZE-1)){
                out.add(sum/Const.CLUSTER_SIZE);
                sum = 0;
            }
        }
        return out;
    }

    public void packDataToCSV(){
        try{
            FileWriter fw=new FileWriter(Const.PATH_TO_FILE + "Wynik Ln(" + x + "), n=" +N+ ".csv");
            fw.write( "n" +Const.SEPARATOR+ "errNaiveNormal" +Const.SEPARATOR+ "errNaiveReversed" +Const.SEPARATOR+ "errSmartNormal" +Const.SEPARATOR+ "errSmartReversed\n");

            for (int i = 0; i<N; i++) {
                fw.write((i+1 +Const.SEPARATOR+ errNaiveNormal.get(i)+Const.SEPARATOR+ errNaiveReversed.get(i) +Const.SEPARATOR+ errSmartNormal.get(i) +Const.SEPARATOR+ errSmartReversed.get(i) + "\n").replace('.', ','));
                System.out.println(i);
            }

            fw.close();
        }catch(Exception e){System.out.println(e);}
        System.out.println("Direct Data extracted...");
    }
    public void packShortDataToCSV(){
        System.out.println("Buduję plik skrócony");
        try{
            FileWriter fw=new FileWriter(Const.PATH_TO_FILE + "Skrócony wynik Ln(" + x + ").csv");
            fw.write( "n" +Const.SEPARATOR+ "errNaiveNormal" +Const.SEPARATOR+ "errNaiveReversed" +Const.SEPARATOR+ "errSmartNormal" +Const.SEPARATOR+ "errSmartReversed\n");

            for (int i = 0; i<N/Const.CLUSTER_SIZE; i++) {
                fw.write(((i+1)*1000 +Const.SEPARATOR+ errNaiveNormalShort.get(i)+Const.SEPARATOR+ errNaiveReversedShort.get(i) +Const.SEPARATOR+ errSmartNormalShort.get(i).toString() +Const.SEPARATOR+ errSmartReversedShort.get(i) + "\n").replace('.', ','));
                System.out.println(i);
            }

            fw.close();
        }catch(Exception e){System.out.println(e);}
        System.out.println("Direct Data extracted...");
    }

    public int getPrecision(ArrayList<Double> in){
        for(int n = 0; n < N; n++){
            if(in.get(n) < 0.000001){
                return n+1;
            }
        }
        return Integer.MAX_VALUE;
    }
}
