using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using mt = Lab6_Medium.MyTools;

namespace Lab6_Medium
{
    class Solution
    {
        private static void Main(string[] args)
        {
            mt.DisplayResults(
                (Medium2, nameof(Medium2)),
                (Medium3, nameof(Medium3)),
                (Medium4, nameof(Medium4))
                );
        }

        private static void Medium2()
        {
            const int
                numberOfStudents = 30,
                minRes           =  2,
                maxRes           =  5;

            List<Student> students = new List<Student> { };

            int i = numberOfStudents;
            while (i --> 0)
                students.Add(new Student(
                    mt.randomIntCustomBound(minRes, maxRes + 1),
                    mt.randomIntCustomBound(minRes, maxRes + 1),
                    mt.randomIntCustomBound(minRes, maxRes + 1)));

            Group group = new Group(students);

            mt.wrtLn($"List of all students:\n");
            group.ListGroup();
            mt.wrtLn($"\n\nList of students who passed the exams in descending order:\n\n");
            group.Expulsion();
            group.ListGroup();
        }

        private static void Medium3()
        {
            const int
                numberOfJumpers =  20,
                numberOfResults =   3,
                minRes          = 155,
                maxRes          = 230;

            List<Jumper> jumpers = new List<Jumper> { };

            int i = numberOfJumpers;
            while (i --> 0)
                jumpers.Add(new Jumper(
                    mt.RandomSurname(),
                    new int[numberOfResults]
                    {
                        mt.randomIntCustomBound(minRes, maxRes + 1),
                        mt.randomIntCustomBound(minRes, maxRes + 1),
                        mt.randomIntCustomBound(minRes, maxRes + 1)
                    })
                );
            Protocol protocol = new Protocol(jumpers);

            protocol.PrintProtocol();
        }

