using aoc2021.Puzzles.Core;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Syntax Scoring")]
public sealed class Day10 : SolutionBase
{
    public override string Part1(string input)
    { 
        var lines = GetLines(input);

        var illegalCharacter = new Stack<char>();

        foreach(var line in lines)
        {
            var stack = new Stack<char>();
            foreach (var bracket in line)
            {
                if ("<({[".Contains(bracket))
                    stack.Push(bracket);
                else if (ClosingBracketMatchesOpenBracket(bracket, stack.Peek()))
                    stack.Pop();
                else
                {
                    illegalCharacter.Push(bracket);
                    break;
                }
            }
        }

        int score = 0;
        foreach (var @char in illegalCharacter)
        {
            score = @char switch
            {
                ')' => score += 3,
                ']' => score += 57,
                '}' => score += 1197,
                '>' => score += 25137,
                _ => throw new ArgumentException($"{nameof(@char)} is invalid : '{@char}'.")
            };
        }

        return score.ToString();
    }

    private static bool ClosingBracketMatchesOpenBracket(char closingBracket, char openBracket)
        => $"{openBracket}{closingBracket}" switch
        {
            "<>" => true,
            "[]" => true,
            "{}" => true,
            "()" => true,
            _ => false
        };

    public override string Part2(string input)
    {
        var lines = GetLines(input);

        var illegalCharacter = new Stack<char>();
        Dictionary<string, Stack<char>> incompleteLines = new();

        foreach (var line in lines)
        {
            var stack = new Stack<char>();
            bool corruptedLine = false;
            foreach (var bracket in line)
            {
                if ("<({[".Contains(bracket))
                    stack.Push(bracket);
                else if (ClosingBracketMatchesOpenBracket(bracket, stack.Peek()))
                    stack.Pop();
                else
                {
                    illegalCharacter.Push(bracket);
                    corruptedLine = true;
                    break;
                }
            }
            if (!corruptedLine)
                incompleteLines.Add(line, stack);
        }

        List<BigInteger> scores = new();
        foreach(var line in incompleteLines)
        {
            var closingCharacters = CompleteLine(line.Value);
            BigInteger score = CalculateScore(closingCharacters);
            scores.Add(score);
        }


        return GetMedianScore(scores).ToString();
    }

    private BigInteger GetMedianScore(List<BigInteger> scores)
    {
        scores.Sort();
        int count = scores.Count;
        int halfIndex = count / 2;
        BigInteger median;
        if ((count % 2) == 0)
            median = BigInteger.Divide(scores[halfIndex] + scores[halfIndex + 1], 2);
        else
            median = scores[halfIndex];
        return median;
    }

    private UInt64 CalculateScore(string closingCharacters)
    {
        UInt64 score = 0;
        foreach (var @char in closingCharacters)
        {
            score = @char switch
            {
                ')' => score = score * 5 + 1,
                ']' => score = score * 5 + 2,
                '}' => score = score * 5 + 3,
                '>' => score = score * 5 + 4,
                _ => throw new ArgumentException($"{nameof(@char)} is invalid : '{@char}'.")
            };
        }
        return score;
    }

    private string CompleteLine(Stack<char> openBrackets)
    {
        StringBuilder closingBrackets = new();
        foreach(var openBracket in openBrackets)
        {
            var closingBracket = openBracket switch
            {
                '<' => '>',
                '{' => '}',
                '[' => ']',
                '(' => ')',
                _ => throw new ArgumentException($"{nameof(openBracket)} is invalid : '{openBracket}'.")
            };
            closingBrackets.Append(closingBracket);
        }
        return closingBrackets.ToString();
    }
}