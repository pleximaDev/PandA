using System;
using System.Collections.Generic;

namespace Lab2_Hard
{
    class Program
    {
        static void Main(string[] args)
        {
            Hard12();
        }
        static private void Hard12()
        {
            double r = 0, area = 0;
            string choice = "";
            string input = "";

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

            Console.WriteLine("Enter value of r:");
            Console.Write("r = ");
            while (!string.IsNullOrEmpty(input = Console.ReadLine()))
            {
                r = double.Parse(input);

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
                    $" S = {area}\n");

                Console.WriteLine("Enter value of r");
                Console.Write("r = ");
            }
        }
    }
}
