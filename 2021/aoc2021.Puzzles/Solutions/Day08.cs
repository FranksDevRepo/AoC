using aoc2021.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        var signals = GetLines(input);

        var signalParser = new Regex(@"(?'Segments'^.*)\| (?'Digits'.*)", RegexOptions.Compiled);
        var sum = 0L;
        foreach (var signal in signals)
        {
            var match = signalParser.Match(signal);

            if (!match.Success)
                continue;

            var segments = match.Groups["Segments"].Value.Split(' ').ToArray();
            var digits = match.Groups["Digits"].Value.Split(' ').ToArray();

            Dictionary<string, int> decodedDigits = DecodeSegments(segments);
            var number = DecodeDigits(digits, decodedDigits);

            sum += number;
        }
        return sum.ToString();
    }

    private Dictionary<string, int> DecodeSegments(string[] segments)
    {
        Dictionary<string, int> result = new();

        // digits 1, 4, 7, 8 have a unique number of segments (2, 4, 3, 7)
        result.Add(SortChars(segments.First(s => s.Length == 2)), 1);
        result.Add(SortChars(segments.First(s => s.Length == 4)), 4);
        result.Add(SortChars(segments.First(s => s.Length == 3)), 7);
        result.Add(SortChars(segments.First(s => s.Length == 7)), 8);

        // digits 2, 3, 5 have 5 segments
        Decode5CharSegments(segments.Where(s => s.Length == 5).ToArray(), result)
            .ToList()
            .ForEach(x => result.Add(x.Key, x.Value));
        // digits 6, 9, 0 have 6 segments
        Decode6CharSegments(segments.Where(s => s.Length == 6).ToArray(), result)
            .ToList()
            .ForEach(x => result.Add(x.Key, x.Value));

        return result;
    }

    private Dictionary<string, int> Decode5CharSegments(string[] segments, Dictionary<string, int> uniqueSegments)
    {
        var result = new Dictionary<string, int>();
        var four = uniqueSegments.First(kvp => kvp.Value == 4).Key;
        var one = uniqueSegments.First(kvp => kvp.Value == 1).Key;
        var three = segments
            .First(s => CommonChars(s, one) == segments.Max(s => CommonChars(s, one)));
        var five = segments
            .Where(s => !s.Equals(three))
            .First(s => CommonChars(s, four) == segments.Max(s => CommonChars(s, four)));
        result.Add(SortChars(five), 5);
        result.Add(SortChars(three), 3);
        result.Add(SortChars(segments.First(s => !s.Equals(five) && !s.Equals(three))), 2);

        return result;
    }

    private Dictionary<string, int> Decode6CharSegments(string[] segments, Dictionary<string, int> uniqueSegments)
    {
        var result = new Dictionary<string, int>();
        var four = uniqueSegments.First(kvp => kvp.Value == 4).Key;
        var one = uniqueSegments.First(kvp => kvp.Value == 1).Key;
        var nine = segments
            .First(s => CommonChars(s, four) == segments.Max(s => CommonChars(s, four)));
        var six = segments
            .First(s => CommonChars(s, one) == segments.Min(s => CommonChars(s, one)));
        result.Add(SortChars(nine), 9);
        result.Add(SortChars(six), 6);
        result.Add(SortChars(segments.First(s => !s.Equals(nine) && !s.Equals(six))), 0);

        return result;
    }


    private int DecodeDigits(string[] digits, Dictionary<string, int> decodedDigits)
    {
        StringBuilder number = new();
        foreach(var digit in digits)
        {
            number.Append(decodedDigits[SortChars(digit)]);
        }
        return Convert.ToInt32(number.ToString());
    }

    private int CommonChars(string left, string right)
    {
        return left.GroupBy(c => c)
            .Join(
                right.GroupBy(c => c),
                g => g.Key,
                g => g.Key,
                (lg, rg) => lg.Zip(rg, (l, r) => l).Count())
            .Sum();
    }

    private string SortChars(string segments) => string.Concat(segments.OrderBy(c => c));
}