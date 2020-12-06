using aoc2020.Puzzles.Solutions;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day05Test : TestBase<Day05>
    {
        [Fact]
        public async Task Part1()
        {
            var input = @"FBFBBFFRLR
BFFFBBFRRR
FFFBBBFRRR
BBFFBBFRLL";
            Assert.Equal("820", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2()
        {
            var input = @"";
            Assert.Equal("", await Solution.Part2Async(input));
        }
    }
}
