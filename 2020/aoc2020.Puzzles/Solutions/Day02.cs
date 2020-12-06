﻿using aoc2020.Puzzles.Core;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Password Philosophy")]
    public sealed class Day02 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            var lines = GetLines(input);
            var solution = new List<string>();
            int countValidPasswords = 0;
            foreach (var line in lines)
            {
                var elements = line.Split(' ', StringSplitOptions.None);
                var minMax = elements[0].Split('-');
                var min = Int32.Parse(minMax[0]);
                var max = Int32.Parse(minMax[1]);
                var letter = elements[1].ToCharArray().First();
                var password = elements[2];
                //var pattern = $"({letter}){{{min},{max}}}";
                //var isValidPassword = Regex.IsMatch(password, pattern, RegexOptions.Compiled);
                var countNumOfLetters = password.Count(s => s == letter);
                bool isValidPassword = (min <= countNumOfLetters && countNumOfLetters <= max);
                countValidPasswords += isValidPassword ? 1 : 0;
                solution.Add($"{line,-40}: {letter} : {countNumOfLetters,2} => {isValidPassword.ToString()}");
            }
            File.WriteAllLines(@"C:\_Daten\source\repos\FranksDevRepo\AoC\2020\aoc2020.Puzzles\Input\solution day02.txt", solution);
            return countValidPasswords.ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            var lines = GetLines(input);
            int countValidPasswords = 0;
            foreach (var line in lines)
            {
                var elements = line.Split(' ', StringSplitOptions.None);
                var positions = elements[0].Split('-');
                var pos1 = Int32.Parse(positions[0]);
                var pos2 = Int32.Parse(positions[1]);
                var letter = elements[1].ToCharArray().First();
                var password = elements[2];
                bool isValidPassword = (password[pos1 - 1] == letter ^ password[pos2 - 1] == letter);
                countValidPasswords += isValidPassword ? 1 : 0;
            }
            return countValidPasswords.ToString();
        }
    }
}