using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day02Test : TestBase<Day02>
    {
        private readonly string myInput = @"1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc";

        [Fact]
        public async Task Part1()
        {
            Assert.Equal("2", await Solution.Part1Async(myInput));
        }

        [Fact]
        public async Task Part2()
        {
            Assert.Equal("1", await Solution.Part2Async(myInput));
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
