using System;

namespace aoc2022.Puzzles.Core;

public sealed class SolutionProgressEventArgs : EventArgs
{
    public SolutionProgress Progress;

    public SolutionProgressEventArgs(SolutionProgress solutionProgress)
    {
        Progress = solutionProgress;
    }
}