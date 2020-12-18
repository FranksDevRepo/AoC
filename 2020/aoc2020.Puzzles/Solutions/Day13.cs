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
            // https://stackoverflow.com/questions/2403631/how-do-i-find-the-next-multiple-of-10-of-any-integer
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
                .Select((bus, index) => new {bus, index})
                .Where(x => x.bus != 0)
                .OrderByDescending(tuple => tuple.bus)
                .ToDictionary(x => x.bus, x => x.index);

            var departureInterval = busIDs.First().Key;
            var timestamp = busIDs.First().Key - (long) busIDs.First().Value;

            for (int n = 1; n <= busIDs.Count; n++)
            {
                while (busIDs.Take(n).Any(kvp => (timestamp + kvp.Value) % kvp.Key != 0))
                {
                    timestamp += departureInterval;
                }

                departureInterval = busIDs.Take(n).Select(kvp => kvp.Key).Aggregate(LCM);
            }

            return timestamp.ToString();
        }

        // taken from https://rosettacode.org/wiki/Least_common_multiple#C.23
        // not efficient enough for big primes
        //static long gcd(long m, long n)
        //{
        //    return n == 0 ? Math.Abs(m) : gcd(n, n % m);
        //}
        //static long lcm(long m, long n)
        //{
        //    return Math.Abs(m * n) / gcd(m, n);
        //}


        // see https://github.com/mathnet/mathnet-numerics/blob/master/src/Numerics/Euclid.cs
        // public static long LeastCommonMultiple(long a, long b)
        // public static long GreatestCommonDivisor(long a, long b)
        public static long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        public static long LCM(long a, long b)
        {
            return (a / GCD(a, b)) * b;
        }
    }
}

