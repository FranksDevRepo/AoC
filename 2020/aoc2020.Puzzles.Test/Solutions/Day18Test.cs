using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day18Test : TestBase<Day18>
    {
        [Theory]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", "71")]
        [InlineData("1 + (2 * 3) + (4 * (5 + 6))", "51")]
        [InlineData("2 * 3 + (4 * 5)", "26")]
        [InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", "437")]
        [InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", "12240")]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", "13632")]
        public async Task Part1(string input, string expected)
        {
            Assert.Equal(expected, await Solution.Part1Async(input));
        }

        [Theory]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", "231")]
        [InlineData("1 + (2 * 3) + (4 * (5 + 6))", "51")]
        [InlineData("2 * 3 + (4 * 5)", "46")]
        [InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", "1445")]
        [InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", "669060")]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", "23340")]
        public async Task Part2(string input, string expected)
        {
            Assert.Equal(expected, await Solution.Part2Async(input));
        }

        [Fact]
        public async Task Part1WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = File.ReadAllText(Path.Combine(rootDir, "Input", "Day18.txt"));

            Assert.Equal("6640667297513", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = File.ReadAllText(Path.Combine(rootDir, "Input", "Day18.txt"));

            Assert.Equal("", await Solution.Part2Async(input));
        }

    }
}
