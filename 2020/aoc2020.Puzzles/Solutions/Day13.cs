using MoreLinq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aoc2020.Puzzles.Core;
using static aoc2020.Puzzles.Solutions.Day09;
using static aoc2020.Puzzles.Solutions.Day10;
using static aoc2020.Puzzles.Solutions.Day11;
using static aoc2020.Puzzles.Solutions.Day11.SynchronousIntMachine;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Care Package")]
    public sealed class Day13 : SolutionBase
    {
        public List<List<(Day10.Point, long)>> VisualizationFrames { get; private set; }

        public override async Task<string> Part1Async(string input)
        {
            var intMachine = new Day11.SynchronousIntMachine(input);
            var tiles = await LoadTiles(intMachine);
            int blockCount = tiles.SelectMany(x => x).Count(x => x == Tile.Block);

            return blockCount.ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            VisualizationFrames = new List<List<(Day10.Point, long)>>();
            var memory = Day09.IntMachineBase.ParseProgram(input);
            memory[0] = 2; // Insert Coin
            var intMachine = new Day11.SynchronousIntMachine(memory);
            var tiles = await LoadTiles(intMachine);
            int maxBlockCount = tiles.SelectMany(x => x).Count(x => x == Tile.Block);

            long score = 0;
            var blockCount = maxBlockCount;
            var ball = Day10.Point.Empty;
            var paddle = Day10.Point.Empty;
            var frame = new List<(Day10.Point, long)>();
            Day11.SynchronousIntMachine.ReturnCode returnCode;
            while ((returnCode = intMachine.RunUntilBlockOrComplete()) != Day11.SynchronousIntMachine.ReturnCode.Completed)
            {
                if (IsUpdateProgressNeeded()) { await UpdateProgressAsync(maxBlockCount - blockCount, maxBlockCount); }

                switch (returnCode)
                {
                    case Day11.SynchronousIntMachine.ReturnCode.WaitingForInput:
                        VisualizationFrames.Add(frame); frame = new List<(Day10.Point, long)>();
                        var joystickInput = ball.X.CompareTo(paddle.X);
                        intMachine.InputQueue.Enqueue(joystickInput);
                        break;
                    case Day11.SynchronousIntMachine.ReturnCode.WrittenOutput:
                        HandleTileChange(intMachine, tiles, frame, ref score, ref blockCount, ref ball, ref paddle);
                        break;
                }
            }
            VisualizationFrames.Add(frame);

            return score.ToString();
        }

        private void HandleTileChange(Day11.SynchronousIntMachine intMachine, Tile[][] tiles, List<(Day10.Point, long)> frame,
            ref long score, ref int blockCount, ref Day10.Point ball, ref Day10.Point paddle)
        {
            var (x, y, t) = GetTile(intMachine);
            frame.Add((new Day10.Point(x, y), t));
            if (x == -1)
            {
                score = t;
            }
            else
            {
                var tile = (Tile)t;
                if (tile != Tile.Block && tiles[x][y] == Tile.Block) { blockCount--; }
                tiles[x][y] = tile;

                if (tile == Tile.Ball)
                {
                    ball = new Day10.Point(x, y);
                }
                else if (tile == Tile.Paddle)
                {
                    paddle = new Day10.Point(x, y);
                }
            }
        }

        private async Task<Tile[][]> LoadTiles(Day11.SynchronousIntMachine intMachine)
        {
            var tilesDict = new Dictionary<Day10.Point, long>();
            while (intMachine.RunUntilBlockOrComplete() == Day11.SynchronousIntMachine.ReturnCode.WrittenOutput)
            {
                if (IsUpdateProgressNeeded()) { await UpdateProgressAsync(); }
                var (x, y, tile) = GetTile(intMachine);
                tilesDict[new Day10.Point(x, y)] = tile;
            }

            VisualizationFrames?.Add(tilesDict.Select(t => (t.Key, t.Value)).ToList());

            var width = tilesDict.Keys.Max(p => p.X) + 1;
            var height = tilesDict.Keys.Max(p => p.Y) + 1;
            var tiles = Enumerable.Range(0, width).Select(x => new Tile[height]).ToArray();
            tilesDict.Where(t => t.Key.X >= 0).ForEach(t => tiles[t.Key.X][t.Key.Y] = (Tile)t.Value);

            return tiles;
        }

        private (int x, int y, long t) GetTile(Day11.SynchronousIntMachine intMachine)
        {
            while (intMachine.OutputQueue.Count < 3) { intMachine.RunUntilBlockOrComplete(); }
            var x = (int)intMachine.OutputQueue.Dequeue();
            var y = (int)intMachine.OutputQueue.Dequeue();
            var tile = intMachine.OutputQueue.Dequeue();

            return (x, y, tile);
        }

        public enum Tile
        {
            Empty = 0,
            Wall = 1,
            Block = 2,
            Paddle = 3,
            Ball = 4
        }
    }
}
