using aoc2020.Puzzles.Core;
using aoc2020.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Combo Breaker")]
    public sealed class Day25 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            var keys = (from line in GetLines(input)
                where !string.IsNullOrWhiteSpace(line)
                select line).ToList();

            var cardsPublicKey = long.Parse(keys.First());
            var doorsPublicKey = long.Parse(keys.Last());

            var secrectLoopSizeCard = DetermineLoopSize(cardsPublicKey);
            var secrectLoopSizeDoor = DetermineLoopSize(doorsPublicKey);

            var encryptionKeyCard = TransformSubjectNumber(doorsPublicKey, secrectLoopSizeCard);
            var encryptionKeyDoor = TransformSubjectNumber(cardsPublicKey, secrectLoopSizeDoor);

            return encryptionKeyDoor.ToString();
        }

        private long DetermineLoopSize(in long cardsPublicKey)
        {
            long result = 0;
            long loopSize = 0;
            do
            {
                loopSize++;
                result = TransformSubjectNumber(7, loopSize);
            } while (result != cardsPublicKey);

            return loopSize;
        }

        private long TransformSubjectNumber(long subjectNumber, long loopSize)
        {
            long result = 1;
            for (int i = 0; i < loopSize; i++)
            {
                result *= subjectNumber;
                result %= 20201227;
            }

            return result;
        }

        public override async Task<string> Part2Async(string input)
        {
            throw new NotImplementedException();
        }
    }
}
