namespace AdventOfCode.Day05
{
    public class Page : IComparable<Page>
    {
        public int Number { get; set; }
        public List<int> PagesThatComeAfter { get; set; } = new List<int>();

        public int CompareTo(Page? other)
        {
            if (this.PagesThatComeAfter.Contains(other.Number))
            {
                return -1;
            }
            else if (other.PagesThatComeAfter.Contains(this.Number))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
