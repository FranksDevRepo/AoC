using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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
            public Position(int eastWest, int northSouth)
            {
                EastWest = eastWest;
                NorthSouth = northSouth;
            }
            public int EastWest { get; set; }
            public int NorthSouth { get; set; }

            public override string ToString()
            {
                return $"East/West {EastWest,3} North/South: {NorthSouth,3}";
            }
        }

        public override async Task<string> Part1Async(string input)
        {
            var instructions = GetLines(input)
                .Select(line => (action: (Move)line[0], value: int.Parse(line[1..])))
                .ToArray();
            int manhattanDistance = CalculateManhattanDistance(instructions, Direction.East);
            return manhattanDistance.ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            Position wayPoint = new Position(10, 1);
            Position shipPosition = new Position(0, 0);
            var instructions = GetLines(input)
                .Select(line => (action: (Move)line[0], value: int.Parse(line[1..])))
                .ToArray();
            int manhattanDistance = CalculateManhattanDistance(instructions, wayPoint, shipPosition);
            return manhattanDistance.ToString();
        }

        private int CalculateManhattanDistance((Move action, int value)[] instructions, Direction direction)
        {
            var position = new Position(0, 0);

            var solution = new List<string>();
            if (Debugger.IsAttached)
            {
                solution.Add(
                    "current Position               => current Direction => instruction     => new Position                   => new Direction");
            }

            foreach (var instruction in instructions)
            {
                string currentPosition = position.ToString();
                string currentDirection = direction.ToString();
                (position, direction) = MoveShip(instruction, position, direction);
                if (Debugger.IsAttached)
                {
                    solution.Add(
                        $"{currentPosition} => {currentDirection,-17} => {instruction,-15} => {position,5} => {direction,-5}");
                }
            }

            if (Debugger.IsAttached)
            {
                var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                File.WriteAllLines(
                    Path.Combine(rootDir, @"..\\..\\..\\..\\aoc2020.Puzzles", "Input", "solution day12.txt"), solution);
            }

            return Math.Abs(position.NorthSouth) + Math.Abs(position.EastWest);
        }

        private int CalculateManhattanDistance((Move action, int value)[] instructions, Position wayPoint, Position shipPosition)
        {
            foreach (var instruction in instructions)
            {
                if ("NSEWLR".Contains((char)instruction.action))
                {
                    wayPoint = CalculateWayPoint(wayPoint, instruction);
                }
                else
                {
                    shipPosition = MoveShip(shipPosition, wayPoint, instruction);
                }
            }
            return Math.Abs(shipPosition.NorthSouth) + Math.Abs(shipPosition.EastWest);
        }

        private (Position, Direction) MoveShip((Move action, int value) instruction, Position position, Direction direction)
        {
            if (instruction.action == Move.Right)
            {
                direction = (Direction)(((int)direction + instruction.value) % 360);
            }
            else if (instruction.action == Move.Left)
            {
                direction = (Direction)((360 + (int)direction - instruction.value) % 360);
            }
            else if (instruction.action == Move.Forward)
            {
                position.EastWest = direction switch
                {
                    Direction.East => position.EastWest + instruction.value,
                    Direction.West => position.EastWest - instruction.value,
                    _ => position.EastWest + 0
                };
                position.NorthSouth = direction switch
                {
                    Direction.North => position.NorthSouth + instruction.value,
                    Direction.South => position.NorthSouth - instruction.value,
                    _ => position.NorthSouth + 0
                };
            }
            else
            {
                position.EastWest = instruction.action switch
                {
                    Move.East => position.EastWest + instruction.value,
                    Move.West => position.EastWest - instruction.value,
                    _ => position.EastWest + 0
                };
                position.NorthSouth = instruction.action switch
                {
                    Move.North => position.NorthSouth + instruction.value,
                    Move.South => position.NorthSouth - instruction.value,
                    _ => position.NorthSouth + 0
                };
            }

            return (position, direction);
        }

        private Position MoveShip(Position shipPosition, Position wayPoint, (Move action, int value) instruction)
        {
            switch (instruction.action)
            {
                case Move.Forward:
                    shipPosition.EastWest += wayPoint.EastWest * instruction.value;
                    shipPosition.NorthSouth += wayPoint.NorthSouth * instruction.value;
                    break;
                default:
                    throw new InvalidOperationException($"{instruction.action} not expected.");
            }

            return shipPosition;
        }

        private Position CalculateWayPoint(Position wayPoint, (Move action, int value) instruction)
        {
            switch (instruction.action)
            {
                case Move.North:
                    wayPoint.NorthSouth += instruction.value;
                    break;
                case Move.South:
                    wayPoint.NorthSouth -= instruction.value;
                    break;
                case Move.East:
                    wayPoint.EastWest += instruction.value;
                    break;
                case Move.West:
                    wayPoint.EastWest -= instruction.value;
                    break;
                case Move.Left:
                    for (var i = 0L; i < instruction.value / 90; i++)
                    {
                        var newWaypoint = new Position(-wayPoint.NorthSouth, wayPoint.EastWest);
                        wayPoint.EastWest = newWaypoint.EastWest;
                        wayPoint.NorthSouth = newWaypoint.NorthSouth;
                    }
                    break;
                case Move.Right:
                    for (var i = 0L; i < instruction.value / 90; i++)
                    {
                        var newWaypoint = new Position(wayPoint.NorthSouth, -wayPoint.EastWest);
                        wayPoint.EastWest = newWaypoint.EastWest;
                        wayPoint.NorthSouth = newWaypoint.NorthSouth;
                    }
                    break;
            }

            return wayPoint;
        }
    }
}
