using aoc2020.Puzzles.Core;
using aoc2020.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Encoding Error")]
    public sealed class Day09 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            var invalidNumber = FindInvalidNumber(input);

            return invalidNumber.ToString();
        }

        public static BigInteger FindInvalidNumber(string input, int preambleLength = 25)
        {
            var numbers = GetLines(input).Select(n => BigInteger.Parse(n));
            var preamble = numbers.Take(preambleLength).ToList();

            numbers = numbers.Skip(preambleLength).ToList();
            bool isValid;
            BigInteger invalidNumber = 0L;
            foreach (int number in numbers)
            {
                var preambleDict = new HashSet<BigInteger>(preamble);
                isValid = false;
                foreach (int preambleNumber in preamble)
                {
                    Int64 difference = number - preambleNumber;
                    if (preambleDict.Contains(difference) && difference != preambleNumber)
                    {
                        isValid = true;
                        break;
                    }
                }

                if (isValid)
                {
                    preamble.RemoveAt(0);
                    preamble.Add(number);
                }
                else
                {
                    invalidNumber = number;
                    break;
                }
            }

            return invalidNumber;
        }

        public override async Task<string> Part2Async(string input)
        {
            throw new NotImplementedException();
        }
    }
}
