using aoc2020.Puzzles.Solutions;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day06Test : TestBase<Day06>
    {
        private readonly string myInput = @"abc

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

        [Fact]
        public async Task Part1()
        {
            Assert.Equal("11", await Solution.Part1Async(myInput));
        }

        [Fact]
        public async Task Part2()
        {
            Assert.Equal("6", await Solution.Part2Async(myInput));
        }
    }
}
