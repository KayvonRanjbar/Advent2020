using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_10
{
    class Program
    {
        static void Main(string[] args)
        {
            // What is the total number of distinct ways you can arrange
            // the adapters to connect the charging outlet to your device?
            
            List<long> joltages = File.ReadAllLines("Joltage_ratings.txt").Select(long.Parse).ToList();

            long result = CountDistinctArrangements(joltages);

            Console.WriteLine(result);
        }

        private static long CountDistinctArrangements(List<long> joltages)
        {
            joltages.Add(0);
            List<long> orderedJoltages = joltages.OrderBy(r => r).ToList();

            List<List<long>> subsequences = GetSubsequences(orderedJoltages);

            long numberOfArrangements = GetNumberOfArrangements(subsequences);

            return numberOfArrangements;
        }

        private static long GetNumberOfArrangements(List<List<long>> subsequences)
        {
            long totalNumberOfArrangements = 1;

            foreach (List<long> subsequence in subsequences)
            {
                switch (subsequence.Count)
                {
                    case 1:
                        totalNumberOfArrangements *= 2;
                        break;
                    case 2:
                        totalNumberOfArrangements *= 4;
                        break;
                    case 3:
                        totalNumberOfArrangements *= 7;
                        break;
                    default:
                        throw new Exception("Invalid length of subsequence!!!");
                }
            }

            return totalNumberOfArrangements;
        }

        private static List<List<long>> GetSubsequences(List<long> orderedSequence)
        {
            List<long> differences = GetDifferences(orderedSequence);

            List<List<long>> subsequences = new List<List<long>>();

            List<long> subsequence = new List<long>();

            for (int i = 0; i < differences.Count - 1; i++)
            {
                if (differences[i] == 3 || differences[i+1] == 3)
                {
                    if (subsequence.Count > 0)
                    {
                        subsequences.Add(subsequence);
                        subsequence = new List<long>();
                    }
                }

                else
                {
                    subsequence.Add(orderedSequence[i + 1]);
                }
            }

            if (subsequence.Count > 0)
            {
                subsequences.Add(subsequence);
            }

            return subsequences;
        }

        private static List<long> GetDifferences(List<long> orderedSequence)
        {
            List<long> differences = new List<long>();
            
            for (int i = 0; i < orderedSequence.Count - 1; i++)
            {
                differences.Add(orderedSequence[i+1] - orderedSequence[i]);
            }

            return differences;
        }
    }
}