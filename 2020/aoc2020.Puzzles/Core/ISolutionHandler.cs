using System.Collections.Generic;

namespace aoc2020.Puzzles.Core
{
    public interface ISolutionHandler
    {
        IReadOnlyDictionary<int, SolutionMetadata> Solutions { get; }
    }
}
