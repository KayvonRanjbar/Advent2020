using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent2020
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHighestSeatId();
        }

        private static void PrintHighestSeatId()
        {
            string[] boardingPasses = File.ReadAllLines("Boarding_Passes.txt");

            foreach (var boardingPass in boardingPasses)
            {
                int seatId = GetSeatId(boardingPass);
            }
        }

        private static int GetSeatId(string boardingPass)
        {
            Tuple<int, int> rowRange = new Tuple<int, int>(0, 127);
            

            rowRange = ProcessByLetter(rowRange, boardingPass[0]);

            return 0;
        }

        private static Tuple<int, int> ProcessByLetter(Tuple<int, int> rowRange, char letter)
        {
            if (letter == 'F')
            {
            }

            return new Tuple<int, int>(0, 0);
        }

        private static void PrintNumberOfValidPassports()
        {
            string passportsString = File.ReadAllText("Passports.txt");
            string[] passports = passportsString.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            List<string> cleanPassports = new List<string>();
            foreach (string passport in passports)
            {
                var cleanPassport = passport.Replace('\n', ' ');
                cleanPassports.Add(cleanPassport);
            }

            List<Dictionary<string, string>> passportDictionaryList = new List<Dictionary<string, string>>();

            foreach (var passport in cleanPassports)
            {
                Dictionary<string, string> passportDictionary = new Dictionary<string, string>();
                string[] keyValuePairs = passport.Split(' ');
                foreach (var keyValuePairString in keyValuePairs)
                {
                    string[] keyValuePair = keyValuePairString.Split(':');
                    passportDictionary.Add(keyValuePair[0], keyValuePair[1]);
                }
                passportDictionaryList.Add(passportDictionary);
            }

            int numberOfValidPassports = 0;

            foreach (Dictionary<string, string> passport in passportDictionaryList)
            {
                if (IsValidPassport(passport))
                {
                    numberOfValidPassports++;
                }
            }

            Console.WriteLine(numberOfValidPassports);
        }

        private static bool IsValidPassport(Dictionary<string, string> passport)
        {
            string[] validKeys = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            foreach (var validKey in validKeys)
            {
                if (!passport.ContainsKey(validKey))
                {
                    return false;
                }
            }

            int birthYear = int.Parse(passport["byr"]);
            if (birthYear is not (>= 1920 and <= 2002))
            {
                return false;
            }

            int issueYear = int.Parse(passport["iyr"]);
            if (issueYear is not (>= 2010 and <= 2020))
            {
                return false;
            }

            int expirationYear = int.Parse(passport["eyr"]);
            if (expirationYear is not (>= 2020 and <= 2030))
            {
                return false;
            }

            string height = passport["hgt"];
            if (height[(height.Length - 2)..] is not ("cm" or "in"))
            {
                return false;
            }
            int heightValue = height[(height.Length - 2)..] is "cm" ? int.Parse(string.Concat(height.TakeWhile(d => !d.Equals('c')))) :
                int.Parse(string.Concat(height.TakeWhile(d => !d.Equals('i'))));
            if (height[(height.Length - 2)..] is "cm" && heightValue is not (>= 150 and <= 193))
            {
                return false;
            }
            if (height[(height.Length - 2)..] is "in" && heightValue is not (>= 59 and <= 76))
            {
                return false;
            }

            string hairColor = passport["hcl"];
            Regex rgx = new Regex(@"^#{1}[0-9a-f]{6}$");
            if (!rgx.IsMatch(hairColor))
            {
                return false;
            }

            string eyeColor = passport["ecl"];
            if (eyeColor is not ("amb" or "blu" or "brn" or "gry" or "grn" or "hzl" or "oth"))
            {
                return false;
            }

            string passportId = passport["pid"];
            Regex rgx2 = new Regex(@"^[0-9]{9}$");
            if (!rgx2.IsMatch(passportId))
            {
                return false;
            }

            return true;
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
