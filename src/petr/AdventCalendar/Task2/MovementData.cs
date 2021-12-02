using System;
using System.Collections.Generic;
using System.Text;

namespace AdventCalendar.Task2
{
  public class MovementData
  {
    public Directions Direction { get; }
    public int Value { get; }

    public MovementData(Directions direction, int value)
    {
      this.Direction = direction;
      this.Value = value;
    }

    public static MovementData ParseFromString(string text)
    {
      var words = text.Split(' ');
      Directions direction = Directions.Forward;
      switch (words[0])
      {
        case "forward":
          direction = Directions.Forward;
          break;
        case "down":
          direction = Directions.Down;
          break;
        case "up":
          direction = Directions.Up;
          break;
        default:
          break;
      }
      int value = int.Parse(words[1]);
      return new MovementData(direction, value);
    }
  }
}
