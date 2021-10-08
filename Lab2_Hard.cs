using System;
using System.Collections.Generic;

namespace Lab2_Medium
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Medium12();
            /*
            List<string> lis = new List<string>
            {
                "1", "1.", "square"
            };
            */

            Dictionary<string, string> figure = new Dictionary<string, string>();
            //List<string> keys = ["1","1", "square"];
            //figure.Add(keys, "Square");
            figure.Add("1", "Square");
            figure.Add("1.", "Square");
            figure.Add("square", "Square");

            figure.Add("2", "Circle");
            figure.Add("2.", "Circle");
            figure.Add("circle", "Circle");

            figure.Add("3", "Equilateral Triangle");
            figure.Add("3.", "Equilateral Triangle");
            figure.Add("triangle", "Equilateral Triangle");

            Console.WriteLine(figure.ContainsValue("Square"));
            Console.WriteLine(figure.ContainsKey("Square"));
            Console.WriteLine(figure.ContainsKey("1"));
            Console.WriteLine(figure.ContainsKey("1."));
            Console.WriteLine(figure["triangle"]);

            string test = "sqUaRe";

            if (figure.ContainsKey(test.ToLower()))
            {
                Console.WriteLine(figure[test.ToLower()]);
            }
            

        }
        //static private void Medium10()
        //{
        //    int n = 14;
        //}
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

                Console.WriteLine($"Area of " +
                    $"{(figure.ContainsKey(choice.ToLower()) ? figure[choice.ToLower()] : "")}" +
                    $" S = {area}");
                //Console.WriteLine("Area of {0} == {1}",
                //    (choice == "1" || choice == "1." || )?:);
            }

        }
    }
}
