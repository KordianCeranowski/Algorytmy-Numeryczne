import java.io.FileWriter;
import java.util.ArrayList;

public class AverageErrorDataStorage {

    public class AverageErrorIndex{

        double average;
        double percentNN;
        double percentNR;
        double percentSN;
        double percentSR;

        public AverageErrorIndex(GraphDataStorage.GraphIndex graphIndex) {
            double sum = 0;
            sum += graphIndex.naiveNormErr;
            sum += graphIndex.naiveRevErr;
            sum += graphIndex.smartNormErr;
            sum += graphIndex.smartRevErr;

            this.average = sum/4;
            this.percentNN = graphIndex.naiveNormErr/sum*4;
            this.percentNR = graphIndex.naiveRevErr/sum*4;
            this.percentSN = graphIndex.smartNormErr/sum*4;
            this.percentSR = graphIndex.smartRevErr/sum*4;
        }

        @Override
        public String toString() {
            return  String.format(GraphDataStorage.PRECISION, average)   +   GraphDataStorage.SEPARATOR +
                    String.format(GraphDataStorage.PRECISION, percentNN)   +   GraphDataStorage.SEPARATOR +
                    String.format(GraphDataStorage.PRECISION, percentNR)    +   GraphDataStorage.SEPARATOR +
                    String.format(GraphDataStorage.PRECISION, percentSN)   +   GraphDataStorage.SEPARATOR +
                    String.format(GraphDataStorage.PRECISION, percentSR)    +   "\n" ;
        }
    }

    ArrayList<AverageErrorIndex> errorData;

    public AverageErrorDataStorage(GraphDataStorage gds) {
        errorData = new ArrayList<>();

        for (GraphDataStorage.GraphIndex gi : gds.graph){
            errorData.add(new AverageErrorIndex(gi));
        }
    }

    public void packDataToCSV(){
        try{
            FileWriter fw=new FileWriter("C:\\Users\\Kordian\\Desktop\\algorytmy numeryczne\\Percentage Data.csv");
            fw.write(   "Average Error" + GraphDataStorage.SEPARATOR +
                            "%NN"   + GraphDataStorage.SEPARATOR +
                            "%NR"   + GraphDataStorage.SEPARATOR +
                            "%SN"   + GraphDataStorage.SEPARATOR +
                            "%SR\n");

            for (AverageErrorIndex aei : this.errorData) {
                fw.write(aei.toString());
            }

            fw.close();
        }catch(Exception e){System.out.println(e);}
        System.out.println("Percentage Data extracted...");
    }

}
