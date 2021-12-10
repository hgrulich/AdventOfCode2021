using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_2021
{
    internal class Day10
    {
        char[] openingBrackets = {'(', '[', '{', '<' };
        char[] closingBrackets = { ')', ']', '}', '>' };
        Dictionary<char, char> mapping = new Dictionary<char, char> {{ ')', '(' },{ ']', '[' },{ '}', '{' },{ '>', '<' }, { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
        public int SolveFirst()
        {
            var input = File.ReadAllLines(Path.Combine("Input", "Day10.txt"));
            var score = 0;
            foreach (var line in input)
            {
                var openedCharacters = new Stack<char>();
                char corruptedElement = char.MinValue;
                foreach (var character in line)
                {
                    if(openingBrackets.Contains(character))
                        openedCharacters.Push(character);
                    else
                    { 
                        if(openedCharacters.Pop() != mapping[character])
                        {
                            corruptedElement = character;
                        }
                    }

                }
                score += corruptedElement switch
                {
                    ')' => 3,
                    ']' => 57,
                    '}' => 1197,
                    '>' => 25137,
                    _ => 0
                };
            }
            return score;
        }

        public long SolveSecond()
        {
            var input = File.ReadAllLines(Path.Combine("Input", "Day10.txt"));
            var scores = new List<long>();
            foreach (var line in input)
            {
                var openedCharacters = new Stack<char>();
                char corruptedElement = char.MinValue;
                foreach (var character in line)
                {
                    if (openingBrackets.Contains(character))
                        openedCharacters.Push(character);
                    else
                    {
                        if (openedCharacters.Pop() != mapping[character])
                        {
                            corruptedElement = character;
                        }
                    }

                }
                if(corruptedElement == char.MinValue)
                {
                    long score = 0;
                    while(openedCharacters.Count > 0)
                    {
                        score = mapping[openedCharacters.Pop()] switch
                        {
                            ')' => (score * 5) + 1,
                            ']' => (score * 5) + 2,
                            '}' => (score * 5) + 3,
                            '>' => (score * 5) + 4,
                            _ => 0
                        };
                    }
                    scores.Add(score);
                }
            }
            return scores.OrderBy(x => x).ElementAt((scores.Count - 1) / 2);
        }
    }
}
