using System;
using aoc2020.Puzzles.Core;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Conway Cubes")]
    public sealed class Day17 : SolutionBase
    {
        private class Cube3Dim
        {
            private HashSet<Coordinate> data = new();
            private Coordinate min = new() {X = 0, Y = 0, Z = 0};
            private Coordinate max;
            private readonly StringBuilder output = new();

            public struct Coordinate
            {
                public int X { get; set; }
                public int Y { get; set; }
                public int Z { get; set; }

                private bool Equals(Coordinate other)
                {
                    return X == other.X && Y == other.Y && Z == other.Z;
                }

                public override bool Equals(object obj)
                {
                    return obj is Coordinate other && Equals(other);
                }

                public override int GetHashCode()
                {
                    return HashCode.Combine(X, Y, Z);
                }

                public static bool operator ==(Coordinate c1, Coordinate c2)
                {
                    return c1.Equals(c2);
                }

                public static bool operator !=(Coordinate c1, Coordinate c2)
                {
                    return !c1.Equals(c2);
                }

                public override string ToString() => $"x={X},y={Y},z={Z}";
            }

            public string Output => output.ToString();

            public long CountActiveState => data.Count;

            public void Setup(string input)
            {
                var slice = (from line in GetLines(input)
                    where !string.IsNullOrWhiteSpace(line)
                    select line).ToArray();

                max = new Coordinate {X = slice.Length, Y = slice.Length, Z = 1};

                int y = 0;
                foreach (var line in slice)
                {
                    int x = 0;
                    foreach (var @char in line)
                    {
                        if (@char == '#')
                            data.Add(new Coordinate {X = x, Y = y, Z = 0});
                        x++;
                    }

                    y++;
                }

                if (Debugger.IsAttached)
                    DebugOutput();
            }

            public void ChangeState()
            {
                HashSet<Coordinate> currentCube = new HashSet<Coordinate>();
                for (var z = min.Z - 1; z <= max.Z + 1; z++)
                {
                    for (var y = min.Y - 1; y <= max.Y; y++)
                    {
                        for (var x = min.X - 1; x <= max.X; x++)
                        {
                            var currentPos = new Coordinate {X = x, Y = y, Z = z};
                            var isActiveState = data.Contains(currentPos);

                            var countActiveNeighbors = CountNeighbors(currentPos);
                            if (isActiveState)
                            {
                                if (countActiveNeighbors >= 2 && countActiveNeighbors <= 3)
                                    currentCube.Add(currentPos);
                            }
                            else
                            {
                                if (countActiveNeighbors == 3)
                                    currentCube.Add(currentPos);
                            }
                        }
                    }
                }

                data = currentCube;
                Resize();
                if (Debugger.IsAttached)
                    DebugOutput();
            }

            private void Resize()
            {
                --min.X;
                --min.Y;
                --min.Z;
                ++max.X;
                ++max.Y;
                ++max.Z;
            }

            private long CountNeighbors(Coordinate coordinate)
            {
                long count = 0;
                for (int x = coordinate.X - 1; x < coordinate.X + 2; x++)
                {
                    for (int y = coordinate.Y - 1; y < coordinate.Y + 2; y++)
                    {
                        for (int z = coordinate.Z - 1; z < coordinate.Z + 2; z++)
                        {
                            var neighbor = new Coordinate {X = x, Y = y, Z = z};
                            if (neighbor != coordinate && data.Contains(neighbor)) count++;
                        }
                    }
                }

                return count;
            }

            private void DebugOutput()
            {
                for (var z = min.Z; z < max.Z; z++)
                {
                    output.AppendLine($"z={z}");
                    for (var y = min.Y; y < max.Y; y++)
                    {
                        for (var x = min.X; x < max.X; x++)
                        {
                            var coord = new Coordinate {X = x, Y = y, Z = z};
                            output.Append(data.Contains(coord) ? '#' : '.');
                        }

                        output.AppendLine();
                    }

                    output.AppendLine();
                }

                output.AppendLine(new string('-', 10));
            }
        }

        public override string Part1(string input)
        {
            var cube = new Cube3Dim();
            cube.Setup(input);
            int cycle = 0;
            do
            {
                cycle++;
                cube.ChangeState();
            } while (cycle < 6);

            var cubesInActiveState = cube.CountActiveState;
            return cubesInActiveState.ToString();
        }

        public override string Part2(string input)
        {
            var cube = new Cube4Dim();
            cube.Setup(input);
            int cycle = 0;
            do
            {
                cycle++;
                cube.ChangeState();
            } while (cycle < 6);

            var cubesInActiveState = cube.CountActiveState;
            return cubesInActiveState.ToString();
        }

        private class Cube4Dim
        {
            private HashSet<Coordinate> data = new();
            private Coordinate min = new Coordinate();
            private Coordinate max;
            private readonly StringBuilder output = new();

            public struct Coordinate
            {
                public int X { get; set; }
                public int Y { get; set; }
                public int Z { get; set; }
                public int W { get; set; }

                private bool Equals(Coordinate other)
                {
                    return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
                }

                public override bool Equals(object obj)
                {
                    return obj is Coordinate other && Equals(other);
                }

                public override int GetHashCode()
                {
                    return HashCode.Combine(X, Y, Z, W);
                }

                public static bool operator ==(Coordinate c1, Coordinate c2)
                {
                    return c1.Equals(c2);
                }

                public static bool operator !=(Coordinate c1, Coordinate c2)
                {
                    return !c1.Equals(c2);
                }

                public override string ToString()
                {
                    return $"x={X},y={Y},z={Z},w={W}";
                }
            }

            public string Output => output.ToString();

            public long CountActiveState => data.Count;

            public void Setup(string input)
            {
                var slice = (from line in GetLines(input)
                    where !string.IsNullOrWhiteSpace(line)
                    select line).ToArray();

                max = new Coordinate {X = slice.Length, Y = slice.Length, Z = 1, W = 1};

                int y = 0;
                foreach (var line in slice)
                {
                    int x = 0;
                    foreach (var @char in line)
                    {
                        if (@char == '#')
                            data.Add(new Coordinate {X = x, Y = y, Z = 0, W = 0});
                        x++;
                    }

                    y++;
                }

                if (Debugger.IsAttached)
                    DebugOutput();
            }

            public void ChangeState()
            {
                HashSet<Coordinate> currentCube = new HashSet<Coordinate>();
                for (var w = min.W - 1; w <= max.W; w++)
                {
                    for (var z = min.Z - 1; z <= max.Z + 1; z++)
                    {
                        for (var y = min.Y - 1; y <= max.Y; y++)
                        {
                            for (var x = min.X - 1; x <= max.X; x++)
                            {
                                var currentPos = new Coordinate {X = x, Y = y, Z = z, W = w};

                                var countActiveNeighbors = CountNeighbors(currentPos);
                                if (data.Contains(currentPos))
                                {
                                    if (countActiveNeighbors >= 2 && countActiveNeighbors <= 3)
                                        currentCube.Add(currentPos);
                                }
                                else
                                {
                                    if (countActiveNeighbors == 3)
                                        currentCube.Add(currentPos);
                                }
                            }
                        }
                    }
                }

                data = currentCube;
                Resize();
                if (Debugger.IsAttached)
                    DebugOutput();
            }

            private void Resize()
            {
                --min.X;
                --min.Y;
                --min.Z;
                --min.W;
                ++max.X;
                ++max.Y;
                ++max.Z;
                ++max.W;
            }

            private long CountNeighbors(Coordinate coordinate)
            {
                long count = 0;
                for (int x = coordinate.X - 1; x < coordinate.X + 2; x++)
                {
                    for (int y = coordinate.Y - 1; y < coordinate.Y + 2; y++)
                    {
                        for (int z = coordinate.Z - 1; z < coordinate.Z + 2; z++)
                        {
                            for (int w = coordinate.W - 1; w < coordinate.W + 2; w++)
                            {
                                var neighbor = new Coordinate {X = x, Y = y, Z = z, W = w};
                                if (neighbor != coordinate && data.Contains(neighbor)) count++;
                            }
                        }
                    }
                }

                return count;
            }

            private HashSet<Coordinate> GetNeighbors(Coordinate coordinate)
            {
                HashSet<Coordinate> neighbors = new HashSet<Coordinate>();
                for (int x = coordinate.X - 1; x < coordinate.X + 2; x++)
                {
                    for (int y = coordinate.Y - 1; y < coordinate.Y + 2; y++)
                    {
                        for (int z = coordinate.Z - 1; z < coordinate.Z + 2; z++)
                        {
                            var neighbor = new Coordinate {X = x, Y = y, Z = z};
                            if (neighbor != coordinate)
                                neighbors.Add(neighbor);
                        }
                    }
                }

                return neighbors;
            }

            private void DebugOutput()
            {
                for (var w = min.W; w < max.W; w++)
                {
                    for (var z = min.Z; z < max.Z; z++)
                    {
                        output.AppendLine($"z={z}, w={w}");
                        for (var y = min.Y; y < max.Y; y++)
                        {
                            for (var x = min.X; x < max.X; x++)
                            {
                                var coord = new Coordinate {X = x, Y = y, Z = z, W = w};
                                output.Append(data.Contains(coord) ? '#' : '.');
                            }

                            output.AppendLine();
                        }

                        output.AppendLine();
                    }

                    output.AppendLine();
                }

                output.AppendLine(new string('-', 10));
            }
        }
    }
}
