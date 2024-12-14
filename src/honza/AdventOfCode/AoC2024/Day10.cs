namespace AoC2024;

public class Day10 : IDay
{
    public long Solve1()
    {
        var map =
            File.ReadAllLines("Input\\Input10.txt")
                .Select(l => l.Select(ch => int.Parse(ch.ToString())).ToArray()).ToArray();

        return GetPossibleTrailHeads(map)
            .Select(x => GetPeaksInDirection(map, x, (-1, -1)).Distinct().Count())
            .Sum();
    }

    (int, int)[] GetPeaksInDirection(int[][] map, (int x, int y) currentPos, (int, int) prevPos)
    {
        if(map[currentPos.y][currentPos.x] == 9)
            return new (int, int)[]{(currentPos.x, currentPos.y)};
        
        var possibleSteps = 
            GetAdjacentPositions(currentPos, map.Length, map[0].Length)
                .Where(nextStep => nextStep != prevPos && 
                                   map[currentPos.y][currentPos.x] == map[nextStep.Item2][nextStep.Item1] - 1);

        return possibleSteps
            .SelectMany(x => GetPeaksInDirection(map, x, currentPos))
            .ToArray();

    }
    
    int GetNrOfHikesInDirection(int[][] map, (int x, int y) currentPos, (int, int) prevPos)
    {
        if(map[currentPos.y][currentPos.x] == 9)
            return 1;
        
        var possibleSteps = 
            GetAdjacentPositions(currentPos, map.Length, map[0].Length)
                .Where(nextStep => nextStep != prevPos && 
                                   map[currentPos.y][currentPos.x] == map[nextStep.Item2][nextStep.Item1] - 1);

        return possibleSteps
            .Select(x => GetNrOfHikesInDirection(map, x, currentPos))
            .Sum();

    }

    (int, int)[] GetAdjacentPositions((int x, int y) position, int mapHeight, int mapWidth)
    {
        var positions = new List<(int, int)>();
        if (position.x > 0)
            positions.Add((position.x - 1, position.y));
        if (position.x < mapWidth - 1)
            positions.Add((position.x + 1, position.y));
        if (position.y > 0)
            positions.Add((position.x, position.y - 1));
        if (position.y < mapHeight - 1)
            positions.Add((position.x, position.y + 1));
        return positions.ToArray();
    }

    public long Solve2()
    {
        var map =
            File.ReadAllLines("Input\\Input10.txt")
                .Select(l => l.Select(ch => int.Parse(ch.ToString())).ToArray()).ToArray();
        
        return GetPossibleTrailHeads(map)
            .Select(x => GetNrOfHikesInDirection(map, x, (-1, -1)))
            .Sum();
    }

    IEnumerable<(int, int)> GetPossibleTrailHeads(int[][] map)
    {
        var possibleTrailHeads = new List<(int, int)>();
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] == 0)
                    yield return (x, y);
            }
        }
    }
}