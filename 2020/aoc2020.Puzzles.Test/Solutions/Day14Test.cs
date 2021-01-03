using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day14Test : TestBase<Day14>
    {
        [Fact]
        public async Task Part1()
        {
            const string input = @"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0";
            Assert.Equal("165", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part1a()
        {
            const string input = @"mask = 01101X001X111X010X0000X1001X010XX0X0
mem[4841] = 3942
mem[9168] = 414370178
";
            Assert.Equal((28111278914 + 28111278914).ToString(), await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2()
        {
            const string input = @"mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1";
            Assert.Equal("208", await Solution.Part2Async(input));
        }

        [Fact]
        public async Task Part1WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = await File.ReadAllTextAsync(Path.Combine(rootDir, "Input", "Day14.txt"));

            var actual = await Solution.Part1Async(input);

            Assert.True(BigInteger.Parse(actual) > 106385978789);

            Assert.Equal("11926135976176", actual);
        }

        [Fact]
        public async Task Part2WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = await File.ReadAllTextAsync(Path.Combine(rootDir, "Input", "Day14.txt"));

            Assert.Equal("4330547254348", await Solution.Part2Async(input));
        }
    }
}
