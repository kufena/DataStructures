using System;
using System.Diagnostics;
using System.Collections.Generic;

using DataStructures;

namespace SimpleTestProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            RedBlackTree<int, int> tree = new RedBlackTree<int, int>();
            Dictionary<int, int> dict = new Dictionary<int, int>();

            Stopwatch sp = new Stopwatch();

            int amount = 10000000;
            int count = 0;
            int start = 0;
            int step = 37;

            int[] keys = new int[amount];
            int[] vals = new int[amount];
            while (count < amount)
            {
                keys[count] = start = (start + step) % amount;
                vals[count] = amount - keys[count];
                count++;
            }

            count = 0;
            sp.Start();
            for (count = 0; count < amount; count++)
            {
                tree.insert(keys[count], vals[count]);
                //dict.Add(keys[count], vals[count]);
            }
            sp.Stop();
            long rbinterval = sp.ElapsedMilliseconds;
            Console.WriteLine("Hello World!");
        }
    }
}
