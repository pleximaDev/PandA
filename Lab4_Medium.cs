using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Lab4_Medium
{
    class Program
    {
        private const int
            lbound = -100, /* left bound of randomization */
            rbound = 100, /* right bound of randomization */
            fieldLen = 10,
            borderLength = 70;

        static void Main(string[] args)
        {
            DisplayResult(
                (() => Medium2(), nameof(Medium2)),
                (() => Medium3(), nameof(Medium3)),
                (() => Medium4(), nameof(Medium4))
                );

        }

        static void Medium2()
        {
            /*  Task:
             * Given a matrix A of size 7 by 5.
             * If the number of positive elements of a column is greater
             * than the number of negative ones,
             * then replace the maximum element of this column with 0,
             * otherwise replace the maximum element with the index
             * of the maximum element of this column */

            /* Assuming 0 is positive element too */

            const int n = 7, m = 5;

            double[,] A = RandomizeArray(n, m, "int"),
                originalArray = A.Clone() as double[,];

            (int negative, int positive) columnElements = (0, 0);
            int maxIndx = 0;

            AdvancedPrintMatrix(A, () => A, 2, 2);

            for (int j = 0; j < m; ++j)
            {
                columnElements = (0, 0);
                maxIndx = 0;
                for (int i = 0; i < n; ++i)
                {
                    if (A[i, j] >= 0) ++columnElements.positive;
                    else ++columnElements.negative;
                    if (A[i, j] > A[maxIndx, j]) maxIndx = i;
                }

                if (columnElements.positive > columnElements.negative)
                    A[maxIndx, j] = 0;
                else A[maxIndx, j] = maxIndx;
            }

            PrintColoredDiff(A, originalArray, 2, 1,
                Console.BackgroundColor, ConsoleColor.Green);
        }

        static void Medium3()
        {
            /*  Task:
             * Given a matrix A of size 10 by 5.
             * Convert the matrix as follows:
             * Replace the first element of
             * a column with the sum of the column
             * elements after the maximum element 
             * if the maximum element is
             * in the first half of the column.
             * Otherwise, leave the column unchanged. */

            const int n = 10, m = 5;

            double[,] A = RandomizeArray(n, m, "int"),
                originalArray = A.Clone() as double[,];
            int maxIndx = 0;
            double sum = 0;

            var maxList = new List<(int i, int j)> { };

            AdvancedPrintMatrix(A, () => A, 2, 2);

            for (int j = 0; j < m; ++j)
            {
                sum = 0;
                maxIndx = 0;
                for (int i = 0; i < n; ++i)
                {
                    if (A[i, j] > A[maxIndx, j])
                    { maxIndx = i; sum = 0; }
                    else sum += A[i, j];
                }
                maxList.Add( (maxIndx, j) );
                if ((maxIndx + 1) <= n / 2) A[0, j] = sum;
            }

            PrintColDiffItem(A, originalArray, 2, 1,
                Console.BackgroundColor, ConsoleColor.Green,
                maxList,
                Console.BackgroundColor, ConsoleColor.Red);
        }

        static void Medium4()
        {
            /*  Task:
             * Given a matrix A of size 7 by 5 and an array B of size 5.
             * Replace the maximum column element with the appropriate
             * element of the array B if this element is larger than the maximum
             * column element found. Otherwise, do not replace */

            const int n = 7, m = 5;

            double[,] A = RandomizeArray(n, m, "int"),
                B = RandomizeArray(1, m, "int"),
                originalArray = A.Clone() as double[,];
            int maxIndx = 0;
            var maxList = new List<(int i, int j)> { };

            AdvancedPrintMatrix(A, () => A, 2, 2);
            AdvancedPrintMatrix(B, () => B, 2, 2);

            for (int j = 0; j < m; maxList.Add((maxIndx, j)), ++j)
            {
                maxIndx = 0;
                for (int i = 0; i < n; ++i)
                    if (A[i, j] > A[maxIndx, j])
                        maxIndx = i;
                if (B[0, j] > A[maxIndx, j]) A[maxIndx, j] = B[0, j];
            }

            PrintColDiffItem(A, originalArray, 2, 1,
                Console.BackgroundColor, ConsoleColor.Green,
                maxList,
                Console.BackgroundColor, ConsoleColor.Red);
        }

        static private double[,] RandomizeArray(int n, int m, string type)
        {
            double[,] rndArray = new double[n, m];
            Random rand = new Random();

            for (int i = 0; i < rndArray.GetLength(0); ++i)
                for (int j = 0; j < rndArray.GetLength(1); ++j)
                    switch (type.ToLower())
                    {
                        case "int":
                        case "integer":
                        case "i":
                            rndArray[i, j] = rand.Next(lbound, rbound);
                            break;
                        case "double":
                        case "floating-point":
                        case "float":
                        case "d":
                        case "f":
                        case "fp":
                            rndArray[i, j] = rand.Next(lbound, rbound) + rand.NextDouble();
                            break;
                        case "zero":
                            rndArray[i, j] = 0;
                            break;
                        case "identity":
                            if (n != m)
                                throw new InvalidOperationException
                                    ("The matrix must be square [n x n]");
                            rndArray[i, j] = (i == j) ? 1 : 0;
                            break;
                        default:
                            throw new System
                                .ComponentModel
                                .InvalidEnumArgumentException();
                    }
            return rndArray;
        }

        static private void DisplayResult
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

        static private void PrintColoredDiff
            (double[,] currArray, double[,] cmpArrray,
            int upperIndents, int lowerIndents,
            System.ConsoleColor backColor,
            System.ConsoleColor foreColor)
        {
            Console.Write(new string('\n', upperIndents));
            for (int i = 0; i < currArray.GetLength(0); ++i)
            {
                for (int j = 0; j < currArray.GetLength(1); ++j)
                    if (currArray[i, j] == cmpArrray[i, j])
                        Console.Write(
                            currArray[i, j] % 1 == 0
                            ?
                            $"{currArray[i, j],fieldLen}" : $"{currArray[i, j],fieldLen:F3}");
                    else
                        ColorString(
                            currArray[i, j] % 1 == 0
                            ?
                            $"{currArray[i, j],fieldLen}" : $"{currArray[i, j],fieldLen:F3}",
                            backColor, foreColor);
                Console.WriteLine();
            }

            Console.Write(new string('\n', lowerIndents));
        }

        static private void PrintColDiffItem
            (double[,] currArray, double[,] cmpArrray,
            int upperIndents, int lowerIndents,
            System.ConsoleColor backColor,
            System.ConsoleColor foreColor,
            List<(int i, int j)> items,
            System.ConsoleColor itemBackColor,
            System.ConsoleColor itemForeColor
            )
        {
            Console.Write(new string('\n', upperIndents));
            for (int i = 0; i < currArray.GetLength(0); ++i)
            {
                for (int j = 0; j < currArray.GetLength(1); ++j)
                    if (currArray[i, j] == cmpArrray[i, j])
                        if (items.Contains((i, j)))
                            ColorString(
                                currArray[i, j] % 1 == 0
                                ?
                                $"{currArray[i, j],fieldLen}"
                                :
                                $"{currArray[i, j],fieldLen:F3}",
                                itemBackColor, itemForeColor);
                        else
                            Console.Write(
                                currArray[i, j] % 1 == 0
                                ?
                                $"{currArray[i, j],fieldLen}"
                                :
                                $"{currArray[i, j],fieldLen:F3}");
                    else
                        ColorString(
                            currArray[i, j] % 1 == 0
                            ?
                            $"{currArray[i, j],fieldLen}"
                            :
                            $"{currArray[i, j],fieldLen:F3}",
                            backColor, foreColor);
                Console.WriteLine();
            }
            Console.Write(new string('\n', lowerIndents));
        }


        static private void AdvancedPrintMatrix<T>
            (double[,] array,
            System.Linq.Expressions.Expression<Func<T>> lmbd,
            int upperIndents,
            int lowerIndents)
        {
            int delimeter = array.GetLength(1),
                delimCounter = 0;
            Console.Write(new string('\n', upperIndents));
            PrintVarName(lmbd, $" [{array.GetLength(0)} x {array.GetLength(1)}]:\n");
            foreach (double element in array)
                if (++delimCounter < delimeter)
                    Console.Write(
                        element % 1 == 0
                        ?
                        $"{element,fieldLen}" : $"{element,fieldLen:F3}");
                else
                {
                    Console.WriteLine(
                        element % 1 == 0
                        ?
                        $"{element,fieldLen}" : $"{element,fieldLen:F3}");
                    delimCounter = 0;
                }
            Console.Write(new string('\n', lowerIndents));
        }

        static private void PrintVarName<T>
            (System.Linq.Expressions.Expression<Func<T>> input,
            string text)
        {
            System.Linq.Expressions.LambdaExpression lambda =
                (System.Linq.Expressions.LambdaExpression)input;
            System.Linq.Expressions.MemberExpression member =
                (System.Linq.Expressions.MemberExpression)lambda.Body;
            Console.WriteLine($"{member.Member.Name}" + text);
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
