namespace AoC2024;

public class Day05 : IDay
{
    public long Solve1()
    {
        var lines = File.ReadAllLines("Input\\Input05.txt");
        var (updates, rules) = ParseInput(lines);

        var sum = 0;
        foreach (var update in updates)
        {
            if (!rules.Any(r =>
                {
                    var i1 = update.IndexOf(r.Item1);
                    var i2 = update.IndexOf(r.Item2);
                    return i1 != -1 && i2 != -1 && i1 > i2;
                }))
            {
                sum += update[(update.Count - 1) / 2];
            }

        }

        return sum;
    }

    public long Solve2()
    {
        var lines = File.ReadAllLines("Input\\Input05.txt");
        var (updates, rules) = ParseInput(lines);

        var sum = 0;
        foreach (var update in updates)
        {
            if (rules.Any(r =>
                {
                    var i1 = update.IndexOf(r.Item1);
                    var i2 = update.IndexOf(r.Item2);
                    return i1 != -1 && i2 != -1 && i1 > i2;
                }))
            {
                int Comparer(int i, int j)
                {
                    if (rules.Any(r => r.Item1 == i && r.Item2 == j))
                        return -1;
                    if (rules.Any(r => r.Item1 == j && r.Item2 == i))
                        return 1;
                    return 0;
                }

                update.Sort(Comparer);
                sum += update[(update.Count - 1) / 2];
            }

        }

        return sum;
    }

    public (List<List<int>> updates, List<(int, int)> rules) ParseInput(string[] inputStrings)
    {
        var ruleStrings = inputStrings.TakeWhile(x => x.Length > 0);
        var updateStrings = inputStrings.SkipWhile(x => x.Length > 0).Skip(1).ToList();

        var rules = ruleStrings.Select(x =>
        {
            var parts = x.Split("|");
            return (int.Parse(parts[0]), int.Parse(parts[1]));
        }).ToList();

        var updates = updateStrings.Select(x => x.Split(',').Select(int.Parse).ToList()).ToList();

        return (updates, rules);
    }
}