namespace AoC2023;

public class Day05_2
{
    public async Task<long> Solve2()
    {
        var lines = await File.ReadAllLinesAsync(@"Input\day05.txt");

        var seedRangeValues = lines
            .First()
            .Split(':')[1]
            .Split(' ')
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(long.Parse)
            .ToArray();

        var ranges = new List<Range>();
        for (int i = 0; i < seedRangeValues.Length; i = i + 2)
        {
            ranges.Add(new Range(seedRangeValues[i], seedRangeValues[i + 1]));
        }
        
        return 0;
    }
    
    public class MapNode
    {
        private readonly List<RangeMapping> _mappings;

        public MapNode(List<RangeMapping> mappings)
        {
            _mappings = mappings;
        }
        public List<Range> Map(List<Range> source)
        {
            foreach (var sourceRange in source)
            {
                
            }
        
            return mapping.Map(source);
        }
    }

    public class RangeMapping
    {
        public long SourceRangeStart { get; set; }
        public long DestinationRangeStart { get; set; }
        public long RangeLength { get; set; }
        public List<Range> Map(List<Range> sources)
        {
            
        }
    }

    public class Range
    {
        public Range(long start, long length)
        {
            Start = start;
            Length = length;
        }

        public long Start { get; set; }
        public long Length { get; set; }
    }
}