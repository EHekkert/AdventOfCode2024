using AdventOfCode.Tools;

namespace AdventOfCode.Day12
{
    public class Region
    {
        public char? Crop;
        public HashSet<Point> Locations = new();
        public int Fences = 0;
    }
}
