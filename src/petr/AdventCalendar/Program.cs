using AdventCalendar.Task2;
using System;

namespace AdventCalendar
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Specify file with input");
      var inputFileName = Console.ReadLine();

      //var task1 = new Task1(inputFileName);
      //task1.Solve();
      //task1.Solve2();

      var task2 = new Task2_2(inputFileName);
      task2.Solve();
      Console.ReadLine();
    }
  }
}
