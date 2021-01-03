using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Passport Processing")]
    public sealed class Day04 : SolutionBase
    {
        public override string Part1(string input)
        {
            var passports = GetPassports(input);
            int countValidPassports = 0;
            foreach (var passport in passports)
            {
                countValidPassports += CheckValidPassport(passport) ? 1 : 0;
            }
            return countValidPassports.ToString();
        }

        public override string Part2(string input)
        {
            var passports = GetPassports(input);
            int countValidPassports = 0;
            foreach (var passport in passports)
            {
                if (!CheckValidPassport(passport))
                    continue;
                if (!IsValidYear(passport["byr"], 1920, 2002))
                    continue;
                if (!IsValidYear(passport["iyr"], 2010, 2020))
                    continue;
                if (!IsValidYear(passport["eyr"], 2020, 2030))
                    continue;
                if (!IsValidHeight(passport["hgt"]))
                    continue;
                if (!IsValidFormat(passport["hcl"], "#[a-f,0-9]{6,6}"))
                    continue;
                if (!IsValidFormat(passport["ecl"], "^(amb|blu|brn|gry|grn|hzl|oth)$"))
                    continue;
                if (!IsValidFormat(passport["pid"], "^[0-9]{9,9}$"))
                    continue;

                countValidPassports++;
            }

            return countValidPassports.ToString();
        }

        private List<Dictionary<string, string>> GetPassports(string input)
        {
            var passports = new List<Dictionary<string, string>>();
            foreach (var passport in input.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                passports.Add(passport.Split(new string[] { Environment.NewLine, " " }, StringSplitOptions.RemoveEmptyEntries)
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
        private bool IsValidYear(string yearString, int min, int max)
        {
            int year = int.Parse(yearString);
            return year >= min && year <= max;
        }

        private bool IsValidFormat(string property, string pattern)
        => Regex.IsMatch(property, pattern, RegexOptions.Compiled);

        private bool IsValidHeight(string heightString)
        {
            string unit = heightString[^2..];
            if (unit != "cm" && unit != "in")
                return false;
            int height = int.Parse(heightString[0..^2]);
            if (unit == "cm")
                return height >= 150 && height <= 193;
            else if (unit == "in")
                return height >= 59 && height <= 76;
            else
                return false;
        }
    }
}
