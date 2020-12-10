using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day10Test : TestBase<Day10>
    {
        [Fact]
        public async Task Part1()
        {
            var input = @"16
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
            Assert.Equal("35", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part1LargerExample()
        {
            var input = @"28
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
            Assert.Equal("220", await Solution.Part1Async(input));
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
            var input = File.ReadAllText(Path.Combine(rootDir, "Input", "Day10.txt"));

            Assert.Equal("2432", await Solution.Part1Async(input));
        }

        [Fact]
        public async Task Part2WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = File.ReadAllText(Path.Combine(rootDir, "Input", "Day10.txt"));

            Assert.Equal("", await Solution.Part2Async(input));
        }

    }
}
