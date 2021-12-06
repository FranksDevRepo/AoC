using aoc2021.Puzzles.Core;
using aoc2021.Puzzles.Extensions;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2021.Puzzles.Solutions;

[Puzzle("Giant Squid")]
public sealed class Day04 : SolutionBase
{
    public override string Part1(string input)
    {
        int lastDraw;
        int unmarkedNumberOnWinningBoard;
        var draws = ParseInputAndCreateBingoBoards(input, out var player);
        (lastDraw, unmarkedNumberOnWinningBoard) = player.Play(draws);
        return (lastDraw * unmarkedNumberOnWinningBoard).ToString();
    }

    public override string Part2(string input)
    {
        int lastDraw;
        int unmarkedNumberOnWinningBoard;
        var draws = ParseInputAndCreateBingoBoards(input, out var player);
        (lastDraw, unmarkedNumberOnWinningBoard) = player.PlayLastWinningBoardStrategy(draws);
        return (lastDraw * unmarkedNumberOnWinningBoard).ToString();
    }

    private static string ParseInputAndCreateBingoBoards(string input, out Player player)
    {
        (string draws, string boards) =
            GetLines(RemoveLineBreaksAndMultipleSpaces(input)).FirstOrDefault()!.Split(' ', 2, StringSplitOptions.None);
        player = new Player(boards);
        player.CreateBingoBoards();
        return draws;
    }

    static string RemoveLineBreaksAndMultipleSpaces(string s)
    {
        s = Regex.Replace(s, @"\s+", " ");
        return s;
    }

}

public class Player
{
    private readonly int[] boardNumbers;
    public List<BingoBoard> BingoBoards { get; } = new List<BingoBoard>();

    public Player(string boards)
    {
        boardNumbers = boards.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(n => Convert.ToInt32(n)).ToArray();
    }

    public void CreateBingoBoards()
    {
        foreach (var boardNumbersForOneBoard in boardNumbers.Batch(25))
        {
            var bingoBoard = new BingoBoard(boardNumbersForOneBoard);
            BingoBoards.Add(bingoBoard);
        }
    }

    public (int lastDraw, int unmarkedNumberOnWinningBoard) Play(string draws)
    {
        var drawNumbers = draws.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s));

        foreach (var drawNumber in drawNumbers)
        {
            foreach (var bingoBoard in BingoBoards)
            {
                bingoBoard.Mark(drawNumber);
                if (bingoBoard.IsWin())
                {
                    return (drawNumber, bingoBoard.SumOfUnmarkedNumber());
                }
            }
        }

        return (0, 0);
    }

    public (int lastDraw, int unmarkedNumberOnWinningBoard) PlayLastWinningBoardStrategy(string draws)
    {
        var drawNumbers = draws.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s));
        int indexBoard = 0;
        Dictionary<int, (int lastDrawNumber, int countNumberOfDraws)> boardDictionary = new();

        foreach (var bingoBoard in BingoBoards)
        {
        int countNumberOfDraws = 0;
            foreach (var drawNumber in drawNumbers)
            {
                countNumberOfDraws++;
                bingoBoard.Mark(drawNumber);
                if (bingoBoard.IsWin())
                {
                    boardDictionary[indexBoard] = (drawNumber, countNumberOfDraws);
                    break;
                }

            }
            indexBoard++;

        }

        var loosingBoard = boardDictionary.Where(b =>
            b.Value.countNumberOfDraws == boardDictionary.Values.Max(x => x.countNumberOfDraws)).FirstOrDefault();
        int lastDrawNumber = loosingBoard.Value.lastDrawNumber;
        
        return (lastDrawNumber, BingoBoards.ElementAt(loosingBoard.Key).SumOfUnmarkedNumber());
    }
}

public class BingoBoard
{
    private readonly (int number, bool isMarked)[,] board;
    public BingoBoard(IEnumerable<int> boardNumbersForOneBoard)
    {
        board = new (int number, bool isMarked)[5, 5];
        using var enumerator = boardNumbersForOneBoard.GetEnumerator();
        enumerator.MoveNext();
        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 5; col++)
            {
                board[row, col] = (enumerator.Current, false);
                enumerator.MoveNext();
            }
        }
    }

    public void Mark(int drawNumber)
    {
        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 5; col++)
            {
                if (board[row, col].number == drawNumber)
                    board[row, col].isMarked = true;
            }
        }
        
    }

    public bool IsWin()
    {
        bool IsWin = false;
        for (int index = 0; index < 5; index++)
        {
            if (IsWinningRow(index) || IsWinningColumn(index))
            {
                IsWin=true;
                break;
            }
        }
        return IsWin;
    }

    private bool IsWinningColumn(int index)
    {
        int countMarkedRows = 0;
        for (int row = 0; row < 5; row++)
        {
            if (board[row, index].isMarked)
                countMarkedRows++;
            if (countMarkedRows < row + 1)
                break;
        }

        return countMarkedRows == 5;
    }

    private bool IsWinningRow(int index)
    {
        int countMarkedColumns = 0;
        for (int column = 0; column < 5; column++)
        {
            if (board[index, column].isMarked)
                countMarkedColumns++;
            if (countMarkedColumns < column + 1)
                break;
        }

        return countMarkedColumns == 5;
    }

    public int SumOfUnmarkedNumber()
    {
        int sum = 0;
        for (int row = 0; row < 5; row++)
        {
            for (int column = 0; column < 5; column++)
            {
                if (!board[row, column].isMarked)
                {
                    sum += board[row, column].number;
                }
            }
        }

        return sum;
    }
}