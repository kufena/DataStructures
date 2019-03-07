using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using DataStructures;
using System.Diagnostics;

namespace DataStructuresTest
{
    class BloomFilterTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void atest()
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
            Assert.Pass();
        }

        [Test]
        public void mass_test()
        {
            double p = 0.0001;
            int n = 1000000;
            BloomFilter bl = new BloomFilter(n, p);

            byte[][] data = new byte[1000000][];
            Random r = new Random(1001);
            for(int i = 0; i < 1000000; i++)
            {
                data[i] = new byte[20];
                for(int j = 0; j < 20; j++)
                {
                    data[i][j] = (byte)(r.Next(256));
                }
            }
            
            Stopwatch sp = new Stopwatch();
            int their = 0;
            int added = 0;
            sp.Start();

            for(int i = 0; i < 1000000; i++)
            {
                if (!bl.check(data[i]))
                {
                    bl.add(data[i]);
                    added++;
                }
                else
                    their++;
            }
            sp.Stop();

            Assert.Pass();
        }
    }
}
