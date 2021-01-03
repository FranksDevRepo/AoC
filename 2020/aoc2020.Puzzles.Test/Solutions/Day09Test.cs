using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day09Test : TestBase<Day09>
    {
        private readonly string myInput = @"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576";

        [Fact]
        public async Task Part1()
        {
            Assert.Equal("127", await Task.FromResult(Day09.FindInvalidNumber(myInput, 5).ToString()));
        }

        [Fact]
        public async Task Part2()
        {
            var invalidNumber = Day09.FindInvalidNumber(myInput, 5);
            var contiguousNumbers = await Task.FromResult(Day09.FindContiguousSetOfAtLeastTwoNumbers(myInput, invalidNumber));
            var solution = Day09.CalculateSolutionPart2(contiguousNumbers).ToString();
            Assert.Equal("62", solution);
        }

        [Fact]
        public async Task Part1WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = await File.ReadAllTextAsync(Path.Combine(rootDir, "Input", "Day09.txt"));

            Assert.Equal("167829540", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = await File.ReadAllTextAsync(Path.Combine(rootDir, "Input", "Day09.txt"));

            Assert.Equal("28045630", await Solution.Part2Async(input));
        }
    }
}
