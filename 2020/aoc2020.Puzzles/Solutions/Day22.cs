using aoc2020.Puzzles.Core;
using aoc2020.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc2020.Puzzles.Solutions
{
    [Puzzle("Crab Combat")]
    public sealed class Day22 : SolutionBase
    {
        public override async Task<string> Part1Async(string input)
        {
            var lines = from line in GetLines(input)
                where !string.IsNullOrEmpty(line)
                select line;

            var game = new Game();
            game.Start(lines);
            game.Play();
            var score = game.CalculateScore();
            return score.ToString();
        }

        public override async Task<string> Part2Async(string input)
        {
            throw new NotImplementedException();
        }
    }

    class Game
    {
        private readonly List<Player> players = new List<Player>();

        public int Round { get; private set; }

        public void Start(IEnumerable<string> lines)
        {
            Dictionary<int, List<int>> playersCards = new Dictionary<int, List<int>>();
            int playerIdx = 0;
            foreach (var line in lines)
            {
                var playerRegex = new Regex(@"^Player (?'player'\d+):");

                var match = playerRegex.Match(line);

                if (match.Success)
                {
                    playerIdx = int.Parse(match.Groups["player"].Value);
                    playersCards.Add(playerIdx, new List<int>());
                }
                else
                {
                    int card = int.Parse(line);
                    playersCards[playerIdx].Add(card);
                }
            }

            foreach (var index in playersCards.Keys)
            {
                var player = new Player(playersCards[index]);
                players.Add(player);
            }
        }

        public void Play()
        {
            while (players.Select(p => p.HasCards).All(c => c == true))
            {
                Round++;
                Dictionary<Player, int> results = new Dictionary<Player, int>();
                foreach (var player in players)
                {
                    int card = player.Draw();
                    results.Add(player, card);
                }

                var winner = results
                    .OrderByDescending(kvp => kvp.Value)
                    .Select(kvp => kvp.Key)
                    .First();

                winner.Win(results.Values.ToList());
            }
        }

        public int CalculateScore()
        {
            var winner = players.Where(p => p.HasCards).First();

            var cards = winner.Deck;

            int totalScore = 0;
            int idx = 0;

            for (int score = cards.Count; score > 0; score--)
            {
                totalScore += cards[idx] * score;
                idx++;
            }

            return totalScore;
        }
    }

    class Player
    {
        public List<int> Deck { get; private set; }

        public bool HasCards => Deck.Any();

        public Player(List<int> cards)
        {
            Deck = new List<int>(cards);
        }

        public int Draw()
        {
            int card = Deck.First();
            Deck.RemoveAt(0);
            return card;
        }

        public void Win(List<int> cards)
        {
            Deck.AddRange(cards.OrderByDescending(c => c));
        }
    }
}
