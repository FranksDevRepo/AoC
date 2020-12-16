﻿using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
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
                    ruleSet.Add(rule.Item2);
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

        private (string, Func<int, bool>) ParseRule(string input)
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
            return (rule, (value) => (value >= min1 && value <= max1) || (value >= min2 && value <= max2));
        }

        public override async Task<string> Part2Async(string input)
        {
            int ticketScanningErrorRate = 0;

            var lines = (from line in GetLines(input)
                where !string.IsNullOrWhiteSpace(line)
                select line).ToArray();

            Dictionary<string, Func<int, bool>> ruleSet = new Dictionary<string, Func<int, bool>>();
            List<int[]> tickets = new List<int[]>();
            List<(int[], int)> errorResults = new List<(int[], int)>();
            int[] myTicket = new int[]{};

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

                if (!isMyTicketLine && !isNearbyTicketLine)
                {
                    var rule = ParseRule(line);
                    ruleSet.Add(rule.Item1, rule.Item2);
                    continue;
                }
                else if (isMyTicketLine && !isNearbyTicketLine)
                    myTicket = line.Split(',').Select(n => int.Parse(n)).ToArray();

                else if (isNearbyTicketLine)
                {
                    var ticket = line.Split(',').Select(n => int.Parse(n)).ToArray();
                    tickets.Add(ticket);
                }

            }

            tickets.RemoveAll(t => CheckTicket(t, ruleSet.Values.ToList()) > 0);

            var departureRules = ruleSet
                .Where(kvp => kvp.Key.StartsWith("departure"))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            Dictionary<string, int> fieldPositions = GetFieldPosition(tickets, ruleSet, departureRules);

            var departureFieldPositions = fieldPositions.Where(kvp => kvp.Key.StartsWith("departure"))
                .Select(kvp => kvp.Value).ToList();

            if(departureFieldPositions.Count > 6)
                throw new ApplicationException();

            long puzzle = 1;

            foreach (var departureFieldPosition in departureFieldPositions)
            {
                puzzle *= myTicket[departureFieldPosition];
            }

            return puzzle.ToString();
        }

        private Dictionary<string, int> GetFieldPosition(List<int[]> tickets, Dictionary<string, Func<int, bool>> ruleSet, Dictionary<string, Func<int, bool>> departureRules)
        {
            int countFields = tickets[0].Length;
            var inValidRuleColumns = new Dictionary<string, List<int>>();
            var validRuleColumns = new Dictionary<string, int>();
            for (int i = 0; i < countFields; i++)
            {
                foreach (var ticket in tickets)
                {
                    foreach (var rule in ruleSet)
                    {
                        if (!rule.Value(ticket[i]))
                        {
                            if (!inValidRuleColumns.ContainsKey(rule.Key))
                                inValidRuleColumns.Add(rule.Key, new List<int>());
                            inValidRuleColumns[rule.Key].Add(i);
                        }
                    }
                }
            }


            var columns = Enumerable.Range(0, 20);
            while (inValidRuleColumns.Count > 0)
            {
                var validColumns = inValidRuleColumns.Where(kvp => kvp.Value.Count == countFields - 1);

                foreach (var validColumn in validColumns)
                {
                    int column = columns.Where(c => !validColumn.Value.Any(e => e == c)).First();
                    validRuleColumns.Add(validColumn.Key, column);
                    inValidRuleColumns.Remove(validColumn.Key);
                    foreach (var inValidRuleColumn in inValidRuleColumns)
                    {
                        inValidRuleColumn.Value.Add(column);
                    }
                }
            }

            return validRuleColumns;
        }
    }
}