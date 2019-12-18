using System;
using System.Collections.Generic;

namespace RecommenderSystem
{
    internal static class ObjectiveFunction
    {
        public static void Calculate(int d, RMatrix R, Matrix U, Matrix P, double lambda)
        {

            double sum1 = 0;

            foreach (var kvp in R.ratings)
            {
                double r_up = kvp.Value;
                int u = kvp.Key.Item1;
                int p = kvp.Key.Item2;

                double vectorSum = 0;
                for (int row = 0; row < d; row++)
                {
                    vectorSum += U.Data[row, u] * P.Data[row, p];
                }

                sum1 += Math.Pow(r_up + vectorSum, 2);
            }


            double sum2 = 0;

            for (int col = 0; col < U.ColumnCount; col++)
            {
                double vectorSum = 0;
                for (int row = 0; row < d; row++)
                {
                    vectorSum += Math.Pow(U.Data[row, col], 2);
                }

                sum2 += vectorSum;
            }


            double sum3 = 0;

            for (int col = 0; col < P.ColumnCount; col++)
            {
                double vectorSum = 0;
                for (int row = 0; row < d; row++)
                {
                    vectorSum += Math.Pow(P.Data[row, col], 2);
                }

                sum3 += vectorSum;
            }


            double total = sum1 + lambda * (sum2 + sum3);

            //Console.WriteLine($"sum1 = ${sum1}, sum2 = ${sum2}, sum3 = ${sum3}, total = ${total}");
            Console.WriteLine(total);
        }
    }
}