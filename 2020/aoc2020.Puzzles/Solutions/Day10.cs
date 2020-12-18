using aoc2020.Puzzles.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Adapter Array")]
    public sealed class Day10 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            var voltageDifferences = VoltageDifferences(input);

            long count1VoltDifferences = voltageDifferences.Where(kvp => kvp.Value == 1).Count();
            long count3VoltDifferences = voltageDifferences.Where(kvp => kvp.Value == 3).Count();

            return (count1VoltDifferences * count3VoltDifferences).ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            var voltageDifferences = VoltageDifferences(input);
            var computedPermutations = new Dictionary<long,long>();
            var count = CountPermutations(voltageDifferences, ref computedPermutations, 0);
            return count.ToString();
        }

        private static Dictionary<long, long> VoltageDifferences(string input)
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
            return voltageDifferences;
        }

        private long CountPermutations(Dictionary<long, long> voltageDifferences, ref Dictionary<long,long> computedPermutations, long currentVoltage)
        {
            long countPermutations = 0;
            if (computedPermutations.TryGetValue(currentVoltage, out var result))
            {
                return result;
            }

            if (currentVoltage == voltageDifferences.Keys.Max())
                return 1L;

            for (long i = 1; i < 4; i++)
            {
                if (voltageDifferences.ContainsKey(currentVoltage + i))
                    countPermutations += CountPermutations(voltageDifferences, ref computedPermutations,currentVoltage + i);
            }
            computedPermutations[currentVoltage] = countPermutations;

            return countPermutations;
        }
    }
}
