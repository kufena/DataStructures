using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using DataStructures;

namespace SimpleTestProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            PrimeStream ps = new PrimeStream();
            foreach(var prime in ps)
            {
                Console.WriteLine(prime);
            }

            Console.WriteLine("Hello World!");
        }
    }
}
