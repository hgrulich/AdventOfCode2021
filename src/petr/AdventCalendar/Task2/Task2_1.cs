using AdventCalendar.Task2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCalendar
{
  public class Location1 : Location
  {
    public Location1(int depth, int distance)
    {
      this.Depth = depth;
      this.Distance = distance;
    }
    public override void Move(MovementData data)
    {
      switch (data.Direction)
      {
        case Directions.Down:
          this.Depth += data.Value;
          break;
        case Directions.Up:
          this.Depth -= data.Value;
          break;
        case Directions.Forward:
          this.Distance += data.Value;
          break;
      }
    }
  }
  public class Task2_1
  {
    public string InputFileName { get; }
    public IEnumerable<MovementData> MovementHistory { get; }
    public Location StartLocation { get; }

    public Task2_1(string fileName)
    {
      this.InputFileName = fileName;
      this.MovementHistory = File.ReadLines(this.InputFileName).Select(l => MovementData.ParseFromString(l));
      this.StartLocation = new Location1(0, 0);
    }

    public void Solve()
    {
      StartLocation.ApplyHistory(MovementHistory);
      Console.WriteLine($"Distance x Depth = {StartLocation.Distance * StartLocation.Depth}");

    }
  }
}
