using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press b - Running BubbleSort Algorithm that is simplest algorithm");
            //Console.WriteLine("------------------------------bonus-------------------------------");
            //Console.WriteLine("Press s - Running SelectionSort Algorithm");
            //Console.WriteLine("Press h - Running HeapSort Algorithm");
            //Console.WriteLine("Press q - Running QuickSort Algorithm");

            Console.WriteLine("Press ESC to stop");
            int[] array = { 112, 9, 555, 3, 6, 109 };
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                var input = Console.ReadKey();
                Console.Write("\n");
                switch (input.Key)
                {
                    case ConsoleKey.B:
                        array.BubbleSort().ToList().ForEach(item => Console.Write(item + " "));
                        break;
                    case ConsoleKey.C:
                        array.SelectionSort().ToList().ForEach(item => Console.Write(item + " "));
                        break;
                    case ConsoleKey.H:
                        array.HeapSort().ToList().ForEach(item => Console.Write(item + " "));
                        break;
                    case ConsoleKey.Q:
                        array.QuickSort().ToList().ForEach(item => Console.Write(item + " "));
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
                Console.Write("\n");
            }
            Console.ReadLine();
        }
    }

    /// <summary>
    ///  SortExtension
    /// </summary>
    public static class SortExtension
    {
        /// <summary>
        /// Bubbles the sort.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static int[] BubbleSort(this int[] array)
        {
            if (array == null)
                return null;

            int temp = 0;
            for (int write = 0; write < array.Length; write++)
            {
                for (int sort = 0; sort < array.Length - 1; sort++)
                {
                    if (array[sort] > array[sort + 1])
                    {
                        temp = array[sort + 1];
                        array[sort + 1] = array[sort];
                        array[sort] = temp;
                    }
                }
            }
            return array;
        }

        /// <summary>
        /// Bubbles the sort.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static int[] SelectionSort(this int[] array)
        {
            if (array == null)
                return null;

            int positionMin = 0;
            int temp = 0;

            for (int i = 0; i < array.Length - 1; i++)
            {
                positionMin = i;//set positionMin to the current index of arrayay

                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[positionMin])
                    {
                        //positionMin will keep track of the index that min is in, this is needed when a swap happens
                        positionMin = j;
                    }
                }

                //if positionMin no longer equals i than a smaller value must have been found, so a swap must occur
                if (positionMin != i)
                {
                    temp = array[i];
                    array[i] = array[positionMin];
                    array[positionMin] = temp;
                }
            }
            return array;
        }

        /// <summary>
        /// Heaps the sort.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static int[] HeapSort(this int[] array)
        {
            if (array == null)
                return null;

            int heapSize = 0;

            Action<int[], int, int> swap = (int[] arr, int x, int y) =>
            {
                int temp = arr[x];
                arr[x] = arr[y];
                arr[y] = temp;
            };

            Action<int[], int> heapify = null;

            heapify = new Action<int[], int>((arr, index) =>
            {
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                int largest = index;
                if (left <= heapSize && arr[left] > arr[index])
                {
                    largest = left;
                }

                if (right <= heapSize && arr[right] > arr[largest])
                {
                    largest = right;
                }

                if (largest != index)
                {
                    swap(arr, index, largest);
                    heapify(arr, largest);
                }
            });

            Action<int[]> buildHeap = (int[] x) =>
            {
                heapSize = x.Length - 1;
                for (int i = heapSize / 2; i >= 0; i--)
                {
                    heapify(x, i);
                }
            };

            buildHeap(array);
            for (int i = array.Length - 1; i >= 0; i--)
            {
                swap(array, 0, i);
                heapSize--;
                heapify(array, 0);
            }

            return array;
        }

        /// <summary>
        /// Quicks the sort.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static int[] QuickSort(this int[] array)
        {
            if (array == null)
                return null;

            Func<int[], int, int, int> partition = (arr, left, right) =>
            {
                int pivot = arr[left];
                while (true)
                {
                    while (arr[left] < pivot)
                        left++;

                    while (arr[right] > pivot)
                        right--;

                    if (left < right)
                    {
                        int temp = arr[right];
                        arr[right] = arr[left];
                        arr[left] = temp;
                    }
                    else
                    {
                        return right;
                    }
                }
            };

            Action<int[], int, int> recursive = null;
            recursive = new Action<int[], int, int>((arr, left, right) =>
            {
                if (left < right)
                {
                    int pivot = partition(arr, left, right);

                    if (pivot > 1)
                        recursive(arr, left, pivot - 1);

                    if (pivot + 1 < right)
                        recursive(arr, pivot + 1, right);
                }
            });

            recursive(array, 0, array.Length - 1);

            return array;
        }
    }
}
