﻿using aoc2020.Puzzles.Core;
using aoc2020.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Crab Cups")]
    public sealed class Day23 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            var game = new CrapCupsGame(input);
            game.Play(100);
            return game.Result();
        }

        public override async Task<string> Part2Async(string input)
        {
            throw new NotImplementedException();
        }

        class CrapCupsGame
        {
            private readonly LinkedList<int> _cups;
            private LinkedListNode<int> _currentNode;
            private LinkedListNode<int> _destination;
            private int[] _pickup = new int[3];

            public int CurrentCup { get; private set; }

            public LinkedListNode<int> CurrentNode
            {
                get => _currentNode;
                set => _currentNode = value;
            }

            public void SetDestinationNode()
            {
                    var dest = CurrentCup;
                    do
                    {
                        dest--;
                        if (dest < 1)
                            dest = 9;
                    } while (_pickup.Contains(dest));

                    _destination = _cups.Find(dest);
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
                var cups = (from @char in input.Replace("\n", string.Empty)
                        select int.Parse(@char.ToString()))
                    .ToList();

                _cups = new LinkedList<int>(cups);

                CurrentNode = _cups.First;
                CurrentCup = CurrentNode.Value;
            }

            public void Play(int rounds)
            {
                for (int i = 0; i < rounds; i++)
                {
                    Move();
                }
            }

            private void Move()
            {
                _pickup[0] = CurrentNode.NextOrFirst<int>().Value;
                _pickup[1] = CurrentNode.NextOrFirst().NextOrFirst().Value;
                _pickup[2] = CurrentNode.NextOrFirst().NextOrFirst().NextOrFirst().Value;

                _cups.Remove(CurrentNode.NextOrFirst());
                _cups.Remove(CurrentNode.NextOrFirst());
                _cups.Remove(CurrentNode.NextOrFirst());

                SetDestinationNode();

                _cups.AddAfter(_destination, _pickup[0]);
                _cups.AddAfter(_destination.NextOrFirst(), _pickup[1]);
                _cups.AddAfter(_destination.NextOrFirst().NextOrFirst(), _pickup[2]);

                CurrentNode = CurrentNode.NextOrFirst();
                CurrentCup = CurrentNode.Value;
            }
        }
    }
    static class CircularLinkedList
    {
        public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> node) =>
            node.Next ?? node.List.First;
    }

}
