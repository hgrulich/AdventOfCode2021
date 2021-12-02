using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCalendar.Task2
{

  public class Location2 : Location
  {
    public Location2(int depth, int distance, int aim)
    {
      this.Depth = depth;
      this.Distance = distance;
      this.Aim = aim;
    }

    public override void Move(MovementData data)
    {
      switch (data.Direction)
      {
        case Directions.Down:
          this.Aim += data.Value;
          break;
        case Directions.Up:
          this.Aim -= data.Value;
          break;
        case Directions.Forward:
          this.Distance += data.Value;
          this.Depth += this.Aim * data.Value;
          break;
      }
    }
  }
  public class Task2_2
  {
    public string InputFileName { get; }
    public IEnumerable<MovementData> MovementHistory { get; }
    public Location StartLocation { get; }

    public Task2_2(string fileName)
    {
      this.InputFileName = fileName;
      this.MovementHistory = File.ReadLines(this.InputFileName).Select(l => MovementData.ParseFromString(l));
      this.StartLocation = new Location2(0, 0, 0);
    }

    public void Solve()
    {
      StartLocation.ApplyHistory(MovementHistory);
      Console.WriteLine($"Distance x Depth = {StartLocation.Distance * StartLocation.Depth}");
    }
  }
}
