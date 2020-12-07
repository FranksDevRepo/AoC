using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Binary Boarding")]
    public sealed class Day05 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            var boardingPasses = GetLines(input);
            var seatNumbers = GetSeatNumbers(boardingPasses);
            return seatNumbers.Max().ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            var boardingPasses = GetLines(input);
            var seatNumbers = GetSeatNumbers(boardingPasses);
            int minSeatNumber = seatNumbers.Min();
            int maxSeatNumber = seatNumbers.Max();
            var availableSeats = Enumerable.Range(minSeatNumber, maxSeatNumber - minSeatNumber);
            var mySeat = availableSeats.Except(seatNumbers).First();
            return mySeat.ToString();
        }
        private IEnumerable<int> GetSeatNumbers(List<string> boardingPasses)
        {
            foreach (var boardingPass in boardingPasses)
            {
                yield return GetSeatNumber(boardingPass);
            }
        }

        private int GetSeatNumber(string boardingPass)
        {
            int row = 127;
            var rowRange = (lower: 0, upper: 127);
            int seat = 7;
            var seatRange = (lower: 0, upper: 7);
            foreach (char c in boardingPass)
            {
                rowRange = c switch
                {
                    'F' => (rowRange.lower, (rowRange.lower + rowRange.upper) / 2),
                    'B' => ((rowRange.lower + rowRange.upper) / 2 + 1, rowRange.upper),
                    _ => rowRange
                };
                seatRange = c switch
                {
                    'L' => (seatRange.lower, (seatRange.lower + seatRange.upper) / 2),
                    'R' => ((seatRange.lower + seatRange.upper) / 2 + 1, seatRange.upper),
                _ => seatRange
                };
            }

            row = Math.Min(rowRange.lower, rowRange.upper);
            seat = Math.Min(seatRange.lower, seatRange.upper);
            return row * 8 + seat;
        }
    }
}
