using AdventOfCode.Tools;

namespace AdventOfCode.Day12
{
    public class Region
    {
        public char? Crop;
        public HashSet<(Point? location, HashSet<Point> fences)> Locations = new();
        //public int Sides = 0;
    }
}
