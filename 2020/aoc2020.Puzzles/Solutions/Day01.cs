using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Report Repair")]
    public sealed class Day01 : SolutionBase
    {
        public override string Part1(string input)
        {
            var expenses = GetExpenses(input);
            var solution = PairExists(expenses, 2020);
            return solution != null ? (solution.Value.x1 * solution.Value.x2).ToString() : string.Empty;
        }

        public override string Part2(string input)
        {
            var expenses = GetExpenses(input);
            var solution = TripleExists(expenses, 2020);
            return solution != null ? (solution.Value.x1 * solution.Value.x2 * solution.Value.x3).ToString() : string.Empty;
        }

        private (int x1, int x2, int x3)? TripleExists(IEnumerable<int> numbers, int sum)
        {
            var set = new HashSet<int>(numbers);
            foreach (var number in set)
            {
                var pair = PairExists(numbers, sum - number);
                if (pair.HasValue)
                {
                    return (pair.Value.x1, pair.Value.x2, x3: number);
                }
            }

            return null;
        }

        private static IEnumerable<int> GetExpenses(string input) => GetLines(input).Select(x => Convert.ToInt32(x));

        private static (int x1, int x2)? PairExists(IEnumerable<int> numbers, int sum)
        {
            var set = new HashSet<int>(numbers);
            foreach (var elem in set)
            {
                if (set.Contains(sum - elem))
                {
                    return (x1: elem, x2: sum - elem);
                }
            }

            return null;
        }
    }
}
