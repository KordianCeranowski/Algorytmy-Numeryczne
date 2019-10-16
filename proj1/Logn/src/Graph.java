//Kordian Ceranowski
import java.io.FileWriter;
import java.util.ArrayList;

public class Graph {
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

        private GraphIndex(double x, double varNN, double varNR, double varSN, double varSR, double errNN, double errNR, double errSN, double errSR) {
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
                    Const.SEPARATOR + varNN +
                    Const.SEPARATOR + varNR +
                    Const.SEPARATOR + varSN +
                    Const.SEPARATOR + varSR +
                    Const.SEPARATOR + errNN +
                    Const.SEPARATOR + errNR +
                    Const.SEPARATOR + errSN +
                    Const.SEPARATOR + errSR +
                    Const.SEPARATOR + Math.log(x))
                    .replace('.', ',') ;
        }
    }

    private int n;
    private double startX;
    private double finishX;
    private double parts;

    private ArrayList<GraphIndex> graphData;

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
                    ln.errSmartReversed.get(n-1)
                    ));
        }
    }

    public void packDataToCSV(){
        try{
            System.out.println("Saving graph data...");
            FileWriter fw=new FileWriter(Const.PATH_TO_FILE + "Graf(" + startX + "," + finishX + ")," + parts + " parts, n="+n+ ".csv");
            fw.write( "X" +Const.SEPARATOR+ "ValueNaiveNormal" +Const.SEPARATOR+ "ValueNaiveReversed" +Const.SEPARATOR+ "ValueSmartNormal" +Const.SEPARATOR+ "ValueSmartReversed"+Const.SEPARATOR+ "ErrorNaiveNormal" +Const.SEPARATOR+ "ErrorNaiveReversed" +Const.SEPARATOR+ "ErrorSmartNormal" +Const.SEPARATOR+ "ErrorSmartReversed" +Const.SEPARATOR+ "TrueLogn\n");

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
