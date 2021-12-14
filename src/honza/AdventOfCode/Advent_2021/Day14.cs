using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_2021
{
    internal class Day14
    {
        public long SolveFirst()
        {
            var input = File.ReadAllLines(Path.Combine("Input", "Day14.txt"));

            var polymer = input[0];
            var rules = new Dictionary<string, string>();

            for (int i = 2; i < input.Length; i++)
            {
                var split = input[i].Split(" -> ");
                rules.Add(split[0], split[1]);
            }

            for (int i = 0; i < 10; i++)
            {
                var newPolymer = new StringBuilder();
                for (int j = 0; j < polymer.Length - 1; j++)
                {
                    newPolymer.Append(polymer[j]);
                    newPolymer.Append(rules[polymer.Substring(j,2)]);
                }
                newPolymer.Append(polymer.Last());
                polymer = newPolymer.ToString();
            }
            var result = polymer.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var mostCommonCount = result.Where(x => x.Value == result.Values.Max()).First().Value;
            var leastCommonCount = result.Where(x => x.Value == result.Values.Min()).First().Value;

            return mostCommonCount - leastCommonCount;
        }

        public long SolveSecond()
        {
            var input = File.ReadAllLines(Path.Combine("Input", "Day14.txt"));

            var polymer = input[0];
            var rules = new Dictionary<string, string>();

            for (int i = 2; i < input.Length; i++)
            {
                var split = input[i].Split(" -> ");
                rules.Add(split[0], split[1]);
            }

            var possibleLetters = rules.Select(x => x.Value).Distinct().ToDictionary(x => x, x => (long)0);

            foreach (var character in polymer)
            {
                possibleLetters[character.ToString()]++;
            }

            var counts = new Dictionary<string, long>();
            for (int i = 0; i < polymer.Length - 1; i++)
            {
                var pair = polymer.Substring(i, 2);
                if(counts.ContainsKey(pair))
                    counts[pair]++;
                else
                    counts.Add(pair, 1);
            }
            for (int i = 0; i < 40; i++)
            {
                var newCounts = new Dictionary<string, long>();
                for (int j = 0; j < counts.Count; j++)
                {
                    var keyPair = counts.ElementAt(j);
                    var newElement = rules[keyPair.Key];
                    var firstNewPair = keyPair.Key[0] + newElement;
                    var secondNewPair = newElement + keyPair.Key[1];

                    possibleLetters[newElement] += keyPair.Value;

                    if (newCounts.ContainsKey(firstNewPair))
                        newCounts[firstNewPair] += keyPair.Value;
                    else
                        newCounts.Add(firstNewPair, keyPair.Value);

                    if (newCounts.ContainsKey(secondNewPair))
                        newCounts[secondNewPair] += keyPair.Value;
                    else
                        newCounts.Add(secondNewPair, keyPair.Value);
                }
                counts = newCounts;
            }
            var values = possibleLetters.Select(x => x.Value);
            return values.Max() - values.Min();
        }
    }
}
