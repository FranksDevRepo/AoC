using System;
using aoc2021.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2021.Puzzles.Test.Solutions;

public sealed class Day01Test : TestBase<Day01>
{
    private readonly string _input = @"199
200
208
210
200
207
240
269
260
263";

    [Fact]
    public async Task Part1()
    {
        Assert.Equal("7", await Solution.Part1Async(_input));
    }

    [Fact]
    public async Task Part2()
    {
        Assert.Equal("5", await Solution.Part2Async(_input));
    }

    [Fact]
    public async Task Part1WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day01.txt"));

        Assert.Equal("1154", await Solution.Part1Async(input));
    }

    [Fact]
    public async Task Part2WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day01.txt"));

        Assert.Equal("1127", await Solution.Part2Async(input));
    }

}