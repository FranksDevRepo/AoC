using aoc2021.Puzzles.Core;
using System;
using System.Text.RegularExpressions;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Dive!")]
public sealed class Day02 : SolutionBase
{
    public enum Command
    {
        Forward,
        Up,
        Down
    }

    public interface IPosition
    {
        void Move(Command command, int units);
    }

    public struct Position : IPosition
    {
        public int Horizontal { get; set; }
        public int Depth { get; set; }

        public Position(int horizontal, int depth)
        {
            this.Horizontal = horizontal;
            this.Depth = depth;
        }

        public void Move(Command command, int units)
        {
            this.Horizontal = command switch
            {
                Command.Forward => this.Horizontal + units,
                _ => this.Horizontal
            };
            this.Depth = command switch
            {
                Command.Up => this.Depth - units,
                Command.Down => this.Depth + units,
                _ => this.Depth
            };
        }

        public override string ToString()
        {
            return (this.Horizontal * this.Depth).ToString();
        }
    }

    public override string Part1(string input)
    {
        IPosition position = new Position(0, 0);
        position = ProcessInputAndMovePosition(input, position);
        return position.ToString();
    }

    private static IPosition ProcessInputAndMovePosition(string input, IPosition position)
    {
        var lines = GetLines(input);
        var instructionParser = new Regex("^(?'command'\\b\\w*?\\b) (?'units'\\d*)", RegexOptions.Compiled);
        foreach (var line in lines)
        {
            var match = instructionParser.Match(line);
            if (!match.Success)
                continue;
            Enum.TryParse(match.Groups["command"].Value, true, out Command command);
            var units = Convert.ToInt32(match.Groups["units"].Value);
            position.Move(command, units);
        }

        return position;
    }

    public override string Part2(string input)
    {
        IPosition position = new AimedPosition(0, 0, 0);
        position = ProcessInputAndMovePosition(input, position);
        return position.ToString();
    }

    public struct AimedPosition : IPosition
    {
        public int Horizontal { get; set; }
        public int Depth { get; set; }
        public int Aim { get; set; }

        public AimedPosition(int horizontal, int depth, int aim)
        {
            this.Horizontal = horizontal;
            this.Depth = depth;
            this.Aim = aim;
        }

        public void Move(Command command, int units)
        {
            this.Aim = command switch
            {
                Command.Down => this.Aim + units,
                Command.Up => this.Aim - units,
                _ => this.Aim
            };
            (this.Horizontal, this.Depth) = command switch
            {
                Command.Forward => (this.Horizontal + units, this.Depth + this.Aim * units),
                _ => (this.Horizontal, this.Depth)
            };
        }

        public override string ToString()
        {
            return (this.Horizontal * this.Depth).ToString();
        }
    }
}