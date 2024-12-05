using System.Text.RegularExpressions;

namespace AdventOfCode.Day03
{
    public static class Code
    {
        public static long Part1(string line)
        {
            return CalculateSum(line);
        }

        public static long Part2(string line)
        {
            var stringToCheck = GetStringToCheck(line);

            return CalculateSum(stringToCheck);
        }

        public static long CalculateSum(string line)
        {
            long sum = 0;

            var expressionPattern = @"mul\((\d{1}|\d{2}|\d{3}),(\d{1}|\d{2}|\d{3})\)";
            var numbersPattern = @"\d+";
            var expressions = Regex.Matches(line, expressionPattern);

            foreach (var expression in expressions)
            {
                if (expression.ToString() != null)
                {
                    var numbers = Regex.Matches(expression.ToString(), numbersPattern).Select(x => int.Parse(x.ToString())).ToList();
                    sum += Math.BigMul(numbers[0], numbers[1]);
                }
            }

            return sum;
        }

        private static string GetStringToCheck(string line)
        {
            string doCondition = @"do\(\)";
            string dontCondition = @"don't\(\)";

            var doConditionIndexes = Regex.Matches(line, doCondition).Select(x => x.Index).ToList();
            var dontConditionIndexes = Regex.Matches(line, dontCondition).Select(x => x.Index).ToList();

            int? startIndex = 0;
            var stringToCheck = "";

            foreach (var dontIndex in dontConditionIndexes)
            {
                if (startIndex != null && dontIndex > startIndex)
                {
                    stringToCheck += line.Substring((int)startIndex, (dontIndex - (int)startIndex));
                }

                startIndex = doConditionIndexes.FirstOrDefault(x => x > dontIndex);
            }

            if (startIndex != null)
            {
                stringToCheck += line.Substring((int)startIndex, (line.Length - (int)startIndex));
            }

            return stringToCheck;
        }
    }
}
