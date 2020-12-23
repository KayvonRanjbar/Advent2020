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
        }
    }
}