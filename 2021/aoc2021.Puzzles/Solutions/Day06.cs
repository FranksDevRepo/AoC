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
        return ParseInputCreateLanternFishesAndSimulate(input, 80);
    }

    // way too slow
    //public override string Part2(string input)
    //{
    //    return ParseInputCreateLanternFishesAndSimulate(input, 256);
    //}

    public override string Part2(string input)
    {
        int dayToPass = 256;
        string[] fish = GetLines(input).First().Split(',');

        long[] fishDays = new long[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        long[] fishDaysTemp = new long[9];

        foreach (string s in fish)
        {
            switch (s)
            {
                case "1":
                    fishDays[1]++;
                    break;
                case "2":
                    fishDays[2]++;
                    break;
                case "3":
                    fishDays[3]++;
                    break;
                case "4":
                    fishDays[4]++;
                    break;
                case "5":
                    fishDays[5]++;
                    break;
            }
        }

        for (int i = 0; i < dayToPass; i++)
        {
            long nextGen = 0;
            if (fishDays[0] > 0)
            {
                nextGen = fishDays[0];
            }

            for (int d = 0; d < 8; d++)
            {
                fishDaysTemp[d] = fishDays[d + 1];
            }

            fishDaysTemp[6] += nextGen;
            fishDaysTemp[8] += nextGen;

            fishDays = fishDaysTemp.ToArray();
            for (int x = 0; x < 9; x++)
            {
                fishDaysTemp[x] = 0;
            }
        }

        long counter = 0;

        foreach (long l in fishDays)
        {
            counter += l;
        }

        return counter.ToString();
    }

    private static string ParseInputCreateLanternFishesAndSimulate(string input, int numberOfDaysToSimulate)
    {
        var lanternFishes = GetLines(input)
            .First()
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(n => new LanternFish(Convert.ToSByte(n)))
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
        } while (days < numberOfDaysToSimulate);

        return lanternFishes.Count.ToString();
    }

    public class LanternFish
    {
        public sbyte Timer { get; set; }

        public LanternFish(sbyte timer)
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
}