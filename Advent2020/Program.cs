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
            List<long> numbers = File.ReadAllLines("Inputs/Numbers.txt").Select(long.Parse).ToList();

            const long invalidNumber = 25918798;

            long encryptionWeakness = FindEncryptionWeakness(numbers, invalidNumber);

            Console.WriteLine(encryptionWeakness);
        }

        private static long FindEncryptionWeakness(List<long> numbers, in long invalidNumber)
        {
            List<long> contiguousRange = FindContiguousRangeThatSumsToInvalidNumber(numbers, invalidNumber);

            long encryptionWeakness = contiguousRange.Min() + contiguousRange.Max();

            return encryptionWeakness;
        }

        private static List<long> FindContiguousRangeThatSumsToInvalidNumber(List<long> numbers, in long invalidNumber)
        {
            List<long> contiguousRange = new List<long>();

            for (int i = 0; i < numbers.Count; i++)
            {
                contiguousRange = new List<long>();
                contiguousRange.Add(numbers[i]);
                long sum = contiguousRange.Sum();
                
                for (int j = i + 1; sum < invalidNumber; j++)
                {
                    contiguousRange.Add(numbers[j]);
                    sum = contiguousRange.Sum();
                }

                if (sum == invalidNumber)
                {
                    return contiguousRange;
                }
            }
            
            throw new Exception("The contiguous range was not found");
        }
    }
}