using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_2021;

internal class Day04
{
    public int SolveFirst()
    {
        var bingo = new Bingo();

        return bingo.Play();
    }

    public int SolveSecond()
    {
        var bingo = new Bingo();

        return bingo.FindLatest();
    }



}

internal class Bingo
{
    IEnumerable<int> drawnNumbers;
    IList<Board> boards = new List<Board>();
    public Bingo()
    {
        var file = new StreamReader(Path.Combine("Input", "Day04.txt"));
        drawnNumbers = file.ReadLine().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
        string line;
        var buffer = new List<string>(5);
        while ((line = file.ReadLine()) is not null)
        {
            if (string.IsNullOrEmpty(line))
            {
                if (buffer.Count == 5)
                {
                    boards.Add(new Board(buffer.ToArray()));
                }
                buffer.Clear();
            }
            else
            {
                buffer.Add(line);
            }
        }
        if (buffer.Count == 5)
        {
            boards.Add(new Board(buffer.ToArray()));
        }
    }

    public int Play()
    {
        foreach (var number in drawnNumbers)
        {
            foreach (var board in this.boards)
            {
                var result = board.CheckNumberAndVictory(number);
                if(result > 0)
                    return result;
            }
        }
        return 0;
    }

    public int FindLatest()
    {
        foreach (var number in drawnNumbers)
        {
            foreach (var board in this.boards)
            {
                if (!board.IsCompleted)
                {
                    var result = board.CheckNumberAndVictory(number);
                    if (result > 0 && boards.Count(b => !b.IsCompleted) == 0)
                    {
                        return result;
                    }
                }

            }
        }
        return 0;
    }
}

internal class Board
{
    public bool IsCompleted { get; set; }
    public BingoNumber[,] innerBoard;
    public Board(string[] lines)
    {
        innerBoard = new BingoNumber[lines.Length, lines.Length];
        for (int y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            var lineNumbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            for (int x = 0; x < lines.Length; x++)
            {
                innerBoard[y,x] = new BingoNumber(lineNumbers![x]);
            }
        }
    }

    public int CheckNumberAndVictory(int number)
    {
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                if (innerBoard[y, x].Value == number)
                    innerBoard[y, x].WasDrawn = true;
            }
        }

        if (this.CheckVictory())
        {
            this.IsCompleted = true;
            var sum = 0;
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (!innerBoard[y, x].WasDrawn)
                        sum += innerBoard[y, x].Value;
                }
            }
            return sum * number;
        }
        else
            return -1;
    }

    bool CheckVictory()
    {
        for (int y = 0; y < 5; y++)
        {
            var drawn = false;
            for (int x = 0; x < 5; x++)
            {
                if (innerBoard[y, x].WasDrawn)
                    drawn = true;
                else
                {
                    drawn = false;
                    break;
                }    
            }
            if(drawn)
                return true;
        }

        for (int x = 0; x < 5; x++)
        {
            var drawn = false;
            for (int y = 0; y < 5; y++)
            {
                if (innerBoard[y, x].WasDrawn)
                    drawn = true;
                else
                {
                    drawn = false;
                    break;
                }
            }
            if (drawn)
                return true;
        }

        return false;
    }

}

internal class BingoNumber
{
    public int Value { get; private set; }
    public bool WasDrawn { get; set; }
    public BingoNumber(int value)
    {
        this.Value = value; 
    }
    public override string ToString()
    {
        return this.Value.ToString();
    }
}