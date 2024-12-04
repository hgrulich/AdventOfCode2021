namespace AoC2023;

public class Day04
{
    public async Task<long> Solve1()
    {
        var lines = await File.ReadAllLinesAsync(@"Input\day04.txt");

        long sum = 0;
        
        foreach (var line in lines)
        {
            var headerContent = line.Split(':');
            var parts = headerContent[1].Split('|');
            
            var winningNumbers = parts[0].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToList();
            var myNumbers = parts[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToList();

            sum += (long)Math.Pow(2, winningNumbers.Intersect(myNumbers).Count() - 1);
        }
        
        return sum;
    }
    
    public async Task<long> Solve2()
    {
        var lines = await File.ReadAllLinesAsync(@"Input\day04.txt");

        var counts = Enumerable.Repeat(1, lines.Length).ToList();
        for (int i = 0; i < lines.Length; i++)
        {
            var nrOfCards = counts[i];
            var line = lines[i];
            var headerContent = line.Split(':');
            var parts = headerContent[1].Split('|');
            
            var winningNumbers = parts[0].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToList();
            var myNumbers = parts[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToList();

            var matches = winningNumbers.Intersect(myNumbers).Count();

            if (matches == 0)
                continue;
            
            for (int j = 1; j <= matches; j++)
            {
                counts[i + j] += nrOfCards;
            }
        }
        
        return counts.Sum();
    }
}