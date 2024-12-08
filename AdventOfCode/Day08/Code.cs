using AdventOfCode.Tools;

namespace AdventOfCode.Day08
{
    public class Code
    {
        private int _width;
        private int _height;
        HashSet<Point> _antiNodes = new();

        public int Part1(string[] lines)
        {
            HashSet<(char Type, Point Location)> allAntennas = new ();

            _width = lines[0].Length;
            _height = lines.Length;

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    var objectOnMap = lines[y][x];
                    if (objectOnMap != '.')
                    { 
                        allAntennas.Add((objectOnMap, new Point(x, y)));
                    }
                }
            }

            var antennaTypes = allAntennas.Select(x => x.Type).Distinct().ToArray();
            foreach (var antennaType in antennaTypes)
            {
                var antennas = allAntennas.Where(x => x.Type == antennaType).ToArray();
                foreach (var antenna1 in antennas)
                {
                    foreach (var antenna2 in antennas.Except([antenna1]))
                    {
                        var antinode = antenna1.Location.AntiNodeLocation(antenna2.Location);

                        if (!IsOutOfBounds(antinode))
                        {
                            _antiNodes.Add(antinode);
                        }
                    }
                }
            }

            return _antiNodes.Count;
        }

        public int Part2(string[] lines)
        {
            HashSet<(char Type, Point Location)> allAntennas = new();

            _width = lines[0].Length;
            _height = lines.Length;

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    var objectOnMap = lines[y][x];
                    if (objectOnMap != '.')
                    {
                        allAntennas.Add((objectOnMap, new Point(x, y)));
                    }
                }
            }
            
            var antennaTypes = allAntennas.Select(x => x.Type).Distinct().ToArray();
            foreach (var antennaType in antennaTypes)
            {
                var antennas = allAntennas.Where(x => x.Type == antennaType).ToArray();
                foreach (var antenna1 in antennas)
                {
                    foreach (var antenna2 in antennas.Except([antenna1]))
                    {
                        _antiNodes.Add(antenna2.Location);
                        FindResonancePoints(antenna1.Location, antenna2.Location);
                    }
                }
            }

            return _antiNodes.Count;
        }

        private void FindResonancePoints(Point location1, Point location2)
        {
            var path = location1.Path(location2);
            var resonanceHarmonic = location2 + new Point(path.X, path.Y);

            if (!IsOutOfBounds(resonanceHarmonic))
            {
                _antiNodes.Add(resonanceHarmonic);

                FindResonancePoints(location2, resonanceHarmonic);
            }
        }

        private bool IsOutOfBounds(Point antiNodePosition)
        {
            return antiNodePosition.X < 0 || antiNodePosition.X >= _width || antiNodePosition.Y < 0 || antiNodePosition.Y >= _height;
        }
    }
}
