using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2019.Day3
{
    internal class PointCalculator
    {
        public Point StartPoint { get; }
        public string Moves { get; }

        public PointCalculator(Point start, string moves)
        {
            this.StartPoint = start;
            this.Moves = moves;
        }

        public IEnumerable<Point> Calculate()
        {
            List<Point> points = new List<Point>();
            var currentPoint = StartPoint;

            var moves = Moves.Split(',').Select(s => s.Trim()).ToList();
            foreach(string move in moves)
            {
                var parsedDirection = move.Take(1).First();
                Direction direction = (Direction)parsedDirection;
                int count = Convert.ToInt32(String.Concat(move.Skip(1)));

                for(int i=0; i < count; i++)
                {
                    currentPoint = currentPoint.Move(direction);
                    points.Add(currentPoint);
                }
            }

            return points.AsEnumerable<Point>();
        }
    }
}
