namespace AdventOfCode.Day07
{
    public class Code
    {
        public long Part1(string[] lines)
        {
            long sum = 0;

            foreach (var line in lines)
            {
                //Get the desired result
                var desiredResult = Convert.ToInt64(line.Split(':')[0]);
                //Get the numbers for the calculation
                var calculationNumbers = line.Split(':')[1].Trim().Split(' ').Select(x => long.Parse(x)).ToArray();

                sum += Calculate1(desiredResult, calculationNumbers[0], calculationNumbers.Skip(1).ToArray(), calculationNumbers[0].ToString()) ? desiredResult : 0;
            }

            return sum;
        }

        public long Part2(string[] lines)
        {
            long sum = 0;

            foreach (var line in lines)
            {
                //Get the desired result
                var desiredResult = Convert.ToInt64(line.Split(':')[0]);
                //Get the numbers for the calculation
                var calculationNumbers = line.Split(':')[1].Trim().Split(' ').Select(x => long.Parse(x)).ToArray();

                sum += Calculate1(desiredResult, calculationNumbers[0], calculationNumbers.Skip(1).ToArray(), calculationNumbers[0].ToString()) ? desiredResult : 0;
            }

            return sum;
        }

        private bool Calculate1(long desiredResult, long totalSoFar, long[] numbersRemaining, string calculationBeingTested)
        {
            if (numbersRemaining.Length == 1)
            {
                var finalAddition = totalSoFar + numbersRemaining[0];
                //File.AppendAllText(@"c:\temp\AoC2024-day07.txt", $"{calculationBeingTested} + {numbersRemaining[0]} = {finalAddition} ({desiredResult}){Environment.NewLine}");
                if (desiredResult == finalAddition)
                {
                    return true;
                }
                var finalMultiplication = totalSoFar * numbersRemaining[0];
                //File.AppendAllText(@"c:\temp\AoC2024-day07.txt", $"{calculationBeingTested} * {numbersRemaining[0]} = {finalMultiplication} ({desiredResult}){Environment.NewLine}");
                if (desiredResult == finalMultiplication)
                {
                    return true;
                }
                var finalConcatination = Convert.ToInt64($"{totalSoFar}{numbersRemaining[0]}");
                //File.AppendAllText(@"c:\temp\AoC2024-day07.txt", $"{calculationBeingTested} * {numbersRemaining[0]} = {finalConcatination} ({desiredResult}){Environment.NewLine}");
                if (desiredResult == finalConcatination)
                {
                    return true;
                }

                return false;
            }

            if (Calculate1(desiredResult, totalSoFar + numbersRemaining[0], numbersRemaining.Skip(1).ToArray(), $"{calculationBeingTested} + {numbersRemaining[0]}") ||
                Calculate1(desiredResult, totalSoFar * numbersRemaining[0], numbersRemaining.Skip(1).ToArray(), $"{calculationBeingTested} * {numbersRemaining[0]}") ||
            Calculate1(desiredResult, Convert.ToInt64($"{totalSoFar}{numbersRemaining[0]}"), numbersRemaining.Skip(1).ToArray(), $"{calculationBeingTested} || {numbersRemaining[0]}"))
            {
                return true;
            }

            return false;
        }

        private bool Calculate2(long desiredResult, long totalSoFar, long[] numbersRemaining, string calculationBeingTested)
        {
            if (numbersRemaining.Length == 1)
            {
                var finalAddition = totalSoFar + numbersRemaining[0];
                //File.AppendAllText(@"c:\temp\AoC2024-day07.txt", $"{calculationBeingTested} + {numbersRemaining[0]} = {finalAddition} ({desiredResult}){Environment.NewLine}");
                if (desiredResult == finalAddition)
                {
                    return true;
                }
                var finalMultiplication = totalSoFar * numbersRemaining[0];
                //File.AppendAllText(@"c:\temp\AoC2024-day07.txt", $"{calculationBeingTested} * {numbersRemaining[0]} = {finalMultiplication} ({desiredResult}){Environment.NewLine}");
                if (desiredResult == finalMultiplication)
                {
                    return true;
                }

                return false;
            }

            if (Calculate2(desiredResult, totalSoFar + numbersRemaining[0], numbersRemaining.Skip(1).ToArray(), $"{calculationBeingTested} + {numbersRemaining[0]}") ||
                Calculate2(desiredResult, totalSoFar * numbersRemaining[0], numbersRemaining.Skip(1).ToArray(), $"{calculationBeingTested} * {numbersRemaining[0]}"))
            {
                return true;
            }

            return false;
        }
    }
}
