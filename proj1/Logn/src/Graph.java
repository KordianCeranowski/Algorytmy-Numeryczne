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

    public void shortenData() {
        System.out.println("");
        ArrayList<GraphIndex> newGraphData = new ArrayList<>();
        double varNNsum = 0;
        double varNRsum = 0;
        double varSNsum = 0;
        double varSRsum = 0;
        double errNNsum = 0;
        double errNRsum = 0;
        double errSNsum = 0;
        double errSRsum = 0;

        for(int i=0; i<graphData.size(); i++){
            varNNsum += graphData.get(i).varNN;
            varNRsum += graphData.get(i).varNR;
            varSNsum += graphData.get(i).varSN;
            varSRsum += graphData.get(i).varSR;
            errNNsum += graphData.get(i).errNN;
            errNRsum += graphData.get(i).errNR;
            errSNsum += graphData.get(i).errSN;
            errSRsum += graphData.get(i).errSR;
            if(i % Const.CLUSTER_SIZE == (Const.CLUSTER_SIZE-1)){
                newGraphData.add(new GraphIndex(
                        graphData.get(i+1).x,
                        varNNsum/Const.CLUSTER_SIZE,
                        varNRsum/Const.CLUSTER_SIZE,
                        varSNsum/Const.CLUSTER_SIZE,
                        varSRsum/Const.CLUSTER_SIZE,
                        errNNsum/Const.CLUSTER_SIZE,
                        errNRsum/Const.CLUSTER_SIZE,
                        errSNsum/Const.CLUSTER_SIZE,
                        errSRsum/Const.CLUSTER_SIZE
                ));
                varNNsum = 0;
                varNRsum = 0;
                varSNsum = 0;
                varSRsum = 0;
                errNNsum = 0;
                errNRsum = 0;
                errSNsum = 0;
                errSRsum = 0;
            }
        }
        this.graphData = newGraphData;
    }

}
