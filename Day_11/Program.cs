using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_11
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("Seat_Layout.txt");
            SolvePart1(input);
        }

        private static void SolvePart1(string[] input)
        {
            char[][] initialSeatLayout = input.Select(s => s.Select(c => c).ToArray()).ToArray();

            int seatsThatEndUpOccupied = SeatsThatEndUpOccupied(initialSeatLayout);
            
            Console.WriteLine(seatsThatEndUpOccupied);
        }

        private static int SeatsThatEndUpOccupied(char[][] initialSeatLayout)
        {
            char[][] finalSeatLayout = GetFinalSeatLayout(initialSeatLayout);

            return CountNumberOfOccupiedSeats(finalSeatLayout);
        }

        private static char[][] GetFinalSeatLayout(char[][] initialSeatLayout)
        {
            char[][] seatLayout = initialSeatLayout;
            bool isDifferent;
            do
            {
                seatLayout = GetNextSeatLayout(seatLayout, out isDifferent);
            } while (isDifferent);

            return seatLayout;
        }

        private static char[][] GetNextSeatLayout(char[][] seatLayout, out bool isDifferent)
        {
            isDifferent = false;

            char[][] nextSeatLayout = new char[seatLayout.Length][];
            for (int i = 0; i < nextSeatLayout.Length; i++)
            {
                nextSeatLayout[i] = new char[seatLayout.Length];
            }

            for (int i = 0; i < seatLayout.Length; i++)
            {
                for (int j = 0; j < seatLayout[i].Length; j++)
                {
                    char currentPosition = seatLayout[i][j];

                    int numberOfAdjacentOccupiedSeats = GetNumberOfAdjacentOccupiedSeats(i, j, seatLayout);

                    if (currentPosition == 'L' && numberOfAdjacentOccupiedSeats == 0)
                    {
                        nextSeatLayout[i][j] = '#';
                        isDifferent = true;
                    }
                    
                    else if (currentPosition == '#' && numberOfAdjacentOccupiedSeats >= 4)
                    {
                        nextSeatLayout[i][j] = 'L';
                        isDifferent = true;
                    }

                    else
                    {
                        nextSeatLayout[i][j] = currentPosition;
                    }
                }
            }

            return nextSeatLayout;
        }

        private static int GetNumberOfAdjacentOccupiedSeats(int i, int j, char[][] seatLayout)
        {
            int numberOfAdjacentOccupiedSeats = 0;

            // Get a list of tuples that are adjacent seats to i,j
            List<Tuple<int, int>> adjacentSeats = GetAdjacentSeats(i, j, seatLayout);

            foreach (Tuple<int,int> adjacentSeat in adjacentSeats)
            {
                if (seatLayout[adjacentSeat.Item1][adjacentSeat.Item2] == '#')
                {
                    numberOfAdjacentOccupiedSeats++;
                }
            }

            return numberOfAdjacentOccupiedSeats;
        }

        private static List<Tuple<int, int>> GetAdjacentSeats(int i, int j, char[][] seatLayout)
        {
            List<Tuple<int, int>> adjacentSeats = new List<Tuple<int, int>>();

            bool rowAbove = i - 1 >= 0;
            bool rowBelow = i + 1 <= seatLayout.GetUpperBound(0);
            bool colLeft = j - 1 >= 0;
            bool colRight = j + 1 <= seatLayout[i].GetUpperBound(0);

            if (rowAbove && colLeft)
            {
                adjacentSeats.Add(new Tuple<int, int>(i - 1, j - 1));
            }

            if (rowAbove)
            {
                adjacentSeats.Add(new Tuple<int, int>(i - 1, j));
            }

            if (rowAbove && colRight)
            {
                adjacentSeats.Add(new Tuple<int, int>(i - 1, j + 1));
            }

            if (colLeft)
            {
                adjacentSeats.Add(new Tuple<int, int>(i, j - 1));
            }

            if (colRight)
            {
                adjacentSeats.Add(new Tuple<int, int>(i, j + 1));
            }

            if (rowBelow && colLeft)
            {
                adjacentSeats.Add(new Tuple<int, int>(i + 1, j - 1));
            }

            if (rowBelow)
            {
                adjacentSeats.Add(new Tuple<int, int>(i + 1, j));
            }

            if (rowBelow && colRight)
            {
                adjacentSeats.Add(new Tuple<int, int>(i + 1, j + 1));
            }

            return adjacentSeats;
        }

        private static int CountNumberOfOccupiedSeats(char[][] seatLayout)
        {
            IEnumerable<char> positions = seatLayout.SelectMany(c => c);

            int numberOfOccupiedSeats = positions.Count(c => c == '#');

            return numberOfOccupiedSeats;
        }
    }
}