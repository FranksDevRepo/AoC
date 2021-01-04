using aoc2020.Puzzles.Solutions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day04Test : TestBase<Day04>
    {
        private readonly string myInput = @"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in";

        [Fact]
        public async Task Part1()
        {
            Assert.Equal("2", await Solution.Part1Async(myInput).ConfigureAwait(false));
        }

        [Fact]
        public async Task Part2()
        {
            Assert.Equal("2", await Solution.Part2Async(myInput).ConfigureAwait(false));
        }

        [Fact]
        public async Task Part1WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = await File.ReadAllTextAsync(Path.Combine(rootDir, "Input", "day04.txt")).ConfigureAwait(false);

            Assert.Equal("170", await Solution.Part1Async(input).ConfigureAwait(false));
        }

        [Fact]
        public async Task Part2WithInputFile()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = await File.ReadAllTextAsync(Path.Combine(rootDir, "Input", "day04.txt")).ConfigureAwait(false);

            Assert.Equal("103", await Solution.Part2Async(input).ConfigureAwait(false));
        }
    }
}
