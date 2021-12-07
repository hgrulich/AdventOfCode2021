using System;
using System.Collections.Generic;
using System.Text;

namespace AdventCalendar.Task4
{
  class Cell
  {
    public int Number { get; }
    public bool Marked { get; set; }

    public Cell(int number)
    {
      Number = number;
    }

    public void TryMark(int num)
    {
      if(num == Number)
        Marked = true;
    }
  }
}
