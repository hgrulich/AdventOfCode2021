using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_2021;

internal class Day08
{
    public int SolveFirst()
    {
        var numbers = new List<int>() { 2, 4, 7, 3 };
        var signals = File.ReadAllLines(Path.Combine("Input", "Day08.txt")).Select(x => new Signal(x));
        return signals.Select(s => s.Output.Count(o => numbers.Contains(o.Length))).Sum();
    }

    public int SolveSecond()
    {
        var signals = File.ReadAllLines(Path.Combine("Input", "Day08.txt")).Select(x => new Signal(x));
        return signals.Sum(x => this.SolveLine(x));
    }

    private int SolveLine(Signal signal)
    {
        var segments = new char[7];
        var one = signal.Pattern.First(x => x.Length == 2);
        var seven = signal.Pattern.First(x => x.Length == 3);
        var four = signal.Pattern.First(x => x.Length == 4);
        var eight = signal.Pattern.First(x => x.Length == 7);
        segments[0] = seven.First(x => !one.Contains(x));
        var six = signal.Pattern.First(x => x.Length == 6 && one.Count(o => x.Contains(o)) == 1);
        segments[2] = one.First(x => !six.Contains(x));
        segments[5] = one.First(x => x != segments[2]);
        var five = signal.Pattern.First(x => x.Length == 5 && four.Where(f => !one.Contains(f)).All(f => x.Contains(f)) && x.Contains(segments[0]));
        segments[6] = five.First(x => !four.Contains(x) && x != segments[0]);
        var threeKnown = new char[] { segments[0], segments[2], segments[5], segments[6] };
        var three = signal.Pattern.First(x => x.Length == 5 && threeKnown.All(t => x.Contains(t)));
        segments[3] = three.First(x => !threeKnown.Contains(x));
        var fourKnown = new char[] { segments[2], segments[3], segments[5]};
        segments[1] = four.First(x => !fourKnown.Contains(x));
        segments[4] = "abcdefg".First(x => !segments.Contains(x));


        var patterns = new char[][]
        {
            new char[]{ segments[0], segments[1], segments[2], segments[4], segments[5], segments[6] },
            new char[]{ segments[2], segments[5] },
            new char[]{ segments[0], segments[2], segments[3], segments[4], segments[6] },
            new char[]{ segments[0], segments[2], segments[3], segments[5], segments[6] },
            new char[]{ segments[1], segments[2], segments[3], segments[5] },
            new char[]{ segments[0], segments[1], segments[3], segments[5], segments[6] },
            new char[]{ segments[0], segments[1], segments[4], segments[5], segments[6] },
            new char[]{ segments[0], segments[2], segments[5] },
            new char[]{ segments[0], segments[1], segments[2], segments[3], segments[4], segments[5], segments[6] },
            new char[]{ segments[0], segments[1], segments[2], segments[3], segments[5] },
        };

        return int.Parse(new string(signal.Output.Select(x => GetNumber(x, patterns)).ToArray()));
    }

    char GetNumber(string s, char[][] patterns)
    {
        if (s.Length == 6 && patterns[0].All(x => s.Contains(x))) return '0';
        if (s.Length == 2 && patterns[1].All(x => s.Contains(x))) return '1';
        if (s.Length == 5 && patterns[2].All(x => s.Contains(x))) return '2';
        if (s.Length == 5 && patterns[3].All(x => s.Contains(x))) return '3';
        if (s.Length == 4 && patterns[4].All(x => s.Contains(x))) return '4';
        if (s.Length == 5 && patterns[5].All(x => s.Contains(x))) return '5';
        if (s.Length == 6 && patterns[6].All(x => s.Contains(x))) return '6';
        if (s.Length == 3 && patterns[7].All(x => s.Contains(x))) return '7';
        if (s.Length == 7 && patterns[8].All(x => s.Contains(x))) return '8';
        if (s.Length == 6 && patterns[9].All(x => s.Contains(x))) return '9';
        throw new Exception();
    }
}

class Signal
{
    public IEnumerable<string> Pattern { get; private set; }
    public IEnumerable<string> Output { get; private set; }

    public Signal(string input)
    {
        var parts = input.Split('|');
        this.Pattern = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        this.Output = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }
}

