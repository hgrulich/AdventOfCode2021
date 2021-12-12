using Advent_2021;
using System.Diagnostics;

//var day = new Day01();
//var day = new Day02();
//var day = new Day03();
//var day = new Day04();
//var day = new Day05();
//var day = new Day06();
//var day = new Day07();
//var day = new Day08();
//var day = new Day09();
//var day = new Day10();
//var day = new Day11();
var day = new Day12();

var sw = Stopwatch.StartNew();
Console.WriteLine($"First puzzle: {day.SolveFirst()}");
Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds} ms");
sw.Restart();
Console.WriteLine($"Second puzzle: {day.SolveSecond()}");
Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds} ms");