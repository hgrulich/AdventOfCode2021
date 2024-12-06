namespace AoC2024;

public class Day06 : IDay
{
    public long Solve1()
    {
        var height = 130;
        var width = 130;
        var currentPosition = (0, 0);
        var map = 
            File.ReadAllLines("Input\\Input06.txt")
                .Select((line, lineIndex) => line.Select((x, charIndex) =>
                {
                    if (x == '^')
                    {
                        currentPosition = (charIndex, lineIndex);
                    }
                    return x != '#';
                }).ToArray()).ToArray();
        
        var visitedPositions = new HashSet<(int, int)>() {currentPosition};
        var isInMap = true;
        var direction = Direction.Up;

        while (isInMap)
        {
            var nextStep = GetNextStep(direction, currentPosition);
            if (IsOutOfMap(nextStep, height, width))
            {
                isInMap = false;
            }
            else
            {
                if (map[nextStep.y][nextStep.x])
                {
                    visitedPositions.Add(nextStep);
                    currentPosition = nextStep;
                }
                else
                {
                    direction = TurnRight(direction);
                }
            }
        }

        return visitedPositions.Count;
    }

    public long Solve2()
    {
        var height = 130;
        var width = 130;
        var initialPosition = (0, 0);
        var map = 
            File.ReadAllLines("Input\\Input06.txt")
                .Select((line, lineIndex) => line.Select((x, charIndex) =>
                {
                    if (x == '^')
                    {
                        initialPosition = (charIndex, lineIndex);
                    }
                    return x != '#';
                }).ToArray()).ToArray();
        var currentPosition = initialPosition;
        var visitedPositions = new HashSet<(int, int)>() {currentPosition};
        var isInMap = true;
        var direction = Direction.Up;
        
        while (isInMap)
        {
            var nextStep = GetNextStep(direction, currentPosition);
            if (IsOutOfMap(nextStep, height, width))
            {
                isInMap = false;
            }
            else
            {
                if (map[nextStep.y][nextStep.x])
                {
                    visitedPositions.Add(nextStep);
                    currentPosition = nextStep;
                }
                else
                {
                    direction = TurnRight(direction);
                }
            }
        }

        var loopCount = 0;
        var counter = 0;
        foreach (var position in visitedPositions.Skip(1))
        {
            Console.WriteLine(counter++);
            direction = Direction.Up;
            var visited = new HashSet<(Direction, (int, int))>() { (Direction.Up, initialPosition) };
            currentPosition = initialPosition;
            isInMap = true;
            
            map[position.Item2][position.Item1] = false;
            while (isInMap)
            {
                var nextStep = GetNextStep(direction, currentPosition);
                if (IsOutOfMap(nextStep, height, width))
                {
                    isInMap = false;
                }
                else
                {
                    if (map[nextStep.y][nextStep.x])
                    {
                        if(visited.Any(x => x.Item1 == direction && x.Item2 == nextStep))
                        {
                            loopCount++;
                            break;
                        }
                        currentPosition = nextStep;
                        visited.Add((direction, currentPosition));
                    }
                    else
                    {
                        direction = TurnRight(direction);
                    }

                    
                }
            }
            
            
            map[position.Item2][position.Item1] = true;
        }

        return loopCount;
    }
    
    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    
    Func<Direction, (int x, int y), (int x, int y)> GetNextStep = (direction, position) =>
    {
        return direction switch
        {
            Direction.Up => (position.x, position.y - 1),
            Direction.Down => (position.x, position.y + 1),
            Direction.Left => (position.x - 1, position.y),
            Direction.Right => (position.x + 1, position.y),
            _ => throw new ArgumentOutOfRangeException()
        };
    };
    
    Func<Direction, Direction> TurnRight = direction =>
    {
        return direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new ArgumentOutOfRangeException()
        };
    };
    
    Func<(int x,int y), int, int, bool> IsOutOfMap = (position, height, width) =>
    {
        return position.x < 0 || position.x >= width || position.y < 0 || position.y >= height;
    };
}