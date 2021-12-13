using aoc2021.Puzzles.Core;
using aoc2021.Puzzles.Extensions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Smoke Basin")]
public sealed class Day09 : SolutionBase
{
    public override string Part1(string input)
    {
        var heigtMap = GetHeightMap(input);

        var lowPoints = GetLowPoints(heigtMap);

        return CalculateRiskLevel(lowPoints).ToString();
    }

    private int[] GetLowPoints(int[][] heigtMap)
    {
        List<int> lowPoints = new();

        return lowPoints.ToArray();
    }

    private int CalculateRiskLevel(int[] lowPoints)
    {
        var riskLevel = 0;
        foreach(var lowPoint in lowPoints)
        {
            riskLevel = riskLevel + lowPoint + 1;
        }
        return riskLevel;
    }

    public override string Part2(string input)
    {
        //foreach(var subKeyName in Registry.ClassesRoot.GetSubKeyNames())
        //{
        //    foreach(var a in Registry.GetValue(subKeyName))
        //    {

        //    }
        //}
        throw new NotImplementedException();
    }

    private int[][] GetHeightMap(string input)
    {
        var heightMap = input
            .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row
                .Select(c => Convert.ToInt32(c)).ToArray()).ToArray();
        return heightMap;
    }
}