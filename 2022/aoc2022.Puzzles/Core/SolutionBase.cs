﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace aoc2022.Puzzles.Core;

public abstract class SolutionBase : ISolution, IProgressPublisher
{
    public event EventHandler<SolutionProgressEventArgs> ProgressUpdated;

    public int MillisecondsBetweenProgressUpdates { get; set; } = 200;

    public CancellationToken CancellationToken { get; set; }

    public virtual string Part1(string input) => throw new NotImplementedException();

    public virtual string Part2(string input) => throw new NotImplementedException();

    public virtual Task<string> Part1Async(string input) => Task.FromResult(Part1(input));

    public virtual Task<string> Part2Async(string input) => Task.FromResult(Part2(input));

    /// <summary>
    /// Breaks the input into lines and removes empty lines at the end.
    /// </summary>
    public static List<string> GetLines(string input)
    {
        return input.Replace("\r", string.Empty).Split('\n').Reverse().SkipWhile(string.IsNullOrEmpty).Reverse().ToList();
    }

    bool IProgressPublisher.IsUpdateProgressNeeded() => IsUpdateProgressNeeded();

    Task IProgressPublisher.UpdateProgressAsync(double current, double total) => UpdateProgressAsync(current, total);

    protected virtual SolutionProgress Progress { get; set; } = new();

    /// <summary>
    /// Returns true if <see cref="UpdateProgressAsync"/> should be called to update the UI of the solution runner. This happens every couple of milliseconds.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool IsUpdateProgressNeeded() => Environment.TickCount >= myUpdateTick;

    /// <summary>
    /// Updates the UI of the solution runner with the current progress, and schedules the next update a couple of milliseconds in the future.
    /// </summary>
    protected Task UpdateProgressAsync(double current, double total)
    {
        Progress.Percentage = current / Math.Max(total, double.Epsilon) * 100;
        return UpdateProgressAsync();
    }

    /// <summary>
    /// Updates the UI of the solution runner with the current progress, and schedules the next update a couple of milliseconds in the future.
    /// </summary>
    protected Task UpdateProgressAsync()
    {
        myUpdateTick = Environment.TickCount + MillisecondsBetweenProgressUpdates;
        ProgressUpdated?.Invoke(this, new SolutionProgressEventArgs(Progress));
        return Task.Delay(1, CancellationToken);
    }

    /// <summary>
    /// A scheduled tick from <see cref="Environment.TickCount"/>, when a progress update should happen.
    /// </summary>
    private int myUpdateTick = Environment.TickCount;
}