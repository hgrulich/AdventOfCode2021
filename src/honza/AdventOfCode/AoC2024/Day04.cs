namespace AoC2024;

public class Day04
{
    public long Solve1()
    {
        var strings = File.ReadAllLines("Input\\input04.txt");
        var sum = 0;
        var directions = new List<(Func<int, int>, Func<int, int>)>()
        {
            (x => x + 1, y => y),
            (x => x - 1, y => y),
            (x => x, y => y + 1),
            (x => x, y => y - 1),
            (x => x + 1, y => y + 1),
            (x => x - 1, y => y - 1),
            (x => x + 1, y => y - 1),
            (x => x - 1, y => y + 1)
        };
        
        for (int y = 0; y < strings.Length; y++)
        {
            for (int x = 0; x < strings[y].Length; x++)
            {
                if (strings[y][x] == 'X')
                {
                    sum += directions.Count(d => IsXMASInDirection(strings, x, y, d.Item1, d.Item2));
                }
            }              
        }
        
        return sum;
    }

    private bool IsXMASInDirection(string[] lines, int xOrigin, int yOrigin, Func<int, int> xMovement, Func<int, int> yMovement)
    {
        var chars = new List<char>() {'X'};
        int x = xOrigin, y = yOrigin;
        for (int j = 0; j < 3; j++)
        {
            x = xMovement(x);
            y = yMovement(y);
            if (x < 0 || x >= lines[0].Length || y < 0 || y >= lines.Length)
            {
                return false;
            }
            chars.Add(lines[y][x]);
        }

        return new string(chars.ToArray()) == "XMAS";
    }

    public long Solve2()
    {
        var strings = File.ReadAllLines("Input\\input04.txt");
        var sum = 0;
        
        for (int y = 1; y < strings.Length - 1; y++)
        {
            for (int x = 1; x < strings[y].Length - 1; x++)
            {
                if (strings[y][x] == 'A' && IsMASDiag(strings, x, y))
                    sum++;
            }              
        }
        
        return sum;
    }

    public bool IsMASDiag(string[] lines, int xOrigin, int yOrigin)
    {
        var diag1 = new char[2];
        diag1[0] = lines[yOrigin + 1][xOrigin + 1];
        diag1[1] = lines[yOrigin - 1][xOrigin - 1];
        
        var diag2 = new char[2];
        diag2[0] = lines[yOrigin + 1][xOrigin - 1];
        diag2[1] = lines[yOrigin - 1][xOrigin + 1];
        
        return diag1.Contains('M') && diag1.Contains('S') && diag2.Contains('M') && diag2.Contains('S');
    }
}