using aoc2020.Puzzles.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Toboggan Trajectory")]
    public sealed class Day03 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            var terrain = input.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(line => line.Select(c => c is '#').ToArray()).ToArray();
            var countTrees = CountTrees(terrain, 3, 1);

            return countTrees.ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            var terrain = input.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(line => line.Select(c => c is '#').ToArray()).ToArray();
            var countTreesSlope1 = CountTrees(terrain, 1, 1);
            var countTreesSlope2 = CountTrees(terrain, 3, 1);
            var countTreesSlope3 = CountTrees(terrain, 5, 1);
            var countTreesSlope4 = CountTrees(terrain, 7, 1);
            var countTreesSlope5 = CountTrees(terrain, 1, 2);

            Int64 solution = countTreesSlope1 * countTreesSlope2 * countTreesSlope3 * countTreesSlope4 * countTreesSlope5;
            return solution.ToString();
        }

        private int CountTrees(bool[][] terrain, int right, int down)
        {
            int countTrees = 0;
            int col = right;
            int row = down;
            while (row < terrain.Length)
            {
                if (terrain[row][col % terrain[row].Length])
                    countTrees++;
                col += right;
                row += down;
            }

            return countTrees;
        }
    }
}
