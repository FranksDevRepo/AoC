using aoc2021.Puzzles.Core;
using aoc2021.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using aoc2021.Puzzles.Graphs;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Chiton")]
public sealed class Day15 : SolutionBase
{
    public override string Part1(string input)
    {
        var map = input
            .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row
                .Select(c => Convert.ToInt32(Char.GetNumericValue(c))).ToArray()).ToArray();

        var graph = new Graph<Coordinate>(false, true);
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
                }

                if (y + 1 < map[0].Length)
                {
                    var n3 = nodes[new Coordinate(x, y + 1)];
                    graph.AddEdge(n1, n3, map[y + 1][x]);
                }
            }
        }

        //Node<int> n1 = graph.AddNode(1);
        //Node<int> n2 = graph.AddNode(2);
        //Node<int> n3 = graph.AddNode(3);
        //Node<int> n4 = graph.AddNode(4);
        //graph.AddEdge(n1, n2, 9);
        //graph.AddEdge(n1, n3, 5);
        //graph.AddEdge(n3, n4, 12);
        //graph.AddEdge(n2, n4, 18);

        var start = nodes[new Coordinate(0, 0)];
        var target = nodes[new Coordinate(map[0].Length - 1, map.Length - 1)];
        var path = graph.GetShortestPathDijkstra(start, target);

        path.ForEach(a => Console.WriteLine(a));

        return path.Sum(p => p.Weight).ToString();
    }

    public override string Part2(string input)
    {
        throw new NotImplementedException();
    }

    public record Coordinate(int X, int Y);
}