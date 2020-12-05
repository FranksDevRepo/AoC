using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day02Test : TestBase<Day02>
    {
        [Fact]
        public async Task Part1()
        {
            var input = @"1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc";
            Assert.Equal("2", await Solution.Part1Async(input));
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
            var input = File.ReadAllText(
                @"C:\_Daten\source\repos\FranksDevRepo\AoC\2020\aoc2020.Puzzles\Input\day02.txt");
            Assert.Equal("556", await Solution.Part1Async(input));
        }
    }
}
