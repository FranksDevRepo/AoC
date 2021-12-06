using aoc2021.Puzzles.Solutions;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2021.Puzzles.Test.Solutions;

public sealed class Day03Test : TestBase<Day03>
{
    [Fact]
    public async Task Part1()
    {
        var input = @"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010";
        Assert.Equal("198", await Solution.Part1Async(input));
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
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day03.txt"));

        Assert.Equal("", await Solution.Part1Async(input));
    }

    [Fact]
    public async Task Part2WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day03.txt"));

        Assert.Equal("", await Solution.Part2Async(input));
    }
}