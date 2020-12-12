using aoc2020.Puzzles.Core;
using aoc2020.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using MoreLinq.Extensions;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Rain Risk")]
    public sealed class Day12 : SolutionBase
    {
        public enum Direction
        {
            North = 0,
            East = 90,
            South = 180,
            West = 270
        }
        public enum Move
        {
            East = 'E',
            West = 'W',
            North = 'N',
            South = 'S',
            Left = 'L',
            Right = 'R',
            Forward = 'F'

        }

        public struct Position
        {
            public Position(int northSouth, int eastWest)
            {
                NorthSouth = northSouth;
                EastWest = eastWest;
            }
            public int NorthSouth { get; set; }
            public int EastWest { get; set; }

            public override string ToString()
            {
                return $"North/South: {NorthSouth, 3} East/West {EastWest, 3}";
            }
        }

        public override async Task<string> Part1Async(string input)
        {
            Direction startDirection = Direction.East;
            var instructions = GetLines(input)
                .Select(line => (action: (Move)line.ElementAt(0), value: int.Parse(line.Substring(1))))
                .ToArray();
            int manhattanDistance = CalculateManhattanDistance(instructions, Direction.East);
            return manhattanDistance.ToString();
        }

        private int CalculateManhattanDistance((Move action, int value)[] instructions, Direction direction)
        {
            var position = new Position(0,0);
            var solution = new List<string>();
            solution.Add("current Position               => current Direction => instruction     => new Position                   => new Direction");
            
            foreach (var instruction in instructions)
            {
                string currentPosition = position.ToString();
                string currentDirection = direction.ToString();
                (position, direction) = MoveShip(instruction, position, direction);
                solution.Add($"{currentPosition} => {currentDirection,-17} => {instruction.ToString(),-15} => {position.ToString(),5} => {direction.ToString(), -5}");
            }
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            File.WriteAllLines(Path.Combine(rootDir, @"..\\..\\..\\..\\aoc2020.Puzzles", "Input", "solution day12.txt"), solution);
            return Math.Abs(position.NorthSouth) + Math.Abs(position.EastWest);
        }

        private (Position, Direction) MoveShip((Move action, int value) instruction, Position position, Direction direction)
        {
            if (instruction.action == Move.Right)
            {
                direction = (Direction)(((int) direction + instruction.value) % 360);
            }
            else if (instruction.action == Move.Left)
            {
                direction = (Direction)((360 + (int)direction - instruction.value) % 360);
            }
            else if (instruction.action == Move.Forward)
            {
                position.EastWest = direction switch
                {
                    Direction.East => position.EastWest += instruction.value,
                    Direction.West => position.EastWest -= instruction.value,
                    _ => position.EastWest += 0
                };
                position.NorthSouth = direction switch
                {
                    Direction.North => position.NorthSouth += instruction.value,
                    Direction.South => position.NorthSouth -= instruction.value,
                    _ => position.NorthSouth += 0
                };
            }
            else
            {
                position.EastWest = instruction.action switch
                {
                    Move.East => position.EastWest += instruction.value,
                    Move.West => position.EastWest -= instruction.value,
                _ => position.EastWest += 0
                };
                position.NorthSouth = instruction.action switch
                {
                    Move.North => position.NorthSouth += instruction.value,
                    Move.South => position.NorthSouth -= instruction.value,
                    _ => position.NorthSouth += 0
                };
            }

            return (position, direction);
        }

        public override async Task<string> Part2Async(string input)
        {
            throw new NotImplementedException();
        }
    }
}
