namespace AdventOfCode.Day06
{
    public class Map
    {
        private List<Coordinate> _permanentObstructions = new List<Coordinate>();
        private Coordinate? _temporaryObstruction;

        public Limits? Limits { get; init; }

        public List<Coordinate> Obstructions
        {
            get
            {
                if (_temporaryObstruction == null)
                {
                    return _permanentObstructions;
                }

                return _permanentObstructions.Union(new List<Coordinate> { _temporaryObstruction }).ToList();
            }
        }

        public Guard? Guard { get; set; }
        public List<Coordinate> Visited { get; } = new();
        public List<Coordinate> Path { get; } = new();

        public void RegisterLocation(Coordinate coordinate)
        {
            //Register a new object
            var currentLocation = new Coordinate { X = coordinate.X, Y = coordinate.Y };

            //Did the location change
            if (!Path.Any() || !currentLocation.Equals(Path.Last()))
            {
                var index = Path.FindIndex(p => p.X == currentLocation.X && p.Y == currentLocation.Y);
                while (index > 0)
                {
                    var locationbefore = Path[index - 1];
                    if (locationbefore.Equals(Path.Last()))
                    {
                        throw new LoopException();
                    }

                    index = Path.FindIndex(index + 1, p => p.X == currentLocation.X && p.Y == currentLocation.Y);
                }

                if (!Visited.Any(v => v.X == currentLocation.X && v.Y == currentLocation.Y))
                {
                    Visited.Add(currentLocation);
                }

                Path.Add(currentLocation);
            }
        }

        public void AddObstructions(List<Coordinate> obstructions)
        {
            _permanentObstructions.AddRange(obstructions);
        }

        public void SetTemporaryObstruction(Coordinate location)
        {
            _temporaryObstruction = location;
        }
    }
}
