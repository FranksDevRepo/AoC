using aoc2020.Puzzles.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Seating System")]
    public sealed class Day11 : SolutionBase
    {
        public enum Tile
        {
            Floor = '.',
            EmptySeat = 'L',
            OccupiedSeat = '#'
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right,
            LeftUp,
            LeftDown,
            RightUp,
            RightDown
        }

        public override async Task<string> Part1Async(string input)
        {
            var seatPlan = GetSeatPlan(input);
            int occupiedSeats = ApplyRules(seatPlan, CountOccupiedAdjacentSeats, 4);

            return occupiedSeats.ToString();
        }

        private int ApplyRules(Tile[][] seatPlan, Func<Tile[][], int, int, int> countOccupiedSeatsFunc,
            int maxOccupiedSeats)
        {
            var currentSeatPlan = seatPlan.Select(s => s.ToArray()).ToArray();
            int countOccupiedSeats = 0;
            do
            {
                for (int rowIndex = 0; rowIndex < seatPlan.Length; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < seatPlan[rowIndex].Length; colIndex++)
                    {
                        int occupiedAdjacentSeats = countOccupiedSeatsFunc(currentSeatPlan, rowIndex, colIndex);
                        if (currentSeatPlan[rowIndex][colIndex] == Tile.EmptySeat && occupiedAdjacentSeats == 0)
                            seatPlan[rowIndex][colIndex] = Tile.OccupiedSeat;
                        else if (currentSeatPlan[rowIndex][colIndex] == Tile.OccupiedSeat &&
                                 occupiedAdjacentSeats >= maxOccupiedSeats)
                            seatPlan[rowIndex][colIndex] = Tile.EmptySeat;
                    }
                }

                bool isEqual = Compare(currentSeatPlan, seatPlan);
                if (isEqual)
                    break;
                else
                    currentSeatPlan = seatPlan.Select(s => s.ToArray()).ToArray();
            } while (true);

            for (int row = 0; row < seatPlan.Length; row++)
            {
                for (int col = 0; col < seatPlan[row].Length; col++)
                    countOccupiedSeats += seatPlan[row][col] == Tile.OccupiedSeat ? 1 : 0;
            }

            return countOccupiedSeats;
        }

        private bool Compare(Tile[][] currentSeatPlan, Tile[][] seatPlan)
        {
            bool isEqual = true;
            for (int rowIndex = 0; rowIndex < seatPlan.Length; rowIndex++)
            {
                for (int colIndex = 0; colIndex < seatPlan[rowIndex].Length; colIndex++)
                {
                    if (currentSeatPlan[rowIndex][colIndex] != seatPlan[rowIndex][colIndex])
                    {
                        isEqual = false;
                        break;
                    }
                    if (!isEqual) break;
                }
            }
            return isEqual;
        }

        private int CountOccupiedAdjacentSeats(Tile[][] seatPlan, int rowIndex, int colIndex)
        {
            int countOccupiedAdjacentSeats = 0;

            for (int row = Math.Max(rowIndex - 1, 0); row < Math.Min(rowIndex + 2, seatPlan.Length); row++)
            {
                for (int col = Math.Max(colIndex - 1, 0); col < Math.Min(colIndex + 2, seatPlan[row].Length); col++)
                {
                    if (row == rowIndex && col == colIndex) continue;
                    countOccupiedAdjacentSeats += seatPlan[row][col] switch
                    {
                        Tile.OccupiedSeat => 1,
                        _ => 0
                    };
                }
            }
            return countOccupiedAdjacentSeats;
        }

        private Tile[][] GetSeatPlan(string input)
        {
            var seatPlan = input
                .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(row => row
                    .Select(c => c switch
                    {
                        'L' => Tile.EmptySeat,
                        '#' => Tile.OccupiedSeat,
                        _ => Tile.Floor,

                    }).ToArray()).ToArray();
            return seatPlan;
        }

        public override async Task<string> Part2Async(string input)
        {
            var seatPlan = GetSeatPlan(input);
            int occupiedSeats = ApplyRules(seatPlan, CountOccupiedVisibleSeats, 5);

            return occupiedSeats.ToString();
        }

        private int CountOccupiedVisibleSeats(Tile[][] seatPlan, int rowIndex, int colIndex)
        {
            int countOccupiedAdjacentSeats = 0;
            countOccupiedAdjacentSeats += CountOccupiedVisibleSeats(seatPlan, rowIndex, colIndex, Direction.Up);
            countOccupiedAdjacentSeats += CountOccupiedVisibleSeats(seatPlan, rowIndex, colIndex, Direction.Down);
            countOccupiedAdjacentSeats += CountOccupiedVisibleSeats(seatPlan, rowIndex, colIndex, Direction.Left);
            countOccupiedAdjacentSeats += CountOccupiedVisibleSeats(seatPlan, rowIndex, colIndex, Direction.Right);
            countOccupiedAdjacentSeats += CountOccupiedVisibleSeats(seatPlan, rowIndex, colIndex, Direction.LeftUp);
            countOccupiedAdjacentSeats += CountOccupiedVisibleSeats(seatPlan, rowIndex, colIndex, Direction.LeftDown);
            countOccupiedAdjacentSeats += CountOccupiedVisibleSeats(seatPlan, rowIndex, colIndex, Direction.RightUp);
            countOccupiedAdjacentSeats += CountOccupiedVisibleSeats(seatPlan, rowIndex, colIndex, Direction.RightDown);
            return countOccupiedAdjacentSeats;
        }

        private int CountOccupiedVisibleSeats(Tile[][] seatPlan, int rowIndex, int colIndex, Direction direction)
        {
            var (row, col) = direction switch
            {
                Direction.Up => (row: -1, col: 0),
                Direction.Down => (row: 1, col: 0),
                Direction.Left => (row: 0, col: -1),
                Direction.Right => (row: 0, col: 1),
                Direction.LeftUp => (row: -1, col: -1),
                Direction.LeftDown => (row: 1, col: -1),
                Direction.RightUp => (row: -1, col: 1),
                Direction.RightDown => (row: 1, col: 1)
            };
            do
            {
                rowIndex += row;
                if (rowIndex < 0 || rowIndex > seatPlan.Length - 1)
                    break;
                do
                {
                    colIndex += col;
                    if (colIndex < 0 || colIndex > seatPlan[rowIndex].Length - 1)
                        break;
                    if (seatPlan[rowIndex][colIndex] == Tile.OccupiedSeat)
                        return 1;

                    if (seatPlan[rowIndex][colIndex] == Tile.EmptySeat)
                        return 0;

                } while (col != 0 && direction != Direction.RightUp && direction != Direction.LeftUp && direction != Direction.LeftDown && direction != Direction.RightDown);

                if (colIndex < 0 || colIndex > seatPlan[rowIndex].Length - 1)
                    break;
            } while (row != 0);

            return 0;
        }
    }
}
