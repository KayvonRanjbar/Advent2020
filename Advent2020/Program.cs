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
            string[] boardingPasses = File.ReadAllLines("Boarding_Passes.txt");
            List<int> seatIdsFromBoardingPasses = GetSeatIdsFromBoardingPasses(boardingPasses);
            int seatId = GetMySeatId(seatIdsFromBoardingPasses);
            Console.WriteLine(seatId);
        }

        private static List<int> GetSeatIdsFromBoardingPasses(string[] boardingPasses)
        {
            List<int> seatIds = new List<int>();
            foreach (var boardingPass in boardingPasses)
            {
                seatIds.Add(GetSeatId(boardingPass));
            }

            return seatIds;
        }

        private static int GetMySeatId(List<int> otherSeatIds)
        {
            int maxSeatId = 127 * 8 + 7;
            int mySeatId = 0;

            while (mySeatId <= maxSeatId)
            {
                if (!otherSeatIds.Contains(mySeatId) && otherSeatIds.Contains(mySeatId + 1) && otherSeatIds.Contains(mySeatId - 1))
                {
                    return mySeatId;
                }
                mySeatId++;
            }

            return mySeatId;
        }

        private static void PrintHighestSeatId()
        {
            string[] boardingPasses = File.ReadAllLines("Boarding_Passes.txt");
            int highestSeatId = int.MinValue;

            foreach (var boardingPass in boardingPasses)
            {
                int seatId = GetSeatId(boardingPass);
                if (seatId > highestSeatId)
                {
                    highestSeatId = seatId;
                }
            }

            Console.WriteLine(highestSeatId);
        }

        private static int GetSeatId(string boardingPass)
        {
            Tuple<int, int> rowMinMax = new Tuple<int, int>(0, 127);
            
            rowMinMax = ProcessByLetter(rowMinMax, boardingPass[0]);
            rowMinMax = ProcessByLetter(rowMinMax, boardingPass[1]);
            rowMinMax = ProcessByLetter(rowMinMax, boardingPass[2]);
            rowMinMax = ProcessByLetter(rowMinMax, boardingPass[3]);
            rowMinMax = ProcessByLetter(rowMinMax, boardingPass[4]);
            rowMinMax = ProcessByLetter(rowMinMax, boardingPass[5]);
            rowMinMax = ProcessByLetter(rowMinMax, boardingPass[6]);

            int row = rowMinMax.Item1;

            Tuple<int, int> colMinMax = new Tuple<int, int>(0, 7);

            colMinMax = ProcessByLetter(colMinMax, boardingPass[7]);
            colMinMax = ProcessByLetter(colMinMax, boardingPass[8]);
            colMinMax = ProcessByLetter(colMinMax, boardingPass[9]);

            int col = colMinMax.Item1;

            int seatId = row * 8 + col;

            return seatId;
        }

        private static Tuple<int, int> ProcessByLetter(Tuple<int, int> minMax, char letter)
        {
            var (min, max) = minMax;
            int range = max - min;

            return letter is ('F' or 'L') ? new Tuple<int, int>(min, min + range / 2) : new Tuple<int, int>(min + range / 2 + 1, max);
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
