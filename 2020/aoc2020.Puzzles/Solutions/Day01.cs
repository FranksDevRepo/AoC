using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Report Repair")]
    public sealed class Day01 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            var expenses = GetExpenses(input);
            var solution = PairExists(expenses, 2020);
            return (solution.Value.x1 * solution.Value.x2).ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            var expenses = GetExpenses(input);
            var solution = TripleExists(expenses, 2020);
            return (solution.Value.x1 * solution.Value.x2 * solution.Value.x3).ToString();
        }

        private (int x1, int x2, int x3)? TripleExists(IEnumerable<int> numbers, int sum)
        {
            var set = new HashSet<int>(numbers);
            foreach (var number in set)
            {
                var pair = PairExists(numbers, sum - number);
                if (pair.HasValue)
                {
                    return (x1: pair.Value.x1, x2: pair.Value.x2, x3: number);
                }
            }
            return null;
        }

        private static IEnumerable<int> GetExpenses(string input) => GetLines(input).Select(x => Convert.ToInt32(x));

        static (int x1, int x2)? PairExists(IEnumerable<int> numbers, int sum)
        {
            var set = new HashSet<int>(numbers);
            foreach (var elem in set)
                if (set.Contains(sum - elem))
                {
                    return (x1: elem, x2: sum - elem);
                }
            return null;
        }
    }
}
