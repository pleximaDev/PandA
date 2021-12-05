using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using mt = Lab6_Easy.MyTools;

namespace Lab6_Easy
{
    class Solution
    {
        private static Random rnd = new Random();

        private const int
            lbound    = -10,
            rbound    =  10,
            minResult =  80;

        private static Func<int, int, int>randomIntCustomBound =
            (leftBound, rightBound) => { return rnd.Next(leftBound, rightBound); };

        private Func<double> randomIntBound =
            () => { return rnd.Next(lbound, rbound); };

        private static T RandomEnumValue<T>()
        { var v = Enum.GetValues(typeof(T)); return (T) v.GetValue(rnd.Next(v.Length)); }

        static void Main(string[] args)
        {
            MyTools.DisplayResults(
                (Easy5, nameof(Easy5)),
                (Easy1, nameof(Easy1)),
                (Easy2, nameof(Easy2))
                );
        }

        private struct Student
        {
            public int
                grade,
                numberMissedClasses;

            public Student(int grade, int numberMissedClasses)
            {
                this.grade = grade;
                this.numberMissedClasses = numberMissedClasses;
            }
        }

        private enum Grades : int
        {
            notCertified,
            OkOne,
            OkTwo,
            OkThree,
            Good,
            Great
        }

        private static void Easy5()
        {
            const int groupSize = 25;
            Student[] students = new Student[groupSize];

            Console.WriteLine($"List of all students:\n");

            for (int i = 0; i < students.Length; ++i)
                students[i] = new Student(
                    (int)RandomEnumValue<Grades>(),
                    randomIntCustomBound(0, 80));
            
            ListGroup(students, "all");

            Console.WriteLine("\nList of unsucive students:\n");

            ListGroup(students, "unsucive");
        }

        private static void ListGroup(Student[] students, string option = "all")
        {
            switch (option.ToLower())
            {
                case "all":
                    for (int i = 0; i < students.Length; ++i)
                    {
                        /*students[i].grade = (int)(Grades)(randomIntCustomBound(0, 5));*/
                        Console.WriteLine(
                            $"№{i+1,-2} student's grade = {students[i].grade} and " +
                            $"number of missed classes = {students[i].numberMissedClasses,2}");
                    }
                    break;
                case "unsucive":
                    for (int i = 0; i < students.Length; ++i)
                    {
                        /*students[i].grade = (int)(Grades)(randomIntCustomBound(0, 5));*/
                        if(students[i].grade <= 2)
                            Console.WriteLine(
                                $"№{i+1,-2} student's grade = {students[i].grade} and " +
                                $"number of missed classes = {students[i].numberMissedClasses,2}");
                    }
                    break;
            }
        }

