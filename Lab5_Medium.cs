using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Lab5_Medium
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
                (Medium20, nameof(Medium20)),
                (Medium21, nameof(Medium21)),
                (Medium22, nameof(Medium22))
                );
        }

        private static void Medium20()
        {
            const int
                n = 5, m = 7;
            double[,]
                matrix1 = RandomizeArray(n, m, "int", -5, 5),
                matrix2 = RandomizeArray(n, m, "int", 0, 8);

            AdvancedPrintMatrix(matrix1, () => matrix1, 2, 2);
            AdvancedPrintMatrix(matrix2, () => matrix2, 2, 2);

            ZeroColumnTrimMatrix(matrix1, out matrix1);
            ZeroColumnTrimMatrix(matrix2, out matrix2);

            AdvancedPrintMatrix(matrix1, () => matrix1, 2, 2);
            AdvancedPrintMatrix(matrix2, () => matrix2, 2, 2);
        }

        private static void ZeroColumnTrimMatrix(double[,] matrix, out double[,] matrixZeros)
        {
            List<int> ZeroColumns = new List<int> { };

            for (int j = 0; j < matrix.GetLength(1); ++j)
                for (int i = 0; i < matrix.GetLength(0); ++i)
                    if (matrix[i, j] == 0) { ZeroColumns.Add(j); break; }

            matrixZeros = new double[matrix.GetLength(0), ZeroColumns.Count];

            int k = 0;
            foreach (int j in ZeroColumns)
            {
                for (int i = 0; i < matrix.GetLength(0); ++i)
                    matrixZeros[i, k] = matrix[i, j];
                ++k;
            }
        }


        private static void Medium21()
        {
            const int n = 5;

            double[,]
                matrix1 = RandomizeArray(n, n, "int"),
                matrix2 = RandomizeArray(n, n, "int", -15, 15);

            AdvancedPrintMatrix(matrix1, () => matrix1, 2, 2);
            AdvancedPrintMatrix(matrix2, () => matrix2, 2, 2);

            MinRowsElemsAfterDiag(matrix1, out double[] minRowsElems1);
            MinRowsElemsAfterDiag(matrix2, out double[] minRowsElems2);

            Console.WriteLine(
                $"Array of min elements of rows " +
                $"after diag elements(including) of {nameof(matrix1)}:\n\n" +
                string.Join("  ", minRowsElems1) +
                $"\n\n\n" +
                $"Array of min elements of rows " +
                $"after diag elements(including) of {nameof(matrix2)}:\n\n" +
                string.Join("  ", minRowsElems2));
        }

        private static void MinRowsElemsAfterDiag(double[,] matrix, out double[] array1d)
        {
            array1d = new double[matrix.GetLength(0)];
            for (int i = 0, jmin = 0; i < matrix.GetLength(0); ++i, jmin = i)
            {
                for (int j = i; j < matrix.GetLength(1); ++j)
                    if (matrix[i, j] < matrix[i, jmin]) jmin = j;
                array1d[i] = matrix[i, jmin];
            }
        }

        private static void Medium22()
        {
            const int n = 5;
            double[,] matrix = RandomizeArray(n, n, "int");

            AdvancedPrintMatrix(matrix, () => matrix, 2, 2);

            AmountNegRowElems(matrix, out double[] negRowElems);
            MaxNegColumnElems(matrix, out double[] negColElems);

            Console.WriteLine(
                $"Array of number of negative elements of rows " +
                $"of {nameof(matrix)}:\n\n" +
                string.Join("  ", negRowElems) +
                $"\n\n\n" +
                $"Array of max negative elements of columns " +
                $"of {nameof(matrix)}:\n\n" +
                string.Join("  ", negColElems)
                );
        }


        private static void AmountNegRowElems(double[,] matrix, out double[] array1d)
        {
            array1d = new double[matrix.GetLength(0)];

            for (int i = 0, negCntr; i < matrix.GetLength(0); ++i)
            {
                negCntr = 0;
                for (int j = 0; j < matrix.GetLength(1); ++j)
                    if (matrix[i, j] < 0) ++negCntr;
                array1d[i] = negCntr;
            }
        }

        private static void MaxNegColumnElems(double[,] matrix, out double[] array1d)
        {
            array1d = new double[matrix.GetLength(0)];

            for (int j = 0, imaxNeg; j < matrix.GetLength(1); ++j)
            {
                imaxNeg = 0;
                for (int i = 0; i < matrix.GetLength(0); ++i)
                        if
                        (matrix[i, j] < 0 &&
                        (matrix[i, j] > matrix[imaxNeg, j] || matrix[imaxNeg, j] >= 0))
                        imaxNeg = i;
                array1d[j] = matrix[imaxNeg, j];
            }
        }

        static private double[,] RandomizeArray
            (int n, int m, string type, /*optional: */ int leftBound = lbound, int rightBound = rbound)
        {
            double[,] rndArray = new double[n, m];
            Random rand = new Random();
            Func<double> randNumBounded = () => { return rand.Next(leftBound, rightBound); };

            for (int i = 0; i < rndArray.GetLength(0); ++i)
                for (int j = 0; j < rndArray.GetLength(1); ++j)
                    switch (type.ToLower())
                    {
                        case "int":
                        case "integer":
                        case "i":
                            rndArray[i, j] = randNumBounded();
                            break;
                        case "double":
                        case "floating-point":
                        case "float":
                        case "d":
                        case "f":
                        case "fp":
                            rndArray[i, j] = randNumBounded() + rand.NextDouble();
                            break;
                        case "zero":
                        case "zeros":
                        case "0":
                            rndArray[i, j] = 0;
                            break;
                        case "identity":
                        case "eye":
                            if (n != m)
                                throw new InvalidOperationException
                                    ("The matrix must be square [n x n]");
                            rndArray[i, j] = (i == j) ? 1 : 0;
                            break;
                        case "repunit":
                        case "ru":
                        case "one":
                        case "ones":
                        case "1":
                            rndArray[i, j] = 1;
                            break;
                        case "sparse":
                            break;
                        default:
                            throw new System
                                .ComponentModel
                                .InvalidEnumArgumentException();
                    }
            return rndArray;
        }

        static private void AdvancedPrintMatrix<T>
            (double[,] array,
            System.Linq.Expressions.Expression<Func<T>> lmbd,
            int upperIndents,
            int lowerIndents)
        {
            Func<System.Linq.Expressions.Expression<Func<T>>, string> GetName =
                (input) =>
            {
                System.Linq.Expressions.LambdaExpression lambda = input;
                System.Linq.Expressions.MemberExpression member =
                    (System.Linq.Expressions.MemberExpression)lambda.Body;
                return member.Member.Name;
            };
            int delimeter = array.GetLength(1),
                delimCounter = 0;
            Console.Write(new string('\n', upperIndents));
            Console.WriteLine($"{GetName(lmbd)}  [{array.GetLength(0)} x {array.GetLength(1)}]:\n");
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
