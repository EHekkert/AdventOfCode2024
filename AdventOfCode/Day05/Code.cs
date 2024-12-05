namespace AdventOfCode.Day05
{
    public class Code
    {
        private Dictionary<int, List<int>> _rulesDict = new();

        public long Part1(string[] lines)
        {
            int sum = 0;

            //Split data into rules and sequences
            var data = SeperateLines(lines);

            //Process rules
            FillRulesDictionary(data.Rules);

            //Process sequences
            foreach (var line in data.Sequences)
            {
                //turn string into list of pages
                List<Page> sequence = GetPages(line);

                //Evaluate sequence
                if (IsValidSequence(sequence))
                {
                    //find index of the middle value
                    var index = (int)Math.Ceiling((float)sequence.Count / (float)2) - 1;
                    sum += sequence[index].Number;
                }
            }

            return sum;
        }

        public long Part2(string[] lines)
        {
            int sum = 0;

            //Split data into rules and sequences
            var data = SeperateLines(lines);

            //Process rules
            FillRulesDictionary(data.Rules);

            //Process sequences
            foreach (var line in data.Sequences)
            {
                //turn string into list of pages
                List<Page> sequence = GetPages(line);

                //Evaluate sequence
                if (!IsValidSequence(sequence))
                {
                    sequence.Sort();

                    //find index of the middle value
                    var index = (int)Math.Ceiling((float)sequence.Count / (float)2) - 1;
                    sum += sequence[index].Number;
                }
            }

            return sum;
        }

        private Boolean IsValidSequence(List<Page> sequence)
        {
            for (int i = 0; i < sequence.Count(); i++)
            {
                //Check each number
                foreach (var pageNumber in sequence[i].PagesThatComeAfter)
                {
                    //Find first occurrence in sequence
                    var index = sequence.FindIndex(x => x.Number == pageNumber);

                    //if earlier in the sequence
                    if (index != -1 && index < i)
                    {
                        //sequence invalid
                        return false;
                    }
                }
            }

            //sequence valid
            return true;
        }

        private (List<string> Rules, List<string> Sequences) SeperateLines(string[] lines)
        {
            bool seperatorLineReached = false;

            List<string> rules = new();
            List<string> sequences = new();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line.Trim()))
                {
                    seperatorLineReached = true;
                    continue;
                }

                if (!seperatorLineReached)
                {
                    rules.Add(line);
                }
                else
                {
                    sequences.Add(line);
                }
            }

            return (rules, sequences);
        }

        private void FillRulesDictionary(List<string> rules)
        {
            foreach (var rule in rules)
            {
                var values = rule.Trim().Split('|');
                var key = Convert.ToInt32(values[0]);
                var value = Convert.ToInt32(values[1]);

                if (!_rulesDict.ContainsKey(key))
                {
                    _rulesDict.Add(key, new List<int> { Convert.ToInt32(value) });
                }
                else
                {
                    _rulesDict[key].Add(Convert.ToInt32(value));
                }
            }
        }

        private List<Page> GetPages(string line)
        {
            List<Page> pages = new();

            var pageNumbers = line.Trim().Split(',').Select(x => int.Parse(x)).ToList();
            foreach (var pageNumber in pageNumbers)
            {
                Page page = new Page { Number = pageNumber };
                if (_rulesDict.ContainsKey(pageNumber))
                {
                    page.PagesThatComeAfter = _rulesDict[pageNumber];
                }

                pages.Add(page);
            }

            return pages;
        }
    }
}
