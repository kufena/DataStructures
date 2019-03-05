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
            double p = 0.001;
            int n = 1000000;
            int size = BloomFilter.determine_size(n, p);
            Console.WriteLine("Size required for 1000 items with P=0.01 is " + size);
            Console.WriteLine("Hashes required for 1000 items and 64bit hash." + BloomFilter.hashes_required(n, size));

            var bl = new BloomFilter(n, p);
            string s = "Andrew Douglas";
            string t = "Jimbob Reynolds";
            byte[] sb = Encoding.ASCII.GetBytes(s);
            byte[] tb = Encoding.ASCII.GetBytes(t);

            bl.add(sb);

            Console.WriteLine("Can we find \"" + s + "\"? " + bl.check(sb));
            Console.WriteLine("Can we find \"" + t + "\"? " + bl.check(tb));

            Console.WriteLine("Hello World!");
        }
    }
}
