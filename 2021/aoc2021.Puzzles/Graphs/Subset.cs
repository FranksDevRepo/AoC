using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2021.Puzzles.Graphs;

public class Subset<T>
{
    public Node<T> Parent { get; set; }
    public int Rank { get; set; }

    public override string ToString()
    {
        return $"Subset with rank {Rank}, parent: {Parent.Data} (index: {Parent.Index})";
    }
}