        private static void Medium4()
        {
            const int
                numberOfJumpers = 15,
                numberOfJudges = 7,
                numberOfJumps = 4,
                maxScore = 6;
            (int min, int max) factor = (2, 3);

            List<WaterJumper> waterJumpers = new List<WaterJumper> { };

            int i = numberOfJumpers;
            while (i --> 0)
            {
                
                double[] diffFactors = new double[numberOfJumps];
                for (int t = 0; t < numberOfJumps; ++t)
                    diffFactors[t] = mt.randomIntCustomBound(factor.min, factor.max + 1) + (double)mt.randomIntCustomBound(5, 9 + 1) / 10;

                List<List<int>> allJudgesScores = new List<List<int>>
                {
                    new List<int>
                    {
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1)
                    },
                    new List<int>
                    {
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1)
                    },
                    new List<int>
                    {
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1)
                    },
                    new List<int>
                    {
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1),
                        mt.randomIntCustomBound(0, maxScore + 1)
                    }
                };

                waterJumpers.Add(new WaterJumper(
                    mt.RandomSurname(),
                    diffFactors,
                    allJudgesScores
                    )
                );
            }

            Contest contest = new Contest(waterJumpers);

            contest.PrintContest();
        }

        public struct Group
        {
            private List<Student> students;

            private void DescendingSortStudents(List<Student> students)
            {
                students.Sort((s1, s2) => s1.getMath() + s1.getPhysics() + s1.getRussian() - s2.getMath() - s2.getPhysics() - s2.getRussian());
                students.Reverse();
            }

            public void Expulsion()
            {
                students.RemoveAll((x) => x.getMath() <= 2 || x.getPhysics() <= 2 || x.getRussian() <= 2);
            }

            public void ListGroup()
            {
                int i = 1;
                foreach (Student student in this.students)
                    Console.WriteLine($"student №{i++, -2} " +
                        $"math: {student.getMath()}, physics: {student.getPhysics()}, russian: {student.getRussian()}");
            }

            public Group(List<Student> students)
            {
                this.students = students;
                DescendingSortStudents(this.students);
            }
        }

        public struct Student
        {
            private int
                math,
                physics,
                russian;

            public Func<int>
                getMath,
                getPhysics,
                getRussian;

            public static bool operator ==(Student s1, Student s2)
            { return ((s1.math + s1.physics + s1.russian) == (s2.math + s2.physics + s2.russian)) ? true : false; }

            public static bool operator !=(Student s1, Student s2)
            { return ((s1.math + s1.physics + s1.russian) != (s2.math + s2.physics + s2.russian)) ? true : false; }

            public static bool operator >(Student s1, Student s2)
            { return ((s1.math + s1.physics + s1.russian) > (s2.math + s2.physics + s2.russian)) ? true : false; }

            public static bool operator <(Student s1, Student s2)
            { return ((s1.math + s1.physics + s1.russian) < (s2.math + s2.physics + s2.russian)) ? true : false; }

            public static bool operator >=(Student s1, Student s2)
            { return ((s1.math + s1.physics + s1.russian) >= (s2.math + s2.physics + s2.russian)) ? true : false; }

            public static bool operator <=(Student s1, Student s2)
            { return ((s1.math + s1.physics + s1.russian) <= (s2.math + s2.physics + s2.russian)) ? true : false; }

            public Student(
                int math,
                int physics,
                int russian)
            {
                this.math = math;
                this.physics = physics;
                this.russian = russian;

                getMath    = () => { return math;    };
                getPhysics = () => { return physics; };
                getRussian = () => { return russian; };
            }

            public override bool Equals(object obj)
            {
                return Equals((Student)obj);
            }

            public bool Equals(Student other)
            {
                return
                    (this.math + this.physics + this.russian)
                    ==
                    (other.math + other.physics + other.russian);
            }

            public override int GetHashCode()
            {
                throw new NotImplementedException();
            }
        }

        public struct Jumper
        {
            private string surname;
            private int[] results;
            private int bestResult;

            public string getSurname()    { return this.surname;    }
            public int[]  getResults()    { return this.results;    }
            public int    getBestResult() { return this.bestResult; }

            public Jumper(
                string surname,
                int[] results)
            {
                this.surname    = surname;
                this.results    = results;
                this.bestResult = results.Max();
            }
        }

        public struct Protocol
        {
            private List<Jumper> jumpers;

            private void SortProtocol()
            {
                jumpers.Sort((j1, j2) => j1.getBestResult() - j2.getBestResult());
                jumpers.Reverse();
            }

            public void PrintProtocol()
            {
                Console.WriteLine($"{"Surname", 10} | Result №1 | Result №2 | Result №3 | Best result");
                foreach (Jumper jumper in this.jumpers)
                    Console.WriteLine($"{jumper.getSurname(), 10} |" +
                        new string(' ', 7) +
                        $"{string.Join(" |" + new string(' ', 7), jumper.getResults())}" +
                        $" | {jumper.getBestResult(), 11}");
            }

            public Protocol(List<Jumper> jumpers)
            {
                this.jumpers = jumpers;

                SortProtocol();
            }
        }

        public struct WaterJumper
        {
            private string surname;
            private double[] difficultyFactors;
            private List<List<int>> judgesScores;
            private double finalScore;

            private double CalculationOfScore(List<List<int>> scores, double[] factors)
            {
                double result = 0;
                int i = 0;
                foreach (List<int> jumpScores in scores)
                {
                    result +=
                        (jumpScores.Sum() - jumpScores.Max() - jumpScores.Min()) *
                        factors[i++];
                }
                return result;
            }

            public string getSurname() { return this.surname; }

            public double getFinalScore() { return this.finalScore; }

            public WaterJumper(
                string surname,
                double[] difficultyFactors,
                List<List<int>> judgesScores)
            {
                this.surname = surname;
                this.difficultyFactors = difficultyFactors;
                this.judgesScores = judgesScores;

                finalScore = 0;

                int i = 0;
                foreach (List<int> jumpScores in judgesScores)
                {
                    finalScore +=
                        (jumpScores.Sum() - jumpScores.Max() - jumpScores.Min()) *
                        difficultyFactors[i++];
                }
            }
        }

        public struct Contest
        {
            private List<WaterJumper> waterJumpers;

            private void SortContest()
            {
                waterJumpers.Sort((j1, j2) => (int) (j1.getFinalScore() - j2.getFinalScore()) );
                waterJumpers.Reverse();
            }

            public void PrintContest()
            {
                int i = 1;
                Console.WriteLine($" Place | {"Surname",10} | Final Score");
                foreach (WaterJumper waterJumper in this.waterJumpers)
                    Console.WriteLine($" {$"№{i++}", 5} | {waterJumper.getSurname(),10} | " +
                        $"{waterJumper.getFinalScore(), 0:F1}");
            }

            public Contest(List<WaterJumper> waterJumpers)
            {
                this.waterJumpers = waterJumpers;

                SortContest();
            }
        }
    }

    class MyTools
    {
        private const int
            borderLength = 70;

        private static Random rnd = new Random();

        private static List<string> surnameList =
        new List<string>
        {
            "Corbyn",
            "Smith",
            "Jones",
            "Williams",
            "Brown",
            "Taylor",
            "Davies",
            "Wilson",
            "Evans",
            "Thomas",
            "Johnson",
            "Roberts",
            "Walker",
            "Wright",
            "Thompson",
            "Robinson",
            "White",
            "Hughes",
            "Edwards",
            "Hall",
            "Green",
            "Martin",
            "Wood",
            "Lewis",
            "Harris",
            "Clarke",
            "Jackson",
            "Clark",
            "Turner",
            "Scott",
            "Hill",
            "Moore",
            "Cooper",
            "Ward",
            "Morris",
            "King",
            "Watson",
            "Harrison",
            "Morgan",
            "Baker",
            "Patel",
            "Allen",
            "Anderson",
            "Mitchell",
            "Phillips",
            "James",
            "Campbell",
            "Bell",
            "Lee",
            "Hawking",
            "Patt",
            "Aho",
            "Ullman",
            "Martin",
            "Curry",
            "Knuth",
            "Morris",
            "Pratt",
            "Landau",
            "Trémaux",
            "Landau",
            "Courcelle",
            "Wadler",
            "Blott",
            "Legendre",
            "Kronrod",
            "Fermi",
            "Hooke",
            "Jeeves",
            "Nelder",
            "Mead",
            "Dahlquist",
            "Runge",
            "Kutta",
            "Nyquist",
            "Shannon",
            "Fourier",
            "Karatsuba",
            "Ritchie",
            "Thompson",
            "Babbage",
            "Lovelace",
            "Neumann",
            "Gödel",
            "Denning",
            "Dijkstra",
            "Boole",
            "Leibniz",
            "Turing",
            "Drake",
            "Church",
            "Kleene",
            "Rosser",
            "Howard",
            "Peirce",
            "Bernoulli",
            "Riemann"
        };

        public static string RandomSurname()
        {
            return surnameList[rnd.Next(0, surnameList.Count)];
        }

        public static Func<int, int, int> randomIntCustomBound =
            (leftBound, rightBound) => { return rnd.Next(leftBound, rightBound); };

        public static Action<string> wrtLn = (str) => { Console.WriteLine(str); };

        public static Action<int> nl = (i) => { while(i --> 0) Console.WriteLine(); };

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
    }
}
