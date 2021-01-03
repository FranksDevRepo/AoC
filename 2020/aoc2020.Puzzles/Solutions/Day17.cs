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
            private Coordinate min = new() {x = 0, y = 0, z = 0};
            private Coordinate max;
            private readonly StringBuilder output = new();

            public struct Coordinate
            {
                public int x;
                public int y;
                public int z;

                private bool Equals(Coordinate other)
                {
                    return x == other.x && y == other.y && z == other.z;
                }

                public override bool Equals(object obj)
                {
                    return obj is Coordinate other && Equals(other);
                }

                public override int GetHashCode()
                {
                    return HashCode.Combine(x, y, z);
                }

                public static bool operator ==(Coordinate c1, Coordinate c2)
                {
                    return c1.Equals(c2);
                }

                public static bool operator !=(Coordinate c1, Coordinate c2)
                {
                    return !c1.Equals(c2);
                }

                public override string ToString() => $"x={x},y={y},z={z}";
            }

            public string Output => output.ToString();

            public long CountActiveState => data.Count;

            public void Setup(string input)
            {
                var slice = (from line in GetLines(input)
                    where !string.IsNullOrWhiteSpace(line)
                    select line).ToArray();

                max = new Coordinate {x = slice.Length, y = slice.Length, z = 1};

                int y = 0;
                foreach (var line in slice)
                {
                    int x = 0;
                    foreach (var @char in line)
                    {
                        if (@char == '#')
                            data.Add(new Coordinate {x = x, y = y, z = 0});
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
                for (var z = min.z - 1; z <= max.z + 1; z++)
                {
                    for (var y = min.y - 1; y <= max.y; y++)
                    {
                        for (var x = min.x - 1; x <= max.x; x++)
                        {
                            var currentPos = new Coordinate {x = x, y = y, z = z};
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
                --min.x;
                --min.y;
                --min.z;
                ++max.x;
                ++max.y;
                ++max.z;
            }

            private long CountNeighbors(Coordinate coordinate)
            {
                long count = 0;
                for (int x = coordinate.x - 1; x < coordinate.x + 2; x++)
                {
                    for (int y = coordinate.y - 1; y < coordinate.y + 2; y++)
                    {
                        for (int z = coordinate.z - 1; z < coordinate.z + 2; z++)
                        {
                            var neighbor = new Coordinate {x = x, y = y, z = z};
                            if (neighbor != coordinate && data.Contains(neighbor)) count++;
                        }
                    }
                }

                return count;
            }

            private void DebugOutput()
            {
                for (var z = min.z; z < max.z; z++)
                {
                    output.AppendLine($"z={z}");
                    for (var y = min.y; y < max.y; y++)
                    {
                        for (var x = min.x; x < max.x; x++)
                        {
                            var coord = new Coordinate {x = x, y = y, z = z};
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
                public int x;
                public int y;
                public int z;
                public int w;

                private bool Equals(Coordinate other)
                {
                    return x == other.x && y == other.y && z == other.z && w == other.w;
                }

                public override bool Equals(object obj)
                {
                    return obj is Coordinate other && Equals(other);
                }

                public override int GetHashCode()
                {
                    return HashCode.Combine(x, y, z, w);
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
                    return $"x={x},y={y},z={z},w={w}";
                }
            }

            public string Output => output.ToString();

            public long CountActiveState => data.Count;

            public void Setup(string input)
            {
                var slice = (from line in GetLines(input)
                    where !string.IsNullOrWhiteSpace(line)
                    select line).ToArray();

                max = new Coordinate {x = slice.Length, y = slice.Length, z = 1, w = 1};

                int y = 0;
                foreach (var line in slice)
                {
                    int x = 0;
                    foreach (var @char in line)
                    {
                        if (@char == '#')
                            data.Add(new Coordinate {x = x, y = y, z = 0, w = 0});
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
                for (var w = min.w - 1; w <= max.w; w++)
                {
                    for (var z = min.z - 1; z <= max.z + 1; z++)
                    {
                        for (var y = min.y - 1; y <= max.y; y++)
                        {
                            for (var x = min.x - 1; x <= max.x; x++)
                            {
                                var currentPos = new Coordinate {x = x, y = y, z = z, w = w};

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
                --min.x;
                --min.y;
                --min.z;
                --min.w;
                ++max.x;
                ++max.y;
                ++max.z;
                ++max.w;
            }

            private long CountNeighbors(Coordinate coordinate)
            {
                long count = 0;
                for (int x = coordinate.x - 1; x < coordinate.x + 2; x++)
                {
                    for (int y = coordinate.y - 1; y < coordinate.y + 2; y++)
                    {
                        for (int z = coordinate.z - 1; z < coordinate.z + 2; z++)
                        {
                            for (int w = coordinate.w - 1; w < coordinate.w + 2; w++)
                            {
                                var neighbor = new Coordinate {x = x, y = y, z = z, w = w};
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
                for (int x = coordinate.x - 1; x < coordinate.x + 2; x++)
                {
                    for (int y = coordinate.y - 1; y < coordinate.y + 2; y++)
                    {
                        for (int z = coordinate.z - 1; z < coordinate.z + 2; z++)
                        {
                            var neighbor = new Coordinate {x = x, y = y, z = z};
                            if (neighbor != coordinate)
                                neighbors.Add(neighbor);
                        }
                    }
                }

                return neighbors;
            }

            private void DebugOutput()
            {
                for (var w = min.w; w < max.w; w++)
                {
                    for (var z = min.z; z < max.z; z++)
                    {
                        output.AppendLine($"z={z}, w={w}");
                        for (var y = min.y; y < max.y; y++)
                        {
                            for (var x = min.x; x < max.x; x++)
                            {
                                var coord = new Coordinate {x = x, y = y, z = z, w = w};
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
