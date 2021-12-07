using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventCalendar.Task4
{
  class Board
  {
    public IEnumerable<IEnumerable<Cell>> Lines { get; }
    public int LastMarked { get; set; }
    public bool Won { get; set; }
    public Board(IEnumerable<string> text)
    {
      Lines = this.ParseFromString(text);
      Won = false;
    }

    public void Mark(int num)
    {
      LastMarked = num;
      foreach (var line in Lines)
      {
        foreach (var cell in line)
        {
          cell.TryMark(num);
        }
      }
      this.CheckIfBingo();
    }

    private void CheckIfBingo()
    {
      var anyLine = Lines.Any(l => l.All(c => c.Marked));
      var rows = Lines.Select((_, i) => Lines.Select(l => l.ToList()[i]));
      var anyRow = rows.Any(r => r.All(c => c.Marked));
      if (anyLine || anyRow)
      {
        Console.WriteLine("BINGO!!");
        if (!Won)
        {
          Won = true;
          this.PrintResult();
        }
      }
    }

    private void PrintResult()
    {
      var notMarked = Lines.Select(l => l.Where(c => !c.Marked));
      var sum = notMarked.Sum(l => l.Sum(c => c.Number));
      Console.WriteLine($"Result: {sum * LastMarked}");
    }

    private IEnumerable<IEnumerable<Cell>> ParseFromString(IEnumerable<string> text)
    {
      var cells = text.Select(l => l.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(c => new Cell(int.Parse(c))).ToList()).ToList();
      return cells;
    }
  }
}
