using aoc2020.Puzzles.Core;

namespace aoc2020.WebApp.Visualizers
{
    public interface IVisualizer
    {
        ISolution SolutionInstance { get; set; }
    }
}
