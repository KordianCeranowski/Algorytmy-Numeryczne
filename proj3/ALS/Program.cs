using System;
using System.Text.RegularExpressions;

namespace ALS
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Parser parser = new Parser("10.txt");

            var R = parser.GetR();


            Console.Beep();
        }
    }
}

