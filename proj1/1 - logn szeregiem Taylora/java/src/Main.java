public class Main {

    public static void main(String[] args) {

//        long startTime = System.nanoTime();
//        LnExt ln = new LnExt(1000000, 1.5);
//        long endTime = System.nanoTime();
//        System.out.println((endTime - startTime)/1000000);
//        System.out.println();

//  Przykładowe obliczenie 4 sposobami wartości Ln i podanie wyniku w konsoli
//        Ln ln = new Ln(1000, 1.5);
//        System.out.print("Obliczanie ze wzoru:\n\t");
//        System.out.println(ln.getRelativeError(ln.getSum(ln.naiveValues)));
//        System.out.print("przy odwrotnym sumowaniu\n\t");
//        System.out.println(ln.getRelativeError(ln.getReverseCountedSum(ln.naiveValues)));
//
//        System.out.print("\nObliczanie przez mnożenie wyrazów:\n\t");
//        System.out.println(ln.getRelativeError(ln.getSum(ln.smartValues)));
//        System.out.print("przy odwrotnym sumowaniu\n\t");
//        System.out.println(ln.getRelativeError(ln.getReverseCountedSum(ln.smartValues)));
//
//  Tworzy pliki z danymi na temat błędów
        GraphDataStorage graphDataStorage = Ln.createGraph(1, 1000, 0.5);
        graphDataStorage.packDataToCSV();
//
//        AverageErrorDataStorage averageErrorDataStorage = new AverageErrorDataStorage(graphDataStorage);
//        averageErrorDataStorage.packDataToCSV();
//
////  Tworzy plik z danymi na temat ilości prób wymaganych do osiągnięcia precyzji 10^-6
//        Ln.precisionTest(0.05, 0.95, 0.05, 1000 );

    }
}
