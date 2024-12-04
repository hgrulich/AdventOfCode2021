namespace AoC2023;

public class Day05
{
    public async Task<long> Solve1()
    {
        var lines = await File.ReadAllLinesAsync(@"Input\day05.txt");

        var seedNumbers = lines
            .First()
            .Split(':')[1]
            .Split(' ')
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(long.Parse);

        var mappingRawLines = new List<string>();
        var nodes = new List<MapNode>();
        foreach (var line in lines.Skip(2))
        {
            if (string.IsNullOrEmpty(line))
            {
                nodes.Add(CreateMapNode(mappingRawLines));
                mappingRawLines.Clear();
            }
            else
            {
                if (char.IsDigit(line.First()))
                {
                    mappingRawLines.Add(line);
                }
            }
        }
        nodes.Add(CreateMapNode(mappingRawLines));
        
        var min = long.MaxValue;

        foreach (var seed in seedNumbers)
        {
            long temp = seed;
            foreach (var node in nodes)
            {
                temp = node.Map(temp);
            }
            if(temp < min)
                min = temp;
        }
        
        return min;
    }

    private MapNode CreateMapNode(List<string> rawMappings)
    {
        var mappings = rawMappings
            .Select(x => x.Split(' ').Select(long.Parse).ToArray())
            .Select(x => new Mapping()
            {
                DestinationRangeStart = x[0],
                SourceRangeStart = x[1],
                RangeLength = x[2]
            })
            .ToList();
        return new MapNode(mappings);
    }

    
}

public class MapNode
{
    private readonly List<Mapping> _mappings;

    public MapNode(List<Mapping> mappings)
    {
        _mappings = mappings;
    }
    public long Map(long source)
    {
        var mapping = _mappings.FirstOrDefault(x => x.IsSourceInRange(source));
        if (mapping is null)
            return source;
        
        return mapping.Map(source);
    }
}

public class Mapping
{
    public long SourceRangeStart { get; set; }
    public long DestinationRangeStart { get; set; }
    public long RangeLength { get; set; }

    public long Map(long source)
    {
        if (!IsSourceInRange(source))
            throw new ArgumentOutOfRangeException(nameof(source));
        return DestinationRangeStart + (source - SourceRangeStart);
    }
    
    public bool IsSourceInRange(long source)
    {
        return source >= SourceRangeStart && source < SourceRangeStart + RangeLength;
    }
}
