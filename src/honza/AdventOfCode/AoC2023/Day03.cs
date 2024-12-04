namespace AoC2023;

public class Day03
{
    public async Task<long> Solve1()
    {
        var lines = await File.ReadAllLinesAsync(@"Input\day03.txt");
        var sum = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int rowIndex = 0; rowIndex < line.Length; rowIndex++)
            {
                if (char.IsDigit(line[rowIndex]))
                {
                    var isAdjacent = false;
                    var numberStartIndex = rowIndex++;
                    while (rowIndex < line.Length && char.IsDigit(line[rowIndex]))
                    {
                        rowIndex++;
                    }

                    var numberEndIndex = rowIndex - 1;

                    if (numberStartIndex - 1 >= 0 && line[numberStartIndex - 1] != '.')
                        isAdjacent = true;
                    if (numberEndIndex + 1 < line.Length && line[numberEndIndex + 1] != '.')
                        isAdjacent = true;
                    if (i > 0)
                    {
                        for (int j = Math.Max(numberStartIndex - 1, 0);
                             j <= Math.Min(numberEndIndex + 1, line.Length - 1);
                             j++)
                        {
                            if (lines[i - 1][j] != '.')
                            {
                                isAdjacent = true;
                                break;
                            }
                        }
                    }

                    if (i < lines.Length - 1)
                    {
                        for (int j = Math.Max(numberStartIndex - 1, 0);
                             j <= Math.Min(numberEndIndex + 1, line.Length - 1);
                             j++)
                        {
                            if (lines[i + 1][j] != '.')
                            {
                                isAdjacent = true;
                                break;
                            }
                        }
                    }

                    if (isAdjacent)
                    {
                        sum += int.Parse(line.Substring(numberStartIndex, numberEndIndex - numberStartIndex + 1));
                    }
                }
            }
        }

        return sum;
    }

    public async Task<long> Solve2()
    {
        var lines = await File.ReadAllLinesAsync(@"Input\day03.txt");
        var sum = 0;
        var gears = new Dictionary<(int, int), List<int>>();
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int rowIndex = 0; rowIndex < line.Length; rowIndex++)
            {
                if (char.IsDigit(line[rowIndex]))
                {
                    var adjacentGears = new List<(int, int)>();
                    var numberStartIndex = rowIndex++;
                    while (rowIndex < line.Length && char.IsDigit(line[rowIndex]))
                    {
                        rowIndex++;
                    }

                    var numberEndIndex = rowIndex - 1;

                    if (numberStartIndex - 1 >= 0 && line[numberStartIndex - 1] == '*')
                        adjacentGears.Add((i, numberStartIndex - 1));
                    if (numberEndIndex + 1 < line.Length && line[numberEndIndex + 1] == '*')
                        adjacentGears.Add((i, numberEndIndex + 1));
                    if (i > 0)
                    {
                        for (int j = Math.Max(numberStartIndex - 1, 0);
                             j <= Math.Min(numberEndIndex + 1, line.Length - 1);
                             j++)
                        {
                            if (lines[i - 1][j] == '*')
                            {
                                adjacentGears.Add((i - 1, j));
                            }
                        }
                    }

                    if (i < lines.Length - 1)
                    {
                        for (int j = Math.Max(numberStartIndex - 1, 0);
                             j <= Math.Min(numberEndIndex + 1, line.Length - 1);
                             j++)
                        {
                            if (lines[i + 1][j] == '*')
                            {
                                adjacentGears.Add((i + 1, j));
                            }
                        }
                    }

                    foreach (var gear in adjacentGears)
                    {
                        if (gears.ContainsKey(gear))
                        {
                            gears[gear].Add(int.Parse(line.Substring(numberStartIndex, numberEndIndex - numberStartIndex + 1)));
                        }
                        else
                        {
                            gears.Add(gear, new List<int>());
                            gears[gear].Add(int.Parse(line.Substring(numberStartIndex, numberEndIndex - numberStartIndex + 1)));
                        }
                    }
                }
            }
        }

        return gears.Where(x => x.Value.Count == 2).Sum(x => x.Value.Aggregate((a, b) => a * b));
    }
}