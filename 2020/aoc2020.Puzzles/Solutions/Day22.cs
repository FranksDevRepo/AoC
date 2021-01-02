using aoc2020.Puzzles.Core;
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
            var lines = from line in GetLines(input)
                where !string.IsNullOrEmpty(line)
                select line;

            var game = new Game();
            game.Start(lines);
            game.PlayRecursiveCombat(game.Players);
            var score = game.CalculateScore();
            return score.ToString();
        }
    }

    internal class Game
    {
        //private readonly List<string> debugOutput = new List<string>();
        public int GameNbr { get; private set; }
        public int Round { get; private set; }
        public List<Player> Players { get; } = new List<Player>();

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
                var player = new Player(index, playersCards[index]);
                Players.Add(player);
            }
        }

        public void Play()
        {
            while (Players.Select(p => p.HasCards).All(c => c))
            {
                Round++;
                Dictionary<Player, int> results = new Dictionary<Player, int>();
                foreach (var player in Players)
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
            var winner = Players.First(p => p.HasCards);

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

        public int PlayRecursiveCombat(List<Player> players)
        {
            GameNbr++;
            //debugOutput.Add($"=== Game {GameNbr} ===\n");
            int winner = 0;
            Round = 0;
            while (players.Select(p => p.HasCards).All(c => c))
            {
                Round++;
                //debugOutput.Add($"-- Round {Round} (Game {GameNbr}) --");
                //debugOutput.Add($"Player 1's deck: {players[0].GetCardsKey}");
                //debugOutput.Add($"Player 2's deck: {players[1].GetCardsKey}");

                if (players.Any(p => p.HadSameCards))
                {
                    //debugOutput.Add($"...anyway, back to game {--GameNbr}.");
                    return 1;
                }

                Dictionary<Player, (int, bool)> results = new Dictionary<Player, (int, bool)>();

                foreach (var player in players)
                {
                    int card = player.Draw();
                    //debugOutput.Add($"Player {player.Number} plays: {card}");
                    bool hasEnoughRemainingCards = card <= player.RemainingCards;
                    var result = (card: card, hasEnoughRemainingCards: hasEnoughRemainingCards);
                    results.Add(player, result);
                }

                if (results.All(r => r.Value.Item2))
                {
                    //debugOutput.Add($"Playing a sub-game to determine the winner...\n");
                    List<Player> recursivePlayers = new List<Player>();
                    int card = results[players[0]].Item1;
                    Player p1 = new Player(players[0].Number, players[0].Deck.Take(card).ToList());
                    recursivePlayers.Add(p1);
                    card = results[players[1]].Item1;
                    Player p2 = new Player(players[1].Number, players[1].Deck.Take(card).ToList());
                    recursivePlayers.Add(p2);
                    winner = PlayRecursiveCombat(recursivePlayers);
                }
                else
                {
                    winner = results
                        .Where(p => p.Value.Item1 == results.Max(r => r.Value.Item1))
                        .Select(r => r.Key.Number)
                        .First();
                    //debugOutput.Add($"Player {winner} wins round {Round} of game {GameNbr}\n");
                }

                players[winner - 1].Win(results
                        .SkipWhile(r => r.Key.Number != winner)
                        .Concat(results.TakeWhile(r => r.Key.Number != winner))
                        .Select(r => r.Value.Item1)
                        .ToList(), false
                );

                //var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                //File.WriteAllLines(
                //    Path.Combine(rootDir, @"..\\..\\..\\..\\aoc2020.Puzzles", "Input", "solution day22 part 2.txt"),
                //    debugOutput);
            }

            return winner;
        }
    }

    internal class Player
    {
        private readonly Dictionary<string, int> _hadSameCards;
        public List<int> Deck { get; }

        public int Number { get; }
        public bool HasCards => Deck.Any();
        public bool HadSameCards => _hadSameCards.Any(kvp => kvp.Value > 1);
        public int RemainingCards => Deck.Count;
        public string GetCardsKey => string.Join(',', Deck);

        public Player(int number, List<int> cards)
        {
            Number = number;
            Deck = new List<int>(cards);
            _hadSameCards = new Dictionary<string, int> {{GetCardsKey, 1}};
        }

        public int Draw()
        {
            int card = Deck[0];
            Deck.RemoveAt(0);
            return card;
        }

        public void Win(List<int> cards, bool part1 = true)
        {
            if (part1)
                Deck.AddRange(cards.OrderByDescending(c => c));
            else
                Deck.AddRange(cards);
            string cardsKey = GetCardsKey;
            bool hadSameCards = _hadSameCards.TryGetValue(cardsKey, out var count);
            if (hadSameCards)
                _hadSameCards[cardsKey] = ++count;
            else
                _hadSameCards.Add(cardsKey, 1);
        }
    }
}
