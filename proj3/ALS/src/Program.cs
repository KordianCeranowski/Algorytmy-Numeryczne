using System;
using System.Diagnostics;

namespace RecommenderSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // tworzenie .rmt
            //ALS als = new ALS(10, 500, 5000);

            // odczyt .rmt
            Stopwatch s = new Stopwatch();
            s.Start();
            ALS als = new ALS(10, "p50u500");
            als.HidingTest(0.1, 10, 0.1);
            Console.WriteLine(((double)s.ElapsedMilliseconds)/1000);
            // uruchamianie
            //als.HidingTest(0.1, 10, 0.1);

            //ALS small = new ALS(10, 500, 5000);

        }
    }
}
