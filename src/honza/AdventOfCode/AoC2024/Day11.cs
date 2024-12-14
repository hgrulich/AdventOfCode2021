namespace AoC2024;

public class Day11 : IDay
{
    public long Solve1()
    {
        var stones = File.ReadAllText("Input\\Input11.txt").Split(' ').Select(long.Parse).ToArray();

        return GetNrOfStonesAfterBlinks(stones, 25);
    }

    public long Solve2()
    {
        var stones = File.ReadAllText("Input\\Input11.txt").Split(' ').Select(long.Parse).ToArray();

        return GetNrOfStonesAfterBlinks(stones, 75);
    }

    long GetNrOfStonesAfterBlinks(long[] initialStones, int nrOfBlinks)
    {
        var counts = new Dictionary<long, long>();
        foreach (var stone in initialStones)
        {
            if(!counts.TryAdd(stone, 1))
                counts[stone]++;
        }
        
        for (int i = 0; i < nrOfBlinks; i++)
        {
            var newCounts = new Dictionary<long, long>();
            foreach (var pair in counts)
            {
                var numLength = Math.Floor(Math.Log10(pair.Key)) + 1;
                if (pair.Key == 0)
                {
                    if(!newCounts.TryAdd(1, pair.Value))
                        newCounts[1] += pair.Value;
                }
                else if (numLength % 2 == 0)
                {
                    var numSize = numLength / 2;
                    var left = (long)Math.Floor(pair.Key / Math.Pow(10, numSize));
                    var right = pair.Key - left * (long)Math.Pow(10, numSize);
                    
                    if(!newCounts.TryAdd(left, pair.Value))
                        newCounts[left] += pair.Value;
                    
                    if(!newCounts.TryAdd(right, pair.Value))
                        newCounts[right] += pair.Value;
                }
                else
                {
                    if(!newCounts.TryAdd(pair.Key * 2024, pair.Value))
                        newCounts[pair.Key * 2024] += pair.Value;
                }
            }
            
            counts = newCounts;
        }
        
        return counts.Sum(x => x.Value);
    }
}