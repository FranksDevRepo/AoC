using aoc2020.Puzzles.Core;
using aoc2020.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Ticket Translation")]
    public sealed class Day16 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            int ticketScanningErrorRate = 0;

            Func<int, bool> isValidClass = (ticketClass) => (ticketClass >=1 && ticketClass <= 3) || (ticketClass >= 5 && ticketClass <= 7);
            Func<int, bool> isValidRow = (row) => (row >= 6 && row <= 11) || (row >= 33 && row <= 44);
            Func<int, bool> isValidSeat = (seat) => (seat >= 13 && seat <= 40) || (seat >= 45 && seat <= 50);

            var lines = (from line in GetLines(input)
                where !string.IsNullOrWhiteSpace(line)
                select line).ToArray();

            List<Func<int, bool>> ruleSet = new List<Func<int, bool>>();
            List<int[]> tickets = new List<int[]>();
            List<(int[], int)> errorResults = new List<(int[], int)>();

            bool isMyTicketLine = false;
            bool isNearbyTicketLine = false;

            foreach (var line in lines)
            {
                if (line.StartsWith("your ticket"))
                {
                    isMyTicketLine = true;
                    continue;
                }
                else if (line.StartsWith("nearby tickets"))
                {
                    isNearbyTicketLine = true;
                    continue;
                }

                if(!isMyTicketLine && !isNearbyTicketLine)
                {
                    var rule = ParseRule(line);
                    ruleSet.Add(rule);
                    continue;
                }

                if (isNearbyTicketLine)
                {
                    var ticket = line.Split(',').Select(n => int.Parse(n)).ToArray();
                    tickets.Add(ticket);
                }

            }

            foreach (var ticket in tickets)
            {
                int errorRate = CheckTicket(ticket, ruleSet);
                errorResults.Add((ticket, errorRate));
                ticketScanningErrorRate += errorRate;
            }
            return ticketScanningErrorRate.ToString();
        }

        private int CheckTicket(int[] ticket, List<Func<int, bool>> ruleSet)
        {
            int errorRate = 0;
            foreach (var field in ticket)
            {
                bool isValid = false;
                foreach (var isValidFunc in ruleSet)
                {
                    if (isValidFunc(field))
                    {
                        isValid = true;
                        break;
                    }
                }

                if (!isValid)
                    errorRate += field;
            }

            return errorRate;
        }

        private Func<int, bool> ParseRule(string input)
        {
            var regexRules =
                new Regex(@"(?'type'[\w\s]+): ((?'min1'\d+)-(?'max1'\d+)) or ((?'min2'\d+)-(?'max2'\d+))");
            var match = regexRules.Match(input);
            if (!match.Success)
                throw new InvalidOperationException();
            string rule = match.Groups["type"].Value;
            int min1 = int.Parse(match.Groups["min1"].Value);
            int max1 = int.Parse(match.Groups["max1"].Value);
            int min2 = int.Parse(match.Groups["min2"].Value);
            int max2 = int.Parse(match.Groups["max2"].Value);
            return (value) => (value >= min1 && value <= max1) || (value >= min2 && value <= max2);
        }

        public override async Task<string> Part2Async(string input)
        {
            throw new NotImplementedException();
        }
    }
}
