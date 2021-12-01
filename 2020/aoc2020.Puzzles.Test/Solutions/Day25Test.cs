using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day25Test : TestBase<Day25>
    {
        [Fact]
        public async Task Part1()
        {
            const string input = @"5764801
17807724";
            Assert.Equal("14897079", await Solution.Part1Async(input));
        }

        [Fact(Skip = "need to solve all remaining puzzles to get second star")]
        public async Task Part2()
        {
            const string input = "";
            Assert.Equal("", await Solution.Part2Async(input));
        }

        [Fact]
        public async Task Part1WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = await File.ReadAllTextAsync(Path.Combine(rootDir, "Input", "Day25.txt"));

            Assert.Equal("6408263", await Solution.Part1Async(input));
        }

        [Fact(Skip = "need to solve all remaining puzzles to get second star")]
        public async Task Part2WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = await File.ReadAllTextAsync(Path.Combine(rootDir, "Input", "Day25.txt"));

            Assert.Equal("", await Solution.Part2Async(input));
        }
    }
}
