//Kordian Ceranowski
public class Main {
    public static void main(String[] args) {
        long startTime = System.nanoTime();

// Przed użyciem proszę ustawić ścieżkę zapisu pliku!
// Znajduje się ona w zmiennej Const.PATH_TO_FILE

// Do tworzenia wykresów z osią x
        Graph graph = new Graph(0, 2, 32000, 1000000);
        graph.packDataToCSV();

// Do tworzenia wykresów z osią n
//        Ln ln = new Ln(0.0001, 1000);
//        ln.packDataToCSV();
//        ln.packShortDataToCSV();

// Do tworzeni wykresów precyzji (do pytania nr2)
//        new PrecisionTest(0, 2, 0.002, 1000);

        System.out.println(((System.nanoTime() - startTime)/1000000 + "ms"));
    }
}