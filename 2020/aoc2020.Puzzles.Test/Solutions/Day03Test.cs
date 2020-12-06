using System.IO;
using aoc2020.Puzzles.Solutions;
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
        public async Task Part2WithInputFile()
        {
            var input = File.ReadAllText(
                @"C:\_Daten\source\repos\FranksDevRepo\AoC\2020\aoc2020.Puzzles\Input\day03.txt");
            Assert.Equal("336", await Solution.Part2Async(input));
        }
    }
}
