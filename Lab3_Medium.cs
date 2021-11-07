using System;

namespace Lab3_Medium
{
    class Program
    {
        private const int
            sizeOfArray = 12,
            lbound = -10, /* left bound of randomization */
            rbound = 10; /* right bound of randomization */

        static public void Main(string[] args)
        {
            const int borderLength = 50;
            string border = new string('-', borderLength);

            Console.WriteLine(border);
            Console.WriteLine("Task #2:");
            Medium2();
            Console.WriteLine(border + "\n\n\n" +
                border + "\nTask #3:");
            Medium3();
            Console.WriteLine(border + "\n\n\n" +
                border + "\nTask #4:");
            Medium4();
            Console.WriteLine(border);
        }

        static private void Medium2()
        {
            /*                      Task
             * Find the sum of elements of the one-dimensional array
             * that are located before the max element of the array
             */
            
            /* 
             * Assuming the first appearance of the maximum of elements of the array as current maximum index.
             *        indxs  ==  0  1   2  3 4 5  6 7
             * For cases like [ -2 -6 -10 -7 8 6 -3 8 ]
             * maxIndx will keep index 4 of the first appearance of maximum == 8
             */

            const int SIZE = sizeOfArray;
            double[] array = new double[SIZE];
            int maxIndx = 0;
            double sum = 0;

            Random rand = new Random();

            /* Input */
            Console.Write("Original array:\n[");
            for (int i = 0; i < SIZE; ++i)
                Console.Write($" {array[i] = rand.Next(lbound, rbound)} ");
            Console.WriteLine("]");

            for (int i = 0; i < SIZE; ++i)
                if (array[i] > array[maxIndx]) maxIndx = i;

            Console.WriteLine($"\nMax element of the array:\n" +
                $"array[{maxIndx}] == {{{array[maxIndx]}}}\n");

            for (int i = 0; i < maxIndx; ++i) sum += array[i];

            ColorString("Sum", ConsoleColor.Black, ConsoleColor.Green);
            Console.Write($" of the elements that are located " +
                $"before the max element array[{maxIndx}] == {array[maxIndx]} " +
                $"is equal to sum == ({string.Join(") + (", array[..maxIndx])}) == ");
            ColorString($"{sum}\n", ConsoleColor.Black, ConsoleColor.Green);
        }

        static private void Medium3()
        {
            /* Double the elements that are located before the min element of the array */
            const int SIZE = sizeOfArray;
            double[] array = new double[SIZE];
            int minIndx = 0;

            Random rand = new Random();

            /* Input */
            Console.Write("Original array:\n[");
            for (int i = 0; i < SIZE; ++i)
                Console.Write($" {array[i] = rand.Next(lbound, rbound)} ");
            Console.WriteLine("]");

            for (int i = 0; i < SIZE; ++i)
                if (array[i] < array[minIndx]) minIndx = i;

            Console.WriteLine($"\nMin element of the array:\n" +
                $"array[{minIndx}] == {{{array[minIndx]}}}\n");

            for (int i = 0; i < minIndx; ++i) array[i] *= 2;

            /* Output */
            Console.Write("Array after doubling elements before min element:\n[");
            for (int i = 0; i < SIZE; ++i)
            {
                if (i == minIndx)
                    ColorString($" {array[minIndx]} ",
                        ConsoleColor.Black, ConsoleColor.Green);
                else
                    Console.Write($" {array[i]} ");
            }
            Console.WriteLine("]");
        }

        static private void Medium4()
        {
            /*
             * Replace the elements of the one-dimensional array 
             * that are located after the max element of the array 
             * with average value of the elements of the array
             */

            const int SIZE = sizeOfArray;
            double[] array = new double[SIZE];
            int maxIndx = 0;
            double average = 0;

            Random rand = new Random();

            /* Input */
            Console.Write("Original array:\n[");
            for (int i = 0; i < SIZE; ++i)
            {
                Console.Write($" {array[i] = rand.Next(lbound, rbound)} ");
                average += array[i];
                if (array[i] > array[maxIndx]) maxIndx = i;
            }
            Console.WriteLine("]");
            average = Math.Round(average/array.Length, 3);

            Console.WriteLine($"\nMax element of the array:\n" +
                $"array[{maxIndx}] == {{{array[maxIndx]}}}\n" +
                $"\nAverage value of the elements of the array:\n" +
                $"average == {average}\n");

            for (int i = maxIndx + 1; i < SIZE; ++i) array[i] = average;

            Console.Write($"Output array received by " +
                $"replacing elements after max element with average value:\n" +
                $"[ {string.Join(" ", array[..maxIndx])}");
            ColorString($" {array[maxIndx]} ", ConsoleColor.Black, ConsoleColor.Green);
            Console.WriteLine($"{string.Join(" ", array[(maxIndx + 1)..] )} ]");
        }

        static private void ColorString
            (string inputString,
            System.ConsoleColor backColor,
            System.ConsoleColor foreColor)
        {
            Console.BackgroundColor = backColor;
            Console.ForegroundColor = foreColor;
            Console.Write($"{inputString}");
            Console.ResetColor();
        }
    }
}
