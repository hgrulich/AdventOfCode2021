using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_2021
{
    internal class Day03
    {
        public int SolveFirst()
        {
            var bits = File.ReadAllLines(Path.Combine("Input", "Day03.txt")).Select(Parse);
            var length = bits.First().Count;
            var rowCount = bits.Count();

            var gamma = new bool[length];
            var epsilon = new bool[length];
            for (int i = 0; i < length; i++)
            {
                var oneMostCommon = bits.Count(x => x[i]) > (rowCount / 2);
                gamma[i] = oneMostCommon;
                epsilon[i] = !oneMostCommon;
            }
            return this.ToInt(new BitArray(gamma)) * this.ToInt(new BitArray(epsilon));
        }

        public int SolveSecond()
        {
            var bits = File.ReadAllLines(Path.Combine("Input", "Day03.txt")).Select(Parse);
            return GetRating(bits, true, (searchedCount, totalCount) => searchedCount >= totalCount) 
                * GetRating(bits, false, (searchedCount, totalCount) => searchedCount <= totalCount);
        }

        public int GetRating(IEnumerable<BitArray> bits, bool searchedFor, Func<int, double, bool> comparer)
        {
            var length = bits.First().Count;
            var index = 0;
            IEnumerable<BitArray> workBits = new List<BitArray>(bits);
            while (workBits.Count() > 1)
            {
                var isMostFrequent = comparer(workBits.Count(x => x[index] == searchedFor), (workBits.Count() / 2d));
                workBits = workBits.Where(x => x[index] == (isMostFrequent ? searchedFor : !searchedFor)).ToList();
                index++;
            }
            return this.ToInt(workBits.First());
        }

        private BitArray Parse(string s)
        { 
            return new BitArray(s.Select(ch => ch == '1').ToArray());    
        }

        private int ToInt(BitArray bitArray)
        {
            int value = 0;
            for (int i = 0; i < bitArray.Count; i++)
            {
                if (bitArray[i])
                    value += Convert.ToInt16(Math.Pow(2, bitArray.Count - 1 - i));
            }
            return value;
        }
    }
}
