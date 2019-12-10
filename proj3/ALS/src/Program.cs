using System;
using System.Diagnostics;

namespace RecommenderSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            ALS als = new ALS(10, 100, 100);
            Console.WriteLine("Czas na czytanie z pliku .txt\t" + stopwatch.ElapsedMilliseconds / 1000 + "s");

            stopwatch.Restart();
            ALS als2 = new ALS(10, "p100u100");
            Console.WriteLine("Czas na czytanie z pliku .rmat\t" + stopwatch.ElapsedMilliseconds / 1000 + "s");

            Console.WriteLine("\n.txt");
            als.HidingTest(0.1, 10, 0.1);
            Console.WriteLine(stopwatch.ElapsedMilliseconds / 1000 + "s");

            Console.WriteLine("\n.rmat");
            als2.HidingTest(0.1, 10, 0.1);
            Console.WriteLine(stopwatch.ElapsedMilliseconds / 1000 + "s");

            Console.WriteLine();
        }
    }
}
