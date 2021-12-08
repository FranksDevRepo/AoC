using aoc2021.Puzzles.Core;
using System;
using System.Collections.Generic;
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
        var data = ParseInput(input);

        var oxygenGeneratorRating = CalculateRating(data, MostCommonRatingFunc);
        var co2ScrubberRating = CalculateRating(data, LeastCommonRatingFunc);

        return (oxygenGeneratorRating * co2ScrubberRating).ToString();
    }

    Func<long, long, bool> MostCommonRatingFunc => (zeros, ones) => zeros > ones;
    Func<long, long, bool> LeastCommonRatingFunc => (zeros, ones) => zeros <= ones;

    private static long CalculateRating(char[][] data, Func<long, long, bool> ratingFunc)
    {
        List<char[]> numbers = new List<char[]>(data);

        var rating = 0L;

        for (int idx = 0; idx < data[0].Length; idx++)
        {
            var zeros = 0L;
            var ones = 0L;

            foreach (var number in numbers)
            {
                if (number[idx] == '0')
                    zeros++;
                else
                    ones++;
            }

            if (ratingFunc(zeros, ones))
            {
                numbers.RemoveAll(n => n[idx] == '1');
            }
            else
            {
                numbers.RemoveAll(n => n[idx] == '0');
            }

            if (numbers.Count == 1)
                break;
        }

        for (int idx = 0; idx < data[0].Length; idx++)
        {
            if (numbers.First()[idx] == '1')
                rating = rating * 2 + 1;
            else
                rating *= 2;
        }
        return rating;
    }

    private static char[][] ParseInput(string input) =>
        (from line in GetLines(input)
            where !string.IsNullOrWhiteSpace(line)
            select line.ToCharArray()).ToArray();
}