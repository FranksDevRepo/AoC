using aoc2021.Puzzles.Core;
using System.Linq;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Binary Diagnostic")]
public sealed class Day03 : SolutionBase
{
    public override string Part1(string input)
    {
        var data = ParseInput(input);

        var gamma = 0L;
        var epsilon = 0L;

        for (int idx = 0; idx < data[0].Length; idx++)
        {
            var zeros = 0L;
            var ones = 0L;

            foreach (var line in data)
            {
                if (line[idx] == '0')
                    zeros++;
                else
                    ones++;
            }

            if (zeros > ones)
            {
                gamma *= 2;
                epsilon = epsilon * 2 + 1;
            }
            else
            {
                gamma = gamma * 2 + 1;
                epsilon *= 2;

            }
        }

        return (gamma * epsilon).ToString();
    }

    public override string Part2(string input)
    {
        throw new NotImplementedException();
    }

    private static char[][] ParseInput(string input) =>
        (from line in GetLines(input)
            where !string.IsNullOrWhiteSpace(line)
            select line.ToCharArray()).ToArray();
}