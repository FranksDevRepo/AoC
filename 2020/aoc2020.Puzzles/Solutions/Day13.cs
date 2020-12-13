using aoc2020.Puzzles.Core;
using System.Collections.Generic;
using System.Linq;
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
            var data =
                (from line in GetLines(input).Skip(1)
                 where !string.IsNullOrWhiteSpace(line)
                 select line).ToArray();

            var busIDs = data[0].Split(',')
                .Select(id => id switch
                {
                    "x" => 0,
                    _ => long.Parse(id)
                })
                .Select((bus, index) => new { bus, index })
                .Where(x => x.bus != 0)
                .ToDictionary(x => x.bus, x => x.index);

            long time = 0;

            Dictionary<long, long> validDepartureTimes = new Dictionary<long, long>();
            long firstBus = busIDs.FirstOrDefault(x => x.Value == 0).Key;
            do
            {
                time += firstBus;
                Dictionary<long, long> timeTable = new Dictionary<long, long>();
                foreach (var busID in busIDs.Keys)
                {
                    long departureTime = GetNextDepartureTime(busID, time - 1);
                    timeTable.Add(busID, departureTime);
                }

                long departureTimeFirstBus = timeTable[firstBus];
                validDepartureTimes.Clear();

                foreach (var kvp in timeTable)
                {
                    if (kvp.Key == firstBus) continue;
                    long bus = kvp.Key;
                    long departureTime = kvp.Value;
                    long minutesSinceFirstBusDepartureTime = busIDs[bus];

                    if (departureTime == departureTimeFirstBus + minutesSinceFirstBusDepartureTime)
                    {
                        validDepartureTimes.Add(bus, departureTime);
                    }
                }
                if (validDepartureTimes.Count == busIDs.Count - 1)
                {
                    validDepartureTimes.Add(firstBus, departureTimeFirstBus);
                    break;
                };


            } while (true);

            return validDepartureTimes[firstBus].ToString();
        }
    }
}
