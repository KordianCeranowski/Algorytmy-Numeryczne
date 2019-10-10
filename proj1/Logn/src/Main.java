public class Main {
    public static void main(String[] args) {

        long startTime = System.nanoTime();

        Ln ln = new Ln(1.4, 1000000);

        System.out.println("Math.logn( " + ln.x + " ) = " + ln.trueLn);

        System.out.println();

        long endTime = System.nanoTime();
        System.out.println(((endTime - startTime)/1000000));

//
//        long TEST = 50;
//        long startTime = System.nanoTime();
//        System.out.println(MyMath.pow(2,15));
//        long endTime = System.nanoTime();
//        System.out.println((endTime - startTime/1000000));
//
//        startTime = System.nanoTime();
//        System.out.println(MyMath.power(2,15));
//        endTime = System.nanoTime();
//        System.out.println((endTime - startTime));


    }
}