using System;

namespace Lab1_Hard
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nRecurrent function:");
            Hard2();
            Console.WriteLine("\nUsing usual iterated element:");
            Hard2_iter();
            Console.WriteLine("\nAnalytical function:");
            Analytical();
        }
        static void Hard2() /* Recurrent */
        {
            /* y = (x * sin(pi/4) )/ (1 - 2x * cos(pi/4) + x^2), */
            /* s = sum(1..inf){x^i * sin(i * pi/4)}*/

            const double a = 0.1, b = 0.8, h = 0.1, eps = 0.0001;
            double x, s, recurr;

            for (x = a; x <= b; x += h)
            {
                //s = Math.Sqrt(2)/20; // Calculated first element with x = 0.1 and i = 1
                s = x * Math.Sin(Math.PI/4);
                recurr = 1;

                for (int i = 2; Math.Abs(recurr) >= eps; i++)
                {
                    recurr *= (x * Math.Sin(i * Math.PI/4))/(Math.Sin((i - 1) * Math.PI/4));
                    s += recurr;
                }
                Console.WriteLine("x = {0:f4}  y = {1:f4}", x, s);
            }
        }

        static void Hard2_iter()
        {
            const double a = 0.1, b = 0.8, h = 0.1, eps = 0.0001;
            double x, s;

            for (x = a; x <= b; x += h)
            {
                s = 0;
                for (int i = 1; Math.Abs(Summation_Item(i, x)) >= eps; i++)
                {
                    s += Summation_Item(i, x);
                }
                Console.WriteLine("x = {0:f4}  y = {1:f4}", x, s);
            }
        }

        private static double Summation_Item(double i, double x)
        {
            return Math.Pow(x, i) * Math.Sin( (i * Math.PI)/4 );
        }

        static void Analytical()
        {
            const double a = 0.1, b = 0.8, h = 0.1;
            double x, y;
            for (x = a; x <= b; x += h)
            {
                y = (x * Math.Sin(Math.PI/4)) / (1 - 2 * x * Math.Cos(Math.PI/4) + x * x);
                Console.WriteLine("x = {0:f4}   y = {1:f4}", x, y);
            }
        }
    }
}
