using aoc2020.Puzzles.Core;
using aoc2020.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Adapter Array")]
    public sealed class Day10 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            var adapters = GetLines(input)
                .Select(n => long.Parse(n))
                .OrderBy(n => n)
                .ToList();

            long lastAdapterVoltage = 0;

            Dictionary<long, long> voltageDifferences = new Dictionary<long, long>();
            foreach (var adapter in adapters)
            {
                voltageDifferences.Add(adapter, (adapter - lastAdapterVoltage));
                lastAdapterVoltage = adapter;
            }
            voltageDifferences.Add(voltageDifferences.Keys.Max() + 3, 3);

            long count1VoltDifferences = voltageDifferences.Where(kvp => kvp.Value == 1).Count();
            long count3VoltDifferences = voltageDifferences.Where(kvp => kvp.Value == 3).Count();



            return (count1VoltDifferences * count3VoltDifferences).ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            throw new NotImplementedException();
        }
    }
}
