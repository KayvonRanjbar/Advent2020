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
            string[] testRules = File.ReadAllLines("Rules.txt");

            Dictionary<string, List<Tuple<int, string>>> ruleDictionary = GetRuleDictionary(testRules);

            int countRequiredInsideBags = CountRequiredInsideBags("shiny gold", ruleDictionary);

            Console.WriteLine(countRequiredInsideBags);
        }

        private static int CountRequiredInsideBags(string color, Dictionary<string, List<Tuple<int, string>>> ruleDictionary)
        {
            Tuple<int, string> countColor = new Tuple<int, string>(2, color);

            int bagsCount = CountInnerBags(countColor, ruleDictionary);
            
            return bagsCount;
        }

        private static int CountInnerBags(Tuple<int, string> countColor, Dictionary<string, List<Tuple<int, string>>> ruleDictionary)
        {
            int count = 0;

            List<Tuple<int, string>> innerCountColors = ruleDictionary[countColor.Item2];

            if (innerCountColors.Count == 0)
            {
                return count;
            }
            
            foreach (Tuple<int, string> innerCountColor in innerCountColors)
            {
                count += (CountInnerBags(innerCountColor, ruleDictionary) * innerCountColor.Item1) + innerCountColor.Item1;
            }
            
            return count;
        }

        private static int CountOuterBagsContainingColorBag(Dictionary<string, List<string>> ruleDictionary, string color)
        {
            int count = 0;

            foreach (KeyValuePair<string,List<string>> rule in ruleDictionary)
            {
                bool canContainColor = CanContainColor(rule.Key, ruleDictionary, color);
                if (canContainColor)
                {
                    count++;
                }
            }

            return count;
        }

        private static bool CanContainColor(string outerColor, Dictionary<string,List<string>> ruleDictionary, string goalInnerColor)
        {
            List<string> innerColors = ruleDictionary[outerColor];
            
            if (innerColors.Count == 0)
            {
                return false;
            }
                
            if (innerColors.Contains(goalInnerColor))
            {
                return true;
            }

            foreach (string innerColor in innerColors)
            {
                bool containsColor = CanContainColor(innerColor, ruleDictionary, goalInnerColor);
                if (containsColor)
                {
                    return true;
                }
            }
            //If no inner colors are the goal color, it can't contain the color
            return false;
        }

        private static Dictionary<string, List<Tuple<int, string>>> GetRuleDictionary(string[] rules)
        {
            Dictionary<string, List<Tuple<int, string>>> ruleDictionary = new Dictionary<string, List<Tuple<int, string>>>();
            
            foreach (string rule in rules)
            {
                string outerBagKey = GetOuterBagType(rule);
                List<Tuple<int, string>> innerBagValue = GetInnerBagTypes(rule);
                
                ruleDictionary.Add(outerBagKey, innerBagValue);
            }

            return ruleDictionary;
        }

        private static List<Tuple<int, string>> GetInnerBagTypes(string rule)
        {
            List<Tuple<int, string>> innerBagTypes = new List<Tuple<int, string>>();

            string[] splitIntoWords = rule.Split(' ');

            // If the 5th word in the rule is "no", that means there are no inner bags
            if (splitIntoWords[4] == "no")
            {
                return innerBagTypes;
            }

            int startingIndex = 4;
            while (startingIndex < splitIntoWords.Length)
            {
                Tuple<int, string> countColor = new Tuple<int, string>(int.Parse(splitIntoWords[startingIndex]), $"{splitIntoWords[startingIndex + 1]} {splitIntoWords[startingIndex + 2]}");
                innerBagTypes.Add(countColor);
                startingIndex += 4;
            }

            return innerBagTypes;
        }

        private static string GetOuterBagType(string rule)
        {
            string[] splitIntoWords = rule.Split(' ');
            string bagType = $"{splitIntoWords[0]} {splitIntoWords[1]}";

            return bagType;
        }
    }
}