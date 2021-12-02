using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_2021;
internal enum Direction
{ 
    Forward,
    Up,
    Down
}
internal class Day02
{
    public int SolveFirst()
    {
        var instructions = File.ReadAllLines(Path.Combine("Input", "Day02.txt")).Select(Parse);
        var x = 0;
        var y = 0;
        foreach (var instruction in instructions)
        { 
            if(instruction.direction == Direction.Up)
                y -= instruction.units;
            if (instruction.direction == Direction.Down)
                y += instruction.units;
            if (instruction.direction == Direction.Forward)
                x += instruction.units;
        }
        return x * y;
    }

    public int SolveSecond()
    {
        var instructions = File.ReadAllLines(Path.Combine("Input", "Day02.txt")).Select(Parse);
        var x = 0;
        var y = 0;
        var aim = 0;
        foreach (var instruction in instructions)
        {
            if (instruction.direction == Direction.Up)
                aim -= instruction.units;
            if (instruction.direction == Direction.Down)
                aim += instruction.units;
            if (instruction.direction == Direction.Forward)
            {
                x += instruction.units;
                y += aim * instruction.units;
            }
        }
        return x * y;
    }

    (Direction direction, int units) Parse(string s)
    { 
        var split = s.Split(' ');
        var d = split[0] switch
        { 
            "forward" => Direction.Forward,
            "up" => Direction.Up,
            "down" => Direction.Down,
            _ => throw new Exception("unknown direction")
        };
        return (d, int.Parse(split[1]));
    }
}
