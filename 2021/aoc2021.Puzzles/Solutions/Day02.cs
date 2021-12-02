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
        Backward,
        Up,
        Down
    }

    public struct Position
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
                Command.Backward => this.Horizontal - units,
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
        var lines = GetLines(input);
        var instructionParser = new Regex("^(?'command'\\b\\w*?\\b) (?'units'\\d*)", RegexOptions.Compiled);
        var position = new Position(0, 0);
        foreach (var line in lines)
        {
            var match = instructionParser.Match(line);
            if (!match.Success)
                continue;
            Enum.TryParse(match.Groups["command"].Value, true, out Command command);
            var units = Convert.ToInt32(match.Groups["units"].Value);
            position.Move(command, units);

        }
        return position.ToString();
    }

    public override string Part2(string input)
    {
        throw new NotImplementedException();
    }
}