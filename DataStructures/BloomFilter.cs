using System;
using System.Collections.Generic;
using System.Text;
using System.Data.HashFunction.MurmurHash;

namespace DataStructures
{
    /*
     * A bloom filter class.
     * 
     * I was going to make a generic class, but all the hash functions want are byte
     * arrays, so might as well push that into the client for the time being.
     * 
     * Constructor takes an expected number of items to be stored and a probability p
     * of a false positive.
     */
    public class BloomFilter
    {
        /*
         * Here n is the number of items expected to be stored, and p is the
         * false positive probability requirement.
         */
        public static int determine_size(int n, double p)
        {
            return (int)(-(n * Math.Log(p)) / Math.Pow(Math.Log(2), 2));
        }

        /*
         * How many hashes do we need?
         * Here n is the number of items expected to be stored, and m the size of the bit array
         * (given by determine_size).
         */
        public static int hashes_required(int n, int m)
        {
            double x = ((double)m) / ((double)n);
            return (int) Math.Ceiling(x * Math.Log(2));
        }

        byte[] bit_array;
        IMurmurHash3[] hash_functions;
        int bits;
        int n;
        double p;

        public BloomFilter(int n, double p)
        {
            this.n = n;
            this.p = p;
            this.bits = BloomFilter.determine_size(n, p);
            this.bit_array = new byte[(this.bits / 8) + 1]; // + 1 is caution.
            int nhash = BloomFilter.hashes_required(n, this.bits);
            hash_functions = new IMurmurHash3[nhash];
            for(int i = 0; i < nhash; i++)
            {
                MurmurHash3Config config = new MurmurHash3Config();
                config.Seed = (uint) i;
                config.HashSizeInBits = 32;
                
                hash_functions[i] = MurmurHash3Factory.Instance.Create(config);
            }
        }

        public void add(byte[] item)
        {
            for(int i = 0; i < hash_functions.Length; i++)
            {
                var hashv = hash_functions[i].ComputeHash(item);
                int bit = (int) Math.Abs(BitConverter.ToInt32(hashv.Hash)) % bits;
                setBit(bit);
            }
        }

        public bool check(byte[] item)
        {
            for (int i = 0; i < hash_functions.Length; i++)
            {
                var hashv = hash_functions[i].ComputeHash(item);
                int bit = (int)Math.Abs(BitConverter.ToInt32(hashv.Hash)) % bits;
                if (!isSet(bit))
                    return false;
            }
            return true;
        }

        static byte[] bytes = new byte[] { 1, 2, 4, 8, 16, 32, 64, 128 };
        void setBit(int bit)
        {
            int x = bit / 8;
            int l = bit % 8;
            bit_array[x] = (byte) ((uint)bit_array[x] | (uint)bytes[l]);
        }

        bool isSet(int bit)
        {
            int x = bit / 8;
            int l = bit % 8;
            return (bit_array[x] & bytes[l]) > 0;
        }

        public void play()
        {
            var config1 = new MurmurHash3Config();
            config1.HashSizeInBits = 64;
            config1.Seed = (uint)12534;
            var hashfac = MurmurHash3Factory.Instance.Create();
            string s = "Andrew Douglas";
            var hashval = hashfac.ComputeHash(Encoding.ASCII.GetBytes(s));
        }
    }
}
