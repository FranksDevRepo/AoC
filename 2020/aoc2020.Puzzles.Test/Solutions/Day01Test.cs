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
            Assert.Equal("2", await Solution.Part1Async("12"));
            Assert.Equal("2", await Solution.Part1Async("14"));
            Assert.Equal("654", await Solution.Part1Async("1969"));
            Assert.Equal("33583", await Solution.Part1Async("100756"));
        }

        [Fact]
        public async Task Part2()
        {
            Assert.Equal("2", await Solution.Part2Async("12"));
            Assert.Equal("966", await Solution.Part2Async("1969"));
            Assert.Equal("50346", await Solution.Part2Async("100756"));
        }
    }
}
