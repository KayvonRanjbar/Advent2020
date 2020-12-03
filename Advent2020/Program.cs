using System;
using System.IO;
using System.Linq;

namespace Advent2020
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] map = File.ReadAllLines("Map.txt");

            long trees1 = AmountOfTreeEncounters(map, 1, 1);
            long trees2 = AmountOfTreeEncounters(map, 3, 1);
            long trees3 = AmountOfTreeEncounters(map, 5, 1);
            long trees4 = AmountOfTreeEncounters(map, 7, 1);
            long trees5 = AmountOfTreeEncounters(map, 1, 2);

            long treesMultiplied = trees1 * trees2 * trees3 * trees4 * trees5;

            Console.WriteLine(treesMultiplied);
        }

        private static long AmountOfTreeEncounters(string[] map, int stepsRight, int stepsDown)
        {
            long amountOfTreeEncounters = 0;

            int x = 0;
            int y = 0;

            while (y < map.Count() - 1)
            {
                y += stepsDown;
                x = (x + stepsRight) % map[0].Count();
                if (map[y][x] == '#')
                {
                    amountOfTreeEncounters++;
                }
            }

            return amountOfTreeEncounters;
        }

        private static void PrintNumberOfValidPasswords(string[] passwordsWithCorporatePolicies)
        {
            int numberOfValidPasswords = 0;
            foreach (string passwordWithCorporatePolicy in passwordsWithCorporatePolicies)
            {
                bool isValidPassword = IsValidPasswordNewCriteria(passwordWithCorporatePolicy);
                if (isValidPassword)
                    numberOfValidPasswords++;
            }

            Console.WriteLine(numberOfValidPasswords);
        }

        private static bool IsValidPasswordNewCriteria(string passwordWithCorporatePolicy)
        {
            string corporatePolicy = passwordWithCorporatePolicy.Split(": ").First();

            int index1 = int.Parse(corporatePolicy.Split(' ')[0].Split('-')[0]) - 1;
            int index2 = int.Parse(corporatePolicy.Split(' ')[0].Split('-')[1]) - 1;

            char character = corporatePolicy.Split(' ')[1].First();

            string password = passwordWithCorporatePolicy.Split(": ").Last();

            if (password[index1] != character && password[index2] != character)
            {
                return false;
            }

            if (password[index1] == character && password[index2] == character)
            {
                return false;
            }

            return true;
        }

        private static bool IsValidPasswordOldCriteria(string passwordWithCorporatePolicy)
        {
            string corporatePolicy = passwordWithCorporatePolicy.Split(": ").First();

            string[] allowedInstances = corporatePolicy.Split(' ').First().Split('-');

            int minInstances = int.Parse(allowedInstances.First());
            int maxInstances = int.Parse(allowedInstances.Last());

            char character = corporatePolicy.Split(' ').Last().First();

            string password = passwordWithCorporatePolicy.Split(": ").Last();

            int instancesOfCharacterInPassword = password.Count(c => c == character);

            if (instancesOfCharacterInPassword >= minInstances && instancesOfCharacterInPassword <= maxInstances)
            {
                return true;
            }

            return false;
        }

        private static void FindMultipleOfTwo(string[] entries)
        {
            for (int i = 0; i < entries.Length - 1; i++)
            {
                var add1 = int.Parse(entries[i]);

                for (int j = i + 1; j < entries.Length; j++)
                {
                    var add2 = int.Parse(entries[j]);
                    if (add1 + add2 == 2020)
                    {
                        Console.WriteLine(add1 * add2);
                    }
                }
            }
        }

        private static void FindMultipleOfThree(string[] entries)
        {
            for (int i = 0; i < entries.Length - 2; i++)
            {
                var add1 = int.Parse(entries[i]);

                for (int j = i + 1; j < entries.Length - 1; j++)
                {
                    var add2 = int.Parse(entries[j]);
                    for (int k = j + 1; k < entries.Length; k++)
                    {
                        var add3 = int.Parse(entries[k]);
                        if (add1 + add2 + add3 == 2020)
                        {
                            Console.WriteLine(add1 * add2 * add3);
                        }
                    }
                }
            }
        }
    }
}
