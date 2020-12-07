using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Custom Customs")]
    public sealed class Day06 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        { 
            var customDeclarationForms = GetCustomDeclarationForms(input);
            var positiveGroupAnwers = customDeclarationForms.Sum(hs => hs.Count);
            return positiveGroupAnwers.ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            throw new NotImplementedException();
        }

        private List<HashSet<char>> GetCustomDeclarationForms(string input)
        {
            var customDeclarationForms = new List<HashSet<char>>();
            foreach (var groupAnswers in input.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                HashSet<char> groupAnswerSet = new HashSet<char>();
                foreach (char answer in groupAnswers.Replace("\n", string.Empty))
                {
                    if (!groupAnswerSet.Contains(answer))
                        groupAnswerSet.Add(answer);
                }
                customDeclarationForms.Add(groupAnswerSet);
            }
            return customDeclarationForms;
        }

    }
}
