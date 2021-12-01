using aoc2020.Puzzles.Core;
using System;
using System.Linq;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Combo Breaker")]
    public sealed class Day25 : SolutionBase
    {
        private const long DIVISOR = 20201227;
        private const int SUBJECT_NUMBER = 7;

        public override string Part1(string input)
        {
            var keys = (from line in GetLines(input)
                where !string.IsNullOrWhiteSpace(line)
                select line).ToList();

            var cardsPublicKey = long.Parse(keys[0]);
            var doorsPublicKey = long.Parse(keys.Last());

            var secrectLoopSizeDoor = DetermineLoopSize(doorsPublicKey);

            var encryptionKeyDoor = TransformSubjectNumber(cardsPublicKey, secrectLoopSizeDoor);

            return encryptionKeyDoor.ToString();
        }

        private long DetermineLoopSize(in long cardsPublicKey)
        {
            var result = 1L;
            var loopSize = 0L;
            do
            {
                loopSize++;
                result = result * SUBJECT_NUMBER % DIVISOR;
            } while (result != cardsPublicKey);

            return loopSize;
        }

        private long TransformSubjectNumber(long subjectNumber, long loopSize)
        {
            var result = 1L;
            for (int i = 0; i < loopSize; i++)
            {
                result = result * subjectNumber % DIVISOR;
            }

            return result;
        }

        public override string Part2(string input)
        {
            throw new NotImplementedException();
        }
    }
}
