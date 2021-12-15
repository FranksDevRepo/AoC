using aoc2021.Puzzles.Core;
using aoc2021.Puzzles.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Chiton")]
public sealed class Day15 : SolutionBase
{
    public override string Part1(string input)
    {
        var map = ParseMap(input);

        return FindPathWithLowestTotalRisk(map);
    }

    public override string Part2(string input)
    {
        var map = ParseMap(input, true);

        return FindPathWithLowestTotalRisk(map);
    }

    private int[][] ParseMap(string input, bool resizeMap = false)
    {
        var map = input
            .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row
                .Select(c => Convert.ToInt32(Char.GetNumericValue(c))).ToArray()).ToArray();

        if (resizeMap)
            map = ResizeMap(map, 5, 5);
        return map;
    }

    private static string FindPathWithLowestTotalRisk(int[][] map)
    {
        var graph = new Graph<Coordinate>(true, true);
        Dictionary<Coordinate, Node<Coordinate>> nodes = new();

        for (int y = 0; y < map[0].Length; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                var coordinate = new Coordinate(x, y);
                var node = graph.AddNode(coordinate);
                nodes.Add(coordinate, node);
            }
        }

        for (int y = 0; y < map[0].Length; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                var n1 = nodes[new Coordinate(x, y)];

                if (x + 1 < map[0].Length)
                {
                    var n2 = nodes[new Coordinate(x + 1, y)];
                    graph.AddEdge(n1, n2, map[y][x + 1]);
                    graph.AddEdge(n2, n1, map[y][x]);
                }

                if (y + 1 < map[0].Length)
                {
                    var n3 = nodes[new Coordinate(x, y + 1)];
                    graph.AddEdge(n1, n3, map[y + 1][x]);
                    graph.AddEdge(n3, n1, map[y][x]);
                }
            }
        }

        var start = nodes[new Coordinate(0, 0)];
        var target = nodes[new Coordinate(map[0].Length - 1, map.Length - 1)];
        var paths = graph.GetShortestPathDijkstra(start, target);

        return paths.Sum(p => p.Weight).ToString();
    }

    private int[][] ResizeMap(int[][] map, int resizeX, int resizeY)
    {
        var resizedMap = new int[map.Length * resizeY][];

        for (int y = 0; y < map.Length * resizeY; y++)
        {
            resizedMap[y] = new int[map[0].Length * resizeX];
            for (int x = 0; x < map[0].Length * resizeX; x++)
            {
                var oldRiskLevel = map[y % map.Length][x % map[0].Length];
                var newRisklevel = oldRiskLevel + (x / map.Length) + (y / map[0].Length);
                if (newRisklevel > 9)
                    newRisklevel = newRisklevel % 10 + 1;

                resizedMap[y][x] = newRisklevel;

            }
        }

        return resizedMap;
    }

    public record Coordinate(int X, int Y);
}