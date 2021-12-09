using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_2021
{
    internal class Day09
    {
        public int SolveFirst()
        {
            var map = File.ReadAllLines(Path.Combine("Input", "Day09.txt")).Select(s => s.Select(ch => int.Parse(ch.ToString())).ToArray()).ToArray();
            var lowPoints = new List<int>();
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[0].Length; x++)
                {
                    var points = new List<(int x, int y)>();
                    points.Add((x-1,y));
                    points.Add((x +1, y));
                    points.Add((x, y - 1));
                    points.Add((x, y + 1));
                    
                    if(points.Where(p => p.x>=0 && p.x< map[0].Length && p.y >= 0 && p.y<map.Length).All(p => map[y][x] < map[p.y][p.x]))
                        lowPoints.Add(map[y][x]);
                }
            }
            return lowPoints.Select(x => x+1).Sum();
        }

        public int SolveSecond()
        {
            var map = File.ReadAllLines(Path.Combine("Input", "Day09.txt")).Select(s => s.Select(ch => int.Parse(ch.ToString())).ToArray()).ToArray();
            var lowPoints = new List<(int x, int y)>();
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[0].Length; x++)
                {
                    var points = new List<(int x, int y)>();
                    points.Add((x-1,y));
                    points.Add((x +1, y));
                    points.Add((x, y - 1));
                    points.Add((x, y + 1));
                    
                    if(points.Where(p => p.x>=0 && p.x< map[0].Length && p.y >= 0 && p.y<map.Length).All(p => map[y][x] < map[p.y][p.x]))
                        lowPoints.Add((x,y));
                }
            }
            var basinSizes = new List<int>();
            foreach (var lowPoint in lowPoints)
            {
                var basinSize = 1;
                var checkedPoints = new List<(int x, int y)>();
                var pointsToCheck = new List<(int x, int y)>() { lowPoint };
                while (pointsToCheck.Count > 0)
                {
                    var p = pointsToCheck[0];
                    pointsToCheck.RemoveAt(0);
                    var points = new List<(int x, int y)>();
                    points.Add((p.x - 1, p.y));
                    points.Add((p.x + 1, p.y));
                    points.Add((p.x, p.y - 1));
                    points.Add((p.x, p.y + 1));
                    
                    foreach (var point in points.Where(p => !checkedPoints.Contains(p) && p.x >= 0 && p.x < map[0].Length && p.y >= 0 && p.y < map.Length))
                    { 
                        if(map[point.y][point.x] != 9)
                        { 
                            if(!pointsToCheck.Contains(point))
                            {
                                pointsToCheck.Add(point);
                                basinSize += 1;
                            }
                        }
                    }
                    checkedPoints.Add(p);
                }
                basinSizes.Add(basinSize);
            }
            basinSizes.Sort();
            return basinSizes.Skip(basinSizes.Count - 3).Aggregate((x,y) => x * y);
        }
    }
}
