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
            int local_current = -1;

            public StreamPrimes()
            {
                primes = new List<int>();
                counter = 2;
            }
            int IEnumerator<int>.Current => local_current;

            object IEnumerator.Current => local_current;

            void IDisposable.Dispose()
            {
                throw new NotImplementedException();
            }

            bool IEnumerator.MoveNext()
            {
                bool next = false;
                // sieve of erastothenes, essentially.
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
