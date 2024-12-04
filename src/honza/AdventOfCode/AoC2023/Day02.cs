using System.ComponentModel.Design;

namespace AoC2023;

public class Day02
{
    new Dictionary<string, int> cubeMax = new Dictionary<string, int>()
    {
        {"red", 12},
        {"green", 13},
        {"blue", 14}
    };
    
    public async Task<long> Solve1()
    {
        var lines = await File.ReadAllLinesAsync(@"Input\day02.txt");
        var sum = 0;
        foreach (var game in lines)
        {
            var headerAndRounds = game.Split(':');
            var id = int.Parse(headerAndRounds[0].Split(' ')[1]);

            var gamePossible = true;
            foreach (var round in headerAndRounds[1].Split(';'))
            {
                var reveals = round.Split(',').Select(x => x.TrimStart());
                if (reveals.Select(reveal => reveal.Split(' ')).Any(revealValues => cubeMax[revealValues[1]] < int.Parse(revealValues[0])))
                {
                    gamePossible = false;
                    break;
                }
            }

            if (gamePossible)
            {
                sum += id;
            }
        }

        return sum;
    }
    
    public async Task<long> Solve2()
    {
        var lines = await File.ReadAllLinesAsync(@"Input\day02.txt");
        var sum = 0;
        foreach (var game in lines)
        {
            var headerAndRounds = game.Split(':');
            var id = int.Parse(headerAndRounds[0].Split(' ')[1]);

            var minimums = new Dictionary<string, int>();
            
            foreach (var round in headerAndRounds[1].Split(';'))
            {
                var reveals = round.Split(',').Select(x => x.TrimStart());
                foreach (var reveal in reveals)
                { 
                    var revealValues = reveal.Split(' ');
                    var color = revealValues[1];
                    var value = int.Parse(revealValues[0]);
                    if (minimums.ContainsKey(color))
                    {
                        minimums[color] = Math.Max(minimums[color], value);
                    }
                    else
                    {
                        minimums[color] = value;
                    }
                }
            }

            sum += minimums.Values.Aggregate((a, b) => a * b);
        }

        return sum;
    }
}

public enum Color
{
    Red,
    Green,
    Blue
}