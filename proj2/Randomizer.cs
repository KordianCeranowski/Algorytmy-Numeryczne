using System;

namespace Gauss
{
    class Randomizer<T>
    {
        public static T[][] GenerateMatrix(int rowCount, int colCount) 
        {
            T[][] arr = new T[rowCount][];

            int minRandom = (int)(Math.Pow(2, 16) * -1);
            int maxRandom = (int)(Math.Pow(2, 16) - 1);
            int divider = (int)(Math.Pow(2, 16));
            Random random = new Random();
            dynamic temp;

            for (int row = 0; row < rowCount; row++)
            {
                arr[row] = new T[colCount];
                for (int col = 0; col < colCount; col++)
                {
                    if (typeof(T) == typeof(float))
                    {
                        temp = (float)random.Next(minRandom, maxRandom) / (float)divider;
                        arr[row][col] = temp;
                    }
                    if (typeof(T) == typeof(double))
                    {
                        temp = (double)random.Next(minRandom, maxRandom) / (double)divider;
                        arr[row][col] = temp;
                    }
                    if (typeof(T) == typeof(MyType))
                    {
                        temp = new MyType(random.Next(minRandom, maxRandom), divider);
                        arr[row][col] = temp;
                    }
                }
            }
            return arr;
        }

        public static T[][] GenerateVector(int size)
        {
            return GenerateMatrix(size, 1); 
        }

        public static T[][] GenerateMatrix(int size)
        {
            return GenerateMatrix(size, size);
        }
    }
}
