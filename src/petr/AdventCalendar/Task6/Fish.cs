using System;
using System.Collections.Generic;
using System.Text;

namespace AdventCalendar.Task6
{
  class Fish
  {
    public event EventHandler<Fish> NewFishCreated;
    public int Counter { get; private set; }
    public int InitialCounter { get; }
    public int Days { get; }

    public int BabiesBorn {
      get
      {
        if (Days < InitialCounter)
          return 0;
        else
        {
          return 1 + (Days - InitialCounter - 1) / 7;
        }
      }
    }

    public List<int> BirthDays()
    {
      var result = new List<int>();
      result.Add(InitialCounter + 1);
      return result;
    }

    public Fish(int counter)
    {
      this.Counter = counter;
    }

    public void DecreaseCounter()
    {
      if(Counter == 0)
      {
        Counter = 6;
        NewFishCreated.Invoke(this, new Fish(8));
      }
      else
      {
        Counter--;
      }
    }
  }
}
