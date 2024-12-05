using System.Text.RegularExpressions;

namespace AdventOfCode.Day01
{
    public static class Code
    {
        public static int Part1(string[] lines)
        {
            var lists = LinesToSortedLists(lines);

            var distance = 0;

            for (int i = 0; i < lists.Left.Count; i++)
            {
                var delta = lists.Left[i] - lists.Right[i];
                distance += Math.Abs(delta);
            }

            return distance;
        }

        public static int Part2(string[] lines)
        {
            var lists = LinesToSortedLists(lines);

            var similarityScore = 0;

            foreach (var value in lists.Left)
            {
                var occurances = lists.Right.Count(x => x == value);
                similarityScore += value * occurances;
            }

            return similarityScore;
        }

        private static (List<int> Left, List<int> Right) LinesToSortedLists(string[] lines)
        {
            var pattern = " +";
            var leftList = new List<int>();
            var rightList = new List<int>();

            foreach (var line in lines)
            {
                var splitLine = Regex.Split(line, pattern);
                leftList.Add(Convert.ToInt32(splitLine[0]));
                rightList.Add(Convert.ToInt32(splitLine[1]));
            }

            leftList.Sort();
            rightList.Sort();

            return (leftList, rightList);
        }
    }
}
