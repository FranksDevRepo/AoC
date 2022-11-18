using System;
using aoc2022.Puzzles.Core;

namespace aoc2022.Puzzles.Test;

public abstract class TestBase<TSolution> where TSolution : ISolution
{
    protected TSolution Solution { get; }

    protected TestBase()
    {
        Solution = Activator.CreateInstance<TSolution>();
    }
}