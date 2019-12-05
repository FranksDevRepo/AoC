using System;
using System.Linq;

namespace AoC2019.Day2
{
    class Solution2
    {
        internal class IntCodeProgram
        {
            private string program;

            private enum OpCode { Add = 1, Multiply = 2 };

            public IntCodeProgram(string program = "1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,9,1,19,1,19,6,23,2,6,23,27,2,27,9,31,1,5,31,35,1,35,10,39,2,39,9,43,1,5,43,47,2,47,10,51,1,51,6,55,1,5,55,59,2,6,59,63,2,63,6,67,1,5,67,71,1,71,9,75,2,75,10,79,1,79,5,83,1,10,83,87,1,5,87,91,2,13,91,95,1,95,10,99,2,99,13,103,1,103,5,107,1,107,13,111,2,111,9,115,1,6,115,119,2,119,6,123,1,123,6,127,1,127,9,131,1,6,131,135,1,135,2,139,1,139,10,0,99,2,0,14,0")
            {
                this.program = program;
            }

            internal int GravityAssist(int desiredOutput = 19690720)
            {
                var output = 0;
                string[] values = new string[] { };
                for (int parameter1 = 0; parameter1 < 100; parameter1++)
                {
                    for (int parameter2 = 0; parameter2 < 100; parameter2++)
                    {
                        values = program.Split(',');
                        values[1] = parameter1.ToString();
                        values[2] = parameter2.ToString();
                        var i = 0;
                        while (values.Skip(i * 4).First() != "99")
                        {
                            var opCodes = values.Skip(i * 4).Take(4).ToArray();
                            var opCode = opCodes.First();

                            var value1 = Convert.ToInt32(values[Convert.ToInt32(opCodes[1])]);
                            var value2 = Convert.ToInt32(values[Convert.ToInt32(opCodes[2])]);
                            var result = 0;

                            if (Convert.ToInt32(opCode) == (int)OpCode.Add)
                                result = value1 + value2;
                            else
                                result = value1 * value2;
                            values[Convert.ToInt32(opCodes[3])] = result.ToString();
                            i++;
                        }
                        output = Convert.ToInt32(values.First());
                        if (output == desiredOutput) break;
                    }
                    if (output == desiredOutput) break;
                }
                var noun = Convert.ToInt32(values[1]);
                var verb = Convert.ToInt32(values[2]);
                var solution = 100 * noun + verb;
                return solution;
            }
        }
    }
}
