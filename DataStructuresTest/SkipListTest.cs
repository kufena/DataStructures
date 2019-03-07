using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using DataStructures;
using System.Diagnostics;

namespace DataStructuresTest
{
    class SkipListTest
    {
        [SetUp]
        public void setUp()
        {

        }

        [Test]
        public void add_some_and_find_some()
        {
            SkipList<int, long> skippy = new SkipList<int,long>(int.MinValue, int.MaxValue, 1001);
            skippy.insert(10, 1000L);
            skippy.insert(15, 1500L);
            skippy.insert(5, 500L);
            skippy.insert(int.MaxValue - 1, int.MaxValue * 1000L);
        }

        [Test]
        public void add_lots()
        {
            Random r = new Random(1001);
            int n = 10000;
            int[] keys = new int[n];
            long[] vals = new long[n];
            for(int i = 0; i < n; i++)
            {
                keys[i] = r.Next();
                vals[i] = (long)r.Next();
            }

            long x;
            int ok = 0;
            int clash = 0;
            SkipList<int, long> skippy = new SkipList<int, long>(-1, int.MaxValue,2003);

            Stopwatch sp = new Stopwatch();
            sp.Start();
            for(int i = 0; i < n; i++)
            {
                x = skippy.insert(keys[i], vals[i]);
                if (x != vals[i])
                    clash++;
                else
                    ok++;
            }
            sp.Stop();

            long[] times = new long[n];

            int match = 0;
            int mismatch = 0;
            int missing = 0;
            bool there = false;
            for(int i = 0; i < n; i++)
            {
                sp.Reset();
                sp.Start();
                var opt = skippy.search(keys[i]);
                sp.Stop();
                times[i] = sp.ElapsedMilliseconds;
                there = false;
                foreach(var ox in opt)
                {
                    there = true;
                    if (ox == vals[i])
                        match++;
                    else
                        mismatch++;
                }
                if (!there) missing++;
            }

            long tot = 0;
            for (int i = 0; i < n; i++) tot += times[i];
            double avg = tot / n;

            Assert.AreEqual(clash, mismatch);
            Assert.AreEqual(ok, match);
            Assert.AreEqual(0, missing);
            
        }
    }
}
