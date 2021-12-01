using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCalendar
{
  class Task1
  {
    public string InputFileName { get; set; }
    public IEnumerable<int> Depths { get; set; }


    public Task1(string inputFileName)
    {
      this.InputFileName = inputFileName;
      var lines = File.ReadAllLines(InputFileName);
      Depths = lines.Select(l => ConvertStringToInt(l));
    }

    public void Solve()
    {      
      var diffs = Depths.Zip(Depths.Skip(1), (first, second) => first - second);
      var depthIncreasedCount = diffs.Count(x => x < 0);
      Console.WriteLine($"SOLUTION: {depthIncreasedCount}");
    }

    public void Solve2()
    {
      var windows = Depths.Zip(Depths.Skip(1), (first, second) => new { First = first, Second = second })
        .Zip(Depths.Skip(2), (first, second) => new { First = first.First, Second = first.Second, Third = second });
      var diffs = windows.Zip(windows.Skip(1), (first, second) => (first.First + first.Second + first.Third) - (second.First + second.Second + second.Third));
      foreach (var diff in diffs)
      {
        Console.WriteLine(diff);
      }
      var depthIncreasedCount = diffs.Count(x => x < 0);
      Console.WriteLine($"SOLUTION: {depthIncreasedCount}");
    }

    private int ConvertStringToInt(string num)
    {
      int result;
      try
      {
        int.TryParse(num, out result);
      }
      catch (Exception)
      {
        Console.WriteLine("Wrong number passed");
        result = int.MaxValue;
      }
      return result;
    }
  }
}
