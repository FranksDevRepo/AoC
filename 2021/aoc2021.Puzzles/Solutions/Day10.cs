using aoc2021.Puzzles.Core;
using System;
using System.Collections.Generic;

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
        throw new NotImplementedException();
    }
}