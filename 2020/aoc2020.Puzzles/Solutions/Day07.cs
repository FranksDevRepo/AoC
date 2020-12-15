using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    /*
     * RegEx patterns
     * ([faded blue] bags contain) (no other bags).
     * ^(.*) bags contain no other bags
     * [light red] bags contain (1 [bright white] bag, 2 [muted yellow] bags).
     * ^(.*) bags contain \d* (.*) bag, \d* (.*) bags
     */

    [Puzzle("Handy Haversacks")]
    public sealed class Day07 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            var lines = GetLines(input);
            var outerBagRegex = new Regex("^(?'outerColor'.*?) bags contain (?'innerBags'.*).$", RegexOptions.Compiled);
            var innerBagRegex = new Regex("\\d* (?'colors'.*) bag", RegexOptions.Compiled);

            var coloredBagDict = new Dictionary<string, HashSet<string>>();

            foreach (var line in lines)
            {
                var outerBagMatch = outerBagRegex.Match(line);

                if (!outerBagMatch.Success)
                    continue;
                var outerBagColor = outerBagMatch.Groups["outerColor"].Value;
                var innerBags = outerBagMatch.Groups["innerBags"].Value;
                if (innerBags == "no other bags")
                    continue;

                foreach (var innerBag in innerBags.Split(',', StringSplitOptions.TrimEntries))
                {
                    var innerBagMatch = innerBagRegex.Match(innerBag);
                    if(!innerBagMatch.Success)
                        continue;

                    var innerBagColor = innerBagMatch.Groups["colors"].Value;
                    if (!coloredBagDict.ContainsKey(innerBagColor))
                        coloredBagDict.Add(innerBagColor, new HashSet<string>());
                    coloredBagDict[innerBagColor].Add(outerBagColor);
                }
            }


            var possibleBagColors = new HashSet<string>();
            GetPossibleBagColors("shiny gold", coloredBagDict, ref possibleBagColors);
            return possibleBagColors.Count.ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            var lines = GetLines(input);
            var outerBagRegex = new Regex("^(?'outerColor'.*?) bags contain (?'innerBags'.*).$", RegexOptions.Compiled);
            var innerBagRegex = new Regex("(?'number'\\d*) (?'colors'.*) bag", RegexOptions.Compiled);

            var coloredBagDict = new Dictionary<string, Dictionary<string, int>>();

            foreach (var line in lines)
            {
                var outerBagMatch = outerBagRegex.Match(line);

                if (!outerBagMatch.Success)
                    continue;
                var outerBagColor = outerBagMatch.Groups["outerColor"].Value;
                var innerBags = outerBagMatch.Groups["innerBags"].Value;
                if (innerBags == "no other bags")
                    continue;

                foreach (var innerBag in innerBags.Split(',', StringSplitOptions.TrimEntries))
                {
                    var innerBagMatch = innerBagRegex.Match(innerBag);
                    if (!innerBagMatch.Success)
                        continue;

                    var innerBagCount = int.Parse(innerBagMatch.Groups["number"].Value);
                    var innerBagColor = innerBagMatch.Groups["colors"].Value;
                    if (!coloredBagDict.ContainsKey(outerBagColor))
                        coloredBagDict.Add(outerBagColor, new Dictionary<string, int>());
                    coloredBagDict[outerBagColor].Add(innerBagColor, innerBagCount);
                }
            }


            int numberOfRequiredBags = GetNumberOfRequiredBags("shiny gold", coloredBagDict);
            return numberOfRequiredBags.ToString();
        }

        private int GetNumberOfRequiredBags(string color, Dictionary<string, Dictionary<string, int>> coloredBagDict)
        {
            if (!coloredBagDict.ContainsKey(color))
                return 0;
            int count = 0;
            foreach (var kvp in coloredBagDict[color])
            {
                count += kvp.Value + (kvp.Value * GetNumberOfRequiredBags(kvp.Key, coloredBagDict));
            }
            return count;
        }

        private void GetPossibleBagColors(string color, Dictionary<string, HashSet<string>> coloredBagDict, ref HashSet<string> possibleBagColors)
        {
            foreach (var bagColor in coloredBagDict[color])
            {
                possibleBagColors.Add(bagColor);
                if(coloredBagDict.ContainsKey(bagColor))
                    GetPossibleBagColors(bagColor, coloredBagDict, ref possibleBagColors);
            }
        }
    }
}
