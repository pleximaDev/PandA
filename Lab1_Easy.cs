/*
 * Easy section (first level)
 * 
 * My variant: 20
 * 
 * yields tasks: #2, #3, #4
 * 
 */


using System;

namespace Lab1_easy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Task #2:");
            Easy2();
            Console.WriteLine("--------------------\n\n\n");
            Console.WriteLine("--------------------");
            Console.WriteLine("Task #3:");
            Easy3();
            Console.WriteLine("--------------------\n\n\n");
            Console.WriteLine("--------------------");
            Console.WriteLine("Task #4:");
            Easy4();
            Console.WriteLine("--------------------");
        }

        static void Easy2()
        {
            /* Calculate:
             * s = 1/1 + 1/2 + 1/3 + 1/4 + ... + 1/10 
             */
            double s = 0;
            for (double i = 1; i <= 10; i++)
                s += 1 / i;
            Console.WriteLine("s = {0:f5}", s);
        }

        static void Easy3()
        {
            /* Calculate:
             * s = 2/3 + 4/5 + 6/7 + ... + 112/113
             */
            double s = 0;
            //for (double i = 2; i <= 112; i++, s += i / (i + 1)) /* counts 1 more iteration than needed */;
            for (double i = 2; i <= 112; i++)
                s += i / (i + 1);
            Console.WriteLine("s = {0:f5}", s);

        }

        static void Easy4()
        {
            /* Calculate:
             * s = cos(x)/x^0 + cos(2x)/x^1 + cos(3x)/x^2 + ... + cos(9x)/x^8
             * s = sumOf( cos(n * x)/x^(n-1)), n = 1..9 
             */

            double x = 30;
            double s = 0;

            Console.WriteLine("Enter the argument x of the cosinus" +
                "(or skip this part and use default x = {0}):", x);

            /* Preventing entering string instead of double */
            try
            {
                /* Converting input to double */
                x = double.Parse(Console.ReadLine());
                /* Convert.ToDouble() */
            }
            catch (FormatException f)
            {
                Console.WriteLine(f.Message);
            }

            //for (double xArg = 1, xPow = 0; xArg <= 9; xArg++, xPow++) ;

            for (double n = 1; n <= 9; n++)
                s += (Math.Cos(n * x)) / Math.Pow(x, n - 1);
            Console.WriteLine("s = {0:f5}", s);
        }
    }
}
