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
            // What is the number of 1-jolt differences multiplied by the number of 3-jolt differences?
            
            IEnumerable<long> adapterJoltageRatings =
                File.ReadAllLines("Inputs/Joltage_ratings.txt").Select(long.Parse);

            List<long> sortedAdapterJoltageRatings = adapterJoltageRatings.OrderBy(r => r).ToList();
            
            CountJoltageDifferences(sortedAdapterJoltageRatings, out long OneJoltDifferences, out long ThreeJoltDifferences);
            
            long productOfOneJoltDifferencesAndThreeJoltDifferences = OneJoltDifferences * ThreeJoltDifferences;

            Console.WriteLine(productOfOneJoltDifferencesAndThreeJoltDifferences);
        }

        private static void CountJoltageDifferences(List<long> sortedAdapterJoltageRatings,
            out long oneJoltDifferences, out long threeJoltDifferences)
        {
            oneJoltDifferences = 0;
            threeJoltDifferences = 1; // Start with always-there device joltage difference

            switch (sortedAdapterJoltageRatings.First())
            {
                case 1:
                    oneJoltDifferences++;
                    break;
                case 3:
                    threeJoltDifferences++;
                    break;
            }
            
            for (int i = 0; i < sortedAdapterJoltageRatings.Count - 1; i++)
            {
                long difference = sortedAdapterJoltageRatings[i + 1] - sortedAdapterJoltageRatings[i];
                if (difference == 1)
                {
                    oneJoltDifferences++;
                }
                else if (difference == 3)
                {
                    threeJoltDifferences++;
                }
            }
        }
    }
}