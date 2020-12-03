using aoc2020.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aoc2020.Puzzles.Core;
using static aoc2020.Puzzles.Solutions.Day10;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Donut Maze")]
    public sealed class Day20 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            var map = ParseMap(input);
            var (portals, entrance, exit) = GetPortals(map);

            var shortestPath = int.MaxValue;
            var visited = new Dictionary<Day10.Point, int>();
            var queue = new Queue<(Day10.Point Pos, int Distance)>(new[] { (entrance, 0) });
            while (queue.Count > 0)
            {
                if (IsUpdateProgressNeeded()) { await UpdateProgressAsync(); }

                var (pos, distance) = queue.Dequeue();
                if (visited.TryGetValue(pos, out var storedDistance) && storedDistance <= distance)
                {
                    continue;
                }
                visited[pos] = distance;

                if (pos == exit)
                {
                    shortestPath = distance;
                    break;
                }

                if (portals.TryGetValue(pos, out var portalDestination))
                {
                    queue.Enqueue((portalDestination, distance + 1));
                }
                foreach (var direction in Directions)
                {
                    var nextPos = pos + direction;
                    if (map.TryGetValue(nextPos, out var tile) && tile == Passage)
                    {
                        queue.Enqueue((nextPos, distance + 1));
                    }
                }
            }

            return shortestPath.ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            var map = ParseMap(input);
            var (portals, entrance, exit) = GetPortals(map);
            var (outerPortals, innerPortals) = GetRecursivePortals(map, portals);

            var shortestPath = int.MaxValue;
            var visitedTilesByLevel = new Dictionary<int, Dictionary<Day10.Point, int>>();
            var queue = new Queue<(Day10.Point Pos, int Distance, int Level)>(new[] { (entrance, 0, 0) });
            while (queue.Count > 0)
            {
                var (pos, distance, level) = queue.Dequeue();
                if (IsUpdateProgressNeeded()) { await UpdateProgressAsync(distance, 8000); }

                var visited = visitedTilesByLevel.GetOrAdd(level, _ => new Dictionary<Day10.Point, int>());
                if (visited.TryGetValue(pos, out var storedDistance) && storedDistance <= distance)
                {
                    continue;
                }
                visited[pos] = distance;

                if (level == 0 && pos == exit)
                {
                    shortestPath = distance;
                    break;
                }

                if (level > 0 && outerPortals.TryGetValue(pos, out var outerDestination))
                {
                    queue.Enqueue((outerDestination, distance + 1, level - 1));
                }
                if (level < innerPortals.Count && innerPortals.TryGetValue(pos, out var innerDestination))
                {
                    queue.Enqueue((innerDestination, distance + 1, level + 1));
                }

                foreach (var direction in Directions)
                {
                    var nextPos = pos + direction;
                    if (map.TryGetValue(nextPos, out var tile) && tile == Passage)
                    {
                        queue.Enqueue((nextPos, distance + 1, level));
                    }
                }
            }

            return shortestPath.ToString();
        }

        private (Dictionary<Day10.Point, Day10.Point> OuterPortals, Dictionary<Day10.Point, Day10.Point> InnerPortals) GetRecursivePortals(Dictionary<Day10.Point, char> map, Dictionary<Day10.Point, Day10.Point> portals)
        {
            var nonPortals = map.Where(x => x.Value == Wall || x.Value == Passage).Select(x => x.Key).ToList();
            var topLeft = new Day10.Point(nonPortals.Min(p => p.X), nonPortals.Min(p => p.Y));
            var bottomRight = new Day10.Point(nonPortals.Max(p => p.X), nonPortals.Max(p => p.Y));
            var edgeX = new[] { topLeft.X, bottomRight.X };
            var edgeY = new[] { topLeft.Y, bottomRight.Y };

            var outerPortals = new Dictionary<Day10.Point, Day10.Point>();
            var innerPortals = new Dictionary<Day10.Point, Day10.Point>();
            foreach (var (from, to) in portals)
            {
                if (edgeX.Contains(from.X) || edgeY.Contains(from.Y))
                {
                    outerPortals.Add(from, to);
                }
                else
                {
                    innerPortals.Add(from, to);
                }
            }

            return (outerPortals, innerPortals);
        }

        private (Dictionary<Day10.Point, Day10.Point> Portals, Day10.Point Entrance, Day10.Point Exit) GetPortals(Dictionary<Day10.Point, char> map)
        {
            var (mapEntrance, mapExit) = (Day10.Point.Empty, Day10.Point.Empty);
            var portals = new Dictionary<string, List<Day10.Point>>();
            foreach (var (pos, c) in map)
            {
                if (c < 'A' || c > 'Z') { continue; }

                var connection = Directions.Select(d => (Day10.Point?)(pos + d))
                    .FirstOrDefault(p => map.TryGetValue(p.Value, out var tile) && tile == Passage);
                if (connection == null) { continue; }

                var pos2 = Directions.Select(d => pos + d)
                    .FirstOrDefault(p => map.TryGetValue(p, out var tile) && tile >= 'A' && tile <= 'Z');
                var name = string.Join(string.Empty, new[] { pos, pos2 }.OrderBy(p => p.X).ThenBy(p => p.Y).Select(p => map[p]));
                portals.GetOrAdd(name, _ => new List<Day10.Point>()).Add(connection.Value);

                if (name == Entrance) { mapEntrance = connection.Value; }
                if (name == Exit) { mapExit = connection.Value; }
            }

            var portalPaths = new Dictionary<Day10.Point, Day10.Point>();
            foreach (var exits in portals.Values)
            {
                foreach (var (exit1, index) in exits.WithIndex())
                {
                    foreach (var exit2 in exits.Skip(index + 1))
                    {
                        portalPaths.Add(exit1, exit2);
                        portalPaths.Add(exit2, exit1);
                    }
                }
            }

            return (portalPaths, mapEntrance, mapExit);
        }

        private Dictionary<Day10.Point, char> ParseMap(string input)
        {
            var lines = GetLines(input);
            var map = new Dictionary<Day10.Point, char>();
            foreach (var (line, y) in lines.WithIndex())
            {
                foreach (var (c, x) in line.WithIndex())
                {
                    if (c == Empty) { continue; }
                    map[new Day10.Point(x, y)] = c;
                }
            }

            return map;
        }

        private const char Empty = ' ';
        private const char Passage = '.';
        private const char Wall = '#';
        private const string Entrance = "AA";
        private const string Exit = "ZZ";
        private static readonly Day10.Point[] Directions = new[] { new Day10.Point(0, -1), new Day10.Point(1, 0), new Day10.Point(0, 1), new Day10.Point(-1, 0) };
    }
}
