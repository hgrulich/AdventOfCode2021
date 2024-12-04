using System.Text.RegularExpressions;

namespace AoC2024;

public class Day03
{
    public long Solve1()
    {
        var lines = File.ReadAllLines("Input\\Input03.txt");
        long sum = 0;
        var pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        foreach (var line in lines)
        {
            var matches = Regex.Matches(line, pattern);
            foreach (Match match in matches)
            {
                var x = int.Parse(match.Groups[1].Value);
                var y = int.Parse(match.Groups[2].Value);
                sum += x * y;
            }
        }
        return sum;
    }

    public long Solve2()
    {
        var lines = File.ReadAllLines("Input\\Input03.txt");
        long sum = 0;
        var pattern = @"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)";
        var enabled = true;
        foreach (var line in lines)
        {
            var matches = Regex.Matches(line, pattern);
            foreach (Match match in matches)
            {
                if(match.Value == "do()")
                    enabled = true;
                else if(match.Value == "don't()")
                    enabled = false;
                else
                {
                    if (enabled)
                    {
                        var x = int.Parse(match.Groups[1].Value);
                        var y = int.Parse(match.Groups[2].Value);
                        sum += x * y;
                    }
                }
            }
        }
        return sum;
    }
}