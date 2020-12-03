using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aoc2020.Puzzles.Core;
using static aoc2020.Puzzles.Solutions.Day10;
using static aoc2020.Puzzles.Solutions.Day11;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Oxygen System")]
    public sealed class Day15 : SolutionBase
    {
        public enum Tile { Robot, Empty, Wall, OxygenSystem, Unknown }

        public Dictionary<Day10.Point, Tile> Map { get; private set; }
        public HashSet<Day10.Point> OxygenVisited { get; private set; }
        public List<Day10.Point> PathToOxygenGenerator { get; private set; }

        public Day15() => myDirectionCodes = myDirections.ToDictionary(k => k.Value, v => v.Key);

        public override async Task<string> Part1Async(string input)
        {
            await DiscoverMap(input);

            return PathToOxygenGenerator.Count.ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            await DiscoverMap(input);

            var oxygenGeneratorPos = Map.First(x => x.Value == Tile.OxygenSystem).Key;
            var maxDistance = 0;
            OxygenVisited = new HashSet<Day10.Point>();
            var queue = new Queue<(Day10.Point p, int distance)>(new[] { (oxygenGeneratorPos, 0) });
            while (queue.Count > 0)
            {
                var (pos, distance) = queue.Dequeue();
                if (!OxygenVisited.Add(pos)) { continue; }
                if (distance > maxDistance) { maxDistance = distance; };

                if (IsUpdateProgressNeeded()) { await UpdateProgressAsync(); }

                for (var directionCode = 1; directionCode <= 4; directionCode++)
                {
                    var direction = myDirections[directionCode];
                    var nextPos = pos + direction;
                    if (OxygenVisited.Contains(nextPos) || Map[nextPos] == Tile.Wall) { continue; }

                    queue.Enqueue((nextPos, distance + 1));
                }
            }

            return maxDistance.ToString();
        }

        private async Task DiscoverMap(string input)
        {
            myIntMachine = new Day11.SynchronousIntMachine(input);
            Map = new Dictionary<Day10.Point, Tile>() { [new Day10.Point(0, 0)] = Tile.Empty };
            await Backtrack();
            PathToOxygenGenerator = FindPath(Day10.Point.Empty, Map.First(x => x.Value == Tile.OxygenSystem).Key).Skip(1).ToList();
        }

        private async Task Backtrack()
        {
            var currentPos = Day10.Point.Empty;
            var stack = new Stack<(Day10.Point, int)>(myDirections.Keys.Select(c => (currentPos, c)));
            while (stack.Count > 0)
            {
                if (IsUpdateProgressNeeded()) { await UpdateProgressAsync(Map.Count, Math.Max(Map.Count + 1, 41 * 41)); }

                var (pos, directionCode) = stack.Pop();
                var direction = myDirections[directionCode];
                var nextPos = pos + direction;
                if (Map.ContainsKey(nextPos)) { continue; }

                if (currentPos != pos)
                {
                    currentPos = GoTo(currentPos, pos);
                }

                long tileCode = Step(directionCode);
                switch (tileCode)
                {
                    case 0:
                        Map[nextPos] = Tile.Wall;
                        continue;
                    case 1:
                        Map[nextPos] = Tile.Empty;
                        break;
                    case 2:
                        Map[nextPos] = Tile.OxygenSystem;
                        break;
                }

                currentPos = nextPos;
                for (var nextDirectionCode = 1; nextDirectionCode <= 4; nextDirectionCode++)
                {
                    stack.Push((nextPos, nextDirectionCode));
                }
            }
        }

        private Day10.Point GoTo(Day10.Point currentPos, Day10.Point targetPos)
        {
            var path = FindPath(currentPos, targetPos);
            for (var i = 1; i < path.Count; i++)
            {
                var direction = path[i] - path[i - 1];
                var directionCode = myDirectionCodes[direction];
                Step(directionCode);
            }
            return targetPos;
        }

        private List<Day10.Point> FindPath(Day10.Point start, Day10.Point end)
        {
            var visited = new HashSet<Day10.Point>();
            var queue = new Queue<(Day10.Point, List<Day10.Point>)>(new[] { (start, new List<Day10.Point>()) });
            while (queue.Count > 0)
            {
                var (pos, path) = queue.Dequeue();
                if (!visited.Add(pos)) { continue; }

                var nextPath = path.ToList();
                nextPath.Add(pos);
                if (pos == end) { return nextPath; }

                foreach (var direction in myDirections.Values)
                {
                    var nextPos = pos + direction;
                    if (!Map.TryGetValue(nextPos, out var tile) || tile == Tile.Wall)
                    {
                        continue;
                    }
                    queue.Enqueue((nextPos, nextPath));
                }
            }

            return null;
        }

        private long Step(int directionCode)
        {
            myIntMachine.InputQueue.Enqueue(directionCode);
            myIntMachine.RunUntilBlockOrComplete();
            var tileCode = myIntMachine.OutputQueue.Dequeue();

            return tileCode;
        }

        private Day11.SynchronousIntMachine myIntMachine;

        private readonly Dictionary<int, Day10.Point> myDirections = new Dictionary<int, Day10.Point>
        {
            [1] = new Day10.Point(0, -1), // North
            [2] = new Day10.Point(0, 1),  // South
            [3] = new Day10.Point(-1, 0), // West
            [4] = new Day10.Point(1, 0)   // East
        };

        private readonly Dictionary<Day10.Point, int> myDirectionCodes;
    }
}
