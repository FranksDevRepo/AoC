using System;
using aoc2022.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2022.Puzzles.Test.Solutions;

public sealed class Day_DAYSTRING_Test : TestBase<Day_DAYSTRING_>
{
    [Fact]
    public async Task Part1()
    {
        var input = @"";
        Assert.Equal("", await Solution.Part1Async(input));
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
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day_DAYSTRING_.txt"));

        Assert.Equal("", await Solution.Part1Async(input));
    }

    [Fact]
    public async Task Part2WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day_DAYSTRING_.txt"));

        Assert.Equal("", await Solution.Part2Async(input));
    }
}