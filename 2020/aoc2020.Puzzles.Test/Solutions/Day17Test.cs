using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day17Test : TestBase<Day17>
    {
        private readonly string input = @".#.
..#
###
";

        [Fact]
        public async Task Part1()
        {
            Assert.Equal("112", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2()
        {
            Assert.Equal("848", await Solution.Part2Async(input));
        }

        [Fact]
        public async Task Part1WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = File.ReadAllText(Path.Combine(rootDir, "Input", "Day17.txt"));

            var actual = await Solution.Part1Async(input);

            Assert.True(long.Parse(actual) > 340, $"{actual} is too low.");

            Assert.Equal("375", actual);
        }

        [Fact]
        public async Task Part2WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = File.ReadAllText(Path.Combine(rootDir, "Input", "Day17.txt"));

            var actual = await Solution.Part2Async(input);

            Assert.Equal("2192", actual);
        }

    }
}
