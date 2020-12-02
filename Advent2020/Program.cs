using System;
using System.IO;

namespace Advent2020
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] entries = File.ReadAllLines("Entries.txt");

            FindMultipleOfThree(entries);
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
