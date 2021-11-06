using System;

namespace Lab3_Easy
{
    class Program
    {
        private const int
            lbound = -10, /* left bound of randomization */
            rbound = 10; /* right bound of randomization */
        static void Main(string[] args)
        {
            string border = new string('-', 45);
            Console.WriteLine(border);
            Console.WriteLine("Task #7:");
            Easy7();
            Console.WriteLine(border + "\n\n\n" +
                border + "\nTask #8:");
            Easy8();
            Console.WriteLine(border + "\n\n\n" +
                border + "\nTask #9:");
            Easy9();
            Console.WriteLine(border);
        }

        static private void Easy7()
        {
            /* Replace elements of the array (of size 7) with zeroes
             * that are more than average value of array elements */
            const int SIZE = 7;
            double[] array = new double[SIZE];
            double average = 0;

            Random rand = new Random();

            /* Input */
            Console.Write("Original array: [");
            for (int i = 0; i < SIZE; ++i)
            {
                array[i] = rand.Next(lbound, rbound);
                Console.Write($" {array[i]} ");
                average += array[i];
            }
            Console.WriteLine("]");

            average /= SIZE;

            Console.WriteLine($"\nAverage == " +
                (average % 1 == 0 ?
                $"{average}" : $"{average,0:F4}") +
                $"\n");

            for (int i = 0; i < SIZE; ++i)
                if (array[i] > average) array[i] = double.NaN;

            //writeArray(array);
            Console.Write($"Array after replacement with zeroes:\n[ ");
            for (int i = 0; i < SIZE; ++i)
                if (double.IsNaN(array[i]))
                {
                    array[i] = 0;
                    ColorString($" {array[i]} ",
                        ConsoleColor.Black, ConsoleColor.Green);
                }
                else
                    Console.Write($" {array[i]} ");
            Console.WriteLine($" ]");
        }

        static private void Easy8()
        {
            /* Count amount of negative elements of
             * the one-dimensional array of size 6 */
            const int SIZE = 6;
            double[] array = new double[SIZE];
            int negativeCount = 0;

            Random rand = new Random();

            /* Input */
            Console.Write("Original array: [");
            for (int i = 0; i < SIZE; ++i)
                Console.Write($" {array[i] = rand.Next(lbound, rbound)} ");
            Console.WriteLine("]");

            for (int i = 0; i < SIZE; ++i)
                if (array[i] < 0) ++negativeCount;
            Console.WriteLine($"Amount of negative numbers: {negativeCount}");
        }

        static private void Easy9()
        {
            /*
             * Determine how many elements of the one-dimensional array of size 8
             * are more than average value of elements of this array
             */
            const int SIZE = 8;
            double[] array = new double[SIZE];
            int moreThanAverage = 0;
            double average = 0;

            Random rand = new Random();

            /* Input */
            Console.Write("Original array: [");
            for (int i = 0; i < SIZE; ++i)
                Console.Write($" {array[i] = rand.Next(lbound, rbound)} ");
            Console.WriteLine("]");

            for (int i = 0; i < SIZE; ++i)
                average += array[i];
            average /= SIZE;

            Console.Write($"\n[ ");
            for (int i = 0; i < SIZE; ++i)
                if (array[i] > average)
                {
                    ++moreThanAverage;
                    ColorString($" {array[i]} ",
                        ConsoleColor.Black, ConsoleColor.Green);
                }
                else
                    Console.Write($" {array[i]} ");
            Console.WriteLine($" ]");

            Console.WriteLine($"\nAverage value is: " +
                (average % 1 == 0 ?
                $"{average}" : $"{average,0:F4}") +
                "\n\n" +
                $"Amount of elements that are more " +
                $"than average value of array elements: {moreThanAverage}");
        }

        private static void writeArray(double[] array)
        {
            Console.Write("Array: [");
            for (int i = 0; i < array.Length; ++i)
                Console.Write($" {array[i]} ");
            Console.WriteLine("]");
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
