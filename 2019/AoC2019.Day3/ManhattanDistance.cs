using System;
using System.Collections.Generic;
using System.Text;

namespace AoC2019.Day3
{
    public static class ManhattanDistance
    {
        internal static int Calculate(Point a, Point b) => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }
}
