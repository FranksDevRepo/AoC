using aoc2021.Puzzles.Core;
using aoc2021.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Passage Pathing")]
public sealed class Day12 : SolutionBase
{
    public override string Part1(string input)
    {
        return Explore(input, false).ToString();
        //var nodes = GetLines(input)
        //    .Select(l => l.Split('-'))
        //    .Select(parts => new { From = parts[0], To = parts[1] })
        //    .AsEnumerable()
        //    .Select(n => new[] { (From: n.From, To: n.To), (From: n.To, To: n.From) })
        //    .ToList();

        //var nodes = GetLines(input)
        //    .Select(l => l.Split('-'))
        //    .Select(parts => ( From: parts[0], To: parts[1] ))
        //   ;
    }

    public override string Part2(string input)
    {
        return Explore(input, true).ToString();
    }

    // credits to https://github.com/encse/adventofcode/blob/master/2021/Day11/Solution.cs
    // for this clean and readable solution
    int Explore(string input, bool part2)
    {
        var map = GetMap(input);

        // Recursive approach this time.
        int pathCount(string currentCave, ImmutableHashSet<string> visitedCaves, bool anySmallCaveWasVisitedTwice)
        {

            if (currentCave == "end")
            {
                return 1;
            }

            var res = 0;
            foreach (var cave in map[currentCave])
            {
                var isBigCave = cave.ToUpper() == cave;
                var seen = visitedCaves.Contains(cave);

                if (!seen || isBigCave)
                {
                    // we can visit big caves any number of times, small caves only once
                    res += pathCount(cave, visitedCaves.Add(cave), anySmallCaveWasVisitedTwice);
                }
                else if (part2 && !isBigCave && cave != "start" && !anySmallCaveWasVisitedTwice)
                {
                    // part 2 also lets us to visit a single small cave twice (except for start and end)
                    res += pathCount(cave, visitedCaves, true);
                }
            }
            return res;
        }

        return pathCount("start", ImmutableHashSet.Create<string>("start"), false);
    }

    Dictionary<string, string[]> GetMap(string input)
    {
        // taking all connections 'there and back':
        var connections =
            from line in GetLines(input)
            let parts = line.Split("-")
            let caveA = parts[0]
            let caveB = parts[1]
            from connection in new[] { (From: caveA, To: caveB), (From: caveB, To: caveA) }
            select connection;

        // grouped by "from":
        return (
            from p in connections
            group p by p.From into g
            select g
        ).ToDictionary(g => g.Key, g => g.Select(connnection => connnection.To).ToArray());
    }
}