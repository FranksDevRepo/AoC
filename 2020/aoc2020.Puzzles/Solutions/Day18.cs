using aoc2020.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Operation Order")]
    public sealed class Day18 : SolutionBase
    {
        public override string Part1(string input)
        {
            return ParseInputAndCalcResult(input);
        }

        public override string Part2(string input)
        {
            return ParseInputAndCalcResult(input, true);
        }

        private string ParseInputAndCalcResult(string input, bool evaluationAdditionsFirst = false)
        {
            var lines = (from line in GetLines(input)
                where !string.IsNullOrWhiteSpace(line)
                select line).ToList();

            long result = 0;
            while (lines.Count > 0)
            {
                var expression = new StringBuilder(lines[0]);
                var parenthesisExp = new Regex(@"(\(\d+ [+*-] \d+(?: [+*-] \d+)*\))");

                do
                {
                    var matches = parenthesisExp.Matches(expression.ToString());

                    foreach (Match match in matches)
                    {
                        var subExpression = match.Groups[1].Value;
                        var subResult = CalcResult(subExpression, evaluationAdditionsFirst);
                        expression.Replace(subExpression, subResult.ToString());
                    }
                } while (expression.ToString().Any(c => c == '(' || c == ')'));

                result += CalcResult(expression.ToString(), evaluationAdditionsFirst);
                lines.RemoveAt(0);
            }

            return result.ToString();
        }

        private long CalcResult(string mathExpression, bool evaluateAdditionsFirst = false)
        {
            var expression = new StringBuilder(mathExpression);
            if (evaluateAdditionsFirst)
            {
                var additionRegex = new Regex(@"(\d+ \+ \d+)");
                do
                {
                    var additionMatch = additionRegex.Matches(expression.ToString());
                    foreach (Match match in additionMatch)
                    {
                        var subExpression = match.Groups[1].Value;
                        var subResult = CalcResult(subExpression, false);
                        expression.Replace(subExpression, subResult.ToString());
                    }

                } while (additionRegex.IsMatch(expression.ToString()));
            }

            var tokens = expression.ToString().Split(' ', StringSplitOptions.TrimEntries);

            var stack = new Stack<long>();
            var operationStack = new Stack<string>();

            var isDigitRegex = new Regex(@"\d+");
            var isOperandRegex = new Regex("[+*-]");

            foreach (var token in tokens)
            {
                var trimmedToken = token.Trim(new[] {'(', ')', ' '});

                if (isDigitRegex.IsMatch(trimmedToken))
                {
                    stack.Push(long.Parse(trimmedToken));
                }

                if (stack.Count > 1)
                {
                    var operation = operationStack.Pop();

                    var result = operation switch
                    {
                        "*" => stack.Pop() * stack.Pop(),
#pragma warning disable S1764 // Identical expressions should not be used on both sides of a binary operator
                        "-" => (stack.Pop() - stack.Pop()) * -1,
#pragma warning restore S1764 // Identical expressions should not be used on both sides of a binary operator
                        "+" => stack.Pop() + stack.Pop(),
                        _ => throw new InvalidOperationException($"Found unknown operation: {operation}")
                    };
                    stack.Push(result);
                }

                if (isOperandRegex.IsMatch(trimmedToken))
                {
                    operationStack.Push(trimmedToken);
                }
            }

            return stack.Pop();
        }
    }
}
