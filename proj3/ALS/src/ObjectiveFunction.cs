using System;

namespace RecommenderSystem
{
    static class ObjectiveFunction
    {
        public static double Calculate(RMatrix R, Matrix U, Matrix P, double lambda)
        {
            double firstSum = 0, secondSum = 0, result = 0;
            Matrix uMatrix, pMatrix;

            for (int u = 0; u < U.ColumnCount; u++)
            {
                uMatrix = U.GetVector(u);

                for (int p = 0; p < P.ColumnCount; p++)
                {
                    pMatrix = P.GetVector(p);

                    result += Math.Pow(R[u, p] - (uMatrix.GetTransposed() * pMatrix).Data[0, 0], 2);
                    firstSum += pMatrix.GetSquaredNorm();
                }
                secondSum += uMatrix.GetSquaredNorm();
            }

            result += lambda * (firstSum + secondSum);

            Console.WriteLine(result);

            return result;
        }
    }
}