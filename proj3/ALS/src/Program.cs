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
            ALS als = new ALS(4, 550000, 1000, 1000);
            als.HidingTest(0.1, 10, 0.1);
            Console.WriteLine(stopwatch.ElapsedMilliseconds / 1000 + "s");


            Console.WriteLine();
        }
    }
}
