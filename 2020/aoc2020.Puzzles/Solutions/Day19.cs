using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Monster Messages")]
    public sealed class Day19 : SolutionBase
    {
        public override string Part1(string input)
        {
            var rules = (from line in GetLines(input)
                where !string.IsNullOrWhiteSpace(line) && line.Contains(':')
                let rule = line.Split(':', StringSplitOptions.TrimEntries)
                select rule).ToDictionary(rule => int.Parse(rule[0]), rule => rule[1]);

            var messages = (from line in GetLines(input)
                where !string.IsNullOrWhiteSpace(line) && !line.Contains(':')
                select line).ToList();

            var ruleEngine = new RuleEngine(rules);
            var possibleMessages = ruleEngine.GetRule(0).ToList();

            int numberOfMessages = messages.Intersect(possibleMessages).Count();

            return numberOfMessages.ToString();
        }

        public override string Part2(string input)
        {
            throw new NotImplementedException();
        }
    }

    internal class RuleEngine
    {
        private readonly Dictionary<int, string> rules;

        public RuleEngine(Dictionary<int, string> rules)
        {
            this.rules = new Dictionary<int, string>();
            this.rules = rules;
        }

        public IEnumerable<string> GetRule(int index)
        {
            var rule = rules[index].Trim('"');

            if (rule.Equals("a") || rule.Equals("b"))
            {
                yield return rule;
                yield break;
            }

            List<List<int>> subRules = new List<List<int>>();
            var count = 0;

            var options = rule.Split('|', StringSplitOptions.TrimEntries);

            foreach (var option in options)
            {
                subRules.Add(new List<int>());
                foreach (var entry in option.Split(' ', StringSplitOptions.TrimEntries))
                {
                    subRules[count].Add(int.Parse(entry));
                }

                count++;
            }

            //if (options.Length == 0)
            //{
            //    subRules.Add(new List<int>());
            //    foreach (var entry in rule.Split(' ', StringSplitOptions.TrimEntries))
            //    {
            //        subRules[count].Add(int.Parse(entry));
            //    }

            //    count++;
            //}

            foreach (var subRule in subRules)
            {
                IEnumerable<string> first = GetRule(subRule[0]);
                IEnumerable<string> second = subRule.Count > 1 ? GetRule(subRule[1]) : new List<string>();
                IEnumerable<string> third = subRule.Count > 2 ? GetRule(subRule[2]) : new List<string>();
                foreach (var f in first)
                {
                    foreach (var s in second)
                    {
                        foreach (var t in third)
                        {
                            yield return $"{f}{s}{t}";
                        }

                        if (!third.Any())
                            yield return $"{f}{s}";
                    }

                    if (!second.Any())
                        yield return $"{f}";
                }
            }
        }
    }
}
