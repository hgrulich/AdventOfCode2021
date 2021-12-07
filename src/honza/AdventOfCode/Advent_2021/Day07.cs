using System;
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
            var min = positions.Min();
            var max = positions.Max();
            var minSum = int.MaxValue;
            for (int i = min; i <= max; i++)
            {
                var sum = positions.Select(x => Math.Abs(x - i)).Sum();
                if(minSum > sum)
                    minSum = sum;
            }
            return minSum;
        }

        public int SolveSecond()
        {
            var positions = File.ReadAllLines(Path.Combine("Input", "Day07.txt"))[0].Split(',').Select(int.Parse);
            var min = positions.Min();
            var max = positions.Max();
            var minSum = int.MaxValue;
            for (int i = min; i <= max; i++)
            {
                //var sum = positions.Select(x => Enumerable.Range(1, Math.Abs(x - i)).Sum()).Sum(); //3 times slower than private method
                var sum = positions.Select(x => MakeIntSum(Math.Abs(x - i))).Sum();
                if (minSum > sum)
                    minSum = sum;
            }
            return minSum;
        }

        int MakeIntSum(int value)
        {
            var result = 0;
            for (int i = value; i > 0; i--)
            {
                result += i;
            }
            return result;
        }
    }
}
