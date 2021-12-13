using aoc2021.Puzzles.Core;
using aoc2021.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Dumbo Octopus")]
public sealed class Day11 : SolutionBase
{
    public override string Part1(string input)
    {
        return Part1(input);
    }

    public string Part1(string input, int numberOfSteps = 100)
    {
        var octopusSimulation = new DumpOctopusSimulation(input);

        octopusSimulation.CreateOctopusMap();

        int countFlashes = octopusSimulation.SimulateStepAndCountFlashes();

        return countFlashes.ToString();
    }

    public override string Part2(string input)
    {
        throw new NotImplementedException();
    }

    private record Position(ushort X, ushort Y);

    public class DumpOctopusSimulation
    {
        private readonly string input;
        public ushort[][] OctopusMap { get; set; }

        public DumpOctopusSimulation(string input)
        {
            this.input = input;
        }

        public void CreateOctopusMap()
        {
            this.OctopusMap = this.input
                .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(row => row
                    .Select(c => Convert.ToUInt16(c)).ToArray()).ToArray();
        }

        public int SimulateStepAndCountFlashes()
        {
            var flashingOctopussies = IncreaseEnergyLevel();

            return flashingOctopussies.Count;
        }

        private List<(ushort x, ushort y)> IncreaseEnergyLevel()
        {
            List<(ushort x, ushort y)> flashingOctopussies = new();
            for (ushort y = 0; y < this.OctopusMap[0].Length; y++)
            {
                for(ushort x = 0; x < this.OctopusMap.Length; x++)
                {
                    this.OctopusMap[y][x]++;
                    if(this.OctopusMap[y][x] == 9)
                    {
                        flashingOctopussies.Add((x, y));
                    }
                }
            }
            return flashingOctopussies;
        }

        private List<(ushort x, ushort y)> IncreaseAdjacentEnergyLevel((ushort x, ushort y) coordinate)
        {
            List<(ushort x, ushort y)> flashingOctopussies = new();

            return flashingOctopussies;
        }
    }

}