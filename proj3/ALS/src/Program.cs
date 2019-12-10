using System;
using System.Diagnostics;

namespace RecommenderSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Test.Execute(Test.Size.SMALL, 15, 2, 0.1, false);
        }
    }
}
