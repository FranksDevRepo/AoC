using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day15Test : TestBase<Day15>
    {
        [Fact]
        public async Task Part1()
        {
            var input = @"0,3,6";
            Assert.Equal("436", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2()
        {
            var input = @"";
            Assert.Equal("", await Solution.Part2Async(input));
        }

        [Fact]
        public async Task Part1WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = File.ReadAllText(Path.Combine(rootDir, "Input", "Day15.txt"));

            Assert.Equal("", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = File.ReadAllText(Path.Combine(rootDir, "Input", "Day15.txt"));

            Assert.Equal("", await Solution.Part2Async(input));
        }

    }
}
