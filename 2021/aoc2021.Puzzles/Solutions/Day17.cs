using aoc2021.Puzzles.Core;
using aoc2021.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Trick Shot")]
public sealed class Day17 : SolutionBase
{
    public override string Part1(string input)
    { 
        var lines = GetLines(input);

        // target area: x=20..30, y=-10..-5
        //var targetAreaParser = new Regex("^target area: x=(?\d)");
        return String.Empty;
    }

    public override string Part2(string input)
    {
        throw new NotImplementedException();
    }
}