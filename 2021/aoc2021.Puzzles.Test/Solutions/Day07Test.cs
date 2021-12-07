using System;
using aoc2021.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2021.Puzzles.Test.Solutions;

public sealed class Day07Test : TestBase<Day07>
{
    private readonly string _input = @"16,1,2,0,4,2,7,1,2,14";

    [Fact]
    public async Task Part1()
    {
        Assert.Equal("37", await Solution.Part1Async(_input));
    }

    [Fact]
    public async Task Part2()
    {
        Assert.Equal("168", await Solution.Part2Async(_input));
    }

    [Fact]
    public async Task Part1WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day07.txt"));

        Assert.Equal("335330", await Solution.Part1Async(input));
    }

    [Fact]
    public async Task Part2WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day07.txt"));

        Assert.Equal("", await Solution.Part2Async(input));
    }
}