using System;

namespace AdventCalendar
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Specify file with input");
      var inputFileName = Console.ReadLine();

      var task1 = new Task1(inputFileName);
      task1.Solve();
      task1.Solve2();
      Console.ReadLine();
    }
  }
}
