using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day23Test : TestBase<Day23>
    {
        [Fact]
        public async Task Part1()
        {
            var input = @"389125467";
            // after 10 moves 92658374
            // after 100 moves 67384529
            Assert.Equal("67384529", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2()
        {
            var input = @"389125467";
            Assert.Equal("149245887792", await Solution.Part2Async(input));
        }

        [Fact]
        public async Task Part1WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = File.ReadAllText(Path.Combine(rootDir, "Input", "Day23.txt"));

            Assert.Equal("97342568", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = File.ReadAllText(Path.Combine(rootDir, "Input", "Day23.txt"));

            Assert.Equal("902208073192", await Solution.Part2Async(input));
        }

    }
}
