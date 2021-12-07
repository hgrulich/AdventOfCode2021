using AdventCalendar.Task2;
using AdventCalendar.Task3;
using AdventCalendar.Task4;
using AdventCalendar.Task5;
using AdventCalendar.Task6;
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
      inputFileName = @"C:\Users\hruda\Desktop\adventCalendar\input6.txt";
      //var task3 = new Task3_1(inputFileName);
      //task3.Solve2();

      //var task4 = new Task4_1(inputFileName);
      //task4.Solve();

      //var task5 = new Task5_1(inputFileName);
      //task5.Solve();
      //task5.Solve2();

      var task6 = new Task6_1(inputFileName);
      task6.Solve();
      task6.Solve2();

      Console.ReadLine();
    }
  }
}
