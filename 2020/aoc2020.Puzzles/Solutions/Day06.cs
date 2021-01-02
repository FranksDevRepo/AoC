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
            int commonPositiveGroupAnswers = 0;
            //var solution = new List<string>();
            //int groupCounter = 0;
            //int personCounter = 0;
            foreach (var customDeclarationForm in customDeclarationForms)
            {
                //groupCounter++;
                HashSet<char> commonPositiveGroupAnswerSet = null;
                foreach (var personAnswer in customDeclarationForm)
                {
                    //personCounter++;
                    if (commonPositiveGroupAnswerSet == null)
                        commonPositiveGroupAnswerSet = new HashSet<char>(personAnswer);
                    else
                        commonPositiveGroupAnswerSet.IntersectWith(personAnswer);
                }
                //solution.Add($"{groupCounter.ToString(),5}: {personCounter.ToString(), 5} => {string.Join(", ", commonPositiveGroupAnswerSet.OrderBy(c => c)), -100} => {commonPositiveGroupAnswerSet.Count, 5} => {commonPositiveGroupAnswers.ToString(), 5}");
                if (commonPositiveGroupAnswerSet != null)
                    commonPositiveGroupAnswers += commonPositiveGroupAnswerSet.Count;
            }

            //var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //File.WriteAllLines(Path.Combine(rootDir, @"..\\..\\..\\..\\aoc2020.Puzzles", "Input", "solution day06 part02.txt"), solution);
            return commonPositiveGroupAnswers.ToString();
        }

        private List<HashSet<char>> GetCustomDeclarationForms(string input)
        {
            var customDeclarationForms = new List<HashSet<char>>();
            foreach (var groupAnswers in input.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                HashSet<char> groupAnswerSet = new HashSet<char>();
                foreach (char answer in groupAnswers.Replace(Environment.NewLine, string.Empty))
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
            foreach (var groupAnswers in input.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.TrimEntries))
            {
                List<HashSet<char>> groupAnswerSet = new List<HashSet<char>>();
                HashSet<char> personAnswerSet = new HashSet<char>();
                foreach (char answer in groupAnswers.TrimEnd('\n'))
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
