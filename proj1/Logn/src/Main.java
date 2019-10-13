public class Main {
    public static void main(String[] args) {

        long startTime = System.nanoTime();

        Ln ln = new Ln(1.22, 1000000);
        System.out.println();
        long endTime = System.nanoTime();
        System.out.println(((endTime - startTime)/1000000 + "ms"));

        startTime = System.nanoTime();
        ln.packDataToCSV();
        endTime = System.nanoTime();
        System.out.println(((endTime - startTime)/1000000 + "ms"));

    }
}