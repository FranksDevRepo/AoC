using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2019.Day1
{
    public static class Solution
    {
        public static int CalculateFuel(int mass) => mass / 3 - 2;

        public static int CalculateFuelForGivenInput(string path)
        {
            var modules = ReadModules(path);
            var fuel = modules.Select(line => CalculateFuel(line)).Sum();
            return fuel;
        }

        private static IEnumerable<int> ReadModules(string inputfilepath)
        {
            using (var reader = new StreamReader(inputfilepath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return Convert.ToInt32(line);
                }
            }
        }

        internal static int CalculateFuelIncludingMassOfFuel(int mass)
        {
            int fuel = 0;
            while (CalculateFuel(mass) >= 0)
            {
                mass = CalculateFuel(mass);
                fuel += mass;
            }
            return fuel;
        }

        internal static int CalculateFuelIncludingMassOfFuelForGivenInput(string path)
        {
            var modules = ReadModules(path);
            var fuel = modules.Select(line => CalculateFuelIncludingMassOfFuel(line)).Sum();
            return fuel;
        }
    }
}
