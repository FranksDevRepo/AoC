using System;
using aoc2021.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;

namespace aoc2021.Puzzles.Test.Solutions;

public sealed class Day18Test : TestBase<Day18>
{
    [Fact]
    public async Task Part1()
    {
        var input = @"[1,2]
[[1,2],3]
[9,[8,7]]
[[1,9],[8,5]]
[[[[1,2],[3,4]],[[5,6],[7,8]]],9]
[[[9,[3,8]],[[0,9],6]],[[[3,7],[4,9]],3]]
[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]";
        Assert.Equal("4140", await Solution.Part1Async(input));
    }

    [Fact]
    public async Task Part2()
    {
        var input = @"";
        Assert.Equal("", await Solution.Part2Async(input));
    }

    [Fact]
    public async Task Part1WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day18.txt"));

        Assert.Equal("", await Solution.Part1Async(input));
    }

    [Fact]
    public async Task Part2WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day18.txt"));

        Assert.Equal("", await Solution.Part2Async(input));
    }

    public static IEnumerable<object[]> GetMagnitudeTestcases() =>
    new List<object[]>
    {
            new []
            {
                "[[1,2],[[3,4],5]]", "143"
            },
            new[]
            {
                "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", "1384"
            },
            new[]
            {
                "[[[[1,1],[2,2]],[3,3]],[4,4]]", "445"
            },
            new[]
            {
                "[[[[3,0],[5,3]],[4,4]],[5,5]]", "791"
            },
            new[]
            {
                "[[[[5,0],[7,4]],[5,5]],[6,6]]", "1137"
            },
            new []
            {
                "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", "3488"
            }
    };

    public static IEnumerable<object[]> GetAdditionTestcases() =>
        new List<object[]>
        {
            new []
            {
                "[1,2] + [[3,4],5]", "[[1,2],[[3,4],5]]"
            }
        };

    public static IEnumerable<object[]> GetExplodeTestcases() =>
        new List<object[]>
        {
            new[]
            {
                "[[[[[9,8],1],2],3],4]", "[[[[[9,8],1],2],3],4]"
            }
        };
}