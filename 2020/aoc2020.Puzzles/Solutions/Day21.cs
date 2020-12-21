using aoc2020.Puzzles.Core;
using aoc2020.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Allergen Assessment")]
    public sealed class Day21 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            var foods = ParseInput(input);

            return foods.Count.ToString();
        }

        private List<Food> ParseInput(string input)
        {
            var lines = from line in GetLines(input)
                where !string.IsNullOrWhiteSpace(line)
                select line;

            var foodRegex = new Regex(@"(?'Ingredients'(?:\w+ )+)\(contains (?'Allergens'\w+(?:, \w+)*)\)");

            var foods = new List<Food>();
            foreach (var line in lines)
            {
                var match = foodRegex.Match(line);
                if(match.Success)
                    throw new InvalidOperationException($"Could not parse line: {line}");

                var ingredients = match.Groups["Ingredients"].Value.Split(' ', StringSplitOptions.TrimEntries);
                var allergens = match.Groups["Allergens"].Value.Split(' ', StringSplitOptions.TrimEntries);

                var food = new Food();

                food.Ingredients.UnionWith(ingredients);
                food.Allergens.UnionWith(allergens);
                
                foods.Add(food);
            }

            return foods;
        }

        public override async Task<string> Part2Async(string input)
        {
            throw new NotImplementedException();
        }

        class Ingredient
        {
            public string Name { get; private set; }
            public string Allergen { get; private set; }
            public bool ContainsAllergen => Allergen != string.Empty;
        }

        class Food
        {
            public HashSet<string> Ingredients { get; private set; }
            public HashSet<string> Allergens { get; set; }

            public Food()
            {
                Ingredients = new HashSet<string>();
                Allergens = new HashSet<string>();
            }
        }
    }
}
