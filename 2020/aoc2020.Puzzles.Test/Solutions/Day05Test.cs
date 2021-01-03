using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Reflection;
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

        [Fact(Skip = "missing testcase")]
        public async Task Part2()
        {
            var input = "";
            Assert.Equal("747", await Solution.Part2Async(input));
        }

        [Fact]
        public async Task Part1WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = await File.ReadAllTextAsync(Path.Combine(rootDir, "Input", "day05.txt"));

            Assert.Equal("922", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = await File.ReadAllTextAsync(Path.Combine(rootDir, "Input", "day05.txt"));

            Assert.Equal("747", await Solution.Part2Async(input));
        }
    }
}
