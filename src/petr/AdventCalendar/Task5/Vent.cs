using System;
using System.Collections.Generic;
using System.Text;

namespace AdventCalendar.Task5
{
  class Vent
  {
    public int X1 { get; set; }
    public int Y1 { get; set; }
    public int X2 { get; set; }
    public int Y2 { get; set; }

    public Vent(int x1, int y1, int x2, int y2)
    {
      X1 = x1;
      Y1 = y1;
      X2 = x2;
      Y2 = y2;
    }

    public void MarkOnMap(Map map)
    {
      
    }

    public static Vent Parse(string text)
    {
      var points = text.Split(" -> ");
      var x1 = int.Parse(points[0].Split(",")[0]);
      var y1 = int.Parse(points[0].Split(",")[1]);
      var x2 = int.Parse(points[1].Split(",")[0]);
      var y2 = int.Parse(points[1].Split(",")[1]);
      
      return new Vent(x1, y1, x2, y2);
    }

    public bool IsHorizontal()
    {
      return Y1 == Y2;
    }

    public bool IsVertical()
    {
      return X1 == X2;
    }

    public bool IsDiagonal()
    {
      return X1 != X2 && Y1 != Y2;
    }
  }
}
