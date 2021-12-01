using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day13Test : TestBase<Day13>
    {
        [Theory]
        [InlineData(@"939
7,13,x,x,59,x,31,19", "295")]
        public async Task Part1(string input, string expected)
        {
            Assert.Equal(expected, await Solution.Part1Async(input));
        }

        [Theory]
        [InlineData(@"939
7,13,x,x,59,x,31,19", "1068781")]
        [InlineData(@"
17,x,13,19", "3417")]
        [InlineData(@"
17,x,13", "102")]
        [InlineData(@"
67,7,59,61", "754018")]
        [InlineData(@"
67,x,7,59,61", "779210")]
        [InlineData(@"
67,7,x,59,61", "1261476")]
        [InlineData(@"
1789,37,47,1889", "1202161486")]
        public async Task Part2(string input, string expected)
        {
            Assert.Equal(expected, await Solution.Part2Async(input));
        }

        [Fact]
        public async Task Part1WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = await File.ReadAllTextAsync(Path.Combine(rootDir, "Input", "Day13.txt"));

            Assert.Equal("156", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = await File.ReadAllTextAsync(Path.Combine(rootDir, "Input", "Day13.txt"));

            var actual = await Solution.Part2Async(input);
            long actualNumber = long.Parse(actual);
            Assert.True(actualNumber > 100000000000000);
            Assert.Equal("404517869995362", await Solution.Part2Async(input));
        }
    }
}
