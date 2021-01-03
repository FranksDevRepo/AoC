using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Reflection;
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

        [Fact]
        public async Task Part1WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = await File.ReadAllTextAsync(Path.Combine(rootDir, "Input", "day06.txt"));

            Assert.Equal("6530", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = await File.ReadAllTextAsync(Path.Combine(rootDir, "Input", "day06.txt"));

            Assert.Equal("3323", await Solution.Part2Async(input));
        }
    }
}
