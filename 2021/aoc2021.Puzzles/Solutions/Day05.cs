using aoc2021.Puzzles.Core;
using aoc2021.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Hydrothermal Venture")]
public sealed class Day05 : SolutionBase
{
    public override string Part1(string input)
    {
        var diagram = ParseInputAndCreateDiagram(input);

        return diagram.CountOverlappingPoints().ToString();
    }

    private static Diagram ParseInputAndCreateDiagram(string input, bool considerDiagonalLines = false)
    {
        var lines = GetLines(input);

        var regexLineOfVent = new Regex(@"^(?'start'\d{1,3},\d{1,3}) -> (?'end'\d{1,3},\d{1,3})", RegexOptions.Compiled);
        List<Line> linesOfVent = new();

        foreach (var line in lines)
        {
            var match = regexLineOfVent.Match(line);
            if (!match.Success) continue;
            var start = new Coordinate(match.Groups["start"].Value);
            var end = new Coordinate(match.Groups["end"].Value);
            var lineOfVent = new Line(start, end);
            linesOfVent.Add(lineOfVent);
        }

        var diagram = new Diagram(linesOfVent);
        diagram.Draw(considerDiagonalLines);
        return diagram;
    }

    public override string Part2(string input)
    {
        var diagram = ParseInputAndCreateDiagram(input, true);

        return diagram.CountOverlappingPoints().ToString();
    }
}

public struct Coordinate
{
    public int X { get; }
    public int Y { get; }

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Coordinate(string coordinate)
    {
        var coordinates = coordinate.Split(',');
        X = Convert.ToInt32(coordinates.First());
        Y = Convert.ToInt32(coordinates.Last());
    }
}

public struct Line
{
    public Coordinate Start { get; }
    public Coordinate End { get; }

    public Line(Coordinate start, Coordinate end)
    {
        if (start.X <= end.X && start.Y <= end.Y)
        {
            Start = start;
            End = end;
        }
        else
        {
            Start = end;
            End = start;
        }
    }

    public bool IsHorizontal()
        => Start.Y == End.Y;

    public bool IsVertical()
        => Start.X == End.X;

    public bool IsDiagonal()
        => !IsHorizontal() && !IsVertical();
}

public class Diagram
{
    private int[,] _diagram;
    public List<Line> LinesOfVent { get; }

    public Diagram(List<Line> linesOfVent)
    {
        LinesOfVent = linesOfVent;
    }

    public int MaxX => LinesOfVent.Max(l => Math.Max(l.Start.X, l.End.X));
    public int MaxY => LinesOfVent.Max(l => Math.Max(l.Start.Y, l.End.Y));

    public void Draw(bool considerDiagonalLines = false)
    {
        _diagram = new int[MaxY + 1, MaxX + 1];
        foreach (var lineOfVent in LinesOfVent)
        {
            DrawLine(lineOfVent, considerDiagonalLines);
        }
    }

    private void DrawLine(Line lineOfVent, bool considerDiagonalLines = false)
    {
        if (!considerDiagonalLines && lineOfVent.IsDiagonal())
            return;

        if (lineOfVent.IsHorizontal())
        {
            for (int x = lineOfVent.Start.X; x <= lineOfVent.End.X; x++)
            {
                _diagram[lineOfVent.Start.Y, x]++;
            }
        }

        if (lineOfVent.IsVertical())
        {
            for (int y = lineOfVent.Start.Y; y <= lineOfVent.End.Y; y++)
            {
                _diagram[y, lineOfVent.Start.X]++;
            }
        }

        if (lineOfVent.IsDiagonal())
        {
            if (lineOfVent.Start.Y < lineOfVent.End.Y && lineOfVent.Start.X < lineOfVent.End.X)
            {
                var coordinate = lineOfVent.Start;
                do
                {
                    _diagram[coordinate.Y, coordinate.X]++;
                    coordinate = new Coordinate(coordinate.X + 1, coordinate.Y + 1);
                } while (coordinate.X <= lineOfVent.End.X && coordinate.Y <= lineOfVent.End.Y);
            }
            else if (lineOfVent.Start.X < lineOfVent.End.X)
            {
                var coordinate = lineOfVent.Start;
                do
                {
                    _diagram[coordinate.Y, coordinate.X]++;
                    coordinate = new Coordinate(coordinate.X + 1, coordinate.Y - 1);
                } while (coordinate.X <= lineOfVent.End.X && coordinate.Y >= lineOfVent.End.Y);
            }
            else
            {
                var coordinate = lineOfVent.Start;
                do
                {
                    _diagram[coordinate.Y, coordinate.X]++;
                    coordinate = new Coordinate(coordinate.X - 1, coordinate.Y + 1);
                } while (coordinate.X >= lineOfVent.End.X && coordinate.Y <= lineOfVent.End.Y);
            }
        }
    }

    public int CountOverlappingPoints()
    {
        int countOverlappingLines = 0;
        for (int x = 0; x <= MaxX; x++)
        {
            for (int y = 0; y <= MaxY; y++)
            {
                if (_diagram[y, x] > 1)
                    countOverlappingLines++;
            }
        }

        return countOverlappingLines;
    }
}