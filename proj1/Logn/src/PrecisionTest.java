//Kordian Ceranowski
import java.io.FileWriter;
import java.util.ArrayList;

public class PrecisionTest {
    private static final String SEPARATOR = ";";
    ArrayList<Double> currX;
    ArrayList<Integer> NN;
    ArrayList<Integer> SN;
    ArrayList<Integer> NR;
    ArrayList<Integer> SR;

    public PrecisionTest(double startX, double endX, double differential, int maxN) {
        double startTime = System.nanoTime();
        currX = new ArrayList<>();
        NN = new ArrayList<>();
        SN = new ArrayList<>();
        NR = new ArrayList<>();
        SR = new ArrayList<>();
        Ln tempLn;
        for(double x = startX; x <= endX; x += differential){
            tempLn = new Ln(x, maxN);
            currX.add(x);
            NN.add(tempLn.nForNaiveNormalPrecision);
            SN.add(tempLn.nForSmartNormalPrecision);
            NR.add(tempLn.nForNaiveReversedPrecision);
            SR.add(tempLn.nForSmartReversedPrecision);
        }

        packDataToCSV(startX, endX, (int)((endX-startX)/differential));

        System.out.println(("Proces zajął " + (System.nanoTime() - startTime)/1000000 + "ms"));

    }

    public void packDataToCSV(double start, double stop, int times){
        try{
            System.out.println("Saving precision data...");
            FileWriter fw=new FileWriter("C:\\Users\\Kordian\\Desktop\\LnData\\Wyniki badania precyzji(" +start + ", " + stop + ").csv");
            fw.write( "X" +SEPARATOR+ "NaiveNormal" +SEPARATOR+ "NaiveReversed" +SEPARATOR+ "SmartNormal" +SEPARATOR+ "SmartReversed\n");

            for (int i = 0; i<times; i++) {
                fw.write((currX.get(i) +SEPARATOR+ NN.get(i) +SEPARATOR+ SN.get(i) +SEPARATOR+ NR.get(i) +SEPARATOR+ SR.get(i) + "\n").replace('.', ','));
            }

            fw.close();
        }catch(Exception e){System.out.println(e);}
        System.out.println("Precision Data extracted...");
    }

}
