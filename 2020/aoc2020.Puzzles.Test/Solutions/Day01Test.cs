﻿using aoc2020.Puzzles.Solutions;
using System.Threading.Tasks;
using Xunit;

namespace aoc2020.Puzzles.Test.Solutions
{
    public sealed class Day01Test : TestBase<Day01>
    {
        private readonly string myInput = @"1721
979
366
299
675
1456";

        [Fact]
        public async Task Part1()
        {
            Assert.Equal("514579", await Solution.Part1Async(myInput));
        }

        [Fact]
        public async Task Part2()
        {
            Assert.Equal("241861950", await Solution.Part2Async(myInput));
        }
    }
}
