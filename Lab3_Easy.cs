using System;

namespace Lab3_Easy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Task #7:");
            Easy7();
            Console.WriteLine("--------------------\n\n\n" +
                "--------------------\nTask #8:");
            Easy8();
            Console.WriteLine("--------------------\n\n\n" +
                "--------------------\nTask #9:");
            Easy9();
            Console.WriteLine("--------------------");
        }

        static private void Easy7()
        {
            /* Replace elements with zeroes of the array (of size 7)
             * that are more than average value of array elements */
            const int SIZE = 7;
            double[] array = new double[SIZE];
            double average = 0;

            Random rand = new Random();

            /* Input */
            Console.Write("Original array: [");
            for (int i = 0; i < SIZE; ++i)
            {
                array[i] = rand.Next(-10, 10);
                Console.Write($" {array[i]} ");
                average += array[i];
            }
            Console.WriteLine("]");
            average /= SIZE;
            Console.WriteLine($"\nAverage == {average}\n");

            for (int i = 0; i < SIZE; ++i)
                if (array[i] > average) array[i] = 0;

            writeArray(array);
        }

        static private void Easy8()
        {
            /* Count amount of negative elements of the one-dimensional array of size 6 */
            const int SIZE = 6;
            double[] array = new double[SIZE];
            int negativeCount = 0;

            Random rand = new Random();

            /* Input */
            Console.Write("Original array: [");
            for (int i = 0; i < SIZE; ++i)
            {
                array[i] = rand.Next(-10, 10);
                Console.Write($" {array[i]} ");
            }
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
            {
                array[i] = rand.Next(-10, 10);
                Console.Write($" {array[i]} ");
            }
            Console.WriteLine("]");

            for (int i = 0; i < SIZE; ++i)
                average += array[i];
            average /= SIZE;

            for (int i = 0; i < SIZE; ++i)
                if (array[i] > average) ++moreThanAverage;
            Console.WriteLine($"\nAverage value is: {average}\n\n" +
                $"Amount of elements that are more " +
                $"than average value of array elements: {moreThanAverage}");
        }

        private static void writeArray(double[] array)
        {
            Console.Write("Array: [");
            for (int i = 0; i < array.Length; ++i)
            {
                Console.Write($" {array[i]} ");

            }
            Console.WriteLine("]");
        }
    }
}
