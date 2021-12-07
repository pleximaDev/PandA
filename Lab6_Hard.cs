using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using mt = Lab6_Hard.MyTools;

namespace Lab6_Hard
{
    class Program
    {
        private static void Main(string[] args)
        {
            MyTools.DisplayResults(
                (Hard1, nameof(Hard1)));
        }

        private static void Hard1()
        {
            const int
                numberOfParticipants = 24,
                maxResult = 100;

            List<Participant> participants = new List<Participant> { };

            for (int i = 0; i < numberOfParticipants; ++i)
                participants.Add(new Participant(mt.RandomSurname(), mt.randomIntCustomBound(0, maxResult)));
            
            Contest contest = new Contest(participants);
            contest.FormStage2Participants();
            contest.ListStage2Participants();
        }

        public struct Participant
        {
            public string surname;
            public int result;

            public Participant(string surname, int result)
            {
                this.surname = surname;
                this.result = result;
            }
        }

        public struct Contest
        {
            private const int
                num1Stage = 12,
                num2Stage = 6;

            private List<Participant>
                group1stage1,
                group2stage1,
                group1stage2,
                group2stage2;

            public void FormStage2Participants()
            {
                Sort(group1stage1);
                Sort(group2stage1);

                for (int i = 0; i < num2Stage; ++i)
                {
                    group1stage2.Add(group1stage1[i]);
                    group2stage2.Add(group2stage1[i]);
                }
            }

            public void Sort(List<Participant> participants)
            {
                participants.Sort((j1, j2) => (j1.result - j2.result));
                participants.Reverse();
            }

            public void ListStage2Participants()
            {
                Console.WriteLine($"List of Stage №2 participants:\n");
                Console.WriteLine($"{"Surname", 10} | First group winners of Stage №1 | {"Surname",10} | Second group winners of Stage №1");
                for (int i = 0; i < group1stage2.Count(); ++i)
                    Console.WriteLine($"{group1stage2[i].surname, 10} | {group1stage2[i].result, 31} | {group2stage2[i].surname, 10} | {group2stage2[i].result, 32}");
            }

            public Contest(
                List<Participant> participants)
            {
                group1stage1 = new List<Participant> { };
                group2stage1 = new List<Participant> { };
                group1stage2 = new List<Participant> { };
                group2stage2 = new List<Participant> { };
                int i = 0;
                foreach (var participant in participants)
                    if (i++ < num1Stage) group1stage1.Add(participant);
                    else group2stage1.Add(participant);
            }
        }
    }

    class MyTools
    {
        private const int
            borderLength = 95;

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

        public static Action<int> nl = (i) => { while (i-- > 0) Console.WriteLine(); };

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
