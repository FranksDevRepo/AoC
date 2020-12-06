using aoc2020.Puzzles.Core;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Passport Processing")]
    public sealed class Day04 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            var passports = GetPassports(input);
            int countValidPassports = 0;
            foreach (var passport in passports)
            {
                countValidPassports += CheckValidPassport(passport) ? 1 : 0;
            }
            return countValidPassports.ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            throw new NotImplementedException();
        }

        private List<Dictionary<string, string>> GetPassports(string input)
        {
            var passports = new List<Dictionary<string, string>>();
            foreach (var passport in input.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                passports.Add(passport.Split(new string[] { "\n", " " }, StringSplitOptions.RemoveEmptyEntries)
                    .ToDictionary(x => x.Split(":")[0], x => x.Split(":")[1]));
            }
            return passports;
        }

        private bool CheckValidPassport(Dictionary<string, string> passport)
        {
            return
                passport.ContainsKey("byr")
                && passport.ContainsKey("iyr")
                && passport.ContainsKey("eyr")
                && passport.ContainsKey("hgt")
                && passport.ContainsKey("hcl")
                && passport.ContainsKey("ecl")
                && passport.ContainsKey("pid");
        }
    }
}
