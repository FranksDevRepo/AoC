using System.Collections.Generic;

namespace aoc2021.Puzzles.Core
{
    public interface ISolutionHandler
    {
        IReadOnlyDictionary<int, SolutionMetadata> Solutions { get; }
    }
}
