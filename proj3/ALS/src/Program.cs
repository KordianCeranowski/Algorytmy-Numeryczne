using System;
using System.Diagnostics;

namespace RecommenderSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            ALS als = new ALS(10, "p550u5050");
            als.HidingTest(0.1, 10, 0.1);
            
        }
    }
}
