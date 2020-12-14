using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Docking Data")]
    public sealed class Day14 : SolutionBase
    {
        internal enum Operation
        {
            BitMask,
            Memory,
            Invalid
        }


        internal abstract class Instruction
        {
            public Operation Operation { get; private set; }

            public Instruction(Operation operation)
            {
                Operation = operation;
            }
        }

        internal class BitMaskInstruction : Instruction
        {
            public string BitMask { get; private set; }

            public BitMaskInstruction(string bitMask):base(Operation.BitMask)
            {
                BitMask = bitMask;
            }
        }

        internal class MemoryInstruction : Instruction
        {
            public long Memory { get; private set; }
            public ulong Value { get; private set; }

            public MemoryInstruction(long memory, ulong value):base(Operation.Memory)
            {
                Memory = memory;
                Value = value;
            }
        }

        internal class InvalidInstruction : Instruction
        {
            public string ErrorMessage { get; private set; }

            public InvalidInstruction():base(Operation.Invalid)
            {
            }

            public InvalidInstruction(string errorMessage):base(Operation.Invalid)
            {
                ErrorMessage = errorMessage;
            }
        }

        internal abstract class InstructionHandler
        {
            public static InstructionHandler CreateInstructionHandler(Instruction instruction)
            {

                InstructionHandler instructionHandler = null;
                switch (instruction.Operation)
                {
                    case Operation.BitMask:
                        instructionHandler = new BitMaskInstructionHandler(instruction);
                        break;
                    case Operation.Memory:
                        instructionHandler = new MemoryInstructionHandler(instruction);
                        break;
                    default:
                        instructionHandler = new InvalidInstructionHandler(instruction);
                        break;
                }

                return instructionHandler;
            }

            internal class InvalidInstructionHandler : InstructionHandler
            {
                public InvalidInstruction Instruction { get; private set; }
                public InvalidInstructionHandler(Instruction instruction)
                {
                    Instruction = (InvalidInstruction)instruction;
                }

                public override void Process()
                {
                    throw new NotImplementedException();
                }
            }

            internal class MemoryInstructionHandler : InstructionHandler
            {
                public MemoryInstruction Instruction { get; private set; }
                public MemoryInstructionHandler(Instruction instruction)
                {
                    Instruction = (MemoryInstruction)instruction;
                }


                public override void Process()
                {
                    var bitmask = currentBitMask.BitMask;
                    var value = ApplyBitMask(bitmask, Instruction.Value);
                    if (!memory.ContainsKey(Instruction.Memory))
                    {
                        memory.Add(Instruction.Memory, value);
                    }
                    else
                    {
                        memory[Instruction.Memory] = value;
                    }

                }

                private ulong ApplyBitMask(string bitmask, ulong value)
                {
                    var reversedBitmask = bitmask.Reverse().ToArray();
                    for(int i = 0; i < bitmask.Length; i++)
                    {
                        if (reversedBitmask[i] == 'X')
                            continue;
                        long bitValue = 2 ^ i;
                        if (reversedBitmask[i] == '1')
                            value = unchecked (value | (1uL << i));
                        else if (reversedBitmask[i] == '0')
                        {
                            value = unchecked(value & ~(1uL << i));
                        }
                    }

                    return value;
                }
            }

            public abstract void Process();
        }

        internal class BitMaskInstructionHandler : InstructionHandler
        {
            public BitMaskInstruction Instruction { get; private set; }
            public BitMaskInstructionHandler(Instruction instruction)
            {
                Instruction = (BitMaskInstruction)instruction;
            }

            public override void Process()
            {
                currentBitMask = Instruction;
            }
        }


        internal static BitMaskInstruction currentBitMask = null;
        internal static Dictionary<long, ulong> memory = new Dictionary<long, ulong>();

        public override async Task<string> Part1Async(string input)
        {
            var instructions = from line in GetLines(input)
                where !string.IsNullOrWhiteSpace(line)
                select line;

            foreach (var instructionString in instructions)
            {
                var instruction = ParseInstruction(instructionString);
                var instructionHandler = InstructionHandler.CreateInstructionHandler(instruction);
                instructionHandler.Process();

            }



            return memory.Select(kvp => kvp.Value).Aggregate((currentSum, item) => currentSum + item).ToString();
        }

        private Instruction ParseInstruction(string instructionString)
        {
            var instructionRegex = new Regex(@"((mask) = (?'mask'[X10]{36,36}))|((mem\[(?'memory'\d+)\]) = (?'value'\d+))");
            var instructionMatch = instructionRegex.Match(instructionString);
            if (!instructionMatch.Success)
                return new InvalidInstruction();
            if (!string.IsNullOrWhiteSpace(instructionMatch.Groups["mask"].Value))
            {
                return new BitMaskInstruction(instructionMatch.Groups["mask"].Value);
            }
            else
            {
                var memory = long.Parse(instructionMatch.Groups["memory"].Value);
                var value = ulong.Parse(instructionMatch.Groups["value"].Value);
                return new MemoryInstruction(memory, value);
            }



        }

        public override async Task<string> Part2Async(string input)
        {
            throw new NotImplementedException();
        }
    }
}
