using System;

namespace Gauss
{
    class Program
    {
        static void Main()
        {
            //TestPoprawnosci();

            GaussTestSameMatrix.Run(100, MultiThreading.ON_WITH_MY_TYPE);
            
            Console.Beep();
        }


        static void TestPoprawnosci()
        {
            //Test mnożenia macierzy
            MyType[][] arrA = new MyType[3][];
            arrA[0] = new MyType[] { new MyType(1, 1), new MyType(-3, 1), new MyType(4, 1) };
            arrA[1] = new MyType[] { new MyType(16, 1), new MyType(6, 1), new MyType(7, 1) };
            arrA[2] = new MyType[] { new MyType(-5, 1), new MyType(10, 1), new MyType(4, 1) };
            MyMatrix<MyType> A = new MyMatrix<MyType>(arrA);

            MyType[][] arrX = new MyType[3][];
            arrX[0] = new MyType[] { new MyType(5, 1) };
            arrX[1] = new MyType[] { new MyType(2, 1) };
            arrX[2] = new MyType[] { new MyType(6, 1) };
            MyMatrix<MyType> X = new MyMatrix<MyType>(arrX);

            MyMatrix<MyType> B = A * X;

            Console.WriteLine("Wynik A * X");
            B.ShowVector();

            MyMatrix<MyType> AB = A.Connect(B);

            AB.PerformGaussianElimination(Choice.NONE);
            AB.PerformBackwardsOperation();

            Console.WriteLine("\nWynik A|B");
            AB.ShowVector();
        }
    }
}
