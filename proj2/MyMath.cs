using System;
using System.Numerics;

namespace Gauss
{
    class MyMath
    {

        public static float ABS(float num) 
        {
            return Math.Abs(num);
        }

        public static double ABS(double num)
        {
            return Math.Abs(num);
        }

        public static MyType ABS(MyType num) 
        {
            return new MyType(BigInteger.Abs(num.Numerator), num.Denominator);
        }
    }
}