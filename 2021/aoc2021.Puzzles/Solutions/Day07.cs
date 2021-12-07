using aoc2021.Puzzles.Core;
using aoc2021.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("The Treachery of Whales")]
public sealed class Day07 : SolutionBase
{
    public override string Part1(string input)
    {
        var horizontalPositions = GetLines(input).First().Split(',').Select(s => Convert.ToInt32(s));

        var minPosition = horizontalPositions.Min();
        var maxPosition = horizontalPositions.Max();

        List<(int position, int fuelNeeded)> positionAndFuel = new();

        for (int position = minPosition; position <= maxPosition; position++)
        {
            var fuelNeeded = 0;
            foreach (var horizontalPosition in horizontalPositions)
            {
                fuelNeeded += Math.Abs(horizontalPosition - position);
            }
            positionAndFuel.Add((position, fuelNeeded));
        }

        var leastFuelPossible = positionAndFuel.Min(pf => pf.fuelNeeded);
        return leastFuelPossible.ToString();
    }

    public override string Part2(string input)
    {
        var horizontalPositions = GetLines(input).First().Split(',').Select(s => Convert.ToInt32(s));

        var minPosition = horizontalPositions.Min();
        var maxPosition = horizontalPositions.Max();

        List<(int position, int fuelNeeded)> positionAndFuel = new();

        for (int position = minPosition; position <= maxPosition; position++)
        {
            var fuelNeeded = 0;
            foreach (var horizontalPosition in horizontalPositions)
            {
                var fuel = 0;
                for (int moves = 1; moves <= Math.Abs(horizontalPosition - position); moves++)
                    fuel += moves;
                fuelNeeded += fuel;
            }
            positionAndFuel.Add((position, fuelNeeded));
        }

        var leastFuelPossible = positionAndFuel.Min(pf => pf.fuelNeeded);
        return leastFuelPossible.ToString();
    }
}