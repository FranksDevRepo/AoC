#nullable enable
using aoc2021.Puzzles.Core;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Lanternfish")]
public sealed class Day06 : SolutionBase
{
    public override string Part1(string input)
    {
        var lanternFishes = GetLines(input)
            .First()
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(n => new LanternFish(Convert.ToInt16(n)))
            .ToList();

        int days = 0;
        do
        {
            days++;
            List<LanternFish> newLanternFishes = new();
            foreach (var lanternFish in lanternFishes)
            {
                var newFish = lanternFish.SimulateDay();
                if (newFish != null)
                    newLanternFishes.Add(newFish);
            }

            if (newLanternFishes.Count > 0)
                lanternFishes.AddRange(newLanternFishes);
        } while (days < 80);

        return lanternFishes.Count.ToString();
    }

    public override string Part2(string input)
    {
        throw new NotImplementedException();
    }
}

public class LanternFish
{
    public int Timer { get; set; }

    public LanternFish(int timer)
    {
        Timer = timer;
    }

    public LanternFish? SimulateDay()
    {
        Timer--;
        if (Timer < 0)
        {
            Timer = 6;
            return new LanternFish(8);
        }

        return null;
    }
}