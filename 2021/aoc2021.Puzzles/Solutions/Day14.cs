using aoc2021.Puzzles.Core;
using aoc2021.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoreLinq;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Extended Polymerization")]
public sealed class Day14 : SolutionBase
{
    public override string Part1(string input)
    {
        var polymerTemplate = GetLines(input).First();
        var rules = GetLines(input)
            .Where(l => l.Contains("->", StringComparison.InvariantCulture))
            .Select(l => l.Split("->", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            .ToDictionary(k => k[0], v => v[1]);

        int steps = 0;
        StringBuilder polymer = new StringBuilder(polymerTemplate);
        do
        {
            steps++;
            StringBuilder newPolymer = new();
            for (int index = 0; index < polymer.Length - 1; index++)
            {
                string pair = polymer.ToString()[index..(index + 2)];
                newPolymer.Append($"{pair[0]}{rules[pair]}");
            }

            newPolymer.Append(polymer.ToString().Last());
            polymer.Length = 0;
            polymer.Append(newPolymer);
        } while (steps < 10);

        var charCounter = polymer.ToString()
            .GroupBy(c => c)
            .Select(c => new { Char = c.Key, Count = c.Count() });
        var mostCommonElement = charCounter.First(c => c.Count == charCounter.Max(c => c.Count)).Count;
        var leastCommonElement = charCounter.First(c => c.Count == charCounter.Min(c => c.Count)).Count;

        return (mostCommonElement - leastCommonElement).ToString();
    }

    public override string Part2(string input)
    {
        throw new NotImplementedException();
    }
}