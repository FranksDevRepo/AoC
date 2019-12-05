using System;
using System.Diagnostics.CodeAnalysis;

namespace AoC2019.Day3
{
    public enum Direction { Right = 'R', Down = 'D', Up = 'U', Left = 'L'};

    public class Point : IEquatable<Point>
    {
        public int X { get; }
        public int Y { get; }
        public int Count { get; }

        public Point(int x, int y, int count = 0)
        {
            this.X = x;
            this.Y = y;
            this.Count = count;
        }

        internal Point Move(Direction direction)
        {
            int x = X;
            int y = Y;
            int count = Count;
            switch (direction)
            {
                case Direction.Down:
                    y--;
                    break;
                case Direction.Up:
                    y++;
                    break;
                case Direction.Left:
                    x--;
                    break;
                case Direction.Right:
                    x++;
                    break;
            }
            return new Point(x, y, ++count);
        }

        public bool Equals([AllowNull] Point other)
        {
            if (other is null)
                return false;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj) => Equals(obj as Point);
        public override int GetHashCode() => (X, Y).GetHashCode();
    }
}
