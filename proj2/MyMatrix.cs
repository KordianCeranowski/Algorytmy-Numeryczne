using System;
using System.Collections;

namespace Gauss
{
    [Serializable]
    class MyMatrix<T>
    {
        public T[][] Data { get; set; }
        public readonly int rowCount;
        public readonly int colCount;
        private ArrayList columnSwaps;

        public MyMatrix(int rowCount, int colCount)
        { 
            this.Data = new T[rowCount][];
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = new T[colCount];
            }
            this.rowCount = rowCount;
            this.colCount = colCount;

            columnSwaps = new ArrayList();
        }

        public MyMatrix(T[][] data)
        {
            Data = data;
            this.rowCount = Data.Length;
            this.colCount = Data[0].Length;

            columnSwaps = new ArrayList();
        }


        // Mnożenie macierzy
        public static MyMatrix<T> operator *(MyMatrix<T> left, MyMatrix<T> right)
        {
            if (left.colCount != right.rowCount)
                throw new ArgumentException("Matrixes need to have same number of rows.");

            MyMatrix<T> mm = new MyMatrix<T>(left.rowCount, right.colCount);

            int multiplications = left.colCount;
            dynamic val;

            for (int row = 0; row < mm.rowCount; row++)
            {
                for (int col = 0; col < mm.colCount; col++)
                {
                    if (mm.Data[row][col] == null)
                    {
                        val = new MyType();
                        mm.Data[row][col] = val;
                    }
                    for (int i = 0; i < multiplications; i++)
                    {
                        val = left.Data[row][i];
                        val *= right.Data[i][col];
                        mm.Data[row][col] += val;
                    }
                }
            }

            return mm;
        }




        // Dokleja wektor do macierzy - potrzebne przed użyciem Gaussa
        public MyMatrix<T> Connect(MyMatrix<T> other)
        {
            if (this.rowCount != other.rowCount) throw new ArgumentException("Matrixes need to have same number of rows.");

            T[][] outArr = new T[this.rowCount][];
            for (int i = 0; i < this.colCount; i++)
            {
                outArr[i] = new T[this.colCount + other.colCount];
            }

            for (int row = 0; row < this.rowCount; row++)
            {
                for (int col = 0; col < this.colCount; col++)
                {
                    outArr[row][col] = this.Data[row][col];
                }
                for (int col = 0; col < other.colCount; col++)
                {
                    outArr[row][col + this.rowCount] = other.Data[row][col];
                }
            }

            MyMatrix<T> outMM = new MyMatrix<T>(outArr);

            return outMM;
        }




        // Metoda eliminacji Gaussa
        public void PerformGaussianElimination(Choice choice)
        {
            int percent = -1;
            for (int i = 0; i < rowCount; i++)
            {
                if (percent != i * 100 / rowCount)
                {
                    percent = i * 100 / rowCount;
                    Console.WriteLine(typeof(T) + "  " + choice + "  -  Ukończono: " + percent + "% wierszy");
                }

                switch (choice)
                {
                    case Choice.NONE:
                        break;
                    case Choice.PARTIAL:
                        PerformPartialChoice(i, i);
                        break;
                    case Choice.FULL:
                        PerformFullChoice(i, i);
                        break;
                }

                for (int j = i + 1; j < rowCount; j++)
                {
                    dynamic zerowany = Data[j][i];
                    dynamic zerujacy = Data[i][i];
                    dynamic multiplier = zerowany / zerujacy;
                    for (int col = i; col < colCount; col++)
                    {
                        zerujacy = Data[i][col];
                        Data[j][col] -= zerujacy * multiplier;
                    }
                }

            }
        }




        // Operacja odwrotna po Gaussie
        public void PerformBackwardsOperation()
        {
            dynamic q; // mnożnik dla danego miejsca w wektorze, tj dla 3 pozycji w wektorze to będzie punkt [3,3] w macierzy
            dynamic x; // wartość w danym miejscu w wektorze
            dynamic d; // mnożnik dla aktualnie wyliczanego miejsca w wektorze
            int last = rowCount;

            for (int row = last - 1; row >= 0; row--)
            {
                for (int i = row + 1; i < last; i++)
                {
                    q = Data[row][i];
                    x = Data[i][last];
                    Data[row][last] -= q * x;
                }

                d = Data[row][row];
                Data[row][last] /= d;
            }
        }





        // Częściowy wybór
        private void PerformPartialChoice(int row, int column)
        {
            int rowToSwap = FindMaxInRows(row, column);
            SwapRows(row, rowToSwap);
            
        }

        private int FindMaxInRows(int startRow, int column)
        {
            dynamic maxValue = Data[startRow][column];
            maxValue = MyMath.ABS(maxValue);
            int rowOfMaxValue = startRow;

            dynamic var;
            for (int row = startRow + 1; row < rowCount; row++)
            {
                var = Data[row][column];
                var = MyMath.ABS(var);
                if (var > maxValue)
                {
                    maxValue = var;
                    rowOfMaxValue = row;
                }
            }

            return rowOfMaxValue;
        }

        private void SwapRows(int row1, int row2)
        {
            if(row1 != row2)
            { 
                T[] temp = Data[row1];
                Data[row1] = Data[row2];
                Data[row2] = temp;
            }
        }




        // Całkowity wybór
        private void PerformFullChoice(int row, int column)
        {
            var coordinates = FindMaxInRowsAndColumns(row, column);
            SwapRows(row, coordinates.Item1);
            SwapColumns(column, coordinates.Item2);
        }

        private Tuple<int, int> FindMaxInRowsAndColumns(int startRow, int startColumn)
        {
            int rowOfMaxValue = startRow;
            int colOfMaxValue = startColumn;

            dynamic maxValue = Data[startRow][startColumn];
            maxValue = MyMath.ABS(maxValue);

            dynamic temp;

            for (int row = startRow; row < this.rowCount; row++)
            {
                for (int col = startColumn; col < colCount - 1; col++)
                {
                    temp = this.Data[row][col];
                    temp = MyMath.ABS(temp);

                    if (maxValue < temp)
                    {
                        maxValue = temp;
                        rowOfMaxValue = row;
                        colOfMaxValue = col;
                    }
                }
            }

            return Tuple.Create(rowOfMaxValue, colOfMaxValue);
        }

        private void SwapColumns(int column1, int column2)
        {
            if(column1 != column2)
            {
                // Wyświetla swapy kolumn
                //Console.WriteLine(column1 + ", " + column2);
                dynamic temp;
                for (int row = 0; row < rowCount; row++)
                {
                    temp = this.Data[row][column1];
                    this.Data[row][column1] = this.Data[row][column2];
                    this.Data[row][column2] = temp;
                }

                columnSwaps.Add(Tuple.Create(column1, column2));
            }
        }


        


        // Wyciąga wartości z ostatniej kolumny macierzy do tablicy, bardziej przejrzyste
        // Naprawia kolejność zepsutą mieszaniem kolumn
        public T[] ExtractLastColumn()
        {
            T[] outArr = new T[this.rowCount];

            for (int row = 0; row < this.rowCount; row++)
            {
                outArr[row] = this.Data[row][this.colCount - 1];
            }

            columnSwaps.Reverse();
            foreach (Tuple<int, int> swap in this.columnSwaps)
            {
                T temp = outArr[swap.Item1];
                outArr[swap.Item1] = outArr[swap.Item2];
                outArr[swap.Item2] = temp;
            }

            return outArr;
        }


        public void ShowVector() 
        {
            T[] vec = this.ExtractLastColumn();

            foreach (var i in vec)
            {
                Console.WriteLine("\t" + i);
            }
        }

    }
}
