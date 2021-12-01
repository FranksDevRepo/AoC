using aoc2021.Puzzles.Core;
using System;

namespace aoc2021.Puzzles.Test;

public abstract class TestBase<TSolution> where TSolution : ISolution
{
    protected TSolution Solution { get; }

    protected TestBase()
    {
        Solution = Activator.CreateInstance<TSolution>();
    }
}