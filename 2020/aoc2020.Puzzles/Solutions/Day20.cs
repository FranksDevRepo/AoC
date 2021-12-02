using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Jurassic Jigsaw")]
    public sealed class Day20 : SolutionBase
    {
        private class Checksum
        {
            public ushort top;
            public ushort left;
            public ushort right;
            public ushort bottom;
            public ushort flippedTop;
            public ushort flippedLeft;
            public ushort flippedRight;
            public ushort flippedBottom;
        }

        private static ushort SetSpecificBitAtPosition(ushort value, int bitPosition)
        {
            ushort mask = (ushort)(1 << bitPosition);
            return (ushort)unchecked(value | mask);
        }

        private static ushort UnsetSpecificBitAtPosition(ushort value, int bitPosition)
        {
            ushort mask = (ushort)(1 << bitPosition);
            return (ushort)unchecked(value & ~mask);
        }

        public override string Part1(string input)
        {
            var puzzleSolver = new PuzzleSolver();
            puzzleSolver.ParseInput(input);

            if (Debugger.IsAttached)
                puzzleSolver.WriteDebugChecksums();

            puzzleSolver.FindMatchingTiles();

            return puzzleSolver.CalculateProductOfCornerTileIDs().ToString();
        }

        public override string Part2(string input)
        {
            var puzzleSolver = new PuzzleSolver();
            puzzleSolver.ParseInput(input);

            if (Debugger.IsAttached)
                puzzleSolver.WriteDebugChecksums();

            puzzleSolver.FindMatchingTiles();

            if (Debugger.IsAttached)
                puzzleSolver.WriteDebugMatches();

            puzzleSolver.StartPuzzling();

            int countSeaMonsters = puzzleSolver.CountSeaMonsters();

            return countSeaMonsters.ToString();
        }

        private class PuzzleSolver
        {
            private Lazy<Tile[,]> _puzzle;
            private Dictionary<int, Tile> Tiles { get; }

            private Tile[,] Puzzle => _puzzle.Value;

            public PuzzleSolver()
            {
                Tiles = new Dictionary<int, Tile>();
            }

            public void ParseInput(string input)
            {
                var lines = (from line in input.Split(Environment.NewLine).ToList()
                             select line).ToArray();

                var tileNumberRegex = new Regex(@"^Tile (?'TileNumber'\d+):$");

                for (var idx = 0; idx < lines.Length && !string.IsNullOrWhiteSpace(lines[idx]); ++idx)
                {
                    var match = tileNumberRegex.Match(lines[idx]);

                    if (!match.Success)
                        throw new InvalidOperationException($"Couldn't find tile number in '{lines[idx]}'");

                    var tileNumber = int.Parse(match.Groups["TileNumber"].Value);

                    var tile = new Tile(tileNumber);

                    for (++idx; idx < lines.Length && !string.IsNullOrWhiteSpace(lines[idx]); ++idx)
                        tile.Image.Add(lines[idx]);

                    tile.CalculateChecksums();

                    tile.CalculateSides();

                    Tiles.Add(tileNumber, tile);
                }
            }

            public void FindMatchingTiles()
            {
                foreach (var tile1 in Tiles.Values)
                {
                    foreach (var tile2 in Tiles.Values)
                    {
                        if (tile1.ID == tile2.ID) continue;
                        bool foundMatchingSides =
                            tile1.Sides.Intersect(tile2.Sides).Any();
                        if (foundMatchingSides)
                            tile1.MatchingTiles.Add(tile2);
                    }
                }
            }

            public long CalculateProductOfCornerTileIDs() =>
                Tiles.Values
                    .Where(t => t.IsCorner)
                    .Select(t => (long)t.ID)
                    .Aggregate((a, b) => a * b);

            public void StartPuzzling()
            {
                var size = (int)Math.Sqrt(Tiles.Count);
                _puzzle = new Lazy<Tile[,]>(() => new Tile[size, size]);

                Puzzle[0, 0] = Tiles.Values.First(t => t.IsCorner);

                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        if (x == 0 && y == 0) continue;
                        if (x == 0 || y == 0 || x == size - 1 || y == size - 1)
                        {
                            Puzzle[x, y] = Puzzle[x - 1, y].MatchingTiles.First(t => t.IsEdge);
                        }
                    }
                }
                Puzzle[size - 1, size - 1] = Tiles.Values.Last(t => t.IsCorner);
            }

            public int CountSeaMonsters()
            {
                throw new NotImplementedException();
            }

            public void WriteDebugChecksums()
            {
                var debugOutput = new List<string>();
                foreach (var tile in Tiles.Values)
                {
                    debugOutput.Add($"Tile  : {tile.ID}");
                    debugOutput.Add($"Top   : {tile.Checksum.top,12}");
                    debugOutput.Add($"Left  : {tile.Checksum.left,12}");
                    debugOutput.Add($"Right : {tile.Checksum.right,12}");
                    debugOutput.Add($"Bottom: {tile.Checksum.bottom,12}");
                    debugOutput.Add(string.Empty);
                    debugOutput.Add($"Flipped Top   : {tile.Checksum.flippedTop,4}");
                    debugOutput.Add($"Flipped Left  : {tile.Checksum.flippedLeft,4}");
                    debugOutput.Add($"Flipped Right : {tile.Checksum.flippedRight,4}");
                    debugOutput.Add($"Flipped Bottom: {tile.Checksum.flippedBottom,4}");
                    debugOutput.Add("\n");
                }

                var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                File.WriteAllLines(
                    Path.Combine(rootDir, @"..\\..\\..\\..\\aoc2020.Puzzles", "Input", "solution day20.txt"),
                    debugOutput);
            }

            public void WriteDebugMatches()
            {
                var debugOutput = new List<string>();
                foreach (var tile in Tiles.Values.OrderBy(t => t.MatchingTiles.Count * 10000 + t.ID))
                {
                    debugOutput.Add($"Tile  : {tile.ID} {tile.Position}");
                    foreach (var matchingTile in tile.MatchingTiles.OrderBy(t => t.ID))
                    {
                        debugOutput.Add($"Match : {matchingTile.ID} {matchingTile.Position}");
                    }

                    debugOutput.Add("\n");
                }

                var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                File.WriteAllLines(
                    Path.Combine(rootDir, @"..\\..\\..\\..\\aoc2020.Puzzles", "Input", "solution day20 part 2.txt"),
                    debugOutput);
            }
        }

        private class Tile
        {
            public int ID { get; }
            public List<string> Image { get; }
            public List<ushort> Sides { get; }
            public Checksum Checksum { get; }
            public List<Tile> MatchingTiles { get; }
            public bool IsCorner => MatchingTiles.Count == 2;
            public bool IsEdge => MatchingTiles.Count == 3;
            public bool IsCenter => MatchingTiles.Count == 4;
            public string Position
            {
                get
                {
                    if (IsCorner)
                        return "Ecke";
                    else if (IsEdge)
                        return "Kante";
                    else
                        return "Mitte";
                }
            }

            public Tile(int id)
            {
                ID = id;
                Image = new List<string>();
                Sides = new List<ushort>();
                Checksum = new Checksum();
                MatchingTiles = new List<Tile>();
            }

            public void CalculateSides()
            {
                this.Sides.Add(Convert.ToUInt16(string.Concat(this.Image[0].Select((c) => c == '#' ? '1' : '0')), 2));
                this.Sides.Add(Convert.ToUInt16(string.Concat(this.Image[9].Select((c) => c == '#' ? '1' : '0')), 2));
                this.Sides.Add(Convert.ToUInt16(string.Concat(this.Image.Select(c => c[0] == '#' ? '1' : '0')), 2));
                this.Sides.Add(Convert.ToUInt16(string.Concat(this.Image.Select(c => c[9] == '#' ? '1' : '0')), 2));
                this.Sides.Add(Convert.ToUInt16(string.Concat(this.Image[0].Select((c) => c == '#' ? '1' : '0').Reverse()), 2));
                this.Sides.Add(Convert.ToUInt16(string.Concat(this.Image[9].Select((c) => c == '#' ? '1' : '0').Reverse()), 2));
                this.Sides.Add(Convert.ToUInt16(string.Concat(this.Image.Select(c => c[0] == '#' ? '1' : '0').Reverse()), 2));
                this.Sides.Add(Convert.ToUInt16(string.Concat(this.Image.Select(c => c[9] == '#' ? '1' : '0').Reverse()), 2));
            }

            public void CalculateChecksums()
            {
                this.Checksum.top = Convert.ToUInt16(string.Concat(this.Image[0].Select((c) => c == '#' ? '1' : '0')), 2);
                this.Checksum.bottom = Convert.ToUInt16(string.Concat(this.Image[9].Select((c) => c == '#' ? '1' : '0')), 2);
                this.Checksum.left = Convert.ToUInt16(string.Concat(this.Image.Select(c => c[0] == '#' ? '1' : '0')), 2);
                this.Checksum.right = Convert.ToUInt16(string.Concat(this.Image.Select(c => c[9] == '#' ? '1' : '0')), 2);
                this.Checksum.flippedTop =
                    Convert.ToUInt16(string.Concat(this.Image[0].Select((c) => c == '#' ? '1' : '0').Reverse()), 2);
                this.Checksum.flippedBottom =
                    Convert.ToUInt16(string.Concat(this.Image[9].Select((c) => c == '#' ? '1' : '0').Reverse()), 2);
                this.Checksum.flippedLeft =
                    Convert.ToUInt16(string.Concat(this.Image.Select(c => c[0] == '#' ? '1' : '0').Reverse()), 2);
                this.Checksum.flippedRight =
                    Convert.ToUInt16(string.Concat(this.Image.Select(c => c[9] == '#' ? '1' : '0').Reverse()), 2);
            }
        }
    }
}
