using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class PrimeStream : IEnumerable<int>
    {
        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            return new StreamPrimes();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        class StreamPrimes : IEnumerator<int>
        {
            List<int> primes;
            int counter;
            int local_current = 1;

            public StreamPrimes()
            {
                primes = new List<int>();
                counter = 2;
            }
            int IEnumerator<int>.Current => local_current;

            object IEnumerator.Current => local_current;

            void IDisposable.Dispose()
            {
                ;
            }

            /*
             * Sieve of erastothenes, essentially, but in re-entrant form.
             * That is, find a number from (current prime + 1) that is not divisible
             * by all the previous primes.
             * 
             * counter is that (current prime + 1) but is a class variable.
             */
            bool IEnumerator.MoveNext()
            {
                bool next = false;
                while(true)
                {
                    next = true;
                    foreach(int p in primes)
                    {
                        if (counter % p == 0)
                        {
                            next = false;
                            break;
                        }
                    }

                    if (next)
                    {
                        primes.Add(counter);
                        local_current = counter;
                        counter++;
                        return true;
                    }
                    else
                    {
                        counter++;
                    }
                    
                }
            }

            void IEnumerator.Reset()
            {
                primes = new List<int>();
                counter = 2;
                local_current = -1;
            }
        }
    }
}
