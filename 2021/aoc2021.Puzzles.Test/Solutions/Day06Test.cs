using aoc2021.Puzzles.Solutions;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2021.Puzzles.Test.Solutions;

public sealed class Day06Test : TestBase<Day06>
{
    [Fact]
    public async Task Part1()
    {
        var input = @"3,4,3,1,2";
        Assert.Equal("5934", await Solution.Part1Async(input));
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
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day06.txt"));

        Assert.Equal("", await Solution.Part1Async(input));
    }

    [Fact]
    public async Task Part2WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day06.txt"));

        Assert.Equal("", await Solution.Part2Async(input));
    }
}