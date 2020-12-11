using aoc2020.Puzzles.Core;
using aoc2020.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Seating System")]
    public sealed class Day11 : SolutionBase
    {
        public enum Tile
        {
            Floor = '.',
            EmptySeat = 'L',
            OccupiedSeat = '#'
        }

        public override async Task<string> Part1Async(string input)
        {
            var seatPlan = GetSeatPlan(input);
            int occupiedSeats = ApplyRules(seatPlan);
            
            return occupiedSeats.ToString();
        }

        private int ApplyRules(Tile[][] seatPlan)
        {
            var currentSeatPlan = seatPlan.Select(s => s.ToArray()).ToArray();
            int countOccupiedSeats = 0;
            do
            {
                for (int rowIndex = 0; rowIndex < seatPlan.Length;rowIndex++)
                {
                    for (int colIndex = 0; colIndex < seatPlan[rowIndex].Length; colIndex++)
                    {
                        int occupiedAdjacentSeats = CountOccupiedAdjacentSeats(currentSeatPlan, rowIndex, colIndex);
                        if (currentSeatPlan[rowIndex][colIndex] == Tile.EmptySeat && occupiedAdjacentSeats == 0)
                            seatPlan[rowIndex][colIndex] = Tile.OccupiedSeat;
                        else if (currentSeatPlan[rowIndex][colIndex] == Tile.OccupiedSeat && occupiedAdjacentSeats > 3)
                            seatPlan[rowIndex][colIndex] = Tile.EmptySeat;
                    }
                }

                bool isEqual = Compare(currentSeatPlan, seatPlan);
                if(isEqual)
                    break;
                else
                    currentSeatPlan = seatPlan.Select(s => s.ToArray()).ToArray();
            } while (true);
            for (int row = 0; row < seatPlan.Length; row++)
            for (int col = 0; col < seatPlan[row].Length; col++)
                countOccupiedSeats += seatPlan[row][col] == Tile.OccupiedSeat ? 1 : 0;
            return countOccupiedSeats;
        }

        private bool Compare(Tile[][] currentSeatPlan, Tile[][] seatPlan)
        {
            bool isEqual = true;
            for (int rowIndex = 0; rowIndex < seatPlan.Length; rowIndex++)
            {
                for (int colIndex = 0; colIndex < seatPlan[rowIndex].Length; colIndex++)
                {
                    if (currentSeatPlan[rowIndex][colIndex] != seatPlan[rowIndex][colIndex])
                    {
                        isEqual = false;
                        break;
                    }
                    if (!isEqual) break;
                }
            }
            return isEqual;
        }

        private int CountOccupiedAdjacentSeats(Tile[][] seatPlan, in int rowIndex, in int colIndex)
        {
            int countOccupiedAdjacentSeats=0;

            for (int row = Math.Max(rowIndex - 1, 0); row < Math.Min(rowIndex+2, seatPlan.Length); row++)
            {
                for (int col = Math.Max(colIndex - 1, 0); col < Math.Min(colIndex + 2, seatPlan[row].Length); col++)
                {
                    if (row == rowIndex && col == colIndex) continue;
                    countOccupiedAdjacentSeats += seatPlan[row][col] switch
                    {
                        Tile.OccupiedSeat => 1,
                        _ => 0
                    };
                }
            }
            return countOccupiedAdjacentSeats;
        }

        private Tile[][] GetSeatPlan(string input)
        {
            var seatPlan = input
                .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(row => row
                    .Select(c => c switch
                    {
                        '.' => Tile.Floor,
                        'L' => Tile.EmptySeat,
                        '#' => Tile.OccupiedSeat

                    }).ToArray()).ToArray();
            return seatPlan;
        }

        public override async Task<string> Part2Async(string input)
        {
            throw new NotImplementedException();
        }
    }
}
