using System;
using aoc2021.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2021.Puzzles.Test.Solutions;

public sealed class Day13Test : TestBase<Day13>
{
    private readonly string _input = @"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5";
    
    [Fact]
    public async Task Part1()
    {
        Assert.Equal("17", await Solution.Part1Async(_input));
    }

    [Fact]
    public async Task Part2()
    {
        Assert.Equal(@"#####
#...#
#...#
#...#
#####
", await Solution.Part2Async(_input));
    }

    [Fact]
    public async Task Part1WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day13.txt"));

        Assert.Equal("669", await Solution.Part1Async(input));
    }

    [Fact]
    public async Task Part2WithInputFile()
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", "Day13.txt"));

        Assert.Equal(@"#..#.####.####.####..##..#..#..##....##
#..#.#....#.......#.#..#.#..#.#..#....#
#..#.###..###....#..#....#..#.#.......#
#..#.#....#.....#...#....#..#.#.......#
#..#.#....#....#....#..#.#..#.#..#.#..#
.##..####.#....####..##...##...##...##.
", await Solution.Part2Async(input));
    }
}