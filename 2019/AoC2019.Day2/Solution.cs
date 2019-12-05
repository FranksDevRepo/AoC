using System;
using System.Linq;

namespace AoC2019.Day2
{
    class Solution
    {
        internal class IntCodeProgram
        {
            private string program;

            private enum OpCode { Add = 1, Multiply = 2};

            public IntCodeProgram(string program)
            {
                this.program = program;
            }

            internal string CalculateFinalState()
            {
                var values = program.Split(',');
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
                return String.Join(',', values);
            }
        }
    }
}
