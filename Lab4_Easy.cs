using System;

namespace Lab4_Easy
{
    class Program
    {
        private const int
            lbound = -10, /* left bound of randomization */
            rbound = 10, /* right bound of randomization */
            fieldLen = 10;

        static void Main(string[] args)
        {
            //PrintVarName( () => A, $" == {A}");
            //Easy20();
            //Easy21();
            Easy22();


        }

        static private void Easy20()
        {
            /*  Task:
             * Replace max element in every row of the matrix F (n x m)
             * with half-sum of first and last negative elements in the row
            */

            //const int n = 6, m = 7;
            int n = 6, m = 7;
            try
            {
                Console.WriteLine("Enter number of rows of the array:\nn=");
                n = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter number of columns of the array:\nn=");
                m = int.Parse(Console.ReadLine());
            }
            catch (FormatException f)
            {
                Console.WriteLine(f.Message);
                Console.WriteLine($"\nDefault values [n x m] == [{n} x {m}] were used...");
            }

            /* Due to the lack of specification:
             * assuming absence of negative elements as value of 0s */
            int maxIndx = 0;
            (double first, double last) negative = (0, 0);
            double[,] F = RandomizeArray(n, m);
            double[,] originalArray = F.Clone() as double[,];

            AdvancedPrintMatrix(F, () => F, 2, 2);

            for (int i = 0; i < F.GetLength(0); ++i)
            {
                negative.first = negative.last = 0;
                maxIndx = 0;

                for (int j = 0; j < F.GetLength(1); ++j)
                {
                    if (F[i, j] > F[i, maxIndx]) maxIndx = j;
                    if (F[i, j] < 0)
                    {
                        if (negative.first == 0) negative.first = F[i, j];
                        negative.last = F[i, j];
                    }
                }
                PrintVarName( () => F, $"[{i}, {maxIndx}] == {F[i, maxIndx], 3} -- " +
                    $"Max element; First negative: {negative.first, 3}; " +
                    $"Last negative: {negative.last, 3}");
                F[i, maxIndx] = (negative.first + negative.last) / 2;
            }

            Console.WriteLine($"\n\nResult of replacing max elements of every row " +
                $"with half-sum of first and last negative elements of this row:");
            //AdvancedPrintMatrix(F, () => F, 2, 0);
            PrintColoredDiff(F, originalArray, 2, 2);
        }

        static private void Easy21()
        {
            /*  Task:
             * The first 6 columns are filled with values in the matrix [5 x 7].
             * Put max elements of rows in the last column.
             */
            const int n = 5, m = 7;
            double[,] H = new double[n, m];
            H = RandomizeArray(n, m);
            for (int i = 0; i < n; ++i)
                H[i, m - 1] = double.NaN;
            AdvancedPrintMatrix(H, () => H, 2, 0);

            for (int i = 0; i < H.GetLength(0); ++i)
            {
                H[i, m - 1] = double.MinValue;
                for (int j = 0; j < H.GetLength(1) - 1; ++j)
                    if (H[i, j] > H[i, m - 1]) H[i, m - 1] = H[i, j];
            }

            AdvancedPrintMatrix(H, () => H, 2, 0);
        }

        static private void Easy22()
        {
            /*  Task:
             * Replace max element of the matrix Z [6 x 8]
             * with arithmetic mean of positive elements */

            const int n = 6, m = 8;
            double[,] Z = new double[n, m];
            Z = RandomizeArray(n, m);
            (int i, int j) maxIndx = (0, 0);
            (int n, double sum) arithmMean = (0, 0);

            AdvancedPrintMatrix(Z, () => Z, 2, 0);

            for (int i = 0; i < Z.GetLength(0); ++i)
                for (int j = 0; j < Z.GetLength(1) - 1; ++j)
                {
                    if (Z[i, j] > Z[maxIndx.i, maxIndx.i]) maxIndx = (i, j);
                    /* Assuming 0 is positive element too */
                    if (Z[i, j] >= 0) { ++arithmMean.n; arithmMean.sum += Z[i, j]; }
                }
            Z[maxIndx.i, maxIndx.j] = arithmMean.sum / arithmMean.n;

            AdvancedPrintMatrix(Z, () => Z, 2, 0);
        }

        static private double[,] RandomizeArray(int n, int m)
        {
            double[,] rndArray = new double[n, m];
            Random rand = new Random();

            for (int i = 0; i < rndArray.GetLength(0); ++i)
                for (int j = 0; j < rndArray.GetLength(1); ++j)
                    rndArray[i, j] = rand.Next(lbound, rbound);
                    //rndArray[i, j] = rand.Next(lbound, rbound) + rand.NextDouble();
            return rndArray;
        }

