using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using aoc2020.Puzzles.Core;
using static aoc2020.Puzzles.Solutions.Day09;
using static aoc2020.Puzzles.Solutions.Day10;
using static aoc2020.Puzzles.Solutions.Day11;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Tractor Beam")]
    public sealed class Day19 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            var memory = Day09.IntMachineBase.ParseProgram(input);
            var sum = 0;
            for (var y = 0; y < 50; y++)
            {
                for (var x = 0; x < 50; x++)
                {
                    if (IsUpdateProgressNeeded()) { await UpdateProgressAsync(x + y * 50, 50 * 50); }

                    var isPulling = IsPulling(memory, x, y);
                    sum += isPulling ? 1 : 0;
                }
            }

            return sum.ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            var stepRight = new Day10.Point(1, 0);
            var stepDown = new Day10.Point(0, 1);
            var memory = Day09.IntMachineBase.ParseProgram(input);
            var startTop = GetStartPoint(memory);
            var startBottom = startTop;
            var margins = new List<(Day10.Point Top, Day10.Point Bottom)>();

            var size = 100 - 1;
            Day10.Point targetPoint;

            while (true)
            {
                var top = GetTop(memory, startTop);
                var bottom = GetBottom(memory, startBottom);
                margins.Add((top, bottom));
                startTop = top + stepRight;
                startBottom = bottom + stepRight + stepDown;

                if (IsUpdateProgressNeeded()) { await UpdateProgressAsync(bottom.Y - top.Y, size * 2); }

                var currentMarginIndex = margins.Count - 1;
                if (currentMarginIndex >= size && margins[currentMarginIndex - size].Bottom.Y >= top.Y + size)
                {
                    targetPoint = new Day10.Point(margins[currentMarginIndex - size].Top.X, top.Y);
                    break;
                }
            }

            return (targetPoint.X * 10000 + targetPoint.Y).ToString();
        }

        private static Day10.Point GetTop(long[] memory, Day10.Point startPoint)
        {
            var top = startPoint.Y;

            if (IsPulling(memory, startPoint.X, top))
            {
                while (IsPulling(memory, startPoint.X, top)) { top--; }
                top++;
            }
            else
            {
                while (!IsPulling(memory, startPoint.X, top)) { top++; }
            }


            return new Day10.Point(startPoint.X, top);
        }

        private static Day10.Point GetBottom(long[] memory, Day10.Point startPoint)
        {
            var bottom = startPoint.Y;

            if (IsPulling(memory, startPoint.X, bottom))
            {
                while (IsPulling(memory, startPoint.X, bottom)) { bottom++; }
                bottom--;
            }
            else
            {
                while (!IsPulling(memory, startPoint.X, bottom)) { bottom--; }
            }


            return new Day10.Point(startPoint.X, bottom);
        }

        private static Day10.Point GetStartPoint(long[] memory)
        {
            for (var y = 1; y < 10; y++)
            {
                for (var x = 1; x < 10; x++)
                {
                    if (IsPulling(memory, x, y))
                    {
                        return new Day10.Point(x, y);
                    }
                }
            }
            throw new IndexOutOfRangeException();
        }

        private static bool IsPulling(long[] memory, int x, int y)
        {
            var intMachine = new Day11.SynchronousIntMachine(memory);
            intMachine.InputQueue.Enqueue(x);
            intMachine.InputQueue.Enqueue(y);
            intMachine.RunUntilBlockOrComplete();
            return intMachine.OutputQueue.Dequeue() == 1;
        }
    }
}
