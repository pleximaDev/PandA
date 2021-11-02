using System;

namespace Lab3_Hard
{
    class Program
    {
        public const int sizeOfArray = 12;

        static void Main(string[] args)
        {
            const int borderLength = 30;
            string border = new string('-', borderLength);
            Console.WriteLine(border);
            Console.WriteLine("Task #8:");
            Hard8();
            Console.WriteLine(border);
        }

        static private void Hard8()
        {
            /* Order negative elements of the array in descending order,
             * keeping the rest of the elements in the same positions
             */

            double[] array = RandomizeArray(-15, 15);

            int prevIndx = 0;

            PrintArray(array, $"\nOriginal array:\n\n");

            for (int j = 0; j < sizeOfArray; ++j)
            {
                prevIndx = 0;
                for (int i = 0; i < sizeOfArray; ++i)
                    if (array[i] < 0
                        &&
                        array[prevIndx] < 0
                        &&
                        array[i] > array[prevIndx])
                        MySwap(array, i, prevIndx);
                    else if (array[i] < 0) prevIndx = i;
            }

            PrintArray(array, "\n\nArray received by sorting negative elements in descending order " +
                "keeping positive elements on their places:\n\n");
        }

        static private void
            MySwap
            (double[] array2swap,
            int i,
            int j)
        {
            (array2swap[i], array2swap[j]) = (array2swap[j], array2swap[i]);
        }

        static private void
            PrintArray
            (double[] array2print, string headText)
        {
            Console.WriteLine($"{headText}[ {string.Join(" ", array2print)} ]");
        }

        static private void
            PrintArray /* Overloading for cases without providing headText */
            (double[] array2print)
        {
            Console.WriteLine($"[ {string.Join(" ", array2print)} ]");
        }

        static private double[]
            RandomizeArray
            (double start, double end)
        {
            Random rand = new Random();
            double[] randArray = new double[sizeOfArray];
            for (int i = 0; i < sizeOfArray; ++i)
                randArray[i] = rand.Next((int)start, (int)end);
            return randArray;
        }
    }
}
