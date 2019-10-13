public class Main {
    public static void main(String[] args) {

        long startTime = System.nanoTime();

        Ln ln = new Ln(0.0001, 1000000);
        System.out.println();
        System.out.println(((System.nanoTime() - startTime)/1000000 + "ms"));


        //new PrecisionTest(0, 0.01, 0.0001, 1000000);


        startTime = System.nanoTime();
        //ln.packDataToCSV();
        ln.packShortDataToCSV();
        System.out.println(((System.nanoTime() - startTime)/1000000 + "ms"));

    }
}