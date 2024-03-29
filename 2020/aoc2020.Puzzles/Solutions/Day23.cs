﻿using System;
using aoc2020.Puzzles.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Crab Cups")]
    public sealed class Day23 : SolutionBase
    {
        public override string Part1(string input)
        {
            var game = new CrapCupsGame(input);
            game.Play(100);
            return game.Result();
        }

        public override string Part2(string input)
        {
            var game = new CrapCupsGame(input);
            game.SetupPart2();
            game.Play(10_000_000);
            return game.ResultPart2();
        }

        private class CrapCupsGame
        {
            private readonly LinkedList<int> _cups;
            // create an index to have a "fast" node search
            private readonly Dictionary<int, LinkedListNode<int>> _cupsIndex;
            private LinkedListNode<int> _destination;
            private readonly int[] _pickup = new int[3];

            private int CurrentCup { get; set; }

            private LinkedListNode<int> CurrentNode { get; set; }

            private void SetDestinationNode()
            {
                var dest = CurrentCup;
                do
                {
                    dest--;
                    if (dest < 1)
                        dest = _cupsIndex.Count;
                } while (_pickup.Contains(dest));

                _destination = _cupsIndex[dest];
            }

            public string Result()
            {
                var result = new StringBuilder();
                var nextNode = _cups.Find(1).NextOrFirst();

                for (int i = 0; i < _cups.Count - 1; i++)
                {
                    result.Append(nextNode.Value.ToString());
                    nextNode = nextNode.NextOrFirst();
                }
                return result.ToString();
            }

            public CrapCupsGame(string input)
            {
                var cups = (from @char in input.Replace(Environment.NewLine, string.Empty)
                            select int.Parse(@char.ToString()))
                    .ToList();

                _cups = new LinkedList<int>(cups);

                CurrentNode = _cups.First;
                CurrentCup = CurrentNode.Value;

                _cupsIndex = new Dictionary<int, LinkedListNode<int>>();
            }

            public void Play(int rounds)
            {
                var node = _cups.First;
                while (node != null)
                {
                    _cupsIndex.Add(node.Value, node);
                    node = node.Next;
                }

                for (int i = 0; i < rounds; i++)
                {
                    Move();
                }
            }

            private void Move()
            {
                _pickup[0] = CurrentNode.NextOrFirst().Value;
                _cups.Remove(CurrentNode.NextOrFirst());
                _pickup[1] = CurrentNode.NextOrFirst().Value;
                _cups.Remove(CurrentNode.NextOrFirst());
                _pickup[2] = CurrentNode.NextOrFirst().Value;
                _cups.Remove(CurrentNode.NextOrFirst());

                SetDestinationNode();

                var destNode = _cups.AddAfter(_destination, _pickup[0]);
                _cupsIndex[destNode.Value] = destNode;
                destNode = _cups.AddAfter(_destination.NextOrFirst(), _pickup[1]);
                _cupsIndex[destNode.Value] = destNode;
                destNode = _cups.AddAfter(_destination.NextOrFirst().NextOrFirst(), _pickup[2]);
                _cupsIndex[destNode.Value] = destNode;

                CurrentNode = CurrentNode.NextOrFirst();
                CurrentCup = CurrentNode.Value;
            }

            public void SetupPart2()
            {
                var number = _cups.Max() + 1;
                for (int i = _cups.Count; i < 1_000_000; i++)
                {
                    _cups.AddLast(number);
                    number++;
                }
            }

            public string ResultPart2()
            {
                var node = _cups.Find(1);
                long factor1 = node.Next.Value;
                long factor2 = node.Next.Next.Value;
                var result = factor1 * factor2;
                return result.ToString();
            }
        }
    }

    internal static class CircularLinkedList
    {
        public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> node) =>
            node.Next ?? node.List.First;
    }
}
