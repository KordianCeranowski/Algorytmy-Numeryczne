//Kordian Ceranowski
import java.io.FileWriter;
import java.util.ArrayList;

public class Graph {
    private static final String SEPARATOR = ";";
    private class GraphIndex{
        double x;
        double varNN;
        double varNR;
        double varSN;
        double varSR;
        double errNN;
        double errNR;
        double errSN;
        double errSR;

        public GraphIndex(double x, double varNN, double varNR, double varSN, double varSR, double errNN, double errNR, double errSN, double errSR) {
            this.x = x;
            this.varNN = varNN;
            this.varNR = varNR;
            this.varSN = varSN;
            this.varSR = varSR;
            this.errNN = errNN;
            this.errNR = errNR;
            this.errSN = errSN;
            this.errSR = errSR;
        }

        @Override
        public String toString() {
            return  (x +
                    SEPARATOR + varNN +
                    SEPARATOR + varNR +
                    SEPARATOR + varSN +
                    SEPARATOR + varSR +
                    SEPARATOR + errNN +
                    SEPARATOR + errNR +
                    SEPARATOR + errSN +
                    SEPARATOR + errSR)
                    .replace('.', ',') ;
        }
    }

    int n;
    double startX;
    double finishX;
    double parts;

    ArrayList<GraphIndex> graphData;

    public Graph(double startX, double finishX, double parts, int n) {
        this.n = n;
        this.startX = startX;
        this.finishX = finishX;
        this.parts = parts;
        this.graphData = new ArrayList<>();

        double differential = (finishX - startX)/parts;

        Ln ln;
        for(double x = startX; x <= finishX; x += differential) {
            ln = new Ln(x, n);
            graphData.add(new GraphIndex(
                    x,
                    ln.sumsNaiveNormal.get(n-1),
                    ln.sumsNaiveReversed.get(n-1),
                    ln.sumsSmartNormal.get(n-1),
                    ln.sumsSmartReversed.get(n-1),
                    ln.errNaiveNormal.get(n-1),
                    ln.errNaiveReversed.get(n-1),
                    ln.errSmartNormal.get(n-1),
                    ln.sumsSmartReversed.get(n-1)
                    ));
        }
    }

    public void packDataToCSV(){
        try{
            System.out.println("Saving graph data...");
            FileWriter fw=new FileWriter("C:\\Users\\Kordian\\Desktop\\LnData\\Graf(" + startX + "," + finishX + ")," + parts + " parts, n="+n+ ".csv");
            fw.write( "X" +SEPARATOR+ "ValueNaiveNormal" +SEPARATOR+ "ValueNaiveReversed" +SEPARATOR+ "ValueSmartNormal" +SEPARATOR+ "ValueSmartReversed"+SEPARATOR+ "ErrorNaiveNormal" +SEPARATOR+ "ErrorNaiveReversed" +SEPARATOR+ "ErrorSmartNormal" +SEPARATOR+ "ErrorSmartReversed\n");

            int i=0;
            for (GraphIndex gi : graphData) {
                fw.write(gi.toString() + "\n");
                System.out.println(i/parts*100);
                i++;
            }

            fw.close();
        }catch(Exception e){System.out.println(e);}
        System.out.println("Graph extracted...");
    }

}
