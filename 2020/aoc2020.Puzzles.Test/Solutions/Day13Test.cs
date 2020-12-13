using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day13Test : TestBase<Day13>
    {
        private readonly string input = @"939
7,13,x,x,59,x,31,19";

        [Fact]
        public async Task Part1()
        {
            Assert.Equal("295", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2()
        {
            Assert.Equal("1068781", await Solution.Part2Async(input));
        }

        [Fact]
        public async Task Part2_Example2()
        {
            var input = @"
17,x,13,19";
            Assert.Equal("3417", await Solution.Part2Async(input));
        }

        [Fact]
        public async Task Part2_Example2a()
        {
            var input = @"
17,x,13";
            Assert.Equal("102", await Solution.Part2Async(input));
        }

        [Fact]
        public async Task Part2_Example3()
        {
            var input = @"
67,7,59,61";
            Assert.Equal("754018", await Solution.Part2Async(input));
        }

        [Fact]
        public async Task Part2_Example4()
        {
            var input = @"
67,x,7,59,61";
            Assert.Equal("779210", await Solution.Part2Async(input));
        }

        [Fact]
        public async Task Part2_Example5()
        {
            var input = @"
67,7,x,59,61";
            Assert.Equal("1261476", await Solution.Part2Async(input));
        }

        [Fact]
        public async Task Part2_Example6()
        {
            var input = @"
1789,37,47,1889";
            Assert.Equal("1202161486", await Solution.Part2Async(input));
        }

        [Fact]
        public async Task Part1WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = File.ReadAllText(Path.Combine(rootDir, "Input", "Day13.txt"));

            Assert.Equal("156", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = File.ReadAllText(Path.Combine(rootDir, "Input", "Day13.txt"));

            var actual = await Solution.Part2Async(input);
            long actualNumber = long.Parse(actual);
            Assert.True(actualNumber > 100000000000000);
            Assert.Equal("404517869995362", await Solution.Part2Async(input));
        }

    }
}
