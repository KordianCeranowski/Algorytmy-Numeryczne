using System;
using System.Diagnostics;
using System.IO;

namespace Gauss
{
    class GaussTestSpecific<T>
    {
        private static readonly string FILENAME = GaussTestSameMatrix.FILENAME;

        public static void Run(MyMatrix<T> A, MyMatrix<T> X, Choice choice)
        {
            MyMatrix<T> B = A * X;
            MyMatrix<T> AB = A.Connect(B);

            // Przeprowadzenie Gaussa
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            AB.PerformGaussianElimination(choice);
            stopWatch.Stop();

            AB.PerformBackwardsOperation();

            // Wyciąganie wektorów

            T[][] results = new T[2][];
            results[0] = X.ExtractLastColumn();
            results[1] = AB.ExtractLastColumn();

            T[] errors = new T[results[0].Length];
            for (int index = 0; index < results[0].Length; index++)
            {
                dynamic temp = results[0][index];
                temp = temp - results[1][index];
                errors[index] = MyMath.ABS(temp);
            }

            WriteStatistics(choice, A.Data.Length, errors, stopWatch);
        }


        private static double QuadraticNorm(T[] errors)
        {
            double sum = 0;

            for (int i = 0; i < errors.Length; i++)
            {
                dynamic temp = errors[i];
                sum += Math.Pow((double)temp, 2);
            }


            sum = Math.Sqrt(sum);

            return sum;
        }


        private static void WriteStatistics(Choice choice, int size, T[] errors, Stopwatch stopWatch)
        {
            Console.WriteLine("ZAPISUJĘ "+ typeof(T) + " " + choice);
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:0000}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds);

            StreamWriter sw = File.AppendText(FILENAME);
            sw.WriteLine("\ttype:           \t" + typeof(T));
            sw.WriteLine("\tchoice:         \t" + choice);
            sw.WriteLine("\tsize:           \t" + size);
            sw.WriteLine("\tQuadaratic Norm:\t" + QuadraticNorm(errors));
            sw.WriteLine("\tTime of Gauss:  \t" + elapsedTime);
            sw.Write(sw.NewLine);
            sw.Close();
        }

    }
}
