using System;
using System.Collections.Generic;
using System.Text;

namespace AdventCalendar.Task2
{
  public abstract class Location
  {
    public int Depth { get; set; }
    public int Distance { get; set; }
    public int Aim { get; set; }
    
    public abstract void Move(MovementData movementData);

    public int GetResult()
    {
      return this.Depth * this.Distance;
    }

    public void ApplyHistory(IEnumerable<MovementData> movementHistory)
    {
      foreach (var movementData in movementHistory)
      {
        this.Move(movementData);
        Console.WriteLine($"Moved to: {this.Distance}, {this.Depth}");
      }
    }

  }
}
