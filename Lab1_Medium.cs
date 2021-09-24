
/*
 * Medium section (second level)
 * 
 * My variant: 20
 * 
 * yields tasks: #10, #1, #2
 * 
 */

using System;

namespace Lab1_Medium
{
    class Program
    {
        static void Main(string[] args)
        {

            //Medium10();
            Medium1();
            //Medium2();

            //Console.WriteLine(GetEndingOfNum(3));

        }
        static void Medium10()
        {
            /*
             * Calculate:
             * Sequence member of 1/1, 2/1, 3/2, ... that differs from previous less than 0.001
             * Numerator and denominator of next member calculates by adding numerators and
             * denominators of 2 previous members
             * as
             * (1 + 2)/(1 + 1)= 3/2
             */

            int numOfCurr = 3;
            double a_prev1 = 1; // 1/1
            double b_prev1 = 1;
            double a_prev2 = 2; // 2/1
            double b_prev2 = 1;
            double a_curr = 3; // 3/2
            double b_curr = 2;
            double diff = double.MaxValue;

            
            for (; diff > 0.001; )
            {
                numOfCurr++;
                a_prev1 = a_prev2;
                b_prev1 = b_prev2;

                a_prev2 = a_curr;
                b_prev2 = b_curr;

                a_curr = a_prev1 + a_prev2;
                b_curr = b_prev1 + b_prev2;
                diff = (a_curr / b_curr) - (a_prev2 / b_prev2);
            }
            //Console.WriteLine("The answer is #{0} = {1}/{2} = {3}", numOfCurr, a_curr, b_curr, a_curr/b_curr);
            Console.WriteLine("The answer is {0}{1} member = {2}/{3} = {4}",
                numOfCurr, GetEndingOfNum(numOfCurr), a_curr, b_curr, a_curr / b_curr);
            Console.WriteLine("Difference = {0}", diff);
            Console.WriteLine("Prev1 = {0}/{1} and Prev2 = {2}/{3}", a_prev1, b_prev1, a_prev2, b_prev2);


        }
        static void Medium1()
        {
            /* Sum s of s = cosx + (cos 2x)/(2^2) + ... + (cos nx)/(n^2) + ...  till current abs element is smaller than eps = 0,0001 */
            const double eps = 0.0001;
            double s = 0;
            double x = Math.PI;

            Console.WriteLine("Enter argument x of the cosinus:");
            try
            {
                x = double.Parse(Console.ReadLine());
                //Convert.ToDouble()
            }
            catch (FormatException f)
            {
                Console.WriteLine(f.Message);
                Console.WriteLine("Default x's been used...");
            }


            for (double n = 1; Math.Abs(Math.Cos(n * x)/(n * n))> eps; n++)
            {
                //s += Math.Cos(x * n)/Math.Pow(n, 2);
                s += Math.Cos(x * n) / (n * n);
                //Console.WriteLine("eps = {0}", Math.Cos(n * x) / (n * n));
            }
            Console.WriteLine("Sum = {0}", s);
            
        }
        static void Medium2()
        {
            /* Biggest multiplier n for which product p = 1 * 4 * 7 *...* n smaller than L = 30000 */
            /* 1 4 7 10 13*/
            double p = 1, n = 1;
            const double L = 30000;
            for (; p <= L; )
            {
                n += 3;
                p *= n;
                Console.WriteLine(n);
                
            }
            Console.WriteLine("p = {0}, n = {1}", p/n, n-3);

        }
        private static string GetEndingOfNum(int num)
        {
            int ones = num % 10;
            
            int tens = (num/10) % 10;
            string ending = "";

            if (tens == 1) return "th";
            else
            {
                switch (ones)
                {
                    case 1 : ending = "st";
                        break;
                    case 2 : ending = "nd";
                        break;
                    case 3 : ending = "rd";
                        break;
                    default : ending = "th";
                        break;
                }
            }
            return ending;
        }
    }
}
