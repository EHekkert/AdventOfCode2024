using System.Text.RegularExpressions;

namespace AdventOfCode.Day13
{
    public class Code
    {
        public long Part1(string[] lines)
        {
            long totalTokens = 0;

            string pattern = @"\d+";
            for (int i = 0; i < lines.Count(); i = i+4)
            {
                var matches = Regex.Matches(lines[i], pattern);
                int buttonAX = Convert.ToInt32(matches[0].Value);
                int buttonAY = Convert.ToInt32(matches[1].Value);

                matches = Regex.Matches(lines[i + 1], pattern);
                int buttonBX = Convert.ToInt32(matches[0].Value);
                int buttonBY = Convert.ToInt32(matches[1].Value);

                matches = Regex.Matches(lines[i + 2], pattern);
                long x = Convert.ToInt64(matches[0].Value);
                long y = Convert.ToInt64(matches[1].Value);

                long? tokens = HowManyTokens(x, y, buttonAX, buttonAY, buttonBX, buttonBY);
                if (tokens != null)
                {
                    totalTokens += tokens.Value;
                }
            }

            return totalTokens;
        }

        public long Part2(string[] lines)
        {
            long totalTokens = 0;

            string pattern = @"\d+";
            for (int i = 0; i < lines.Count(); i = i + 4)
            {
                var matches = Regex.Matches(lines[i], pattern);
                int buttonAX = Convert.ToInt32(matches[0].Value);
                int buttonAY = Convert.ToInt32(matches[1].Value);

                matches = Regex.Matches(lines[i + 1], pattern);
                int buttonBX = Convert.ToInt32(matches[0].Value);
                int buttonBY = Convert.ToInt32(matches[1].Value);

                matches = Regex.Matches(lines[i + 2], pattern);
                long x = Convert.ToInt64(matches[0].Value) + 10000000000000;
                long y = Convert.ToInt64(matches[1].Value) + 10000000000000;

                long? tokens = HowManyTokens2(x, y, buttonAX, buttonAY, buttonBX, buttonBY);
                if (tokens != null)
                {
                    totalTokens += tokens.Value;
                }
            }

            return totalTokens;
        }

        public long? HowManyTokens(long x, long y, int buttonAX, int buttonAY, int buttonBX, int buttonBY)
        {
            long? tokens = null;

            for (long i = x / buttonBX; i > 0; i--)
            {
                if ((x - buttonBX * i) % buttonAX == 0)
                {
                    var timesButtonBPushed = i;
                    var timesButtonAPushed = (x - buttonBX * i) / buttonAX;

                    if (buttonAY * timesButtonAPushed + buttonBY * timesButtonBPushed == y)
                    {
                        tokens = timesButtonAPushed * 3 + timesButtonBPushed;
                    }
                }
            }

            return tokens;
        }

        public long? HowManyTokens2(long x, long y, int buttonAX, int buttonAY, int buttonBX, int buttonBY)
        {
            var a = Solve(buttonAX, buttonBX, x);
            var b = Solve(buttonAY, buttonBY, y);

            return 0;
        }

        //public int GetGCD(int a, int b)
        //{
        //    var largest = 0;
        //    var smallest = 0;

        //    if (a < b)
        //    {
        //        largest = a;
        //        smallest = b;
        //    }
        //    else
        //    {
        //        largest = b;
        //        smallest = a;
        //    }

        //    var remainder = 0;
        //    do
        //    {
        //        var x = largest / smallest;
        //        remainder = largest - (smallest * x);

        //        largest = smallest;
        //        smallest = remainder;

        //    } while (remainder != 0);

        //    return largest;
        //}

        //public int GetGCD(int a, int b)
        //{
        //    if (a == 0)
        //        return b;

        //    return GetGCD(b % a, a);
        //}

        public List<(long m, long n)> Solve(long a, long b, long c)
        {
            List<(long m, long n)> solutions = new List<(long m, long n)>();

            long d = GCD(a, b);

            // Check if c is divisible by the GCD of a and b
            if (c % d != 0)
            {
                // No integer solutions exist
                return solutions;
            }

            // Simplify the coefficients
            long a1 = a / d;
            long b1 = b / d;
            long c1 = c / d;

            // Find one particular solution using the Extended Euclidean Algorithm
            (long x0, long y0) = ExtendedEuclideanAlgorithm(a1, b1);

            // Multiply by c1 to get a solution to the original equation
            x0 *= c1;
            y0 *= c1;

            // The general solution is:
            // m = x0 + (b1) * t
            // n = y0 - (a1) * t
            // We need to find all integer values of t for which m > 0 and n > 0

            // Calculate the range of t
            // Since m = x0 + b1 * t > 0 => t > (-x0) / b1
            // Similarly, n = y0 - a1 * t > 0 => t < y0 / a1

            // Compute the lower and upper bounds for t
            double tMin = Math.Ceiling((-1.0 * x0) / b1);
            double tMax = Math.Floor((double)y0 / a1);

            for (long t = (long)tMin; t <= (long)tMax; t++)
            {
                long m = x0 + b1 * t;
                long n = y0 - a1 * t;

                if (m > 0 && n > 0)
                {
                    solutions.Add((m, n));
                }
            }

            return solutions;
        }

        // Function to compute GCD using Euclidean Algorithm
        public long GCD(long a, long b)
        {
            return (b == 0) ? Math.Abs(a) : GCD(b, a % b);
        }

        // Extended Euclidean Algorithm
        // Returns integers x and y such that a * x + b * y = gcd(a, b)
        private (long x, long y) ExtendedEuclideanAlgorithm(long a, long b)
        {
            if (b == 0)
                return (1, 0);

            long x1, y1;
            (x1, y1) = ExtendedEuclideanAlgorithm(b, a % b);

            long x = y1;
            long y = x1 - (a / b) * y1;

            return (x, y);
        }
    }
}