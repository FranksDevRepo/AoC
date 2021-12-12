using System;
using aoc2021.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;

namespace aoc2021.Puzzles.Test.Solutions;

public sealed class Day12Test : TestBase<Day12>
{
    [Theory]
    [MemberData(nameof(GetExampleTestcasesPart1))]
    public async Task Part1(string input, string expected)
    {
        Assert.Equal(expected, await Solution.Part1Async(input));
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
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day12.txt"));

        Assert.Equal("", await Solution.Part1Async(input));
    }

    [Fact]
    public async Task Part2WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day12.txt"));

        Assert.Equal("", await Solution.Part2Async(input));
    }

    public static IEnumerable<object[]> GetExampleTestcasesPart1() =>
        new List<object[]>
        {
            new object[]
            {
                @"start-A
start-b
A-c
A-b
b-d
A-end
b-end", "10"
            },
            new object[]
            {
                @"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc", "19"
            },
            new object[]
            {
                @"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW", "226"
            }
        };
}