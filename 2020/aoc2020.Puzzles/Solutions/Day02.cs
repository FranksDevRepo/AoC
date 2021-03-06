﻿using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
                var countNumOfLetters = password.Count(s => s == letter);
                bool isValidPassword = (min <= countNumOfLetters && countNumOfLetters <= max);
                countValidPasswords += isValidPassword ? 1 : 0;
                if (Debugger.IsAttached)
                    solution.Add($"{line,-40}: {letter} : {countNumOfLetters,2} => {isValidPassword}");
            }

            if (Debugger.IsAttached)
            {
                var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                await File.WriteAllLinesAsync(
                    Path.Combine(rootDir, @"..\\..\\..\\..\\aoc2020.Puzzles", "Input", "solution day02.txt"), solution).ConfigureAwait(false);
            }
            return countValidPasswords.ToString();
        }

        public override string Part2(string input)
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
