using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day08Test : TestBase<Day08>
    {
        private readonly string myInput = @"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6";

        [Fact]
        public async Task Part1()
        {
            Assert.Equal("5", await Solution.Part1Async(myInput));
        }

        [Fact]
        public async Task Part2()
        {
            Assert.Equal("8", await Solution.Part2Async(myInput));
        }

        [Fact]
        public async Task Part1WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = File.ReadAllText(Path.Combine(rootDir, "Input", "day08.txt"));

            Assert.Equal("2003", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = File.ReadAllText(Path.Combine(rootDir, "Input", "day08.txt"));

            Assert.Equal("1984", await Solution.Part2Async(input));
        }

    }
}
