using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day15Test : TestBase<Day15>
    {
        [Theory]
        [InlineData("0,3,6", "436")]
        [InlineData("1,3,2", "1")]
        [InlineData("2,1,3", "10")]
        [InlineData("1,2,3", "27")]
        [InlineData("2,3,1", "78")]
        [InlineData("3,2,1", "438")]
        [InlineData("3,1,2", "1836")]
        public async Task Part1(string input, string expected)
        {
            Assert.Equal(expected, await Solution.Part1Async(input));
        }

        [Theory]
        [InlineData("0,3,6", "175594")]
        [InlineData("1,3,2", "2578")]
        [InlineData("2,1,3", "3544142")]
        [InlineData("1,2,3", "261214")]
        [InlineData("2,3,1", "6895259")]
        [InlineData("3,2,1", "18")]
        [InlineData("3,1,2", "362")]
        public async Task Part2(string input, string expected)
        {
            Assert.Equal(expected, await Solution.Part2Async(input));
        }

        [Fact]
        public async Task Part1WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = await File.ReadAllTextAsync(Path.Combine(rootDir, "Input", "Day15.txt"));

            Assert.Equal("1696", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = await File.ReadAllTextAsync(Path.Combine(rootDir, "Input", "Day15.txt"));

            Assert.Equal("37385", await Solution.Part2Async(input));
        }
    }
}
