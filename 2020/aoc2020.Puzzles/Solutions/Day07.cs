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
            var outerBagRegex = new Regex("^(.*?) bags contain (.*).$", RegexOptions.Compiled);
            var innerBagRegex = new Regex("\\d* (.*) bag", RegexOptions.Compiled);

            var coloredBagDict = new Dictionary<string, HashSet<string>>();

            foreach (var line in lines)
            {
                var outerBagMatch = outerBagRegex.Match(line);

                if (!outerBagMatch.Success)
                    continue;
                var outerBagColor = outerBagMatch.Groups[1].Value;
                var innerBags = outerBagMatch.Groups[2].Value;
                if (innerBags == "no other bags")
                    continue;

                foreach (var innerBag in innerBags.Split(',', StringSplitOptions.TrimEntries))
                {
                    var innerBagMatch = innerBagRegex.Match(innerBag);
                    if(!innerBagMatch.Success)
                        continue;

                    var innerBagColor = innerBagMatch.Groups[1].Value;
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
            throw new NotImplementedException();
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
