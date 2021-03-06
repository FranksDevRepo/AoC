﻿using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Handheld Halting")]
    public sealed class Day08 : SolutionBase
    {
        public override string Part1(string input)
        {
            int accumulator = 0;

            var instructionRegex = new Regex("^(nop|acc|jmp) ((?:\\+|-)\\d+)");

            var lines = GetLines(input);

            var program = new Dictionary<int, Command>();
            int lineCount = 0;
            foreach (var line in lines)
            {
                lineCount++;
                var instructionMatch = instructionRegex.Match(line);
                if (!instructionMatch.Success)
                    continue;
                var command = new Command(instructionMatch.Groups[1].Value,
                    int.Parse(instructionMatch.Groups[2].Value));
                program.Add(lineCount, command);
            }

            var computer = new HandHeldGameConsole(program);
            computer.Execute();
            accumulator = computer.Accumulator;

            return accumulator.ToString();
        }

        public class HandHeldGameConsole
        {
            public Dictionary<int, Command> Program { get; }
            public int Accumulator { get; private set; }
            public int Line { get; private set; }
            private readonly HashSet<int> executedLines = new();

            public HandHeldGameConsole(Dictionary<int, Command> program)
            {
                this.Program = program;
                this.Accumulator = 0;
                this.Line = 1;
            }

            public bool Execute()
            {
                do
                {
                    var command = Program[Line];
                    Line = Execute(command);
                } while (Line > 0 && Line <= Program.Count);

                return Line == Program.Count + 1;
            }

            private int Execute(Command command)
            {
                int nextLine = Line;
                if (executedLines.Contains(Line))
                    return 0;
                if (command.Instruction == "nop")
                {
                    nextLine++;
                }
                else if
                   (command.Instruction == "jmp")
                {
                    nextLine += command.Parameter;
                }
                else if (command.Instruction == "acc")
                {
                    Accumulator += command.Parameter;
                    nextLine++;
                }

                executedLines.Add(Line);
                return nextLine;
            }
        }

        public override string Part2(string input)
        {
            int accumulator = 0;

            var instructionRegex = new Regex("^(nop|acc|jmp) ((?:\\+|-)\\d+)");

            var lines = GetLines(input);

            var program = new Dictionary<int, Command>();
            int lineCount = 0;
            foreach (var line in lines)
            {
                lineCount++;
                var instructionMatch = instructionRegex.Match(line);
                if (!instructionMatch.Success)
                    continue;
                var command = new Command(instructionMatch.Groups[1].Value,
                    int.Parse(instructionMatch.Groups[2].Value));
                program.Add(lineCount, command);
            }

            var corruptedInstructionsCandidates =
                program
                    .Where(i => i.Value.Instruction == "jmp" || i.Value.Instruction == "nop")
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            foreach (var corruptedInstructionsCandidate in corruptedInstructionsCandidates)
            {
                program[corruptedInstructionsCandidate.Key].Instruction =
                    corruptedInstructionsCandidate.Value.Instruction switch
                    {
                        "nop" => "jmp",
                        "jmp" => "nop",
                        _ => throw new InvalidOperationException(
                            $"Found unknown instruction: {corruptedInstructionsCandidate.Value.Instruction}")
                    };
                var computer = new HandHeldGameConsole(program);
                bool success = computer.Execute();
                accumulator = computer.Accumulator;
                if (success) break;
                program[corruptedInstructionsCandidate.Key].Instruction =
                    corruptedInstructionsCandidate.Value.Instruction switch
                    {
                        "nop" => "jmp",
                        "jmp" => "nop",
                        _ => throw new InvalidOperationException(
                            $"Found unknown instruction: {corruptedInstructionsCandidate.Value.Instruction}")
                    };
            }

            return accumulator.ToString();
        }

        public class Command
        {
            public string Instruction { get; set; }
            public int Parameter { get; set; }

            public Command(string instruction, int parameter)
            {
                this.Instruction = instruction;
                this.Parameter = parameter;
            }
        }
    }
}

