using System.Numerics;

namespace AdventOfCode.Day11
{
    public class Code
    {
        private Dictionary<(string stone, int numberOfBlinks), BigInteger> _cache = new Dictionary<(string, int), BigInteger>();
        
        public long Part1(string line)
        {
            var stones = line.Split(' ');

            BigInteger result = 0;
            foreach (var stone in stones)
            {
                result += FindNumberOfStones(stone, 1, 25);
            }

            return (long)result;
        }

        public long Part2(string line)
        {
            var stones = line.Split(' ');

            BigInteger result = 0;
            foreach (var stone in stones)
            {
                result += FindNumberOfStones(stone, 1, 75);
            }

            return (long)result;
        }

        public BigInteger FindNumberOfStones(string stone, int numberOfBlinks, int maxNumberOfBlinks)
        {
            if (numberOfBlinks > maxNumberOfBlinks)
            {
                return 1;
            }

            // Memoization: Check if the result is already cached
            if (_cache.TryGetValue((stone, numberOfBlinks), out BigInteger cachedResult))
            {
                return cachedResult;
            }

            BigInteger result = 0;
            numberOfBlinks++;
            if (string.IsNullOrEmpty(stone) || stone == "0")
            {
                // Handle the case where stone is zero or empty
                result = FindNumberOfStones("1", numberOfBlinks, maxNumberOfBlinks);
            }
            else
            {
                int length = stone.Length;

                if (length % 2 == 0)
                {
                    int mid = length / 2;
                    string left = stone.Substring(0, mid).TrimStart('0');
                    string right = stone.Substring(mid).TrimStart('0');

                    result += FindNumberOfStones(left, numberOfBlinks, maxNumberOfBlinks);
                    result += FindNumberOfStones(right, numberOfBlinks, maxNumberOfBlinks);
                }
                else
                {
                    BigInteger stoneValue = BigInteger.Parse(stone);
                    BigInteger newStoneValue = stoneValue * 2024;
                    string newStone = newStoneValue.ToString().TrimStart('0');

                    result = FindNumberOfStones(newStone, numberOfBlinks, maxNumberOfBlinks);
                }
            }

            // Store the computed result in the cache
            _cache[(stone, numberOfBlinks-1)] = result;
            return result;
        }
    }
}