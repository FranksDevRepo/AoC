using aoc2020.Puzzles.Core;
using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Rambunctious Recitation")]
    public sealed class Day15 : SolutionBase
    {
        public override string Part1(string input)
        {
            return CalculateSolutionForNumbersOfRounds(input, 2020);
        }

        public override string Part2(string input)
        {
            return CalculateSolutionForNumbersOfRounds(input, 30000000);
        }

        private string CalculateSolutionForNumbersOfRounds(string input, long rounds)
        {
            var startingNumbers = (from line in GetLines(input)
                    where !string.IsNullOrWhiteSpace(line)
                    let numbers = line.Split(',')
                    from number in numbers
                    select long.Parse(number))
                .ToList();

            var round = 1L;
            var nextNumber = 0L;
            var spokenNumbersDict = new Dictionary<long, long>();

            while (startingNumbers.Count > 0)
            {
                nextNumber = AnnounceNumber(ref round, startingNumbers[0], spokenNumbersDict);
                startingNumbers.RemoveAt(0);
            }

            while (round < rounds)
            {
                nextNumber = AnnounceNumber(ref round, nextNumber, spokenNumbersDict);
            }

            return nextNumber.ToString();
        }

        private long AnnounceNumber(ref long round, long number, Dictionary<long, long> spokenNumbersDict)
        {
            spokenNumbersDict.TryGetValue(number, out var result);
            if (result != 0)
                result = round - result;

            spokenNumbersDict[number] = round++;
            return result;
        }
    }
}
