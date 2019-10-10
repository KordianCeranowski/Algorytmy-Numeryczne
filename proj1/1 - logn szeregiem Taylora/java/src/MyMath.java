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
            return 1;
        else
            return -1;
    }
}
