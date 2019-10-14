public interface MyMath {

    static double pow(double var1, double var2){
        double tempVar = 1;
        while(var2 > 0){
            tempVar *= var1;
            var2--;
        }
        return tempVar;
    }

    static double powForMinusOne(double var2){
        int temp = (int)var2;
        if( temp % 2 == 1.0d )
            return -1;
        else
            return 1;
    }

    static long factorialNORM( long val ){
        long sum = 1;
        for(int i = 2; i <= val; i++ ){
            sum *= i;
        }
        return sum;
    }

    static long factorialREC(long val){
        if(val == 1)
            return 1;
        return val * factorialREC(val-1);
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