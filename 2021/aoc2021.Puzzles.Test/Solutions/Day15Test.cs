using System;
using aoc2021.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace aoc2021.Puzzles.Test.Solutions;

public sealed class Day15Test : TestBase<Day15>
{
    [Fact]
    public async Task Part1()
    {
        var input = @"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581";
        Assert.Equal("40", await Solution.Part1Async(input));
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
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day15.txt"));

        var actual = int.Parse(await Solution.Part1Async(input));

        actual.Should().BeGreaterThan(401);
        actual.Should().BeLessThan(404);
        actual.Should().Be(403);
    }

    [Fact]
    public async Task Part2WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day15.txt"));

        Assert.Equal("", await Solution.Part2Async(input));
    }
}