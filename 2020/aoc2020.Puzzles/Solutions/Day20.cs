using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Jurassic Jigsaw")]
    public sealed class Day20 : SolutionBase
    {
        struct Checksum
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
            ushort mask = (ushort) (1 << bitPosition);
            return (ushort) unchecked(value | mask);
        }

        private static ushort UnsetSpecificBitAtPosition(ushort value, int bitPosition)
        {
            ushort mask = (ushort) (1 << bitPosition);
            return (ushort) unchecked(value & ~mask);
        }


        public override async Task<string> Part1Async(string input)
        {
            var lines = (from line in input.Replace("\r", "").Split("\n").ToList()
                //where !string.IsNullOrWhiteSpace(line)
                select line).ToArray();

            var nameRegex = new Regex(@"^Tile (?'TileNumber'\d+):$");
            var tiles = new Dictionary<int, List<string>>();
            var tileChecksums = new Dictionary<int, Checksum>();
            var tileSides = new Dictionary<int, List<ushort>>();
            

            for (var idx = 0; idx < lines.Length &&!string.IsNullOrWhiteSpace(lines[idx]); ++idx)
            {
                var match = nameRegex.Match(lines[idx]);

                if (!match.Success)
                    throw new InvalidOperationException($"Couldn't find tile number in '{lines[idx]}'");

                var tile = new List<string>();
                var checksum = new Checksum();

                for (++idx; idx < lines.Length && !string.IsNullOrWhiteSpace(lines[idx]); ++idx)
                {
                    tile.Add(lines[idx]);
                }

                var tileNumber = int.Parse(match.Groups["TileNumber"].Value);
                tiles.Add(tileNumber, tile);

                checksum.top = Convert.ToUInt16(string.Concat(tile[0].Select((c) => c == '#' ? '1' : '0')),2);
                checksum.bottom = Convert.ToUInt16(string.Concat(tile[9].Select((c) => c == '#' ? '1' : '0')), 2);
                checksum.left = Convert.ToUInt16(string.Concat(tile.Select(c => c[0] == '#' ? '1' : '0')), 2);
                checksum.right = Convert.ToUInt16(string.Concat(tile.Select(c => c[9] == '#' ? '1' : '0')), 2);
                checksum.flippedTop = Convert.ToUInt16(string.Concat(tile[0].Select((c) => c == '#' ? '1' : '0').Reverse()), 2);
                checksum.flippedBottom = Convert.ToUInt16(string.Concat(tile[9].Select((c) => c == '#' ? '1' : '0').Reverse()), 2);
                checksum.flippedLeft = Convert.ToUInt16(string.Concat(tile.Select(c => c[0] == '#' ? '1' : '0').Reverse()), 2);
                checksum.flippedRight = Convert.ToUInt16(string.Concat(tile.Select(c => c[9] == '#' ? '1' : '0').Reverse()), 2);
                tileChecksums.Add(tileNumber, checksum);
                var sides = new List<ushort>();
                sides.Add(Convert.ToUInt16(string.Concat(tile[0].Select((c) => c == '#' ? '1' : '0')), 2));
                sides.Add(Convert.ToUInt16(string.Concat(tile[9].Select((c) => c == '#' ? '1' : '0')), 2));
                sides.Add(Convert.ToUInt16(string.Concat(tile.Select(c => c[0] == '#' ? '1' : '0')), 2));
                sides.Add(Convert.ToUInt16(string.Concat(tile.Select(c => c[9] == '#' ? '1' : '0')), 2));
                sides.Add(Convert.ToUInt16(string.Concat(tile[0].Select((c) => c == '#' ? '1' : '0').Reverse()), 2));
                sides.Add(Convert.ToUInt16(string.Concat(tile[9].Select((c) => c == '#' ? '1' : '0').Reverse()), 2));
                sides.Add(Convert.ToUInt16(string.Concat(tile.Select(c => c[0] == '#' ? '1' : '0').Reverse()), 2));
                sides.Add(Convert.ToUInt16(string.Concat(tile.Select(c => c[9] == '#' ? '1' : '0').Reverse()), 2));
                tileSides.Add(tileNumber, sides);
            }

            long result = 0;

            var debugOutput = new List<string>();
            if (Debugger.IsAttached)
            {
                foreach (var tileChecksum in tileChecksums)
                {
                    debugOutput.Add($"Tile  : {tileChecksum.Key}");
                    debugOutput.Add($"Top   : {tileChecksum.Value.top, 12}");
                    debugOutput.Add($"Left  : {tileChecksum.Value.left, 12}");
                    debugOutput.Add($"Right : {tileChecksum.Value.right, 12}");
                    debugOutput.Add($"Bottom: {tileChecksum.Value.bottom, 12}");
                    debugOutput.Add(string.Empty);
                    debugOutput.Add($"Flipped Top   : {tileChecksum.Value.flippedTop,4}");
                    debugOutput.Add($"Flipped Left  : {tileChecksum.Value.flippedLeft,4}");
                    debugOutput.Add($"Flipped Right : {tileChecksum.Value.flippedRight,4}");
                    debugOutput.Add($"Flipped Bottom: {tileChecksum.Value.flippedBottom,4}");
                    debugOutput.Add("\n");
                }
                var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                File.WriteAllLines(
                    Path.Combine(rootDir, @"..\\..\\..\\..\\aoc2020.Puzzles", "Input", "solution day20.txt"), debugOutput);

            }

            var matchingTiles = new Dictionary<int, List<int>>();

            for (int idx1 = 0; idx1 < tileSides.Count; idx1++)
            {
                var tileMatches = new List<int>();
                int countMatches = 0;
                
                for (int idx2 = 0; idx2 < tileSides.Count; idx2++)
                {
                    if (idx1 == idx2) continue;
                    countMatches = tileSides.ElementAt(idx1).Value.Intersect(tileSides.ElementAt(idx2).Value).Count();
                    if(countMatches>0)
                        tileMatches.Add(tileSides.ElementAt(idx2).Key);
                }

                if (tileMatches.Count > 0)
                    matchingTiles.Add(tileSides.ElementAt(idx1).Key, tileMatches);
            }

            result = matchingTiles.Where(kvp => kvp.Value.Count == 2).Select(kvp => (long)kvp.Key).Aggregate((a, b) => a * b);

            return result.ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            throw new NotImplementedException();
        }
    }
}
