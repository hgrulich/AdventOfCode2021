using System;
using System.Collections.Generic;
using System.Text;

namespace AdventCalendar.Task6
{
  class Fish2
  {
    public int InitialCounter { get; }
    public int BirthInterval { get; }
    public int Birthday { get; }
    public int MaxDay { get; }

    private Dictionary<int, long> babiesCache { get; }

    public List<Fish2> Babies
    {
      get
      {
        var babies = new List<Fish2>();
        if (Birthday + InitialCounter + 1 > MaxDay)
          return babies;
        var firstBaby = new Fish2(Birthday + InitialCounter + 1, 8, BirthInterval, MaxDay);
        babies.Add(firstBaby);
        babies.AddRange(firstBaby.Babies);
        for (int day = firstBaby.Birthday + BirthInterval; day <= MaxDay; day += BirthInterval)
        {
          var newBaby = new Fish2(day, 8, BirthInterval, MaxDay);
          babies.Add(newBaby);
          babies.AddRange(newBaby.Babies);
        }
        return babies;
      }
    }

    public Fish2(int birthDay, int initialCounter, int birthInterval, int maxDay)
    {
      this.Birthday = birthDay;
      this.InitialCounter = initialCounter;
      this.BirthInterval = birthInterval;
      this.MaxDay = maxDay;
      this.babiesCache = new Dictionary<int, long>();
    }

    public long GetBabiesCount(int birthDay, int initialCounter, int birthInterval, int maxDay)
    {
      if (initialCounter == 8 && this.babiesCache.ContainsKey(birthDay))
        return this.babiesCache[birthDay];
      long result = 0;
      int firstBirth = birthDay + initialCounter + 1;
      if ( firstBirth > maxDay)
        return result;
      result += (maxDay - firstBirth) / birthInterval + 1;
      for (int day = firstBirth; day <= maxDay; day += birthInterval)
      {
        result += GetBabiesCount(day, 8, birthInterval, maxDay);
      }
      if (initialCounter == 8)
        this.babiesCache[birthDay] = result;
      return result;
    }


  }
}
