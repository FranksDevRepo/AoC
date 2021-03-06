﻿using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Lobby Layout")]
    public sealed class Day24 : SolutionBase
    {
        public struct Coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }

            public static Coordinate operator +(Coordinate a, Coordinate b) =>
                new() {X = a.X + b.X, Y = a.Y + b.Y};

            public override string ToString() => $"x={X},y={Y}";
        }

        public enum Color
        {
            White,
            Black
        }

        public override string Part1(string input)
        {
            var tiles = ParseTiles(input);

            var tileDict = CreateTileDict(tiles);

            return CountBlackTiles(tileDict).ToString();
        }

        public override string Part2(string input)
        {
            var tiles = ParseTiles(input);

            var tileDict = CreateTileDict(tiles);

            tileDict = FlipColors(tileDict, 100);

            return CountBlackTiles(tileDict).ToString();
        }

        private Dictionary<int, List<string>> ParseTiles(string input)
        {
            var lines = (from line in GetLines(input)
                where !string.IsNullOrWhiteSpace(line)
                select line).ToList();

            var count = 0;
            var tiles = new Dictionary<int, List<string>>();
            var tileRegex = new Regex("((?:se)|(?:ne)|(?:sw)|(?:nw)|(?:e)|(?:w))");
            foreach (var line in lines)
            {
                count++;
                var directions = new List<string>();

                var matches = tileRegex.Matches(line);

                foreach (Match match in matches)
                {
                    if (!match.Success)
                        throw new InvalidOperationException($"Could not find match in line number {count} : {line}");
                    directions.Add(match.Value);
                }

                tiles.Add(count, directions);
            }

            return tiles;
        }

        private Dictionary<Coordinate, Color> CreateTileDict(Dictionary<int, List<string>> tiles)
        {
            var tileDict = new Dictionary<Coordinate, Color>();

            foreach (var tileDirections in tiles.Values)
            {
                var coord = FollowDirections(tileDirections);

                var foundTile = tileDict.TryGetValue(coord, out var currentColor);

                currentColor = currentColor switch
                {
                    Color.Black => Color.White,
                    Color.White => Color.Black,
                    _ => throw new InvalidOperationException($"Found unknown color: {currentColor}")
                };
                if (foundTile)
                    tileDict[coord] = currentColor;
                else
                    tileDict.Add(coord, currentColor);
            }

            return tileDict;
        }

        private int CountBlackTiles(Dictionary<Coordinate, Color> tileDict) =>
            tileDict.Count(kvp => kvp.Value == Color.Black);

        private Dictionary<Coordinate, Color> FlipColors(Dictionary<Coordinate, Color> tileDict, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var minX = tileDict.Min(kvp => kvp.Key.X);
                var maxX = tileDict.Max(kvp => kvp.Key.X);
                var minY = tileDict.Min(kvp => kvp.Key.Y);
                var maxY = tileDict.Max(kvp => kvp.Key.Y);
                var evenMinX = minX % 2 == 0 ? minX : minX + 1;
                var oddMinX = minX % 2 == 0 ? minX + 1 : minX;

                var currentTilePlan = tileDict
                    .Select(s => s)
                    .ToDictionary(item => item.Key, item => item.Value);

                for (int y = minY - 1; y <= maxY + 1; y++)
                {
                    for (int x = y % 2 == 0 ? evenMinX - 2 : oddMinX - 2; x <= maxX + 2; x += 2)
                    {
                        var currentCoord = new Coordinate {X = x, Y = y};
                        tileDict.TryGetValue(currentCoord, out var currentColor);

                        if (currentColor == Color.Black)
                        {
                            if (CountAdjacentBlackTiles(currentCoord, tileDict) == 0 ||
                                CountAdjacentBlackTiles(currentCoord, tileDict) > 2)
                            {
                                currentTilePlan[currentCoord] = Color.White;
                            }
                            else
                            {
                                currentTilePlan[currentCoord] = Color.Black;
                            }
                        }
                        else
                        {
                            if (CountAdjacentBlackTiles(currentCoord, tileDict) == 2)
                                currentTilePlan[currentCoord] = Color.Black;
                            else
                                currentTilePlan[currentCoord] = Color.White;
                        }
                    }
                }

                tileDict = currentTilePlan;
            }

            return tileDict;
        }

        private int CountAdjacentBlackTiles(Coordinate coord, Dictionary<Coordinate, Color> tileDict)
        {
            int result = 0;
            var directions = new[]
            {
                new Coordinate {X = 1, Y = -1},
                new Coordinate {X = 2, Y = 0},
                new Coordinate {X = 1, Y = 1},
                new Coordinate {X = -1, Y = 1},
                new Coordinate {X = -2, Y = 0},
                new Coordinate {X = -1, Y = -1}
            };

            foreach (var direction in directions)
            {
                var adjacentTile = coord + direction;
                bool found = tileDict.TryGetValue(adjacentTile, out var tileColor);
                if (found && tileColor == Color.Black) result++;
            }

            return result;
        }

        // see https://www.redblobgames.com/grids/hexagons/#coordinates-doubled
        // see https://stackoverflow.com/questions/1838656/how-do-i-represent-a-hextile-hex-grid-in-memory
        // grid type = odd-r
        private Coordinate FollowDirections(List<string> tileDirections)
        {
            var tile = new Coordinate {X = 0, Y = 0};
            foreach (var direction in tileDirections)
            {
                tile += direction switch
                {
                    "ne" => new Coordinate {X = 1, Y = -1},
                    "e" => new Coordinate {X = 2, Y = 0},
                    "se" => new Coordinate {X = 1, Y = 1},
                    "sw" => new Coordinate {X = -1, Y = 1},
                    "w" => new Coordinate {X = -2, Y = 0},
                    "nw" => new Coordinate {X = -1, Y = -1},
                    _ => throw new InvalidOperationException($"Could not follow direction: {direction}")
                };
            }

            return tile;
        }
    }
}
