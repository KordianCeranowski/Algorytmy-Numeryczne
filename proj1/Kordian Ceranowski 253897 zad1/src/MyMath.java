public interface MyMath {
    static double pow(double var1, double var2){
        double tempVar = 1;
        while(var2 > 0){
            tempVar *= var1;
            var2--;
        }
        return tempVar;
    }
}
