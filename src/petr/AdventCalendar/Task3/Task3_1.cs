using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCalendar.Task3
{
  public class Task3_1
  {
    public string FileName { get; }
    public IEnumerable<List<int>> BitLists { get; }
    public int RecordLength { get; }

    public Task3_1(string fileName)
    {
      var lines = File.ReadLines(fileName);
      this.BitLists = lines.Select(x => x.Select(b => (int)Char.GetNumericValue(b)).ToList());
      this.RecordLength = this.BitLists.First().Count();
    }

    public void Solve()
    {
      var groupedLists = BitLists.FirstOrDefault().Select((x, i) => BitLists.Select(y => y[i]).GroupBy(z => z).OrderByDescending(grp => grp.Count()).Select(w => w.Key));
      var gammaRateBinary = groupedLists.Select(x => x.First()).ToList();
      var epsilonRateBinary = groupedLists.Select(x => x.Last()).ToList();

      var gammaRate = Convert.ToInt32(string.Join("", gammaRateBinary), 2);
      var epsilonRate = Convert.ToInt32(string.Join("", epsilonRateBinary), 2);

      Console.WriteLine($"Solution: {gammaRate * epsilonRate}");
    }

    public void Solve2()
    {
      var oxygenGeneratorRating = BitLists.ToList();
      for(int i = 0; oxygenGeneratorRating.Count() > 1; i++)
      {
        Console.WriteLine($"Count: {oxygenGeneratorRating.Count()}");
        var a = oxygenGeneratorRating.Select(x => x[i]).GroupBy(y => y);
        var ordered = a.OrderByDescending(z => z.Count()).ToList();
        int mostCommonBit;
        if (ordered.Count() > 1 && ordered[0].Count() == ordered[1].Count())
          mostCommonBit = 1;
        else
          mostCommonBit = ordered.First().Key;
        oxygenGeneratorRating = oxygenGeneratorRating.Where(x => x[i] == mostCommonBit).ToList();
        if (i == RecordLength-1)
          i = 0;
      }

      var co2ScrubberRating = BitLists.ToList();
      for (int i = 0; co2ScrubberRating.Count() > 1; i++)
      {
        Console.WriteLine($"Count: {co2ScrubberRating.Count()}");
        var a = co2ScrubberRating.Select(x => x[i]).GroupBy(y => y);
        var ordered = a.OrderByDescending(z => z.Count()).ToList();
        int leastCommonBit;
        if (ordered.Count() > 1 && ordered[0].Count() == ordered[1].Count())
          leastCommonBit = 0;
        else
          leastCommonBit = ordered.Last().Key;
        co2ScrubberRating = co2ScrubberRating.Where(x => x[i] == leastCommonBit).ToList();
        if (i == RecordLength - 1)
          i = 0;
      }

      var oxygenDecimal = Convert.ToInt32(string.Join("", oxygenGeneratorRating.First()), 2);
      var co2Decimal = Convert.ToInt32(string.Join("", co2ScrubberRating.First()), 2);

      Console.WriteLine($"Solution: {oxygenDecimal * co2Decimal}");


    }
  }
}
