using aoc2021.Puzzles.Core;
using aoc2021.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Seven Segment Search")]
public sealed class Day08 : SolutionBase
{
    public override string Part1(string input)
    { 
        var signals = GetLines(input);

        var signalParser = new Regex(@"(?<=^.*\| )(?'Segments'.*)", RegexOptions.Compiled);
        Dictionary<int, int> segmentCounter = new();
        foreach (var signal in signals)
        {
            var digits = signalParser.Match(signal);

            if (!digits.Success)
                continue;

            var segments = digits.Groups["Segments"].Value.Split(' ').ToArray();
            foreach(var segment in segments)
            {
                if(segmentCounter.ContainsKey(segment.Length))
                    segmentCounter[segment.Length] = ++segmentCounter[segment.Length];
                else
                    segmentCounter.Add(segment.Length, 1);
            }
        }

        var countDigits = segmentCounter
            .Where(s => s.Key == 2 || s.Key == 4 || s.Key == 3 || s.Key == 7)
            .Select(kvp => kvp.Value)
            .Sum();
        return countDigits.ToString();
    }

    public override string Part2(string input)
    {
        throw new NotImplementedException();
    }
}