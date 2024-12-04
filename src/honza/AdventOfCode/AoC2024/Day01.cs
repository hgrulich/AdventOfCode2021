namespace AoC2024;

public class Day01
{
    public long Solve1()
    {
        var lines = File.ReadAllLines("Input\\input01.txt");
        var firstColumn = new List<int>();
        var secondColumn = new List<int>();

        foreach (var line in lines)
        {
            var nums = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            firstColumn.Add(int.Parse(nums[0]));
            secondColumn.Add(int.Parse(nums[1]));
        }
        var firstOrdered = firstColumn.OrderBy(x => x).ToList();
        var secondOrdered = secondColumn.OrderBy(x => x).ToList();
        
        return Enumerable.Range(0, firstColumn.Count)
            .Select(i => Math.Abs(firstOrdered[i] - secondOrdered[i]))
            .Sum();
    }

    public long Solve2()
    {
        var lines = File.ReadAllLines("Input\\input01.txt");
        var firstColumn = new List<int>();
        var secondCounts = new Dictionary<int, int>();

        foreach (var line in lines)
        {
            var nums = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            firstColumn.Add(int.Parse(nums[0]));
            if(secondCounts.ContainsKey(int.Parse(nums[1])))
                secondCounts[int.Parse(nums[1])]++;
            else
                secondCounts[int.Parse(nums[1])] = 1;
        }
        var firstOrdered = firstColumn.OrderBy(x => x).ToList();
        
        return Enumerable.Range(0, firstColumn.Count)
            .Select(i => firstOrdered[i] * (secondCounts.ContainsKey(firstOrdered[i]) ? secondCounts[firstOrdered[i]] : 0))
            .Sum();
    }
}