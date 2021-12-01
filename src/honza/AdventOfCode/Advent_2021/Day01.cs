namespace Advent_2021;

internal class Day01
{
    public int SolveFirst()
    {
        var measurements = File.ReadAllLines(Path.Combine("Input","Day01.txt")).Select(int.Parse).ToList();
        return Enumerable.Range(0, measurements.Count - 1).Select(index => measurements[index] < measurements[index + 1]).Count(s => s);
    }

    public int SolveSecond()
    {
        var windowSize = 3;
        var measurements = File.ReadAllLines(Path.Combine("Input", "Day01.txt")).Select(int.Parse).ToList();
        var sums = Enumerable.Range(0, measurements.Count - windowSize + 1).Select(index => measurements.Skip(index).Take(windowSize).Sum()).ToList();
        return Enumerable.Range(0, sums.Count - 1).Select(index => sums[index] < sums[index + 1]).Count(s => s);
    }
}

