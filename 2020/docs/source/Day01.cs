﻿using aoc2019.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019.Puzzles.Solutions
{
    [Puzzle("The Tyranny of the Rocket Equation")]
    public sealed class Day01 : SolutionBase
    {
        public override string Part1(string input)
        {
            return GetModules(input).Sum(mass => mass / 3 - 2).ToString();
        }

        public override string Part2(string input)
        {
            return GetModules(input).Sum(mass =>
            {
                var sum = 0;
                while ((mass = mass / 3 - 2) > 0)
                {
                    sum += mass;
                }

                return sum;
            }).ToString();
        }

        private static IEnumerable<int> GetModules(string input) => GetLines(input).Select(x => Convert.ToInt32(x));
    }
}
