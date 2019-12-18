using System;

namespace RecommenderSystem
{
	internal static class ObjectiveFunction
	{
		public static double Calculate(int d, RMatrix R, Matrix U, Matrix P, double lambda)
		{
			double firstSum = 0, secondSum = 0, result = 0;

			for (int u = 0; u < U.ColumnCount; u++)
			{
				for (int p = 0; p < P.ColumnCount; p++)
				{
					if (R[u, p] != 0)
					{
						double sum = 0;

						for (int row = 0; row < d; row++)
						{
							sum += U.Data[row, u] * P.Data[row, p];
						}
						result += Math.Pow((R[u, p] - sum), 2);
					}
					
					double squaredNormFromP = 0;

					for (int row = 0; row < d; row++)
					{
						squaredNormFromP += Math.Pow(P.Data[row, p], 2);
					}
					firstSum += squaredNormFromP;
				}

				double squaredNormFromU = 0;

				for (int row = 0; row < d; row++)
				{
					squaredNormFromU += Math.Pow(U.Data[row, u], 2);
				}
				secondSum += squaredNormFromU;
			}

			result += lambda * (firstSum + secondSum);

			Console.WriteLine(result);

			return result;
		}
	}
}