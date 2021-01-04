using aoc2020.Puzzles.Core;
using System;

namespace aoc2020.Puzzles.Test
{
    public abstract class TestBase<TSolution> where TSolution : ISolution
    {
        protected TSolution Solution { get; }

        protected TestBase()
        {
            Solution = Activator.CreateInstance<TSolution>();
        }
    }
}
