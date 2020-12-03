using NUnit.Framework;
using System;
using aoc2020.Puzzles.Core;

namespace aoc2020.Puzzles.Test
{
    public abstract class TestBase<TSolution> where TSolution : ISolution
    {
        protected TSolution Solution { get; private set; }

        public TestBase()
        {
            this.SetUp();
        }

        [SetUp]
        protected virtual void SetUp()
        {
            Solution = Activator.CreateInstance<TSolution>();
        }
    }
}
