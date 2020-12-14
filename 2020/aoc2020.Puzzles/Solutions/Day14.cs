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

            public BitMaskInstruction(string bitMask) : base(Operation.BitMask)
            {
                BitMask = bitMask;
            }
        }

        internal class MemoryInstruction : Instruction
        {
            public ulong Memory { get; private set; }
            public ulong Value { get; private set; }

            public MemoryInstruction(ulong memory, ulong value) : base(Operation.Memory)
            {
                Memory = memory;
                Value = value;
            }
        }

        internal class InvalidInstruction : Instruction
        {
            public string ErrorMessage { get; private set; }

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

            public abstract void Process();
        }

        internal abstract class InstructionHandlerPart2 : InstructionHandler
        {
            public new static InstructionHandler CreateInstructionHandler(Instruction instruction)
            {

                InstructionHandler instructionHandler = null;
                switch (instruction.Operation)
                {
                    case Operation.BitMask:
                        instructionHandler = new BitMaskInstructionHandler(instruction);
                        break;
                    case Operation.Memory:
                        instructionHandler = new MemoryInstructionHandlerPart2(instruction);
                        break;
                    default:
                        instructionHandler = new InvalidInstructionHandler(instruction);
                        break;
                }

                return instructionHandler;
            }
        }

        internal class InvalidInstructionHandler : InstructionHandler
        {
            public InvalidInstruction Instruction { get; private set; }

            public InvalidInstructionHandler(Instruction instruction)
            {
                Instruction = (InvalidInstruction) instruction;
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
                Instruction = (MemoryInstruction) instruction;
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
                for (int i = 0; i < bitmask.Length; i++)
                {
                    if (reversedBitmask[i] == 'X')
                        continue;
                    long bitValue = 2 ^ i;
                    if (reversedBitmask[i] == '1')
                        value = SetSpecificBitAtPosition(value, i);
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
            public MemoryInstruction Instruction { get; private set; }

            public MemoryInstructionHandlerPart2(Instruction instruction)
            {
                Instruction = (MemoryInstruction) instruction;
            }

            public override void Process()
            {
                var bitmask = currentBitMask.BitMask;
                var memoryAddresses = ApplyBitMask(bitmask, Instruction.Memory);
                foreach (var memoryAddress in memoryAddresses)
                {
                    if (!memory.ContainsKey(memoryAddress))
                    {
                        memory.Add(memoryAddress, Instruction.Value);
                    }
                    else
                    {
                        memory[memoryAddress] = Instruction.Value;
                    }
                }

            }

            private List<ulong> ApplyBitMask(string bitmask, in ulong address)
            {
                var reversedBitmask = bitmask.Reverse().ToArray();
                var baseAddress = address;
                for (int i = 0; i < bitmask.Length; i++)
                {
                    long bitValue = 2 ^ i;
                    if (reversedBitmask[i] == '1')
                    {
                        baseAddress = SetSpecificBitAtPosition(baseAddress, i);
                    }
                    else if (reversedBitmask[i] == '0')
                        continue;
                }

                var addressList = new List<ulong>() {baseAddress};
                int count = 0;

                for (int i = 0; i < bitmask.Length; i++)
                {
                    long bitValue = 2 ^ i;
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
            public BitMaskInstruction Instruction { get; private set; }

            public BitMaskInstructionHandler(Instruction instruction)
            {
                Instruction = (BitMaskInstruction) instruction;
            }

            public override void Process()
            {
                currentBitMask = Instruction;
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


        internal static BitMaskInstruction currentBitMask = null;
        internal static Dictionary<ulong, ulong> memory = new Dictionary<ulong, ulong>();

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

        public override async Task<string> Part2Async(string input)
        {
            var instructions = from line in GetLines(input)
                where !string.IsNullOrWhiteSpace(line)
                select line;

            foreach (var instructionString in instructions)
            {
                var instruction = ParseInstruction(instructionString);
                var instructionHandler = InstructionHandlerPart2.CreateInstructionHandler(instruction);
                instructionHandler.Process();

            }

            return memory.Select(kvp => kvp.Value).Aggregate((currentSum, item) => currentSum + item).ToString();
        }
    }
}
