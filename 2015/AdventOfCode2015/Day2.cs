using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AdventOfCode2015
{
    public class Day2
    {
        [Theory]
        [InlineData(2, 3, 4, 58)]
        [InlineData(1, 1, 10, 43)]
        public void TestWrappingPaper(int length, int width, int height, int expected)
        {
            var paper = CalcSurfaceArea(length, width, height);
            var slack = CalcSlack(length, width, height);
            Assert.Equal(expected, paper+slack);
        }

        public static int CalcSurfaceArea(int length, int width, int height)
        {
            return 2 * length * width + 2 * width * height + 2 * height * length;
        }

        public static int CalcSlack(int length, int width, int height)
        {
            return Enumerable.Min(new int[] {length * width, width * height, height * length});
        }
    }
}
