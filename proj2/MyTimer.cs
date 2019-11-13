using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Gauss
{
    class MyTimer
    {

        public static Stopwatch stopwatch = new Stopwatch();

        public static void Start()
        {
            stopwatch.Start();

        }


        public static void Stop()
        {
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();
        }

        public static void Show()
        {
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

    }
}
