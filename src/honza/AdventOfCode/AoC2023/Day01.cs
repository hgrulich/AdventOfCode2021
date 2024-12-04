using System.ComponentModel.Design;

namespace AoC2023;

public class Day01
{
    public async Task<long> Solve1()
    {
        var lines = await File.ReadAllLinesAsync(@"Input\day01.txt");
        var sum = 0;

        foreach (var line in lines)
        {
            var firstdigit = line.First(char.IsDigit);
            var seconddigit = line.Last(char.IsDigit);

            sum += int.Parse(firstdigit.ToString()) * 10 + int.Parse(seconddigit.ToString());
        }

        return sum;
    }
    
    public async Task<long> Solve2()
    {
        var search = new []
            {
                "one", "1", 
                "two", "2",
                "three","3",
                "four", "4",
                "five", "5",
                "six", "6",
                "seven", "7",
                "eight", "8", 
                "nine", "9"};
        var lines = await File.ReadAllLinesAsync(@"Input\day01.txt");
        var sum = 0;

        foreach (var line in lines)
        {
            var firstoccurences = search
                .Select(x => (line.IndexOf(x), x))
                .Where(x => x.Item1 > -1)
                .OrderBy(x => x.Item1);
            
            var lastoccurences = search
                .Select(x => (line.LastIndexOf(x), x))
                .Where(x => x.Item1 > -1)
                .OrderBy(x => x.Item1);

            int firstdigit, seconddigit = 0;
            firstdigit = int.Parse(firstoccurences.First().x.Length == 1 ? firstoccurences.First().x : search[Array.IndexOf(search, firstoccurences.First().x) + 1]);
            seconddigit = int.Parse(lastoccurences.Last().x.Length == 1 ? lastoccurences.Last().x : search[Array.IndexOf(search, lastoccurences.Last().x) + 1]);


            sum += int.Parse(firstdigit.ToString()) * 10 + int.Parse(seconddigit.ToString());
        }

        return sum;
    }
}