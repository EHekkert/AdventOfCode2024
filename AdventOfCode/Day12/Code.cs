using AdventOfCode.Tools;

namespace AdventOfCode.Day12
{
    public class Code
    {
        private int _width;
        private int _height;
        private char[,] _map;

        private Point _above = new Point(0, -1);
        private Point _right = new Point(1, 0);
        private Point _below = new Point(0, 1);
        private Point _left = new Point(-1, 0);

        private HashSet<Region> _regions = new();

        public long Part1(string[] lines)
        {
            _width = lines[0].Length;
            _height = lines.Length;
            _map = new char[_width, _height];

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _map[x, y] = lines[y][x];
                }
            }

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    var location = new Point(x, y);
                    if (!IsSurveyed(location))
                    {
                        SurveyLocation(null, location);
                    }
                }
            }

            return CalculateCost();
        }

        public int Part2(string[] lines)
        {
            throw new NotImplementedException();
        }

        private void SurveyLocation(Region? region, Point location)
        {
            if (region == null)
            {
                region = new Region
                {
                    Crop = GetCropAt(location)
                };

                _regions.Add(region);
            }

            if (region.Crop == GetCropAt(location))
            {
                region.Locations.Add(location);
                region.Fences += CountFences(location);

                var locationAbove = location + _above;
                if (!IsOutOfBounds(locationAbove) && !IsSurveyed(locationAbove))
                {
                    SurveyLocation(region, locationAbove);
                }

                var locationToTheRight = location + _right;
                if (!IsOutOfBounds(locationToTheRight) && !IsSurveyed(locationToTheRight))
                {
                    SurveyLocation(region, locationToTheRight);
                }

                var locationBelow = location + _below;
                if (!IsOutOfBounds(locationBelow) && !IsSurveyed(locationBelow))
                {
                    SurveyLocation(region, locationBelow);
                }

                var locationToTheLeft = location + _left;
                if (!IsOutOfBounds(locationToTheLeft) && !IsSurveyed(locationToTheLeft))
                {
                    SurveyLocation(region, locationToTheLeft);
                }
            }
        }

        private bool IsOutOfBounds(Point nextPosition)
        {
            return nextPosition.X < 0 || nextPosition.X >= _width || nextPosition.Y < 0 || nextPosition.Y >= _height;
        }

        private bool IsSurveyed(Point location)
        {
            return _regions.Any(r => r.Locations.Contains(location));
        }

        private char GetCropAt(Point location)
        {
            return _map[location.X, location.Y];
        }

        private int CountFences(Point location)
        {
            var fenceCount = 0;

            //Fence above
            if (location.Y == 0 ||
                (location.Y > 0 && _map[location.X, location.Y - 1] != _map[location.X, location.Y]))
            {
                fenceCount++;
            }

            //Fence below
            if (location.Y == _height - 1 ||
                (location.Y < _height - 1 && _map[location.X, location.Y + 1] != _map[location.X, location.Y]))
            {
                fenceCount++;
            }

            //Fence left
            if (location.X == 0 ||
                (location.X > 0 && _map[location.X - 1, location.Y] != _map[location.X, location.Y]))
            {
                fenceCount++;
            }

            //Fence right
            if (location.X == _width - 1 ||
                (location.X < _width - 1 && _map[location.X + 1, location.Y] != _map[location.X, location.Y]))
            {
                fenceCount++;
            }

            return fenceCount;
        }

        private long CalculateCost()
        {
            long totalCost = 0;
            foreach (var region in _regions)
            {
                totalCost += region.Locations.Count * region.Fences;
            }

            return totalCost;
        }
    }
}
