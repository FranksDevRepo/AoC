using aoc2021.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Transparent Origami")]
public sealed class Day13 : SolutionBase
{
    public override string Part1(string input)
    {
        HashSet<Coordinate> paper;
        List<Instruction> foldInstructions;
        (paper, foldInstructions) = ParseInput(input);
        // New positions of folded dots easily calculated with: foldPos - (dotPos - foldPos) ???

        var foldedPaper = FoldPaper(paper, foldInstructions);

        return foldedPaper.First().Count.ToString();
    }

    public override string Part2(string input)
    {
        HashSet<Coordinate> paper;
        List<Instruction> foldInstructions;
        (paper, foldInstructions) = ParseInput(input);

        var foldedPaper = FoldPaper(paper, foldInstructions);

        var result = foldedPaper.Last();
        return DisplayResult(result);
    }

    private static string DisplayResult(HashSet<Coordinate> result)
    {
        var maxX = result.Max(c => c.X);
        var maxY = result.Max(c => c.Y);

        StringBuilder capitalLetters = new();
        for (int y = 0; y <= maxY; y++)
        {
            for (int x = 0; x <= maxX; x++)
            {
                var currentCoordinate = new Coordinate(x, y);
                capitalLetters.Append(result.Contains(currentCoordinate) ? '#' : '.');
            }
            capitalLetters.AppendLine();
        }

        return capitalLetters.ToString();
    }

    private static (HashSet<Coordinate>, List<Instruction>) ParseInput(string input)
    {
        var paper = GetLines(input)
            .Where(l => l.Contains(',', StringComparison.InvariantCulture))
            .Select(l => l.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .Select(parts => new Coordinate(Convert.ToInt32(parts[0]), int.Parse(parts[1])))
            .ToHashSet();
        List<Instruction> foldInstructions = new();
        foreach (var line in GetLines(input).Where(l => l.Contains('=', StringComparison.InvariantCulture)))
        {
            var foldInstructionsRegex = new Regex(@"^(?:fold along )(?<Fold>[x|y])=(?<Position>\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var foldInstructionMatch = foldInstructionsRegex.Match(line);

            if (!foldInstructionMatch.Success)
                continue;

            var fold = foldInstructionMatch.Groups["Fold"].Value switch
            {
                "x" => Fold.Left,
                "y" => Fold.Up,
                _ => throw new NotImplementedException()
            };
            var position = int.Parse(foldInstructionMatch.Groups["Position"].Value);
            var instruction = new Instruction(fold, fold == Fold.Up ? Line.Horizontal : Line.Vertical, position);
            foldInstructions.Add(instruction);
        }
        return (paper, foldInstructions);
    }

    private IEnumerable<HashSet<Coordinate>> FoldPaper(HashSet<Coordinate> paper, IEnumerable<Instruction> instructions)
    {
        foreach(var instruction in instructions)
        {
            paper = instruction.Fold switch
            {
                Fold.Up => FoldUp(paper, instruction.Position),
                Fold.Left => FoldLeft(paper, instruction.Position),
                _ => throw new NotImplementedException()
            };
            yield return paper;
        }
    }

    private HashSet<Coordinate> FoldLeft(HashSet<Coordinate> paper, int position)
    {
        HashSet<Coordinate> foldedPaper = new();
        foreach (var coordinate in paper)
        {
            if (coordinate.X < position)
                foldedPaper.Add(coordinate);
            else
                foldedPaper.Add(new Coordinate(position - (coordinate.X - position), coordinate.Y));
        }
        return foldedPaper;
    }

    private HashSet<Coordinate> FoldUp(HashSet<Coordinate> paper, int position)
    {
        HashSet<Coordinate> foldedPaper = new();
        foreach(var coordinate in paper)
        {
            if(coordinate.Y < position)
                foldedPaper.Add(coordinate);
            else
                foldedPaper.Add(new Coordinate(coordinate.X, position - (coordinate.Y - position)));
        }
        return foldedPaper;
    }


    public record Coordinate(int X, int Y);

    public record Instruction(Fold Fold, Line Line, int Position);

    public enum Fold
    {
        Left,
        Up
    }

    public enum Line
    {
        Horizontal,
        Vertical
    }
}