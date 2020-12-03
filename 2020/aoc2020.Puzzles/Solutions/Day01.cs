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
            var expenses = GetExpenses(input).ToArray();
            var solution = PairExists(expenses, 2020);
            return (solution.Value.x1 * solution.Value.x2).ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<int> GetExpenses(string input) => GetLines(input).Select(x => Convert.ToInt32(x));

        static (int x1, int x2)? PairExists(int[] arr, int sum)
        {
            var set = new HashSet<int>();
            foreach (int elem in arr) set.Add(elem);
            foreach (int elem in set)
                if (set.Contains(sum - elem))
                {
                    return (x1: elem, x2: sum - elem);
                }
            return null;
        }
    }
}
