using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Allergen Assessment")]
    public sealed class Day21 : SolutionBase
    {
        public override string Part1(string input)
        {
            var foods = ParseInput(input);

            var allergens = foods.SelectMany(f => f.Allergens).Distinct();

            var allergenicIngredients = new Dictionary<string, HashSet<string>>();

            foreach (var allergen in allergens)
            {
                var match = foods.Where(f => f.Allergens.Contains(allergen)).Select(f => f.Ingredients.ToList())
                    .Distinct().ToList();
                var ingredients = match.Aggregate((x, y) => x.Intersect(y).ToList()).ToHashSet();
                allergenicIngredients.Add(allergen, ingredients);
            }

            var countFoodsWithoutAllergens = foods
                .SelectMany(f => f.Ingredients)
                .Count(i => !allergenicIngredients
                    .SelectMany(ai => ai.Value)
                    .Contains(i));

            return countFoodsWithoutAllergens.ToString();

            //var foodsWithOnlyOneAllergen = foods
            //    .Where(f => f.Allergens.Count == 1)
            //    .ToList();
            //foreach (var food in foods.OrderBy(f => f.Allergens.Count))
            //{
            //    foreach (var ingredient in food.Ingredients)
            //    {
            //        foreach (var otherFood in foods.Where(f => f.Ingredients.Contains(ingredient) && !f.Ingredients.Equals(food.Ingredients)))
            //        {
            //            foreach (var allergen in food.Allergens)
            //            {
            //                if (!otherFood.Allergens.Contains(allergen))
            //                {

            //                }
            //            }
            //        }
            //    }
            //}
            //return foods.Count.ToString();
        }

        public override string Part2(string input)
        {
            var foods = ParseInput(input);

            var allergens = foods.SelectMany(f => f.Allergens).Distinct();

            var allergenicIngredients = new Dictionary<string, HashSet<string>>();

            foreach (var allergen in allergens)
            {
                var match = foods.Where(f => f.Allergens.Contains(allergen)).Select(f => f.Ingredients.ToList())
                    .Distinct().ToList();
                var ingredients = match.Aggregate((x, y) => x.Intersect(y).ToList()).ToHashSet();
                allergenicIngredients.Add(allergen, ingredients);
            }

            while (allergenicIngredients.Values.Any(i => i.Count != 1))
            {
                var ingredientsWithOnlyOneAllergen = allergenicIngredients
                    .Where(kvp => kvp.Value.Count == 1)
                    .SelectMany(kvp => kvp.Value).ToList();
                var ingredientsWithMoreThanOneAllergen = allergenicIngredients
                    .Where(kvp => kvp.Value.Count > 1)
                    .Select(kvp => kvp.Key).ToList();
                ingredientsWithMoreThanOneAllergen.ForEach(x =>
                    allergenicIngredients[x] = allergenicIngredients[x]
                        .Where(i => !ingredientsWithOnlyOneAllergen.Contains(i)).ToHashSet());
            }

            var i = string.Join(',', allergenicIngredients.OrderBy(x => x.Key).SelectMany(kvp => kvp.Value));

            return i;
        }

        private List<Food> ParseInput(string input)
        {
            var lines = from line in GetLines(input)
                        where !string.IsNullOrWhiteSpace(line)
                        select line;

            var foodRegex = new Regex(@"(?'Ingredients'\w+(?: \w+)+) \(contains (?'Allergens'\w+(?:, \w+)*)\)");

            var foods = new List<Food>();
            foreach (var line in lines)
            {
                var match = foodRegex.Match(line);
                if (!match.Success)
                    throw new InvalidOperationException($"Could not parse line: {line}");

                var ingredients = match.Groups["Ingredients"].Value.Split(' ', StringSplitOptions.TrimEntries);
                var allergens = match.Groups["Allergens"].Value.Split(',', StringSplitOptions.TrimEntries);

                var food = new Food();

                food.Ingredients.UnionWith(ingredients);
                food.Allergens.UnionWith(allergens);

                foods.Add(food);
            }

            return foods;
        }

        //private class Ingredient
        //{
        //    public string Name { get; }
        //    private string Allergen { get; }
        //    public bool ContainsAllergen => Allergen != string.Empty;
        //}

        private class Food
        {
            public HashSet<string> Ingredients { get; }
            public HashSet<string> Allergens { get; }

            public Food()
            {
                Ingredients = new HashSet<string>();
                Allergens = new HashSet<string>();
            }
        }
    }
}
