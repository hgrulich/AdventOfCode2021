namespace AoC2024;

public class Day08 : IDay
{
    public long Solve1()
    {
        var (width, height, antennas) = GetMap();
        var locations = new List<(int, int)>();

        foreach (var pair in antennas)
        {
            var positions = pair.Value;
            for (int pos1 = 0; pos1 < positions.Count; pos1++)
            {
                for (int pos2 = pos1; pos2 < positions.Count; pos2++)
                {
                    var vector = ((positions[pos1].Item1 - positions[pos2].Item1), (positions[pos1].Item2 - positions[pos2].Item2));

                    var newPos = Move(positions[pos1], vector);
                    if (IsInBounds(newPos, width, height))
                    {
                        locations.Add(newPos);
                    }

                    newPos = Move(positions[pos2], (-vector.Item1, -vector.Item2));
                    if (IsInBounds(newPos, width, height))
                    {
                        locations.Add(newPos);
                    }
                }
            }
        }

        return locations.Distinct().Count();

    }

    (int, int) Move((int, int) pos, (int, int) vector)
    {
        return (pos.Item1 + vector.Item1, pos.Item2 + vector.Item2);
    }
    
    bool IsInBounds((int, int) pos, int width, int height)
    {
        return pos.Item1 >= 0 && pos.Item1 < width && pos.Item2 >= 0 && pos.Item2 < height;
    }
    
    public long Solve2()
    {
        var (width, height, antennas) = GetMap();

        var locations = new List<(int, int)>();

        foreach (var pair in antennas)
        {
            var positions = pair.Value;
            for (int pos1 = 0; pos1 < positions.Count; pos1++)
            {
                for (int pos2 = pos1 + 1; pos2 < positions.Count; pos2++)
                {
                    var vector = ((positions[pos1].Item1 - positions[pos2].Item1), (positions[pos1].Item2 - positions[pos2].Item2));
                    locations.AddRange(GetPositionsInDirection(positions[pos1], vector, width, height));
                    locations.AddRange(GetPositionsInDirection(positions[pos2], (-vector.Item1, -vector.Item2), width, height));
                }
            }
        }

        return locations.Distinct().Count();
    }

    List<(int, int)> GetPositionsInDirection((int, int) start, (int, int) vector, int width, int height)
    {
        var isInBounds = true;
        var positions = new List<(int, int)>();
        var currentPos = start;
        while (isInBounds)
        {
            if (IsInBounds(currentPos, width, height))
            {
                positions.Add(currentPos);
                currentPos = Move(currentPos, vector);
            }
            else
            {
                isInBounds = false;
            }
            
        }

        return positions;
    }

    (int width, int height, Dictionary<char, List<(int, int)>> antenas) GetMap()
    {
        var lines = File.ReadAllLines("Input\\Input08.txt");

        var height = lines.Length;
        var width = lines[0].Length;

        var antenas = new Dictionary<char, List<(int, int)>>();
        
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] != '.')
                {
                    if (antenas.ContainsKey(line[x]))
                    {
                        antenas[line[x]].Add((x, y));
                    }
                    else
                    {
                        antenas.Add(line[x], new List<(int, int)> { (x, y) });
                    }
                }
            }
        }

        return (width, height, antenas);
    }
}