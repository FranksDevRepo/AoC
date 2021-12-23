using aoc2021.Puzzles.Core;
using aoc2021.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Reactor Reboot")]
public sealed class Day22 : SolutionBase
{
    public override string Part1(string input)
    { 
        var rebootSteps = GetLines(input);

        var rebootStepsParser = new Regex(@"^(?<State>(on|off)) x=(?<xMin>-?\d{1,5})..(?<xMax>-?\d{1,5}),y=(?<yMin>-?\d{1,5})..(?<yMax>-?\d{1,5}),z=(?<zMin>-?\d{1,5})..(?<zMax>-?\d{1,5})", RegexOptions.Compiled);

        HashSet<Coord3D> reactor = new();

        foreach(var rebootStep in rebootSteps)
        {
            var match = rebootStepsParser.Match(rebootStep);
            if(!match.Success)
                continue;
            var state = match.Groups["State"].Value;
            var xMin = int.Parse(match.Groups["xMin"].Value);
            var xMax = int.Parse(match.Groups["xMax"].Value);
            var yMin = int.Parse(match.Groups["yMin"].Value);
            var yMax = int.Parse(match.Groups["yMax"].Value);
            var zMin = int.Parse(match.Groups["zMin"].Value);
            var zMax = int.Parse(match.Groups["zMax"].Value);

            for(int z = (zMin < zMax ? zMin : zMax); z <= (zMin < zMax ? zMax : zMin); z += (zMin < zMax) ? 1 : -1)
            {
                for (int y = (yMin < yMax ? yMin : yMax) ; y <= (yMin < yMax ? yMax : yMin); y += (yMin < yMax) ? 1 : -1)
                {
                    for (int x = (xMin < xMax ? xMin : xMax); x <= (xMin < xMax ? xMax : xMin); x += (xMin < xMax) ? 1 : -1)
                    {
                        var coord = new Coord3D(x, y, z);
                        if(state.Equals("on"))
                            reactor.Add(coord);
                        else
                            reactor.Remove(coord);
                    }
                }
            }
        }
        return reactor.Count.ToString();
    }

    public override string Part2(string input)
    {
        throw new NotImplementedException();
    }

    public record Coord3D(int X, int Y, int Z);
}