        private static void Easy1()
        {
            const int
                numOfParticipants = 20,
                maxScore = 10;

            Action<Participant[]> sortByResults = (participants) =>
            {
                for (int j = 0; j < participants.GetLength(0) - 1; ++j)
                    for (int i = 0; i < participants.GetLength(0) - 1; ++i)
                        if
                        (
                        (participants[i].firstAttempt + participants[i].secondAttempt)
                        <
                        (participants[i + 1].firstAttempt + participants[i + 1].secondAttempt))
                        {
                            Participant temp = participants[i];
                            participants[i] = participants[i + 1];
                            participants[i + 1] = temp;
                        }
            };

            Action<Participant[]> displayByTable = (participants) =>
            {
                mt.wrtLn($"\tName\t|\tSociety\t|\tFirst attempt\t|\tSecond attempt\t");
                foreach (var participant in participants)
                {
                    mt.wrtLn($"\t{participant.name}\t|" +
                        $"\t{participant.society}\t|" +
                        $"\t{participant.firstAttempt}\t|" +
                        $"\t{participant.secondAttempt}\t");
                }
                
            };

            Participant[] participants = new Participant[numOfParticipants];

            for (int i = 0; i < numOfParticipants; ++i)
            {
                participants[i] = new Participant
                    (MyTools.RandomName(),
                    MyTools.RandomSociety(),
                    randomIntCustomBound(0, maxScore + 1),
                    randomIntCustomBound(0, maxScore + 1));
            }

            Protocol protocol = new Protocol(participants,

                (participants) =>
                {
                    for (int j = 0; j < participants.GetLength(0) - 1; ++j)
                        for (int i = 0; i < participants.GetLength(0) - 1; ++i)
                            if (
                            (participants[i].firstAttempt + participants[i].secondAttempt)
                            <
                            (participants[i + 1].firstAttempt + participants[i + 1].secondAttempt))
                            {
                                Participant temp = participants[i];
                                participants[i] = participants[i + 1];
                                participants[i + 1] = temp;
                            }
                },

                (participants) =>
                {
                    mt.wrtLn("+" + new string('-', 62 + 5 * 5) + "+");
                    mt.wrtLn(
                        $"|" +
                        $"{"Name",           11}\t|" +
                        $"{"Society",        18}\t|" +
                        $"{"First attempt",  15}\t|" +
                        $"{"Second attempt", 16}\t|"
                        );

                    foreach (var participant in participants)
                    {
                        mt.wrtLn(
                            $"|" +
                            $"{new string('-', 11+4)}+" +
                            $"{new string('-', 18+5)}+" +
                            $"{new string('-', 15+8)}+" +
                            $"{new string('-', 16+7)}"  +
                            $"|");
                        mt.wrtLn(
                            $"|" +
                            $"{participant.name,          11}\t|" +
                            $"{participant.society,       18}\t|" +
                            $"{participant.firstAttempt,  15}\t|" +
                            $"{participant.secondAttempt, 16}\t|"
                            );
                    }

                    mt.wrtLn("+" + new string('-', 62 + 5 * 5) + "+");
                }
                );

            mt.wrtLn($"Unsorted table of long jump competition's results with {numOfParticipants} participants:\n");
            protocol.displayByTable(protocol.participants);
            protocol.sortByResults(protocol.participants);
            mt.wrtLn("\nTable after sorting:\n");
            protocol.displayByTable(protocol.participants);
            mt.nl();
        }

        private static void Easy2()
        {
            /*35-80*/
            const int numberOfRunners = 23;
            int successful = 0;

            Runner[] runners = new Runner[numberOfRunners];

            for (int i = 0; i < numberOfRunners; ++i)
                runners[i] = new Runner(
                    mt.RandomSurname(),
                    mt.RandomSociety(),
                    mt.RandomSurname(),
                    randomIntCustomBound(35, 185));

            foreach (var runner in runners)
                if (runner.GetPass()) ++successful;

            SortRunners(runners);

            mt.wrtLn($"Sorted Table of {numberOfRunners} runners:\n");

            PrintRunnersTable(runners);

            mt.wrtLn($"\nNumber of runners who passed: {successful}");
        }

        private static void SortRunners(Runner[] runners)
        {
            for (int j = 0; j < runners.GetLength(0) - 1; ++j)
                for (int i = 0; i < runners.GetLength(0) - 1; ++i)
                    if (runners[i].GetResult() > runners[i + 1].GetResult())
                    {
                        Runner temp = runners[i];
                        runners[i] = runners[i + 1];
                        runners[i + 1] = temp;
                    }
        }

        private static void PrintRunnersTable(Runner[] runners)
        {
            mt.wrtLn("+" + new string('-', 62 + 5 * 5) + "+");
            mt.wrtLn(
                $"|" +
                $"{"Surname",11}\t|" +
                $"{"Group",18}\t|" +
                $"{"Result (sec)",15}\t|" +
                $"{"Passed",16}\t|"
                );

            foreach (var runner in runners)
            {
                mt.wrtLn(
                    $"|" +
                    $"{new string('-', 11 + 4)}+" +
                    $"{new string('-', 18 + 5)}+" +
                    $"{new string('-', 15 + 8)}+" +
                    $"{new string('-', 16 + 7)}" +
                    $"|");
                mt.wrtLn(
                    $"|" +
                    $"{runner.GetSurname(),11}\t|" +
                    $"{runner.GetGroup(),18}\t|" +
                    $"{runner.GetResult(),15}\t|" +
                    $"{runner.GetPass(),16}\t|"
                    );
            }

            mt.wrtLn("+" + new string('-', 62 + 5 * 5) + "+");
        }

