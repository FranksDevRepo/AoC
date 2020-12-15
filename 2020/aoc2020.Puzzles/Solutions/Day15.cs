using aoc2020.Puzzles.Core;
using aoc2020.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Rambunctious Recitation")]
    public sealed class Day15 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            var startingNumbers = (from line in GetLines(input)
                    where !string.IsNullOrWhiteSpace(line)
                    let numbers = line.Split(',')
                    from number in numbers
                    select long.Parse(number))
                .ToArray();
                //.Select((x, index) => new {key = x, value = index})
                //.ToDictionary(kvp => kvp.key, kvp => kvp.value);

            var spokenNumbers = new List<long>();
            var lastSpokenNumber = 0L;

            for (int count = 0; count < 2020; count++)
            {
                if (count < startingNumbers.Length)
                {
                    lastSpokenNumber = startingNumbers[count];
                    spokenNumbers.Add(lastSpokenNumber);
                    continue;
                }

                if (spokenNumbers.Count(x => x == lastSpokenNumber) == 1)
                {
                    lastSpokenNumber = 0;
                    spokenNumbers.Add(lastSpokenNumber);
                }
                else
                {
                    int turnWhenLastSpoken = spokenNumbers.LastIndexOf(lastSpokenNumber);
                    int turnWhenSpokenPreviously = spokenNumbers.LastIndexOf(lastSpokenNumber, turnWhenLastSpoken - 1);
                    lastSpokenNumber = (long) (turnWhenLastSpoken - turnWhenSpokenPreviously);
                    spokenNumbers.Add(lastSpokenNumber);
                }

            }

            return spokenNumbers.Last().ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            throw new NotImplementedException();
        }
    }
}
