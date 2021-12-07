﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_2021
{
    internal class Day07
    {
        public int SolveFirst()
        {
            var positions = File.ReadAllLines(Path.Combine("Input", "Day07.txt"))[0].Split(',').Select(int.Parse);
            return this.Solve((crabPos, targetPos) => Math.Abs(crabPos - targetPos));
        }

        public int SolveSecond()
        {
            var positions = File.ReadAllLines(Path.Combine("Input", "Day07.txt"))[0].Split(',').Select(int.Parse);
            return this.Solve((crabPos, targetPos) => MakeIntSum(Math.Abs(crabPos - targetPos)));
        }

        int Solve(Func<int, int, int> fuelMapping)
        {
            var positions = File.ReadAllLines(Path.Combine("Input", "Day07.txt"))[0].Split(',').Select(int.Parse);
            var min = positions.Min();
            var max = positions.Max();
            var minSum = int.MaxValue;
            return Enumerable.Range(min, max).Select(i => positions.Select(x => fuelMapping(x, i)).Sum()).Min();
        }

        int MakeIntSum(int value) => (value + 1) * value / 2;
    }
}
