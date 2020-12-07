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
            int positiveGroupAnwers = customDeclarationForms.Sum(hs => hs.Count);
            return positiveGroupAnwers.ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            var customDeclarationForms = GetCustomDeclarationFormsPart2(input);
            int commonPositiveGroupAnswers = 0; //customDeclarationForms.Sum(l => l.Sum(hs => l.Count));
            foreach (var customDeclarationForm in customDeclarationForms)
            {
                HashSet<char> commonPositiveGroupAnswerSet = null;
                foreach (var personAnswer in customDeclarationForm)
                {
                    if (commonPositiveGroupAnswerSet==null)
                        commonPositiveGroupAnswerSet = new HashSet<char>(personAnswer);
                    else
                        commonPositiveGroupAnswerSet.IntersectWith(personAnswer);
                }

                commonPositiveGroupAnswers += commonPositiveGroupAnswerSet.Count;
            }

            return commonPositiveGroupAnswers.ToString();
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
        private List<List<HashSet<char>>> GetCustomDeclarationFormsPart2(string input)
        {
            var customDeclarationForms = new List<List<HashSet<char>>>();
            foreach (var groupAnswers in input.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                List<HashSet<char>> groupAnswerSet = new List<HashSet<char>>();
                HashSet<char> personAnswerSet = new HashSet<char>();
                foreach (char answer in groupAnswers)
                {
                    if (answer == '\n')
                    {
                        groupAnswerSet.Add(personAnswerSet);
                        personAnswerSet = new HashSet<char>();
                        continue;
                    }
                    if (!personAnswerSet.Contains(answer))
                        personAnswerSet.Add(answer);
                }
                groupAnswerSet.Add(personAnswerSet);
                customDeclarationForms.Add(groupAnswerSet);
            }
            return customDeclarationForms;
        }
    }
}
