using aoc2020.Puzzles.Solutions;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day01Test : TestBase<Day01>
    {
        [Fact]
        public async Task Part1()
        {
            var input = @"1721
979
366
299
675
1456";
            Assert.Equal("514579", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2()
        {
            var input = @"";
            Assert.Equal("", await Solution.Part2Async(input));
        }
    }
}