        public struct Participant
        {
            public string name;
            public string society;
            public double firstAttempt;
            public double secondAttempt;

            public Participant(
                string name, string society,
                double firstAttempt, double secondAttempt)
            {
                this.name = name;
                this.society = society;
                this.firstAttempt = firstAttempt;
                this.secondAttempt = secondAttempt;
            }
        }

        public struct Protocol
        {
            public Participant[] participants;
            public Action<Participant[]>
                sortByResults,
                displayByTable;

            public Protocol
                (Participant[] participants,
                Action<Participant[]> sortByResults,
                Action<Participant[]> displayByTable)
            {
                this.participants = participants;
                this.sortByResults = sortByResults;
                this.displayByTable = displayByTable;
            }
        }

        public struct Runner
        {
            private string surname;
            private string group;
            private string teacherSurname;
            private int    result;
            private bool   passed;

            public Runner(
                string surname,
                string group,
                string teacherSurname,
                int    result)
            {
                this.surname = surname;
                this.group = group;
                this.teacherSurname = teacherSurname;
                this.result = result;

                if (result <= minResult) passed = true;
                else passed = false;
            }

            public string GetSurname()        { return surname;        }
            public string GetGroup()          { return group;          }
            public string GetTeacherSurname() { return teacherSurname; }
            public int    GetResult()         { return result;         }
            public bool   GetPass()           { return passed;         }
        }
    }

    class MyTools
    {
        private const int
            borderLength = 90;

        private static Random rnd = new Random();

        private static List<string> nameList =
        new List<string>
        {
            "Bob",
            "Alice",
            "Richard",
            "Tom",
            "Jack",
            "Stephen",
            "Donald",
            "Dennis",
            "Kernighan",
            "Linus",
            "Adrian",
            "Alex",
            "Andrew",
            "Austin",
            "Ben",
            "Bernard",
            "Brian",
            "Charley",
            "Dave",
            "Daniel",
            "Elliot",
            "Eric",
            "Harry",
            "Isaac",
            "James",
            "Jeff",
            "John",
            "Josh",
            "Kevin",
            "Larry",
            "Leonard",
            "Matt",
            "Mike",
            "Nick",
            "Oscar",
            "Patrick",
            "Rick",
            "Ronald",
            "Sean",
            "Tim",
            "Wolfgang",
            "Wilhelm",
            "Alonzo"
        };

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

        private static List<string> societyList =
        new List<string>
        {
            "RunForward",
            "NoStepBack",
            "Luckers",
            "NoTimeToLoose",
            "DivideAndConquer",
            "S.O.L.I.D",
            "FOSS",
            "TeXLancers",
            "UnixLikers",
            "VimMagicians",
            "EmacsGurus",
            "FuncProgLovers",
            "KernelPanics",
            "GetterSetter",
            "ACM",
            "LRU",
            "MRU",
            "OSI",
            "DFA",
            "NFA",
            "GNFA",
            "QFA",
            "FSM",
            "VFSM",
            "IEEE",
            "FPCA",
            "CompletelyTuring",
            "SegFault",
            "ICFP"
        };

        public static string RandomName()
        {
            return nameList[rnd.Next(0, nameList.Count)];
        }

        public static string RandomSurname()
        {
            return surnameList[rnd.Next(0, surnameList.Count)];
        }

        public static string RandomSociety()
        {
            return societyList[rnd.Next(0, societyList.Count)];
        }

        public static Action<string> wrtLn = (str) => { Console.WriteLine(str); };

        public static Action nl = () => { Console.WriteLine(); };

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

        public static int namesMaxLen = nameList
            .OrderByDescending(s => s.Length).First().Length;

        public static int societyMaxLen = societyList
            .OrderByDescending(s => s.Length).First().Length;
    }
}
