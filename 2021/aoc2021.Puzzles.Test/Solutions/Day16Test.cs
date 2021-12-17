using System;
using System.Collections.Generic;
using aoc2021.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2021.Puzzles.Test.Solutions;

public sealed class Day16Test : TestBase<Day16>
{
    [Theory]
    [MemberData(nameof(GetExampleTestcasesPart1))]
    public async Task Part1(string input, string expectedPart1, string _)
    {
        Assert.Equal(expectedPart1, await Solution.Part1Async(input));
    }

    [Theory]
    [MemberData(nameof(GetExampleTestcasesPart1))]
    public async Task Part2(string input, string _, string expectedPart2)
    {
        Assert.Equal(expectedPart2, await Solution.Part2Async(input));
    }

    [Fact]
    public async Task Part1WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(
            rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day16.txt"));

        Assert.Equal("971", await Solution.Part1Async(input));
    }

    [Fact]
    public async Task Part2WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(
            rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day16.txt"));

        Assert.Equal("831996589851", await Solution.Part2Async(input));
    }


    public static IEnumerable<object[]> GetExampleTestcasesPart1() =>
        new List<object[]>
        {
            new []
            {
                "8A004A801A8002F478", "16", "15"
            },
            new []
            {
                "620080001611562C8802118E34", "12", "46"
            },
            new []
            {
                "C0015000016115A2E0802F182340", "23", "46"
            },
            new []
            {
                "A0016C880162017C3686B18A3D4780", "31", "54"
            }
        };
}
