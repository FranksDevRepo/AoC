﻿using System;

namespace aoc2021.Puzzles.Core;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class PuzzleAttribute : Attribute
{
    public int? Day { get; }

    public string Title { get; }

    public PuzzleAttribute(string title = null, int day = -1)
    {
        Day = day < 0 ? null : day;
        Title = title;
    }
}