using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent2020
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] numbers = File.ReadAllLines("Inputs/Numbers.txt");

            const int amountOfPreviousNumbers = 25;

            int? result = FirstNumberNotSumOfPreviousNumbers(numbers, amountOfPreviousNumbers);

            Console.WriteLine(result);
        }

        private static int? FirstNumberNotSumOfPreviousNumbers(string[] numbers, in int amountOfPreviousNumbers)
        {
            int? firstNumberNotSumOfPreviousNumbers = default;

            for (int i = amountOfPreviousNumbers; i < numbers.Length; i++)
            {
                int nextNumber = int.Parse(numbers[i]);
                List<int> previousNumbers =
                    numbers.Take(i).TakeLast(amountOfPreviousNumbers).Select(int.Parse).ToList();
                if (!IsNumberSumOfPreviousNumbers(nextNumber, previousNumbers))
                {
                    firstNumberNotSumOfPreviousNumbers = nextNumber;
                    break;
                }
            }

            return firstNumberNotSumOfPreviousNumbers;
        }

        private static bool IsNumberSumOfPreviousNumbers(int nextNumber, List<int> previousNumbers)
        {
            foreach (int addend in previousNumbers)
            {
                int[] previousNumbersWithoutAddend = new int[previousNumbers.Count];
                previousNumbers.CopyTo(previousNumbersWithoutAddend);
                if (previousNumbersWithoutAddend.Any(n => n == nextNumber - addend))
                    return true;
            }

            return false;
        }
    }
}