namespace AoC2024;

public class Day09 : IDay
{
    List<(int id, int count)> GetMemory()
    {
        var line = File.ReadAllText("Input\\Input09.txt");
        var isFile = true;
        var memory = new List<(int id, int count)>(); //id, count
        int counter = 0;
        foreach (var numStr in line)
        {
            var number = int.Parse(numStr.ToString());
            if (number == 0)
            {
                isFile = !isFile;
                continue;
            }

            if (isFile)
            {
                memory.Add((counter, number));
                counter++;
            }
            else
                memory.Add((-1, number));
            isFile = !isFile;
        }

        return memory;
    }
    

    public long Solve1()
    {
        var memory = GetMemory();

        while (true)
        {
            var indexOfFirstUnused = memory.IndexOf(memory.First(x => x.id == -1));
            var indexOfLastUsed = memory.LastIndexOf(memory.Last(x => x.id != -1));

            if(indexOfFirstUnused > indexOfLastUsed)
                break;
            
            var lastUsed = memory[indexOfLastUsed];
            var firstUnused = memory[indexOfFirstUnused];
            
            if (lastUsed.count == firstUnused.count)
            {
                firstUnused.id = lastUsed.id;
                lastUsed.id = -1;
                
                memory[indexOfLastUsed] = lastUsed;
                memory[indexOfFirstUnused] = firstUnused;
            }
            else if (lastUsed.count < firstUnused.count)
            {
                memory.Insert(indexOfFirstUnused, (lastUsed.id, lastUsed.count));
                //firstUnused.id = lastUsed.id;
                firstUnused.count = firstUnused.count - lastUsed.count;
                lastUsed.id = -1;
                
                memory[indexOfLastUsed + 1] = lastUsed;
                memory[indexOfFirstUnused + 1] = firstUnused;
            }
            else if(lastUsed.count > firstUnused.count)
            {
                memory.Insert(indexOfLastUsed + 1, (-1, firstUnused.count));
                firstUnused.id = lastUsed.id;
                lastUsed.count = lastUsed.count - firstUnused.count;
                
                memory[indexOfLastUsed] = lastUsed;
                memory[indexOfFirstUnused] = firstUnused;
            }
        }
        return CalculateCheckSum(memory);
    }

    public long Solve2()
    {
        var memory = GetMemory();

        var lastMovedId = int.MaxValue;
        
        while (lastMovedId != 0)
        {
            var indexOfLastUnmovedFile = memory.LastIndexOf(memory.Last(x => x.id != -1 && x.id < lastMovedId));
            var lastUnmoved = memory[indexOfLastUnmovedFile];
            lastMovedId = lastUnmoved.id;
            
            var indexOfFirstBigEnoughGape = memory.IndexOf(memory.FirstOrDefault(x => x.id == -1 && x.count >= lastUnmoved.count));
            if (indexOfFirstBigEnoughGape == -1 || indexOfLastUnmovedFile < indexOfFirstBigEnoughGape)
            {
                continue;
            }

            var firstBigEnoughGap = memory[indexOfFirstBigEnoughGape];
            
            if (lastUnmoved.count == firstBigEnoughGap.count)
            {
                firstBigEnoughGap.id = lastUnmoved.id;
                lastUnmoved.id = -1;
                
                memory[indexOfLastUnmovedFile] = lastUnmoved;
                memory[indexOfFirstBigEnoughGape] = firstBigEnoughGap;
            }
            else if (lastUnmoved.count < firstBigEnoughGap.count)
            {
                memory.Insert(indexOfFirstBigEnoughGape, (lastUnmoved.id, lastUnmoved.count));
                //firstUnused.id = lastUsed.id;
                firstBigEnoughGap.count = firstBigEnoughGap.count - lastUnmoved.count;
                lastUnmoved.id = -1;
                
                memory[indexOfLastUnmovedFile + 1] = lastUnmoved;
                memory[indexOfFirstBigEnoughGape + 1] = firstBigEnoughGap;
            }
        }
        
        return CalculateCheckSum(memory);
    }

    long CalculateCheckSum(List<(int id, int count)> memory)
    {
        long sum = 0;
        var positionCounter = 0;
        foreach (var position in memory)
        {
            if (position.id == -1)
            {
                positionCounter += position.count;
                continue;
            }

            for (int i = 0; i < position.count; i++)
            {
                sum += positionCounter * position.id;
                positionCounter++;
            }
        }

        return sum;
    }
}