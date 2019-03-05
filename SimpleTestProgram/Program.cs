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
            int x = 10000;
            Stopwatch sp = new Stopwatch();
            sp.Start();
            foreach(var prime in ps)
            {
                Console.WriteLine(prime);
                x--;
                if (x == 0) break;
            }
            sp.Stop();
            Console.WriteLine("Took " + sp.ElapsedMilliseconds + "ms to complete.");
            Console.WriteLine("Hello World!");
        }
    }
}
