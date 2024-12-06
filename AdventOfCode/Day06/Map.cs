namespace AdventOfCode.Day06
{
    public class Map
    {
        public Limits? Limits { get; init; }
        public List<Coordinate> Obstructions { get; } = new();
        public Guard? Guard { get; set; }
        public List<Coordinate> Visited { get; } = new();

        public void RegisterLocation(Coordinate coordinate)
        {
            //Register a new object
            var location = new Coordinate { X = coordinate.X, Y = coordinate.Y };
            if (!Visited.Any(v => v.X == location.X && v.Y == location.Y))
            {
                Visited.Add(location);
            }
        }
    }
}
