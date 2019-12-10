﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RecommenderSystem
{
    class Test
    {
        const double PERCENT_TO_HIDE = 0.1;

        public static void start(int d, double lambda, Size size, int iterations, bool showObjectiveFunction)
        {
            Stopwatch s = new Stopwatch();

            string dataSize = "null";
            switch (size)
            {
                case Size.SMALL:
                    dataSize = "p50u500";
                    break;
                case Size.MEDIUM:
                    dataSize = "p500u5000";
                    break;
                case Size.LARGE:
                    dataSize = "p1000u10000";
                    break;
                case Size.XLARGE:
                    dataSize = "p2000u10000";
                    break;
            }

            
            ALS.SHOW_TARGET_FUNCTION = showObjectiveFunction;
            ALS als = new ALS(d, dataSize);

            s.Start();
            als.HidingTest(lambda, iterations, PERCENT_TO_HIDE);

            Console.WriteLine($"\nd = {d}, lambda = {lambda}, size = {size}, iterations = {iterations}");
            Console.WriteLine("Czas wykonania: " + ((double)s.ElapsedMilliseconds) / 1000 + "s");
        }


        public enum Size
        {
            SMALL, MEDIUM, LARGE, XLARGE
        }
    }
}