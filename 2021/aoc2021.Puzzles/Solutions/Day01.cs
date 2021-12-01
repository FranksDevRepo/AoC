using aoc2021.Puzzles.Core;
using aoc2021.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Sonar Sweep")]
public sealed class Day01 : SolutionBase
{
    public override string Part1(string input)
    {
        var numbers = GetLines(input).Select(x => Convert.ToInt32(x));
        int? previousNumber = null;
        var countBiggerNumbers = 0;
        foreach (var number in numbers)
        {
            if (number > previousNumber)
                countBiggerNumbers++;
            previousNumber = number;
        }

        return countBiggerNumbers.ToString();
    }

    public override string Part2(string input)
    {
        var numbers = GetLines(input).Select(x => Convert.ToInt32(x)).ToArray();
        int? previousSum = null;
        var count = 0;
        for (var i = 0; i <= numbers.Length - 3; i++)
        {
            var upperRangeWindow = i + 3;
            var currentSum = numbers[i..upperRangeWindow].Sum();
            if (currentSum > previousSum)
                count++;
            previousSum = currentSum;
        }

        return count.ToString();
    }
}