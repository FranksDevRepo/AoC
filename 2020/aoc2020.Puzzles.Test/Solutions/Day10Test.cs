using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day10Test : TestBase<Day10>
    {
        private readonly string _input = @"16
10
15
5
1
11
7
19
6
12
4";

        private readonly string _largerInput = @"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3";

        [Fact]
        public async Task Part1()
        {
            Assert.Equal("35", await Solution.Part1Async(_input));
        }

        [Fact]
        public async Task Part1LargerExample()
        {
            Assert.Equal("220", await Solution.Part1Async(_largerInput));
        }

        [Fact]
        public async Task Part2()
        {
            Assert.Equal("8", await Solution.Part2Async(_input));
        }

        [Fact]
        public async Task Part2WithLargerExample()
        {
            Assert.Equal("19208", await Solution.Part2Async(_largerInput));
        }

        [Fact]
        public async Task Part1WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = await File.ReadAllTextAsync(Path.Combine(rootDir, "Input", "Day10.txt"));

            Assert.Equal("2432", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = await File.ReadAllTextAsync(Path.Combine(rootDir, "Input", "Day10.txt"));

            Assert.Equal("453551299002368", await Solution.Part2Async(input));
        }
    }
}
