using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lab5_Easy
{
    class Program
    {
        private const int
            borderLength = 70;

        static void Main(string[] args)
        {
            Func<int, int> factrec = null;
            factrec = x => x <= 1 ? 1 : x * factrec(x - 1);

            DisplayResult(
                (Easy1, nameof(Easy1)),
                (Easy2, nameof(Easy2)),
                (Easy3, nameof(Easy3))
                );
        }

        private static void Easy1()
        {
            const int k = 5;
            Queue<int> candidates = new Queue<int>();
            candidates.Enqueue(8);
            candidates.Enqueue(10);
            candidates.Enqueue(11);
            
            for (int i = 0, cnt = candidates.Count(), n; i < cnt; ++i)
                Console.WriteLine(
                    $"Ways to select {k} people for team out of " +
                    $"{n = candidates.Dequeue(), 2} candidates: " +
                    $"{BinomialCoefficient(k, n), 3}\n"
                    );
        }

        private static int BinomialCoefficient(int k, int n)
        {
            return factorial(n) / (factorial(k) * factorial(n - k));
        }

        private static int factorial(int n)
        {
            if (n < 0) throw new ArgumentException
                    ("You must supply positive argument only");
            int result = 1;
            while (n > 1)
                result *= n--;
            return result;
        }

        private struct Triangle
        {
            public double a, b, c;

            public Triangle(double a, double b, double c)
            {
                this.a = a;
                this.b = b;
                this.c = c;
            }
        }

        private static void Easy2()
        {
            const int
                lbound = 2,
                rbound = 20;
            Random rand = new Random();
            Func<int> randomizeNum = () => { return rand.Next(lbound, rbound); };
            Func<Triangle, string> strSides =
                (Triangle tr) => { return $"a = {tr.a}, b = {tr.b}, c = {tr.c}"; };
            Triangle
                tr1 = new Triangle
                    (randomizeNum(), randomizeNum(), randomizeNum()),
                tr2 = new Triangle
                    (randomizeNum(), randomizeNum(), randomizeNum());
            double
                tr1Square = HeronsFormula(tr1),
                tr2Square = HeronsFormula(tr2);
            Console.WriteLine(
                $"Triangle #1 with sides: " +
                $"[ {strSides(tr1)} ]\n" +
                $"Area equals to {tr1Square, 0:F6}\n\n" +
                $"Triangle #2 with sides: " +
                $"[ {strSides(tr2)} ]\n" +
                $"Area equals to {tr2Square, 0:F6}\n\n" +
                (tr1Square == tr2Square ?
                $"Triangles' squares are equal."
                :
                ((tr1Square > tr2Square ? $"First Triangle" : $"Second Triangle") +
                $"'s area is bigger than " +
                (tr1Square > tr2Square ? $"Second Triangle" : $"First Triangle") +
                "'s area."))
            );
        }

        private static double HeronsFormula(Triangle tr)
        {
            double
                a = tr.a,
                b = tr.b,
                c = tr.c;

            return Math.Sqrt(
                 (a + b + c) / 2      *
                ((a + b + c) / 2 - a) *
                ((a + b + c) / 2 - b) *
                ((a + b + c) / 2 - c)
            );
        }

        private static void Easy3()
        {
            (double v, double a)
                cyclist1 = (10,   1),
                cyclist2 = ( 9, 1.6);

            //a)
            var times = new List<double> { 1, 4 };

            Console.WriteLine(
                $"First  cyclist has velocity = {cyclist1.v, 2} km/h " +
                $"and acceleration = {cyclist1.a, 3} km/h^2\n\n" +
                $"Second cyclist has velocity = {cyclist2.v, 2} km/h " +
                $"and acceleration = {cyclist2.a, 3} km/h^2\n\n");

            Console.WriteLine("a)");
            double way1, way2;
            foreach (double t in times)
            {
                way1 = Way(cyclist1, t);
                way2 = Way(cyclist2, t);
                Console.WriteLine($"After {t} hour{(t>1?$"s":$"")} the way of " +
                    $"{(way1 > way2 ? $"first cyclist" : $"second cyclist")} " +
                    $"S = {(way1 > way2 ? way1 : way2)} km is bigger than the way of " +
                    $"{(way1 < way2 ? $"first cyclist" : $"second cyclist")} " +
                    $"S = {(way1 < way2 ? way1 : way2)} km\n\n");
            }

            //b)
            Console.WriteLine("b)");
            double time;
            for (time = 1; Way(cyclist1, time) >= Way(cyclist2, time); time += 0.1);
            Console.WriteLine($"The second cyclist will overtake the first one " +
                $"after {((int)time)} hour{(time>1?$"s":$"")} and {(int)(time%1*60)} minutes.");
        }

        private static double Way((double v, double a) cyclist, double t)
        {
            double
                v = cyclist.v,
                a = cyclist.a;
            return v * t + a * t * t / 2;
        }

        private static int Factorial_recur(int n)
        {
            int result = 1;

            if (n < 0) return 0;
            else
            {
                if (n <= 1) return 1;
                else result *= Factorial_recur(n - 1);
            }
            return result;
        }

        private static void DisplayResult
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
    }
}
