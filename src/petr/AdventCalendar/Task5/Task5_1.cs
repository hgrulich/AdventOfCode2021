using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCalendar.Task5
{
  class Task5_1
  {
    private List<Vent> vents = new List<Vent>();
    private Map map;
    public Task5_1(string inputFileName)
    {
      var lines = File.ReadAllLines(inputFileName);
      vents = lines.Select(l => Vent.Parse(l)).ToList();
      
    }

    public void Solve()
    {
      map = new Map();
      Parallel.ForEach(vents, vent =>
      {
        if (vent.IsHorizontal() || vent.IsVertical())
          map.MarkVent(vent);
      });

      Console.WriteLine($"Solution: {map.GetCount(1)}");
    }

    public void Solve2()
    {
      map = new Map();
      int solvedVents = 0;
      int sumVents = vents.Count;
      Parallel.ForEach(vents, vent =>
      {
        var progress = (double)(vents.IndexOf(vent) + 1) / vents.Count();
        map.MarkVent(vent);
        Console.WriteLine($"Progress: {(double)++solvedVents / sumVents}");
      });

      Console.WriteLine($"Solution: {map.GetCount(1)}");
    }
  }
}
