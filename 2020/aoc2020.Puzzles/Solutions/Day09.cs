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

        public override async Task<string> Part2Async(string input)
        {
            var invalidNumber = FindInvalidNumber(input);
            var contiguousSet = FindContiguousSetOfAtLeastTwoNumbers(input, invalidNumber);
            var solution = CalculateSolutionPart2(contiguousSet);
            return solution.ToString();
        }

        public static BigInteger CalculateSolutionPart2(IEnumerable<BigInteger> contiguousSet)
            => contiguousSet.Min() + contiguousSet.Max();

        public static IEnumerable<BigInteger> FindContiguousSetOfAtLeastTwoNumbers(string input, in BigInteger invalidNumber)
        {
            var numbers = GetLines(input).Select(n => BigInteger.Parse(n)).ToList();

            BigInteger sum = 0;
            int firstNumberIndex = 0;
            int lastNumberIndex = 0;
            do
            {
                sum += numbers[lastNumberIndex];
                while (sum > invalidNumber)
                {
                    sum -= numbers[firstNumberIndex];
                    firstNumberIndex++;
                }
                if (sum == invalidNumber)
                    break;

                lastNumberIndex++;
            } while (true);

            return numbers.Skip(firstNumberIndex - 1).Take(lastNumberIndex - firstNumberIndex + 1);
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

    }
}
