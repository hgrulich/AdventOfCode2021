using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_2021
{
    internal class Day13
    {
        enum Fold
        {
            Horizontal_X,
            Vertical_Y
        }
        public long SolveFirst()
        {
            var input = File.ReadAllLines(Path.Combine("Input", "Day13.txt"));
            int index = 0;
            var coords = new List<(int, int)>();
            var instructions = new List<(int, Fold)>();
            while (input[index] != string.Empty)
            {
                var nums = input[index].Split(',');
                coords.Add((int.Parse(nums[0]), int.Parse(nums[1])));
                index++;
            }
            index++;
            while (index < input.Length)
            {
                var instr = input[index].Split(' ')[2];
                var split = instr.Split('=');
                instructions.Add(((int.Parse(split[1]), split[0] == "x" ? Fold.Horizontal_X : Fold.Vertical_Y)));
                index++;
            }
            foreach (var foldInstruction in instructions)
            {
                var foldIndex = foldInstruction.Item1;

                for (int i = 0; i < coords.Count; i++)
                {
                    if (foldInstruction.Item2 == Fold.Horizontal_X)
                    {
                        var coord = coords[i];
                        if (coord.Item1 > foldIndex)
                            coords[i] = (coord.Item1 - (2 * (coord.Item1 - foldIndex)), coord.Item2);
                    }
                    else
                    {
                        var coord = coords[i];
                        if (coord.Item2 > foldIndex)
                            coords[i] = (coord.Item1, coord.Item2 - ((coord.Item2 - foldIndex) * 2));
                    }


                }
                break;
            }
            

            return coords.Distinct().Count();

        } 

        public long SolveSecond()
        {
            var input = File.ReadAllLines(Path.Combine("Input", "Day13.txt"));
            int index = 0;
            var coords = new List<(int, int)>();
            var instructions = new List<(int, Fold)>();
            while (input[index] != string.Empty)
            {
                var nums = input[index].Split(',');
                coords.Add((int.Parse(nums[0]), int.Parse(nums[1])));
                index++;
            }
            index++;
            while (index < input.Length)
            {
                var instr = input[index].Split(' ')[2];
                var split = instr.Split('=');
                instructions.Add(((int.Parse(split[1]), split[0] == "x" ? Fold.Horizontal_X : Fold.Vertical_Y)));
                index++;
            }
            foreach (var foldInstruction in instructions)
            {
                var foldIndex = foldInstruction.Item1;

                for (int i = 0; i < coords.Count; i++)
                {
                    if (foldInstruction.Item2 == Fold.Horizontal_X)
                    {
                        var coord = coords[i];
                        if (coord.Item1 > foldIndex)
                            coords[i] = (coord.Item1 - (2 * (coord.Item1 - foldIndex)), coord.Item2);
                    }
                    else
                    {
                        var coord = coords[i];
                        if (coord.Item2 > foldIndex)
                            coords[i] = (coord.Item1, coord.Item2 - ((coord.Item2 - foldIndex) * 2));
                    }


                }
            }

            var minX = coords.Select(c => c.Item1).Min();
            var maxX = coords.Select(c => c.Item1).Max();
            var minY = coords.Select(c => c.Item2).Min();
            var maxY = coords.Select(c => c.Item2).Max();

            var height = maxY - minY + 1;
            var width = maxX - minX + 1;

            var array = new char[height * width];
            for (int i = 0; i < array.Length; i++)
                array[i] = '.';

            foreach (var coord in coords)
            {
                array[(coord.Item2 - minY) * width + (coord.Item1 - minX)] = '#';
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (i % width == 0)
                    Console.WriteLine();
                Console.Write(array[i]);
            }

            return 0;
        }
    }
}

