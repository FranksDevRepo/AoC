using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day03Test : TestBase<Day03>
    {
        private readonly string myInput = @"..##.........##.........##.........##.........##.........##.......
#...#...#..#...#...#..#...#...#..#...#...#..#...#...#..#...#...#..
.#....#..#..#....#..#..#....#..#..#....#..#..#....#..#..#....#..#.
..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#
.#...##..#..#...##..#..#...##..#..#...##..#..#...##..#..#...##..#.
..#.##.......#.##.......#.##.......#.##.......#.##.......#.##.....
.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#
.#........#.#........#.#........#.#........#.#........#.#........#
#.##...#...#.##...#...#.##...#...#.##...#...#.##...#...#.##...#...
#...##....##...##....##...##....##...##....##...##....##...##....#
.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#";

        [Fact]
        public async Task Part1()
        {
            Assert.Equal("7", await Solution.Part1Async(myInput));
        }

        [Fact]
        public async Task Part2()
        {
            Assert.Equal("336", await Solution.Part2Async(myInput));
        }

        [Fact]
        public async Task Part2WithExtractOfInputFile()
        {
            var input = @".#..#.....#....##..............
...#.#...#...#.#..........#....
#...###...#.#.....#.##.#.#...#.
#.....#.#...##....#...#...#....
##.......##.#.....#........##.#
#..#....#......#..#......#...#.
#..#......#.......#............
##...#.#..#...#........#....##.
#.#.#...#...#..#........#....#.
.......#...........##......#...
##.##.##......#..#............#
..#.###..#..............#......
";
            Assert.Equal("144", await Solution.Part2Async(input));
        }

        [Fact]
        public async Task Part1WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = File.ReadAllText(Path.Combine(rootDir, "Input", "day03.txt"));

            Assert.Equal("289", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = File.ReadAllText(Path.Combine(rootDir, "Input", "day03.txt"));

            Assert.Equal("5522401584", await Solution.Part2Async(input));
        }
    }
}
