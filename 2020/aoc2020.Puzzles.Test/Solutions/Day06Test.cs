using aoc2020.Puzzles.Solutions;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day06Test : TestBase<Day06>
    {
        [Fact]
        public async Task Part1()
        {
            var input = @"abc

a
b
c

ab
ac

a
a
a
a

b";
            Assert.Equal("11", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2()
        {
            var input = @"";
            Assert.Equal("", await Solution.Part2Async(input));
        }
    }
}
