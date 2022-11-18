using System.Collections.Generic;

namespace aoc2022.Puzzles.Core;

public interface ISolutionHandler
{
    IReadOnlyDictionary<int, SolutionMetadata> Solutions { get; }
}