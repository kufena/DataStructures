using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class HeapSort<T>
    {
        Func<int,int> RIGHT = x => (2 * (x+1));
        Func<int, int> LEFT = x => (2 * (x+1)) - 1;
        Func<int, int> PARENT = x => ((x+1) / 2) - 1;

        public void Sort(T[] arr, IComparer<T> comparer)
        {
            buildMaxHeap(arr, comparer);
            for(int i = arr.Length - 1; i > 0; i--)
            {
                var swap = arr[0];
                arr[0] = arr[i];
                arr[i] = swap;
                maxHeapify(arr, 0, comparer, i);
            }
            Console.WriteLine("done!");
        }

        void buildMaxHeap(T[] arr, IComparer<T> comparer)
        {

            if (arr.Length < 2)
            return;

            int mid = (arr.Length / 2) - 1;
            for(int i = mid; i >= 0; i--)
            {
                maxHeapify(arr, i, comparer, arr.Length);
            }
        }

        void maxHeapify(T[] arr, int i, IComparer<T> comparer, int size)
        {
            var l = LEFT(i);
            var r = RIGHT(i);
            int largest;

            if (l < size && comparer.Compare(arr[l],arr[i]) > 0)
                largest = l;
            else
                largest = i;

            if (r < size && comparer.Compare(arr[r], arr[largest]) > 0)
                largest = r;

            if (largest != i)
            {
                T swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;
                maxHeapify(arr, largest, comparer, size);
            }
        }


    }
}
