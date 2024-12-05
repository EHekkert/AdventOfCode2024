namespace AdventOfCode.Day02
{
    public static class Code
    {
        public static int Part1(string[] lines)
        {
            int safeReports = 0;

            foreach (var report in lines)
            {
                var levels = report.Split(' ').Select(int.Parse).ToList();

                var safeReport = EvaluateLevels(levels);
                if (safeReport)
                {
                    safeReports++;
                }
            }

            return safeReports;
        }

        public static int Part2(string[] lines)
        {
            int safeReports = 0;

            foreach (var report in lines)
            {
                var levels = report.Split(' ').Select(int.Parse).ToList();

                var safeReport = EvaluateLevels(levels);
                if (safeReport)
                {
                    safeReports++;
                }
                else
                {
                    for (int i = 0; i < levels.Count; i++)
                    {
                        var levelsSubset = levels.Where((number, index) => index != i).ToList();
                        safeReport = EvaluateLevels(levelsSubset);
                        if (safeReport)
                        {
                            safeReports++;
                            break;
                        }
                    }
                }
            }

            return safeReports;
        }

        private static bool EvaluateLevels(List<int> levels)
        {
            int? direction = null; //-1 = down and +1 = up;

            for (int i = 0; i < levels.Count - 1; i++)
            {
                if (i == 0)
                {
                    direction = GetDirection(levels[i], levels[i + 1]);
                }

                if (!ValidDelta(levels[i], levels[i + 1]) || (i > 0 && GetDirection(levels[i], levels[i + 1]) != direction))
                {
                    //Unsafe
                    return false;
                }
            }

            return true;
        }

        private static bool ValidDelta(int l1, int l2)
        {
            var delta = l2 - l1;
            return Math.Abs(delta) > 0 && Math.Abs(delta) <= 3;
        }

        private static int GetDirection(int l1, int l2)
        {
            var delta = l2 - l1;
            return delta > 0 ? 1 : (delta < 0 ? -1 : 0);
        }
    }
}
