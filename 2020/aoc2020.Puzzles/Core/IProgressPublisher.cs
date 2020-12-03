using System.Threading.Tasks;

namespace aoc2020.Puzzles.Core
{
    public interface IProgressPublisher
    {
        bool IsUpdateProgressNeeded();
        Task UpdateProgressAsync(double current, double total);
    }
}
