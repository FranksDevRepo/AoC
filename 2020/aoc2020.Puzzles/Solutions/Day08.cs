using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Handheld Halting")]
    public sealed class Day08 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            int accumulator = 0;

            var instructionRegex = new Regex("^(nop|acc|jmp) ((?:\\+|-)\\d+)");

            var lines = GetLines(input);
            
            var program = new Dictionary<int,Command>();
            int lineCount = 0;
            foreach (var line in lines)
            {
                lineCount++;
                var instructionMatch = instructionRegex.Match(line);
                if(!instructionMatch.Success)
                    continue;
                var command = new Command(instructionMatch.Groups[1].Value, int.Parse(instructionMatch.Groups[2].Value));
                program.Add(lineCount, command);
            }

            var computer = new HandHeldGameConsole(program);
            accumulator = computer.Execute();

            return accumulator.ToString();
        }

        public class HandHeldGameConsole
        {
            public Dictionary<int, Command> Program { get; private set; }
            public int Accumulator { get; private set; }
            public int Line { get; private set; }
            public HandHeldGameConsole(Dictionary<int, Command> program)
            {
                this.Program = program;
                this.Accumulator = 0;
                this.Line = 1;
            }

            public int Execute()
            {
                do
                {
                    var command = Program[Line];
                    Line = Execute(command);
                } while (Line > 0);

                return Accumulator;
            }

            private int Execute(Command command)
            {
                int nextLine = Line;
                if (command.Executed)
                    return 0;
                if (command.Instruction == "nop")
                    nextLine++;
                else if
                    (command.Instruction == "jmp")
                    nextLine += command.Parameter;
                else if (command.Instruction == "acc")
                {
                    Accumulator += command.Parameter;
                    nextLine++;
                }
                Program[Line].Executed = true;
                return nextLine;
            }
        }

        public override async Task<string> Part2Async(string input)
        {
            throw new NotImplementedException();
        }

        public class Command
        {
            public string Instruction { get; set; }
            public int Parameter { get; set; }
            public bool Executed { get; set; }

            public Command(string instruction, int parameter) : this(instruction, parameter, false)
            {}
            public Command(string instruction, int parameter, bool executed)
            {
                this.Instruction = instruction;
                this.Parameter = parameter;
                this.Executed = executed;
            }
        }
    }

    //public record Command
    //{
    //    public string Instruction { get; }
    //    public int Parameter { get; }

    //    public Command(string instruction, int parameter) => (Instruction, Parameter) = (instruction, parameter);
    //}
}
