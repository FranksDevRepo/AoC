using aoc2021.Puzzles.Core;

namespace aoc2021.WebApp.Visualizers
{
    public interface IVisualizer
    {
        ISolution SolutionInstance { get; set; }
    }
}
