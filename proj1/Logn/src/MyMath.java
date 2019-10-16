public interface MyMath {

    static double powForMinusOne(double var2){
        int temp = (int)var2;
        if( temp % 2 == 1.0d )
            return -1;
        else
            return 1;
    }

    //https://www.techiedelight.com/power-function-implementation-recursive-iterative/
    static double power(double x, int n){
        if (n == 0)
            return 1;
        double halfPower = power(x, n / 2);

        if ((n & 1) == 1){
            return x * halfPower * halfPower;
        }
        return halfPower * halfPower;
    }

}