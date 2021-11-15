using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq; /* Enumerable */

namespace Lab4_Hard
{
    class Program
    {
        private const int
            lbound       = -100, /*  left bound of randomization */
            rbound       =  100, /* right bound of randomization */
            fieldLen     =   10,
            borderLength =   70;

        static void Main(string[] args)
        {
            DisplayResult(
            (() => Hard2(), nameof(Hard2) )

            //(() => Hard6A(), nameof(Hard6A))
            );
        }

        static private void Hard6A()
        {
            /*  Task:
             * In a matrix of size n × n,
             * form two one-dimensional arrays:
             * in one send the upper triangle of the matrix,
             * including elements of the main diagonal,
             * to the other - the lower triangle.
             * Print the top and bottom triangles by rows
             */

            int n;
            string defaultN = "6",
                inp;

            double[,] matrix;

            Console.Write($"Enter number of rows and columns of the square matrix:\nn = ");
            Console.WriteLine($"\nThe square {nameof(matrix)} of size [" +
                $"{n = int.Parse((((inp = Console.ReadLine()) == "") ? null : inp) ?? defaultN)}" +
                $" x {n}]");

            AdvancedPrintMatrix(matrix = RandomizeArray(n, n, "int"), () => matrix, 2, 2);


        }

        static private void HardEasy2()
        {
            int n;
            string defaultN = "6",
                inp;

            double[,] matrix;

            Console.Write($"Enter number of rows and columns of the square matrix:\nn = ");
            Console.WriteLine($"\nThe square {nameof(matrix)} of size [" +
                $"{n = int.Parse((((inp = Console.ReadLine()) == "") ? null : inp) ?? defaultN)}" +
                $" x {n}]");

            AdvancedPrintMatrix(matrix = RandomizeArray(n, n, "ones"), () => matrix, 2, 2);
            double [,] originalArray = matrix.Clone() as double[,];

            for (int i = 0; i < n; ++i)
            { 
                matrix[0, i] = 0;
                matrix[i, 0] = 0;
                if (i != n - 1)
                {
                    matrix[n - 1, i] = 0;
                    matrix[i, n - 1] = 0;
                }
                else matrix[n - 1, n - 1] = 0;
            }
            PrintColoredDiff(matrix, originalArray, 2, 1,
                Console.BackgroundColor, ConsoleColor.Green);
        }


        static private void Hard2()
        {
            int n;
            string defaultN = "6",
                inp;

            double[,] matrix;

            Console.Write($"Enter number of rows and columns of the square matrix:\nn = ");
            Console.WriteLine($"\nThe square {nameof(matrix)} of size [" +
                $"{n = int.Parse((((inp = Console.ReadLine()) == "") ? null : inp) ?? defaultN)}" +
                $" x {n}]");

            AdvancedPrintMatrix(matrix = RandomizeArray(n, n, "ones"), () => matrix, 2, 2);
            double[,] originalArray = matrix.Clone() as double[,];

            List<int> listI =
                Enumerable.Repeat(0, n)
                .Concat(Enumerable.Repeat(n - 1, n))
                .Concat(Enumerable.Range(1, n - 2))
                .Concat(Enumerable.Range(1, n - 2))
                .ToList();
            List<int> listJ =
                Enumerable.Range(0, n)
                .Concat(Enumerable.Range(0, n))
                .Concat(Enumerable.Repeat(0, n - 2))
                .Concat(Enumerable.Repeat(n - 1, n - 2))
                .ToList();

            foreach (var element in listI.Zip(listJ,
                (fIndx, sIndx) => new { i = fIndx, j = sIndx }))
                matrix[element.i, element.j] = 0;

            PrintColoredDiff(matrix, originalArray, 2, 1,
                Console.BackgroundColor, ConsoleColor.Green);
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
                        case "eye":
                            if (n != m)
                                throw new InvalidOperationException
                                    ("The matrix must be square [n x n]");
                            rndArray[i, j] = (i == j) ? 1 : 0;
                            break;
                        case "ones":
                        case "1":
                            rndArray[i, j] = 1;
                            break;
                        default:
                            throw new System
                                .ComponentModel
                                .InvalidEnumArgumentException();
                    }
            return rndArray;
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

        static private void AdvancedPrintMatrix<T>
            (double[,] array,
            System.Linq.Expressions.Expression<Func<T>> lmbd,
            int upperIndents,
            int lowerIndents)
        {
            int delimeter = array.GetLength(1),
                delimCounter = 0;
            Console.Write(new string('\n', upperIndents));
            Console.WriteLine($"{GetVarName(lmbd)}  [{array.GetLength(0)} x {array.GetLength(1)}]:\n");
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

        static private string GetVarName<T>
            (System.Linq.Expressions.Expression<Func<T>> input)
        {
            System.Linq.Expressions.LambdaExpression lambda =
                (System.Linq.Expressions.LambdaExpression)input;
            System.Linq.Expressions.MemberExpression member =
                (System.Linq.Expressions.MemberExpression)lambda.Body;
            return member.Member.Name;
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
    }
}
