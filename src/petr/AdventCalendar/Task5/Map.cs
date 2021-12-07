using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventCalendar.Task5
{
  class Map
  {
    private List<Point> points = new List<Point>();
    private Mutex mutex = new Mutex();
    public void MarkPoint(int x, int y)
    {     
      var point = new Point(x, y);
      mutex.WaitOne();
      points.Add(point);
      mutex.ReleaseMutex();
    }

    public void MarkVent(Vent vent)
    {
      int iDelta = (vent.X1 > vent.X2) ? -1 : +1;
      int jDelta = (vent.Y1 > vent.Y2) ? -1 : +1;
      if (vent.IsHorizontal())
        for (int i = vent.X1; ; i += iDelta)
        {
          this.MarkPoint(i, vent.Y1);
          if (i == vent.X2)
            break;
        }

      else if (vent.IsVertical())
        for (int j = vent.Y1; ; j += jDelta)
        {
          this.MarkPoint(vent.X1, j);
          if (j == vent.Y2)
            break;
        }

      else if (vent.IsDiagonal())
      {
        int i = vent.X1;
        for (int j = vent.Y1; ;)
        {
          this.MarkPoint(i, j);
          if (j == vent.Y2)
            break;
          i += iDelta;
          j += jDelta;
        }
      }
    }

    public int GetCount(int moreThan)
    {
      var groups = points.GroupBy(p => (p.X, p.Y));
      return groups.Count(g => g.Count() > moreThan);
    }
  }
}
