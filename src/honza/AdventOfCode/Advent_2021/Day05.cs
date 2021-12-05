using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Advent_2021
{
    internal class Day05
    {
        public int SolveFirst()
        {
            var lines = File.ReadAllLines(Path.Combine("Input", "Day05.txt")).Select(Parse).Where(l => l.IsPerpendicular);

            return this.Solve(lines);
        }

        public int SolveSecond()
        {
            var lines = File.ReadAllLines(Path.Combine("Input", "Day05.txt")).Select(Parse);

            return this.Solve(lines);
        }

        int Solve(IEnumerable<Line> lines)
        {
            var counts = new Dictionary<(int, int), int>();
            foreach (var line in lines)
            {
                foreach (var point in line.Points)
                {
                    if (counts.ContainsKey(point))
                        counts[point]++;
                    else
                        counts.Add(point, 1);
                }
            }
            return counts.Values.Count(x => x > 1);
        }

        Line Parse(string s)
        { 
            var points = s.Split(" -> ");
            var first = points[0].Split(',').Select(int.Parse);
            var second = points[1].Split(',').Select(int.Parse);

            return new Line(first.ElementAt(0), first.ElementAt(1), second.ElementAt(0), second.ElementAt(1));
        }
    }


    public class Line
    {
        public Line(int x1, int y1, int x2, int y2)
        {
            var additionX = (x2 - x1);
            if(additionX != 0)
                additionX /= Math.Abs(x2 - x1);
            var additionY = (y2 - y1);
            if (additionY != 0)
                additionY /= Math.Abs(y2 - y1);

            var point = (x1, y1);
            while (point != (x2,y2))
            {
                Points.Add(point);
                point = (point.x1 + additionX, point.y1 + additionY);
            }
            Points.Add(point);
            this.IsPerpendicular = x1 == x2 || y1 == y2;
        }

        public bool IsPerpendicular { get; }

        public IList<(int, int)> Points { get; } = new List<(int, int)>();
    }
}
