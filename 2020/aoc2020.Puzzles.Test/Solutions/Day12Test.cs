using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day12Test : TestBase<Day12>
    {
        private readonly string input = @"F10
N3
F7
R90
F11
";

        [Fact]
        public async Task Part1()
        {
            Assert.Equal("25", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2()
        {
            Assert.Equal("286", await Solution.Part2Async(input));
        }

        [Fact]
        public async Task Part1WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = File.ReadAllText(Path.Combine(rootDir, "Input", "Day12.txt"));

            Assert.Equal("439", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = File.ReadAllText(Path.Combine(rootDir, "Input", "Day12.txt"));

            Assert.Equal("12385", await Solution.Part2Async(input));
        }

    }
}
