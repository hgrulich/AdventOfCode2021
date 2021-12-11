using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_2021
{
    internal class Day11
    {
        public int SolveFirst()
        {
            var array = File.ReadAllLines(Path.Combine("Input", "Day11.txt"))
                .Select(x => x.ToArray().Select(ch => ch.ToString()).Select(byte.Parse).ToArray())
                .Aggregate((a,b) => a.Concat(b).ToArray());
            var flashCounter = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int y = 0; y < 10; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        array[y * 10 + x]++;
                    }
                }

                var flashed = new bool[100];

                while (array.Count(b => b > 9) != 0)
                {
                    var additions = new byte[100];
                    for (int y = 0; y < 10; y++)
                    {
                        for (int x = 0; x < 10; x++)
                        {
                            if (array[y * 10 + x] > 9)
                            {
                                flashed[y * 10 + x] = true;
                                foreach (int flashY in Enumerable.Range(-1, 3))
                                {
                                    foreach (var flashX in Enumerable.Range(-1,3))
                                    {
                                        if ((x + flashX) >= 0 && (x + flashX) < 10 && (y + flashY) >= 0 && (y + flashY) < 10)
                                            additions[(y + flashY) * 10 + (x + flashX)]++;
                                    }
                                }
                                array[y * 10 + x] = 0;
                            }
                        }
                    }

                    for (int j = 0; j < 100; j++)
                    {
                        if (!flashed[j])
                            array[j] += additions[j];
                    }
                }
                flashCounter += flashed.Count(x => x);
            }
            
            return flashCounter;
        }

        public long SolveSecond()
        {
            var array = File.ReadAllLines(Path.Combine("Input", "Day11.txt"))
                .Select(x => x.ToArray().Select(ch => ch.ToString()).Select(byte.Parse).ToArray())
                .Aggregate((a, b) => a.Concat(b).ToArray());
            for (int i = 0; i < int.MaxValue; i++)
            {
                for (int y = 0; y < 10; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        array[y * 10 + x]++;
                    }
                }

                var flashed = new bool[100];

                while (array.Count(b => b > 9) != 0)
                {
                    var additions = new byte[100];
                    for (int y = 0; y < 10; y++)
                    {
                        for (int x = 0; x < 10; x++)
                        {
                            if (array[y * 10 + x] > 9)
                            {
                                flashed[y * 10 + x] = true;
                                foreach (int flashY in Enumerable.Range(-1, 3))
                                {
                                    foreach (var flashX in Enumerable.Range(-1, 3))
                                    {
                                        if ((x + flashX) >= 0 && (x + flashX) < 10 && (y + flashY) >= 0 && (y + flashY) < 10)
                                            additions[(y + flashY) * 10 + (x + flashX)]++;
                                    }
                                }
                                array[y * 10 + x] = 0;
                            }
                        }
                    }

                    for (int j = 0; j < 100; j++)
                    {
                        if (!flashed[j])
                            array[j] += additions[j];
                    }
                }
                if (flashed.All(x => x)) 
                    return i + 1;
            }

            return -1;
        }
    }
}
