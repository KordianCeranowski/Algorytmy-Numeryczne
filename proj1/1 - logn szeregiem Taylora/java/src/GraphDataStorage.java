import java.util.ArrayList;
import java.io.FileWriter;

public class GraphDataStorage {

    public static final String PRECISION = "%.32f";
    public static final char SEPARATOR = ';';

    public class GraphIndex {
        int numberOfTries;
        double naiveNormErr;
        double naiveRevErr;
        double smartNormErr;
        double smartRevErr;

        public GraphIndex(int numberOfTries, double naiveNormErr, double naiveRevErr, double smartNormErr, double smartRevErr) {
            this.numberOfTries = numberOfTries;
            this.naiveNormErr = naiveNormErr;
            this.naiveRevErr = naiveRevErr;
            this.smartNormErr = smartNormErr;
            this.smartRevErr = smartRevErr;
        }

        @Override
        public String toString() {
            return  String.format(PRECISION, naiveNormErr)   +   SEPARATOR +
                    String.format(PRECISION, naiveRevErr)    +   SEPARATOR +
                    String.format(PRECISION, smartNormErr)   +   SEPARATOR +
                    String.format(PRECISION, smartRevErr)    +   "\n" ;
        }

        public void scaleUpValues(double zeroes){
            naiveNormErr    *= Math.pow(10, zeroes);
            naiveRevErr     *= Math.pow(10, zeroes);
            smartNormErr    *= Math.pow(10, zeroes);
            smartRevErr     *= Math.pow(10, zeroes);
        }
    }

    ArrayList<GraphIndex> graph;

    public GraphDataStorage() {
        this.graph = new ArrayList<>();
    }

    public void addIndex(int numberOfTries, double naiveNormErr, double naiveRevErr, double smartNormErr, double smartRevErr){
        graph.add(new GraphIndex(numberOfTries, naiveNormErr, naiveRevErr, smartNormErr, smartRevErr));
    }

    public void getTestData(int p, double x){
        Ln tempLn = new Ln(p, x);
        double var1 = Math.abs(tempLn.getRelativeError(tempLn.getSum                 (tempLn.naiveValues)));
        double var2 = Math.abs(tempLn.getRelativeError(tempLn.getReverseCountedSum   (tempLn.naiveValues)));
        double var3 = Math.abs(tempLn.getRelativeError(tempLn.getSum                 (tempLn.smartValues)));
        double var4 = Math.abs(tempLn.getRelativeError(tempLn.getReverseCountedSum   (tempLn.smartValues)));

        this.addIndex(p, var1, var2, var3, var4);
    }

    public void scaleUpDataset(double zeroes){
        for (GraphIndex gi : this.graph) {
            gi.scaleUpValues(zeroes);
        }
    }

    public void packDataToCSV(){
        try{
            FileWriter fw=new FileWriter("C:\\Users\\Kordian\\Desktop\\algorytmy numeryczne\\Direct Data.csv");
            fw.write("naiveNormErr" +SEPARATOR+ "naiveRevErr" +SEPARATOR+ "smartNormErr" +SEPARATOR+ "smartRevErr\n");

            for (GraphIndex graphIndex : this.graph) {
                fw.write(graphIndex.toString());
            }

            fw.close();
        }catch(Exception e){System.out.println(e);}
        System.out.println("Direct Data extracted...");
    }

}
