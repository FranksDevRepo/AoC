using aoc2021.Puzzles.Core;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Extended Polymerization")]
public sealed class Day14 : SolutionBase
{
    public override string Part1(string input)
    {
        var polymer = ParseInputAndCreatePolymer(input, 10);

        int mostCommonElement = 0;
        int leastCommonElement = 0;
        (mostCommonElement, leastCommonElement) = CountCommonElements(polymer);

        return (mostCommonElement - leastCommonElement).ToString();
    }

    public override string Part2(string input)
    {
        var polymer = ParseInputAndCreatePolymer(input, 40);

        int mostCommonElement = 0;
        int leastCommonElement = 0;
        (mostCommonElement, leastCommonElement) = CountCommonElements(polymer);

        return (mostCommonElement - leastCommonElement).ToString();
        //return Solve(input, 40).ToString();
    }

    private static (int, int) CountCommonElements(string polymer)
    {
        var charCounter = polymer.ToString()
            .GroupBy(c => c)
            .Select(c => new { Char = c.Key, Count = c.Count() });
        var mostCommonElement = charCounter.First(c => c.Count == charCounter.Max(c => c.Count)).Count;
        var leastCommonElement = charCounter.First(c => c.Count == charCounter.Min(c => c.Count)).Count;
        return (mostCommonElement, leastCommonElement);
    }

    private static string ParseInputAndCreatePolymer(string input, int count)
    {
        var polymerTemplate = GetLines(input).First();
        var rules = GetLines(input)
            .Where(l => l.Contains("->", StringComparison.InvariantCulture))
            .Select(l => l.Split("->", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            .ToDictionary(k => k[0], v => v[1]);
            //.Select(parts => (molecule: parts[0], element: parts[1]))
            //.ToDictionary(k => k.molecule, v => v.molecule[0] + v.element + v.molecule[1]);

        int steps = 0;
        StringBuilder polymer = new StringBuilder(polymerTemplate);
        do
        {
            steps++;
            StringBuilder newPolymer = new();
            for (int index = 0; index < polymer.Length - 1; index++)
            {
                string pair = polymer.ToString()[index..(index + 2)];
                newPolymer.Append($"{pair[0]}{rules[pair]}");
            }

            newPolymer.Append(polymer.ToString()[polymer.Length - 1]);
            polymer.Length = 0;
            polymer.Append(newPolymer);
        } while (steps < count);

        return polymer.ToString();
    }


    long Solve(string input, int steps)
    {
        var (polymer, elements, polymerFromMolecule) = Parse(input);

        var cache = new Dictionary<(string, int, char), long>();

        // getElementCountAfterNSteps: Determines how many atoms of the element 
        // are present after N steps, if we are starting from the given polymer. 

        // This function needs to be cached or it will never terminate.

        long getElementCountAfterNSteps(string polymer, int steps, char element)
        {
            var key = (polymer, steps, element);
            if (!cache.ContainsKey(key))
            {
                long res = 0L;
                if (steps == 0)
                {
                    // no more steps to do, just count the element in the polymer:
                    res = polymer.Count(ch => ch == element);
                }
                else
                {

                    // The idea is that we can go over the molecules in the polymer, 
                    // and deal with them one by one. Do one replacement, recurse and
                    // sum the element counts:

                    for (var j = 0; j < polymer.Length - 1; j++)
                    {
                        var molecule = polymer.Substring(j, 2);

                        var count = getElementCountAfterNSteps(
                            polymerFromMolecule[molecule],
                            steps - 1,
                            element
                        );

                        // if the molecule ends with the searched element, the next one will include it as well,
                        // we shouldn't count it twice:
                        if (element == molecule[1] && j < polymer.Length - 2)
                        {
                            count--;
                        }

                        res += count;
                    }
                }
                cache[key] = res;
            }
            return cache[key];
        }

        // using the above helper, we can just ask for the count of each element, and quickly compute the answer
        var elementCountsAtTheEnd = (
            from element in elements
            select getElementCountAfterNSteps(polymer, steps, element)
        ).ToArray();

        return elementCountsAtTheEnd.Max() - elementCountsAtTheEnd.Min();
    }

    (string polymer, HashSet<char> elements, Dictionary<string, string> polymerFromMolecule) Parse(string input)
    {
        var blocks = input.Split("\n\n");

        // we will start from this polymer
        var polymer = blocks[0];

        // the map contains the resulted polymer after the replacement, not just the inserted element:
        //var polymerFromMolecule = (
        //    from line in blocks[1].Split("\n")
        //    let parts = line.Split(" -> ")
        //    select (molecule: parts[0], element: parts[1])
        //).ToDictionary(p => p.molecule, p => p.molecule[0] + p.element + p.molecule[1]);
        var polymerFromMolecule = GetLines(input)
            .Where(l => l.Contains("->", StringComparison.InvariantCulture))
            .Select(l => l.Split("->", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            .Select(parts => (molecule: parts[0], element: parts[1]))
            .ToDictionary(p => p.molecule, p => p.molecule[0] + p.element + p.molecule[1]);


        // set of all elements for convenience:
        //var elements = polymerFromMolecule.Keys.Select(molecule => molecule[0]).ToHashSet();
        var elements = System.Linq.Enumerable.ToHashSet(polymerFromMolecule.Keys.Select(molecule => molecule[0]));

        return (polymer, elements, polymerFromMolecule);
    }
}



/*
Python, tracking pair and character counts in two Counter dictionaries. For each replacement:
    decrease the count of the original pair,
    increase the count of the two replacement pairs,
    increase the count of new character.

This has two advantages: 
    1) we keep using the same dictionary throughout the steps, and 
    2) we don't have to compute the individual counts at the end.

from collections import Counter

tpl, _, *rules = open(0).read().split('\n')
rules = dict(r.split(" -> ") for r in rules)
pairs = Counter(map(str.__add__, tpl, tpl[1:]))
chars = Counter(tpl)

for _ in range(40):
    for (a,b), c in pairs.copy().items():
        x = rules[a+b]
        pairs[a+b] -= c
        pairs[a+x] += c
        pairs[x+b] += c
        chars[x] += c

print(max(chars.values())-min(chars.values()))
*/