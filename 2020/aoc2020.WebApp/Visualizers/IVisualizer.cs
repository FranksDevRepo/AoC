using aoc2020.Puzzles.Core;
using System.Threading.Tasks;

namespace aoc2020.WebApp.Visualizers
{
    public interface IVisualizer
    {
        ISolution SolutionInstance { get; set; }
    }
}
