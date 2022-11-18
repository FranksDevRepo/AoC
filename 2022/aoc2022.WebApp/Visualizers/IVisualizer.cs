using aoc2022.Puzzles.Core;

namespace aoc2022.WebApp.Visualizers;

public interface IVisualizer
{
    ISolution SolutionInstance { get; set; }
}