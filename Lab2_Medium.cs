using System;
using System.Collections.Generic;

namespace Lab2_Medium
{
    class Program
    {
        static void Main(string[] args)
        {
            //Medium12();
            //List<int> grades = new List<int>();
            Medium10();
        }
        static private void Medium10()
        {
            int n = 4;
            int counter = 0;

            Console.WriteLine("How many students are in the group?");
            Console.Write("n = ");

            try
            {
                n = int.Parse(Console.ReadLine());
            }
            catch (FormatException f)
            {
                Console.WriteLine(f.Message);
                return;
            }
            for (int i = 1; i <= n; i++)
            {
                counter++;
                for (int j = 1; j <= 4; j++)
                {
                    Console.WriteLine($"Enter {j}{GetEndingOfNum(j)} grade of {i}{GetEndingOfNum(i)} student:");
                    if (int.Parse(Console.ReadLine()) < 4)
                    {
                        counter--;
                        break;
                    }
                }
            }
            Console.WriteLine($"Number of students without '2' and '3' grades: {counter}");
        }
        //static private void Medium11()
        //{ }

        static private void Medium12()
        {
            int n = 1;
            double r = 4, area = 0;
            string choice = "";

            Dictionary<string, string> figure = new Dictionary<string, string>();
            figure.Add("1", "Square");
            figure.Add("1.", "Square");
            figure.Add("square", "Square");

            figure.Add("2", "Circle");
            figure.Add("2.", "Circle");
            figure.Add("circle", "Circle");

            figure.Add("3", "Equilateral Triangle");
            figure.Add("3.", "Equilateral Triangle");
            figure.Add("triangle", "Equilateral Triangle");

            Console.WriteLine("How many r values do you want to enter?");
            Console.Write("n =");
            try
            {
                n = int.Parse(Console.ReadLine());
            }
            catch (FormatException f)
            {
                Console.WriteLine(f.Message);
                return;
            }

            while (n --> 0)
            {
                try
                {
                    Console.WriteLine("Enter value of r");
                    Console.Write("r = ");
                    r = double.Parse(Console.ReadLine());
                }
                catch (FormatException f)
                {
                    Console.WriteLine(f.Message);
                    return;
                }

                Console.WriteLine("Choose what do you want to calculate with r:\n\n" +
                    "1. Area of Square with side r.\n" +
                    "2. Area of Circle with radius r.\n" +
                    "3. Area of Equilateral Triangle (sides are equal) with side r.\n\n" +
                    "You can enter either number of your choice or name of figure (square, circle, triangle).\n\n");
                choice = Console.ReadLine();

                switch (choice.ToLower())
                {
                    case "1":
                    case "1.":
                    case "square":
                        area = r * r;
                        break;
                    case "2":
                    case "2.":
                    case "circle":
                        area = Math.PI * r * r;
                        break;
                    case "3":
                    case "3.":
                    case "triangle":
                        area = r * r * Math.Sqrt(3) / 4;
                        break;
                    default:
                        Console.WriteLine("Wrong input of your choice...");
                        break;
                }

                Console.WriteLine(
                    $"Area of " +
                    $"{(figure.ContainsKey(choice.ToLower()) ? figure[choice.ToLower()] : choice)}" +
                    $" S = {area}");
            }
        }
        private static string GetEndingOfNum(int num)
        {
            int ones = num % 10;

            int tens = (num / 10) % 10;
            string ending = "";

            if (tens == 1) return "th";
            else
            {
                switch (ones)
                {
                    case 1:
                        ending = "st";
                        break;
                    case 2:
                        ending = "nd";
                        break;
                    case 3:
                        ending = "rd";
                        break;
                    default:
                        ending = "th";
                        break;
                }
            }
            return ending;
        }
    }
}
