using System;
using aoc2021.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace aoc2021.Puzzles.Test.Solutions;

public sealed class Day05Test : TestBase<Day05>
{
    private readonly string _input = @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2";

    [Fact]
    public async Task Part1()
    {
        Assert.Equal("5", await Solution.Part1Async(_input));
    }

    [Fact]
    public async Task Part2()
    {
        Assert.Equal("12", await Solution.Part2Async(_input));
    }

    [Fact]
    public async Task Part1WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day05.txt"));

        Assert.Equal("8060", await Solution.Part1Async(input));
    }

    [Fact]
    public async Task Part2WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day05.txt"));

        var actual = Convert.ToInt32(await Solution.Part2Async(input));
        actual.Should().BeGreaterThan(19179);

        actual.Should().Be(21577);
    }
}