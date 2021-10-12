using System;

namespace Lab2_Easy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Task #3:");
            Easy3();
            Console.WriteLine("--------------------");
            Console.WriteLine("Task #4:");
            Easy4();
            Console.WriteLine("--------------------");
            Console.WriteLine("Task #5:");
            Easy5();
        }

        private static void Easy3()
        {
            double a, b, c;
            a = 3; b = 4; /* default */
            try
            {
                Console.WriteLine("Enter argument a:");
                a = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter argument b:");
                b = double.Parse(Console.ReadLine());
            }
            catch (FormatException f)
            {
                Console.WriteLine(f.Message);
                Console.WriteLine("\nDefault values of a and b were used...");
            }

            if (a > 0)
            {
                //c = Math.Max(a, b);
                c = a > b ? a : b;
            }
            else
            {
                //c = Math.Min(a, b);
                c = a < b ? a : b;
            }
            Console.WriteLine("c = {0} = {1}", (a > 0) ? "max(a, b)" : "min(a, b)", c);
        }

        private static void Easy4()
        {
            double a, b, c, z;
            a = 2; b = 9; c = 35; /* default */

            try
            {
                Console.WriteLine("Enter argument a:");
                a = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter argument b:");
                b = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter argument c:");
                c = double.Parse(Console.ReadLine());
            }
            catch (FormatException f)
            {
                Console.WriteLine(f.Message);
                Console.WriteLine("\nDefault values of a, b, c were used...");
            }

            //z = Math.Max(Math.Min(a, b), c);
            z = ((a < b ? a : b) > c ? (a < b ? a : b) : c);

            Console.WriteLine("z = {0} = {1}",  "max(min(a, b), c)", z);
        }

        private static void Easy5()
        {
            (double r, double s) input1 = (70, 36.74);
            (double r, double s) input2 = (0.86, 0.74);
            /*
             * Diagonal of square = a * sqrt(2)
             * a = sqrt(Square)
             * Diagonal = sqrt(square) * sqrt(2)
             */
            double diagonal = Math.Sqrt(input1.s) * Math.Sqrt(2);
            /*
             * Area of circle = PI * R ^2
             * D = 2 * Math.Sqrt(area/PI)
             */
            double diameter = 2 * Math.Sqrt(input1.r / Math.PI);
            Console.WriteLine($"Does Square with area = {input1.s} fits in Circle with area = {input1.r}?\n");
            if (diagonal <= diameter)
                Console.WriteLine("Yes. Square fits in circle\n");
            else
                Console.WriteLine("No. Square doesn't fit in circle\n");

            diagonal = Math.Sqrt(input2.s) * Math.Sqrt(2);
            diameter = 2 * Math.Sqrt(input2.r / Math.PI);
            Console.WriteLine($"Does Square with area = {input2.s} fits in Circle with area = {input2.r}?\n");
            if (diagonal <= diameter)
                Console.WriteLine("Yes. Square fits in circle");
            else
                Console.WriteLine("No. Square doesn't fit in circle");
        }
    }
}