        static private void OrdinaryPrintMatrix(double[,] array)
        {
            Console.WriteLine($"Array [{array.GetLength(0)} x {array.GetLength(1)}]:\n");
            for (int i = 0; i < array.GetLength(0); ++i)
            {
                for (int j = 0; j < array.GetLength(1); ++j)
                    Console.Write($"{array[i, j], fieldLen}");
                Console.WriteLine();
            }
        }

        static private void PrintColoredDiff
            (double[,] currArray, double[,] cmpArrray,
            int upperIndents, int lowerIndents)
        {
            Console.Write(new string('\n', upperIndents));
            for (int i = 0; i < currArray.GetLength(0); ++i)
            {
                for (int j = 0; j < currArray.GetLength(1); ++j)
                    if (currArray[i, j] == cmpArrray[i, j])
                        Console.Write(
                            currArray[i, j] % 1 == 0
                            ?
                            $"{currArray[i, j], fieldLen}" : $"{currArray[i, j], fieldLen:F3}");
                    else
                        ColorString(
                            currArray[i, j] % 1 == 0
                            ?
                            $"{currArray[i, j],fieldLen}" : $"{currArray[i, j],fieldLen:F3}",
                            ConsoleColor.Black, ConsoleColor.Green);
                Console.WriteLine();
            }

            Console.Write(new string('\n', lowerIndents));
        }

        static private void AdvancedPrintMatrix<T> /* Overloading #0 */
            (double[,] array,
            System.Linq.Expressions.Expression<Func<T>> lmbd)
        {
            int delimeter = array.GetLength(1),
                delimCounter = 0;
            //PrintVarName(() => array, $" [{array.GetLength(0)}x{array.GetLength(1)}]:\n");
            PrintVarName(lmbd, $" [{array.GetLength(0)}x{array.GetLength(1)}]:\n");
            foreach (double element in array)
                if (++delimCounter < delimeter)
                    Console.Write($"{element, fieldLen}");
                else { Console.WriteLine($"{element, fieldLen}"); delimCounter = 0; }
        }

        static private void AdvancedPrintMatrix /* Overloading #1 */
            (double[,] array, string nameOfVar)
        {
            /*Usage:
             * AdvancedPrintMatrix(F, nameof(F)); */
            int delimeter = array.GetLength(1),
                delimCounter = 0;
            //PrintVarName(() => array, $" [{array.GetLength(0)}x{array.GetLength(1)}]:\n");
            Console.WriteLine(nameOfVar + $" [{array.GetLength(0)} x {array.GetLength(1)}]:\n");
            foreach (double element in array)
                if (++delimCounter < delimeter)
                    Console.Write(
                        element % 1 == 0
                        ?
                        $"{element,fieldLen}" : $"{element, fieldLen:F3}");
                else
                {
                    Console.WriteLine(
                        element % 1 == 0
                        ?
                        $"{element,fieldLen}" : $"{element, fieldLen:F3}");
                    delimCounter = 0;
                }
        }

        static private void AdvancedPrintMatrix<T> /* Overloading #2 */
            (double[,] array,
            System.Linq.Expressions.Expression<Func<T>> lmbd,
            int upperIndents,
            int lowerIndents)
        {
            int delimeter = array.GetLength(1),
                delimCounter = 0;
            Console.Write(new string('\n', upperIndents));
            //PrintVarName(() => array, $" [{array.GetLength(0)}x{array.GetLength(1)}]:\n");
            PrintVarName(lmbd, $" [{array.GetLength(0)} x {array.GetLength(1)}]:\n");
            foreach (double element in array)
                if (++delimCounter < delimeter)
                    Console.Write(
                        element % 1 == 0
                        ?
                        $"{element, fieldLen}" : $"{element, fieldLen:F3}");
                else
                {
                    Console.WriteLine(
                        element % 1 == 0
                        ?
                        $"{element, fieldLen}" : $"{element, fieldLen:F3}");
                    delimCounter = 0;
                }
            Console.Write(new string('\n', lowerIndents));
        }

        static private void PrintVarName<T> /* Overloading #0 */
            (System.Linq.Expressions.Expression<Func<T>> input,
            string text)
        {
            System.Linq.Expressions.LambdaExpression lambda =
                (System.Linq.Expressions.LambdaExpression) input;
            System.Linq.Expressions.MemberExpression member =
                (System.Linq.Expressions.MemberExpression) lambda.Body;
            Console.WriteLine($"{member.Member.Name}" + text);
        }

        static private void PrintVarName<T> /* Overloading #1 */
            (System.Linq.Expressions.Expression<Func<T>> input)
        {
            System.Linq.Expressions.LambdaExpression lambda =
                (System.Linq.Expressions.LambdaExpression)input;
            System.Linq.Expressions.MemberExpression member =
                (System.Linq.Expressions.MemberExpression)lambda.Body;
            //var val = input.Compile()();
            //Console.WriteLine($"{member.Member.Name}: {val}");
            Console.WriteLine($"{member.Member.Name}");
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
