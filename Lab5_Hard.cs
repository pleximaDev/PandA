using System;
using System.Text.RegularExpressions;

namespace Lab5_Hard
{
    class Solution
    {
        static void Main(string[] args)
        {
            MyTools.DisplayResults(
                (Hard1, nameof(Hard1)),
                (Hard5, nameof(Hard5))
                );
        }

        delegate double approx (double i, double x);

        private static void Hard1()
        {
            const double e = Math.E, pi = Math.PI;
            Func<double, double>
                sin = (x) => Math.Sin(x),
                cos = (x) => Math.Cos(x);
            Func<double, double, double> pow = (x, p) => Math.Pow(x, p);

            Function
                func1 = new Function
                (
                    (i, x) => { return cos(i * x)/MyTools.factorial(i); },
                    (x)    => { return pow(e, cos(x)) * cos(sin(x));    },
                    (0.1, 1), 0.1),
                func2 = new Function
                (
                    (i, x) => { return pow(-1, i) * cos(i * x) / (i * i); },
                    (x)    => { return (x * x - pi * pi / 3) / 4;         },
                    (pi/5, pi), pi/25);

            Console.WriteLine($"{nameof(func1)}\n\tx\t|\tApprox f(x)\t|\tAnalytical f(x)" +
                $"\n\t\t|\t\t\t|\t");
            for (double x = func1.interval.a; x <= func1.interval.b; x += func1.h)
                Console.WriteLine($"\t{x, 3:F1}\t|\t{Sum(func1, x), 0:F3}\t\t|\t{func1.f_analytical(x), 0:F3}");
            Console.WriteLine($"\n\n{nameof(func2)}\n\tx\t|\tApprox f(x)\t|\tAnalytical f(x)" +
                $"\n\t\t|\t\t\t|\t");
            for (double x = func2.interval.a; x <= func2.interval.b; x += func2.h)
                Console.WriteLine($"\t{x,3:F1}\t|\t{Sum(func2, x, 1),0:F3}\t\t|\t{func2.f_analytical(x),0:F3}");
        }

        private static double Sum(Function fnc, double x, double startPnt = 0, double eps = 1e-5)
        {
            double sum = 0;
            double i = startPnt;

            while (fnc.f_approximation(i, x) > eps)
                sum += fnc.f_approximation(i++, x);
            return sum;
        }

        struct Function
        {
            public Func<double, double, double> f_approximation;
            public Func<double, double> f_analytical;
            public (double a, double b) interval;     /* [a, b] */
            public double h;                          /*  step  */

            public Function
                (approx f_approximation,
                Func<double, double> f_analytical,
                (double, double) interval, double h)
            {
                this.f_approximation = f_approximation;
                this.f_analytical = f_analytical;
                this.interval = interval;
                this.h = h;
            }
        }

        struct @Func
        {
            public (double a, double b) interval; /* [a, b] */
            public double h;                      /*  step  */
            public Func<double, double> f;

            public @Func
                (Func<double, double> f, (double, double) interval, double h)
            {
                this.interval = interval;
                this.h = h;
                this.f = f;
            }
        }

        private static void Hard5()
        {
            @Func
                func1 = new @Func
                ((x) => { return x * x - Math.Sin(x); }, (0, 2), 0.1),
                func2 = new @Func
                ((x) => { return Math.Pow(Math.E, x) - 1; }, (-1, 1), 0.2);

            Console.WriteLine(
                $"{nameof(func1)} has {NumberOfIntervalsFuncSignChange(func1)} intervals.\n\n" +
                $"{nameof(func2)} has {NumberOfIntervalsFuncSignChange(func2)} intervals.\n\n");
        }

        private static int NumberOfIntervalsFuncSignChange(@Func fnc)
        {
            int intervals = 1;
            for (double x = fnc.interval.a; x < fnc.interval.b; x += fnc.h)
                if ((fnc.f(x) >= 0 && fnc.f(x + fnc.h) >= 0)
                    ||
                    (fnc.f(x) < 0 && fnc.f(x + fnc.h) < 0))
                    continue;
                else ++intervals;
            return intervals;
        }
    }

    class MyTools
    {
        private const int
            borderLength = 70;

        public static void DisplayResults
            (params (Action, string)[] functions)
        {
            string border = new string('-', borderLength),
                digitsPttrn = @"\d+$";
            Regex rgx = new Regex(digitsPttrn);

            foreach ((Action call, string name) function in functions)
            {
                Console.WriteLine($"{border}\nTask #{rgx.Match(function.name)}:\n");
                function.call();
                Console.WriteLine(border + "\n\n\n");
            }
        }

        public static int factorial(int n)
        {
            if (n < 0) throw new ArgumentException
                    ("You must supply positive argument only");
            int result = 1;
            while (n > 1)
                result *= n--;
            return result;
        }

        public static double factorial(double n)
        {
            if (n < 0) throw new ArgumentException
                    ("You must supply positive argument only");
            double result = 1;
            while (n > 1)
                result *= n--;
            return result;
        }

        public static string Center(string strToCenter, string cmpr)
        {
            int padding;
            padding = cmpr.Length - strToCenter.Length;
            return new string(' ', padding) + strToCenter + new string(' ', padding);
        }
    }
}
