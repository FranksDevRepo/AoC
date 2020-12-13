using aoc2020.Puzzles.Core;
using aoc2020.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Shuttle Search")]
    public sealed class Day13 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            var data =
                (from line in GetLines(input)
                    where !string.IsNullOrWhiteSpace(line)
                    select line).ToArray();

            var earliestPossibeDepartureTime = long.Parse(data[0]);
            var busIDs = data[1].Split(',')
                .Where(id => id != "x")
                .Select(id => long.Parse(id))
                .ToArray();

            Dictionary<long, long> departureTimes = new Dictionary<long, long>();
            foreach (var busID in busIDs)
            {
                long departureTime = GetNextDepartureTime(busID, earliestPossibeDepartureTime);
                departureTimes.Add(departureTime, busID);

            }

            long nextDepartureTime = departureTimes.Keys.Min();
            long minutesTillNextDepartureTime = nextDepartureTime - earliestPossibeDepartureTime;

            return (minutesTillNextDepartureTime * departureTimes[nextDepartureTime]).ToString();
        }

        private long GetNextDepartureTime(in long busId, in long departureTime)
        {
            https://stackoverflow.com/questions/2403631/how-do-i-find-the-next-multiple-of-10-of-any-integer
            return departureTime + (busId - departureTime % busId);
        }

        public override async Task<string> Part2Async(string input)
        {
            throw new NotImplementedException();
        }
    }
}
