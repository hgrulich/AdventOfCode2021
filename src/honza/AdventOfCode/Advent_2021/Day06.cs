using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_2021
{
    internal class Day06
    {
        public long SolveFirst()
        {
            var population = File.ReadAllLines(Path.Combine("Input", "Day06.txt"))[0].Split(',').Select(int.Parse).ToList();
            for (int i = 0; i < 80; i++)
            {
                var newFishCounter = 0;
                for (int f = 0; f < population.Count; f++)
                {
                    if (population[f] == 0)
                    {
                        population[f] = 6;
                        newFishCounter++;
                    }
                    else
                    {
                        population[f] -= 1;
                    }
                }
                for (int j = 0; j < newFishCounter; j++)
                {
                    population.Add(8);
                }
            }
            return population.Count;
        }

        public long SolveSecond()
        {
            var population = File.ReadAllLines(Path.Combine("Input", "Day06.txt"))[0].Split(',').Select(int.Parse).ToList();
            var fishClasses = new long[9];
            foreach (var fish in population)
            {
                fishClasses[fish] += 1;
            }
            for (int i = 0; i < 256; i++)
            {
                var newFishClasses = new long[9];
                newFishClasses[8] = fishClasses[0];
                newFishClasses[6] = fishClasses[0];
                for (int j = 1; j < fishClasses.Length; j++)
                {
                    newFishClasses[j - 1] += fishClasses[j];
                }
                fishClasses = newFishClasses;
            }
            return fishClasses.Sum();
        }
    }
}
