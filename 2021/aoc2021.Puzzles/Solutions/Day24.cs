using aoc2021.Puzzles.Core;
using aoc2021.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Arithmetic Logic Unit")]
public sealed class Day24 : SolutionBase
{
    public override string Part1(string input)
    {
        var instructions = GetLines(input);

        HashSet<string> variables = new();
        var instructionParser = new Regex(@"^(?<command>(inp|add|mul|div|mod|eql)) (?<variable>\w) ?(?<parameter>(\w|-?\d{0,2}))?", RegexOptions.Compiled);
        foreach (var instruction in instructions)
        {
            var match = instructionParser.Match(instruction);
            if(!match.Success)
                continue;
            var command = match.Groups["command"].Value;
            var variable = match.Groups["variable"].Value;
            var parameter = match.Groups["parameter"].Value;
            bool hasValue = int.TryParse(parameter, out var value);
            variables.Add(variable);
            if(!hasValue && parameter.Length > 0)
                variables.Add(parameter);
        }

        return string.Empty;
    }

    public override string Part2(string input)
    {
        throw new NotImplementedException();
    }
}

public interface ICommand
{
    public int Excecute();
}

public class Addition : ICommand
{
    public Addition(int summand1, int summand2)
    {
        
    }

    public int Excecute()
    {
        throw new NotImplementedException();
    }
}

