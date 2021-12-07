using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCalendar.Task4
{
  class Task4_1
  {
    public List<Board> Boards { get; }
    public IEnumerable<int> Sequence { get; }
    public Task4_1(string inputFile)
    {
      var lines = File.ReadAllLines(inputFile);
      Sequence = lines.FirstOrDefault().Split(",").Select(s => int.Parse(s));

      var boardsLines = lines.Skip(2).ToList();
      Boards = new List<Board>();
      while (boardsLines.Count > 5)
      {
        var board = new Board(boardsLines.Take(5));
        Boards.Add(board);
        boardsLines = boardsLines.Skip(5).ToList();
        if(boardsLines.Count > 1)
        {
          boardsLines = boardsLines.Skip(1).ToList();
        }
        else
        {
          break;
        }
      }
    }

    public void Solve()
    {
      foreach (var num in Sequence)
      {
        foreach (var board in Boards)
        {
          board.Mark(num);
        }
      }
      Console.WriteLine("Game is over");
    }
  }
}
