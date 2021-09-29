using System;

namespace Lab2_Medium
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<string> lis = new List<string>
            {
                "1", "1.", "square"
            };
            
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

            while (n-- > 0)
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
                    "1. Area of square with side r.\n" +
                    "2. Area of circle with radius r.\n" +
                    "3. Area of equilateral triangle (sides are equal) with side r.\n\n" +
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
                //Console.WriteLine("Area of {0} == {1}",
                //    (choice == "1" || choice == "1." || )?:);
            }

        }
    }
}
