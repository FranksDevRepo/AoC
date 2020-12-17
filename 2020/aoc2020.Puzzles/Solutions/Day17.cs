using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Conway Cubes")]
    public sealed class Day17 : SolutionBase
    {
        private class Cube3Dim
        {
            private Dictionary<Coordinate, State> data = new Dictionary<Coordinate, State>();
            private Coordinate min = new Coordinate {x = 0, y = 0, z = 0};
            private Coordinate max;
            private StringBuilder output = new StringBuilder();

            public struct Coordinate
            {
                public int x;
                public int y;
                public int z;

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

            private enum State
            {
                Inactive,
                Active
            }
            public string Output
            {
                get => output.ToString();
            }

            public long CountActiveState
            {
                get => data.Count(kvp => kvp.Value == State.Active);

            }

            public void Setup(string input)
            {
                var slice = (from line in GetLines(input)
                    //from cube in cubes
                    where !string.IsNullOrWhiteSpace(line)
                    //let state = (State) Enum.Parse(typeof(State), cube.ToString())
                    select line).ToArray();

                max = new Coordinate {x = slice.Length, y = slice.Length, z = 1};

                int y = 0;
                foreach (var line in slice)
                {
                    int x = 0;
                    foreach (var @char in line)
                    {
                        //var state = (State)Enum.Parse(typeof(State), @char.ToString());
                        var state = @char switch
                        {
                            '#' => State.Active,
                            _ => State.Inactive
                        };
                        data.Add(new Coordinate { x = x, y = y, z = 0 }, state);
                        x++;

                    }
                    y++;
                }

                if (Debugger.IsAttached)
                    DebugOutput();
            }

            public void ChangeState()
            {
                Dictionary<Coordinate, State> currentCube = new Dictionary<Coordinate, State>();
                for (var z = min.z - 1; z <= max.z + 1; z++)
                {
                    for (var y = min.y - 1; y <= max.y; y++)
                    {
                        for (var x = min.x - 1; x <= max.x; x++)
                        {
                            var currentPos = new Coordinate {x = x, y = y, z = z};
                            var currentState = State.Inactive;
                            if (!data.TryGetValue(currentPos, out currentState))
                            {
                                //currentCube.Add(currentPos, currentState);
                            }

                            var countActiveNeighbors = CountNeighbors(currentPos);
                            if (currentState == State.Active)
                            {
                                if (countActiveNeighbors >= 2 && countActiveNeighbors <= 3)

                                    currentCube.Add(currentPos, State.Active);
                            }
                            else
                            {
                                if (countActiveNeighbors == 3)
                                    currentCube.Add(currentPos, State.Active);
                            }
                        }
                    }
                }

                data = currentCube;
                if (Debugger.IsAttached)
                    DebugOutput();
                Resize();
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
                            var neighbor = new Coordinate { x = x, y = y, z = z };
                            if (neighbor != coordinate)
                                if (data.ContainsKey(neighbor) && data[neighbor] == State.Active)
                                    count++;
                        }
                    }
                }
                return count;
            }

            public void DebugOutput()
            {
                for (var z = min.z; z < max.z; z++)
                {
                    output.AppendLine($"z={z}");
                    for (var y = min.y; y < max.y; y++)
                    {
                        for (var x = min.x; x < max.x; x++)
                        {
                            var coord = new Coordinate {x = x, y = y, z = z};
                            var state = State.Inactive;
                            if (data.TryGetValue(coord, out state))
                                output.Append(state == State.Active ? '#' : '.');
                            else output.Append('.');
                        }

                        output.AppendLine();
                    }

                    output.AppendLine();
                }

                output.AppendLine(new string('-', 10));
            }
        }

        public override async Task<string> Part1Async(string input)
        { 
            var cube = new Cube3Dim();
            cube.Setup(input);
            int cycle = 0;
            do
            {
                cycle++;
                cube.ChangeState();
            } while (cycle < 6);
            var cubesInActiveState = cube.CountActiveState; // ChangeState(cube);
            return cubesInActiveState.ToString();
        }

        public override async Task<string> Part2Async(string input)
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
            private HashSet<Coordinate> data = new HashSet<Coordinate>();
            private Coordinate min = new Coordinate { x = 0, y = 0, z = 0 };
            private Coordinate max;
            private StringBuilder output = new StringBuilder();

            public struct Coordinate
            {
                public int x;
                public int y;
                public int z;
                public int w;

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

            public string Output
            {
                get => output.ToString();
            }

            public long CountActiveState
            {
                get => data.Count();

            }

            public void Setup(string input)
            {
                var slice = (from line in GetLines(input)
                             where !string.IsNullOrWhiteSpace(line)
                             select line).ToArray();

                max = new Coordinate { x = slice.Length, y = slice.Length, z = 1, w = 1};

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
                if (Debugger.IsAttached)
                    DebugOutput();
                Resize();
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
                                if (neighbor != coordinate)
                                    if (data.Contains(neighbor))
                                        count++;
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
                            var neighbor = new Coordinate { x = x, y = y, z = z };
                            if (neighbor != coordinate)
                                neighbors.Add(neighbor);
                        }
                    }
                }
                return neighbors;
            }

            public void DebugOutput()
            {
                for (var w = min.w; w < max.w; w++)
                {
                    output.AppendLine($"w={w}");
                    for (var z = min.z; z < max.z; z++)
                    {
                        output.AppendLine($"z={z}");
                        for (var y = min.y; y < max.y; y++)
                        {
                            for (var x = min.x; x < max.x; x++)
                            {
                                var coord = new Coordinate {x = x, y = y, z = z};
                                if (data.Contains(coord))
                                    output.Append('#');
                                else output.Append('.');
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
