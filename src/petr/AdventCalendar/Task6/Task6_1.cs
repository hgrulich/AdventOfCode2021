using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventCalendar.Task6
{
  class Task6_1
  {
    private List<Fish> population = new List<Fish>();
    private List<Fish2> population2 = new List<Fish2>();
    private int babiesCount = 0;
    private Mutex mutex = new Mutex();
    public Task6_1(string inputFileName)
    {
      var numbers = File.ReadAllLines(inputFileName)[0].Split(",");
      population = numbers.Select(n => new Fish(int.Parse(n))).ToList();
      population2 = numbers.Select(n => new Fish2(0, int.Parse(n), 7, 256)).ToList();
      foreach (var fish in population)
      {
        fish.NewFishCreated += On_NewFishCreated;
      }

    }

    public void Solve()
    {
      Console.WriteLine("initial state: ");
      this.PrintPopulation();
      for(int day = 1; ; day++)
      {
        Parallel.ForEach(population, fish =>
        {
          fish.DecreaseCounter();
        });
        AddBabies();
        babiesCount = 0;
        Console.WriteLine($"After {day} days: ");
        //this.PrintPopulation();
        if (day == 80)
          break;
      }
      Console.WriteLine($"There are {population.Count} fish");
    }

    public void Solve2()
    {
      long result = 0;
      foreach (var fish in population2)
      {
        result += fish.GetBabiesCount(fish.Birthday, fish.InitialCounter, fish.BirthInterval, fish.MaxDay);
      }
      result += population2.Count;
      Console.WriteLine($"There are {result} fish");
    }
    
    private void PrintPopulation()
    {
      string popString = "";
      foreach (var fish in population)
      {
        popString += fish.Counter + ",";
      }
      Console.WriteLine(popString);
    }

    private void AddBabies()
    {
      var babies = Enumerable.Range(0, babiesCount).Select(x => new Fish(8)).ToList();
      Parallel.ForEach(babies, baby =>
      {
        baby.NewFishCreated += On_NewFishCreated;
      });
      population.AddRange(babies);
    }

    private void On_NewFishCreated(object sender, Fish e)
    {
      Interlocked.Increment(ref babiesCount);
    }

  }
}
