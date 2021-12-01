using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Custom Customs")]
    public sealed class Day06 : SolutionBase
    {
        private readonly List<string> _debugOutput = new();
        private int _debugGroupCounter = 0;
        private int _debugPersonCounter = 0;

        public override string Part1(string input)
        {
            var customDeclarationForms = GetCustomDeclarationForms(input);
            int positiveGroupAnwers = customDeclarationForms.Sum(hs => hs.Count);
            return positiveGroupAnwers.ToString();
        }

        public override string Part2(string input)
        {
            var customDeclarationForms = GetCustomDeclarationFormsPart2(input);
            int commonPositiveGroupAnswers = 0;
            foreach (var customDeclarationForm in customDeclarationForms)
            {
                if (Debugger.IsAttached)
                    _debugGroupCounter++;
                HashSet<char> commonPositiveGroupAnswerSet = null;
                foreach (var personAnswer in customDeclarationForm)
                {
                    if (Debugger.IsAttached)
                        _debugPersonCounter++;
                    if (commonPositiveGroupAnswerSet == null)
                        commonPositiveGroupAnswerSet = new HashSet<char>(personAnswer);
                    else
                        commonPositiveGroupAnswerSet.IntersectWith(personAnswer);
                }

                if (commonPositiveGroupAnswerSet != null)
                {
                    if (Debugger.IsAttached)
                    {
                        _debugOutput.Add(
                            $"{_debugGroupCounter,5}: {_debugPersonCounter,5} => {string.Join(", ", commonPositiveGroupAnswerSet.OrderBy(c => c)),-100} => {commonPositiveGroupAnswerSet.Count,5} => {commonPositiveGroupAnswers,5}");
                    }

                    commonPositiveGroupAnswers += commonPositiveGroupAnswerSet.Count;
                }
            }

            if (Debugger.IsAttached)
            {
                var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                File.WriteAllLines(
                    Path.Combine(rootDir, @"..\\..\\..\\..\\aoc2020.Puzzles", "Input", "solution day06 part02.txt"),
                    _debugOutput);
            }

            return commonPositiveGroupAnswers.ToString();
        }

        private List<HashSet<char>> GetCustomDeclarationForms(string input)
        {
            var customDeclarationForms = new List<HashSet<char>>();
            foreach (var groupAnswers in input.Split(new string[] {Environment.NewLine + Environment.NewLine},
                StringSplitOptions.RemoveEmptyEntries))
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
            foreach (var groupAnswers in input.Split(new string[] {Environment.NewLine + Environment.NewLine},
                StringSplitOptions.TrimEntries))
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
