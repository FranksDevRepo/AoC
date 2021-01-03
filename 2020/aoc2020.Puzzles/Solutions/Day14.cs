using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
            public Operation Operation { get; }

            protected Instruction(Operation operation)
            {
                Operation = operation;
            }
        }

        internal class BitMaskInstruction : Instruction
        {
            public string BitMask { get; }

            public BitMaskInstruction(string bitMask) : base(Operation.BitMask)
            {
                BitMask = bitMask;
            }
        }

        internal class MemoryInstruction : Instruction
        {
            public ulong Memory { get; }
            public ulong Value { get; }

            public MemoryInstruction(ulong memory, ulong value) : base(Operation.Memory)
            {
                Memory = memory;
                Value = value;
            }
        }

        internal class InvalidInstruction : Instruction
        {
            public string ErrorMessage { get; }

            public InvalidInstruction() : base(Operation.Invalid)
            {
            }

            public InvalidInstruction(string errorMessage) : base(Operation.Invalid)
            {
                ErrorMessage = errorMessage;
            }
        }

        internal abstract class InstructionHandler
        {
            public static InstructionHandler CreateInstructionHandler(Instruction instruction)
            {
                InstructionHandler instructionHandler = instruction.Operation switch
                {
                    Operation.BitMask => new BitMaskInstructionHandler(instruction),
                    Operation.Memory => new MemoryInstructionHandler(instruction),
                    _ => new InvalidInstructionHandler(instruction),
                };
                return instructionHandler;
            }

            public abstract void Process(SessionContainer session);
        }

        internal abstract class InstructionHandlerPart2 : InstructionHandler
        {
            public new static InstructionHandler CreateInstructionHandler(Instruction instruction)
            {
                InstructionHandler instructionHandler = instruction.Operation switch
                {
                    Operation.BitMask => new BitMaskInstructionHandler(instruction),
                    Operation.Memory => new MemoryInstructionHandlerPart2(instruction),
                    _ => new InvalidInstructionHandler(instruction),
                };
                return instructionHandler;
            }
        }

        internal class InvalidInstructionHandler : InstructionHandler
        {
            public InvalidInstruction Instruction { get; }

            public InvalidInstructionHandler(Instruction instruction)
            {
                Instruction = (InvalidInstruction)instruction;
            }

            public override void Process(SessionContainer session)
            {
                throw new NotImplementedException();
            }
        }

        internal class MemoryInstructionHandler : InstructionHandler
        {
            public MemoryInstruction Instruction { get; }

            public MemoryInstructionHandler(Instruction instruction)
            {
                Instruction = (MemoryInstruction)instruction;
            }

            public override void Process(SessionContainer session)
            {
                var bitmask = session.CurrentBitMask.BitMask;
                var value = ApplyBitMask(bitmask, Instruction.Value);
                if (!session.Memory.ContainsKey(Instruction.Memory))
                {
                    session.Memory.Add(Instruction.Memory, value);
                }
                else
                {
                    session.Memory[Instruction.Memory] = value;
                }
            }

            private ulong ApplyBitMask(string bitmask, ulong value)
            {
                var reversedBitmask = bitmask.Reverse().ToArray();
                for (int i = 0; i < bitmask.Length; i++)
                {
                    if (reversedBitmask[i] == 'X')
                        continue;
                    if (reversedBitmask[i] == '1')
                    {
                        value = SetSpecificBitAtPosition(value, i);
                    }
                    else if (reversedBitmask[i] == '0')
                    {
                        value = UnsetSpecificBitAtPosition(value, i);
                    }
                }

                return value;
            }
        }

        internal class MemoryInstructionHandlerPart2 : InstructionHandler
        {
            public MemoryInstruction Instruction { get; }

            public MemoryInstructionHandlerPart2(Instruction instruction)
            {
                Instruction = (MemoryInstruction)instruction;
            }

            public override void Process(SessionContainer session)
            {
                var bitmask = session.CurrentBitMask.BitMask;
                var memoryAddresses = ApplyBitMask(bitmask, Instruction.Memory);
                foreach (var memoryAddress in memoryAddresses)
                {
                    if (!session.Memory.ContainsKey(memoryAddress))
                    {
                        session.Memory.Add(memoryAddress, Instruction.Value);
                    }
                    else
                    {
                        session.Memory[memoryAddress] = Instruction.Value;
                    }
                }
            }

            private List<ulong> ApplyBitMask(string bitmask, in ulong address)
            {
                var reversedBitmask = bitmask.Reverse().ToArray();
                var baseAddress = address;
                for (int i = 0; i < bitmask.Length; i++)
                {
                    if (reversedBitmask[i] == '1')
                    {
                        baseAddress = SetSpecificBitAtPosition(baseAddress, i);
                    }
                }

                var addressList = new List<ulong>() { baseAddress };
                int count = 0;

                for (int i = 0; i < bitmask.Length; i++)
                {
                    if (reversedBitmask[i] == 'X')
                    {
                        count++;
                        var newAddressList = new List<ulong>();
                        foreach (var memoryAddress in addressList)
                        {
                            newAddressList.Add(SetSpecificBitAtPosition(memoryAddress, i));
                            newAddressList.Add(UnsetSpecificBitAtPosition(memoryAddress, i));
                        }

                        addressList = newAddressList;
                        if (count == 0)
                        {
                            addressList.Remove(baseAddress);
                        }
                    }
                }

                return addressList;
            }
        }

        internal class BitMaskInstructionHandler : InstructionHandler
        {
            public BitMaskInstruction Instruction { get; }

            public BitMaskInstructionHandler(Instruction instruction)
            {
                Instruction = (BitMaskInstruction)instruction;
            }

            public override void Process(SessionContainer session)
            {
                session.CurrentBitMask = Instruction;
            }
        }

        //https://stackoverflow.com/questions/24250582/set-a-specific-bit-in-an-int
        //https://stackoverflow.com/questions/8557105/how-to-unset-a-specific-bit-in-an-integer
        //https://stackoverflow.com/questions/37881537/c-sharp-not-bit-wise-operator-returns-negative-values
        //https://www.tutorialspoint.com/csharp/csharp_bitwise_operators.htm
        private static ulong SetSpecificBitAtPosition(ulong value, int bitPosition)
        {
            ulong mask = 1uL << bitPosition;
            return unchecked(value | mask);
        }
        private static ulong UnsetSpecificBitAtPosition(ulong value, int bitPosition)
        {
            ulong mask = 1uL << bitPosition;
            return unchecked(value & ~mask);
        }

        //https://stackoverflow.com/questions/27421208/how-to-share-a-property-amongst-several-c-sharp-classes
        internal class SessionContainer
        {
            internal BitMaskInstruction CurrentBitMask { get; set; }
            internal IDictionary<ulong, ulong> Memory { get; } = new Dictionary<ulong, ulong>();
        }

        public override string Part1(string input)
        {
            var instructions = from line in GetLines(input)
                               where !string.IsNullOrWhiteSpace(line)
                               select line;

            var session = new SessionContainer();

            foreach (var instructionString in instructions)
            {
                var instruction = ParseInstruction(instructionString);
                var instructionHandler = InstructionHandler.CreateInstructionHandler(instruction);
                instructionHandler.Process(session);
            }

            return session.Memory.Select(kvp => kvp.Value).Aggregate((currentSum, item) => currentSum + item).ToString();
        }

        private Instruction ParseInstruction(string instructionString)
        {
            //https://regexr.com/
            var instructionRegex =
                new Regex(@"((mask) = (?'mask'[X10]{36,36}))|((mem\[(?'memory'\d+)\]) = (?'value'\d+))");
            var instructionMatch = instructionRegex.Match(instructionString);
            if (!instructionMatch.Success)
                return new InvalidInstruction();
            if (!string.IsNullOrWhiteSpace(instructionMatch.Groups["mask"].Value))
            {
                return new BitMaskInstruction(instructionMatch.Groups["mask"].Value);
            }
            else
            {
                var memory = ulong.Parse(instructionMatch.Groups["memory"].Value);
                var value = ulong.Parse(instructionMatch.Groups["value"].Value);
                return new MemoryInstruction(memory, value);
            }
        }

        public override string Part2(string input)
        {
            var instructions = from line in GetLines(input)
                               where !string.IsNullOrWhiteSpace(line)
                               select line;

            var session = new SessionContainer();

            foreach (var instructionString in instructions)
            {
                var instruction = ParseInstruction(instructionString);
                var instructionHandler = InstructionHandlerPart2.CreateInstructionHandler(instruction);
                instructionHandler.Process(session);
            }

            return session.Memory.Select(kvp => kvp.Value).Aggregate((currentSum, item) => currentSum + item).ToString();
        }
    }
}
