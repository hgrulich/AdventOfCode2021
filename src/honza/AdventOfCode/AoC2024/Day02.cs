using System.ComponentModel.Design;

namespace AoC2024;

public class Day02
{
    public long Solve1()
    {
        var lines = File.ReadAllLines("Input\\Input02.txt").Select(x => x.Split(" ").Select(int.Parse).ToList()).ToList();
        var sum = 0;
        foreach (var line in lines)
        {
            var levelPairs = line
                .Take(line.Count - 1)
                .Select((x, index) => (line[index], line[index + 1]));
                
                if(levelPairs.All(x => x.Item1 < x.Item2 && Math.Abs(x.Item1 - x.Item2) > 0 && Math.Abs(x.Item1 - x.Item2) < 4))
                   sum++;
                else if(levelPairs.All(x => x.Item1 > x.Item2 && Math.Abs(x.Item1 - x.Item2) > 0 && Math.Abs(x.Item1 - x.Item2) < 4))
                    sum++;
        }
        return sum;
    }
    
    public long Solve2()
    {
        var lines = File.ReadAllLines("Input\\Input02.txt").Select(x => x.Split(" ").Select(int.Parse).ToList()).ToList();
        var sum = 0;
        foreach (var line in lines)
        {
            var levelPairs = line
                .Take(line.Count - 1)
                .Select((x, index) => (line[index], line[index + 1]));
                
            if(levelPairs.All(x => x.Item1 < x.Item2 && Math.Abs(x.Item1 - x.Item2) > 0 && Math.Abs(x.Item1 - x.Item2) < 4))
                sum++;
            else if(levelPairs.All(x => x.Item1 > x.Item2 && Math.Abs(x.Item1 - x.Item2) > 0 && Math.Abs(x.Item1 - x.Item2) < 4))
                sum++;
            else
            {
                for (int i = 0; i < line.Count; i++)
                {
                    var tempLines = new List<int>(line);
                    tempLines.RemoveAt(i);
                    var pairstWithoutOne =
                        tempLines
                            .Take(tempLines.Count - 1)
                            .Select((x, index) => (tempLines[index], tempLines[index + 1]));

                    if (pairstWithoutOne.All(x =>
                            x.Item1 < x.Item2 && Math.Abs(x.Item1 - x.Item2) > 0 && Math.Abs(x.Item1 - x.Item2) < 4))
                    {
                        sum++;
                        break;
                    }
                    if (pairstWithoutOne.All(x =>
                                 x.Item1 > x.Item2 && Math.Abs(x.Item1 - x.Item2) > 0 &&
                                 Math.Abs(x.Item1 - x.Item2) < 4))
                    {
                        sum++;
                        break;
                    }
                }
            }
        }
        return sum;
    }
}