using System;
using aoc2021.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2021.Puzzles.Test.Solutions;

public sealed class Day02Test : TestBase<Day02>
{
    private readonly string _input = @"forward 5
down 5
forward 8
up 3
down 8
forward 2";

    [Fact]
    public async Task Part1()
    {
        Assert.Equal("150", await Solution.Part1Async(_input));
    }

    [Fact]
    public async Task Part2()
    {
        Assert.Equal("900", await Solution.Part2Async(_input));
    }

    [Fact]
    public async Task Part1WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day02.txt"));

        Assert.Equal("1524750", await Solution.Part1Async(input));
    }

    [Fact]
    public async Task Part2WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day02.txt"));

        Assert.Equal("1592426537", await Solution.Part2Async(input));
    }
}