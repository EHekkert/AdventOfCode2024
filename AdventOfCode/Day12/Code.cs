using AdventOfCode.Tools;

namespace AdventOfCode.Day12
{
    public class Code
    {
        private int _width;
        private int _height;
        private char[,] _map;

        private Point _above = new Point(1, 0);
        private Point _right = new Point(0, 1);
        private Point _below = new Point(-1, 0);
        private Point _left = new Point(0, -1);

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
                        SurveyLocation(null, location, false);
                    }
                }
            }

            return CalculateCostByFences();
        }

        public long Part2(string[] lines)
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
                        SurveyLocation(null, location, true);
                    }
                }
            }

            return CalculateCostBySides();
        }

        private void SurveyLocation(Region? region, Point location, bool countSides)
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
                region.Locations.Add((location, CheckFences(location)));
                //region.Sides += CheckNewSides(region, location);

                var locationAbove = location + _above;
                if (!IsOutOfBounds(locationAbove) && !IsSurveyed(locationAbove))
                {
                    SurveyLocation(region, locationAbove, countSides);
                }

                var locationToTheRight = location + _right;
                if (!IsOutOfBounds(locationToTheRight) && !IsSurveyed(locationToTheRight))
                {
                    SurveyLocation(region, locationToTheRight, countSides);
                }

                var locationBelow = location + _below;
                if (!IsOutOfBounds(locationBelow) && !IsSurveyed(locationBelow))
                {
                    SurveyLocation(region, locationBelow, countSides);
                }

                var locationToTheLeft = location + _left;
                if (!IsOutOfBounds(locationToTheLeft) && !IsSurveyed(locationToTheLeft))
                {
                    SurveyLocation(region, locationToTheLeft, countSides);
                }
            }
        }

        private bool IsOutOfBounds(Point nextPosition)
        {
            return nextPosition.X < 0 || nextPosition.X >= _width || nextPosition.Y < 0 || nextPosition.Y >= _height;
        }

        private bool IsSurveyed(Point location)
        {
            return _regions.Any(r => r.Locations.Any(item => item.location.Equals(location)));
        }

        private char GetCropAt(Point location)
        {
            return _map[location.X, location.Y];
        }

        private HashSet<Point> CheckFences(Point location)
        {
            HashSet<Point> fences = new();

            //Fence above
            if (location.Y == 0 ||
                (location.Y > 0 && _map[location.X, location.Y - 1] != _map[location.X, location.Y]))
            {
                fences.Add(_above);
            }

            //Fence right
            if (location.X == _width - 1 ||
                (location.X < _width - 1 && _map[location.X + 1, location.Y] != _map[location.X, location.Y]))
            {
                fences.Add(_right);
            }

            //Fence below
            if (location.Y == _height - 1 ||
                (location.Y < _height - 1 && _map[location.X, location.Y + 1] != _map[location.X, location.Y]))
            {
                fences.Add(_below);
            }

            //Fence left
            if (location.X == 0 ||
                (location.X > 0 && _map[location.X - 1, location.Y] != _map[location.X, location.Y]))
            {
                fences.Add(_left);
            }

            return fences;
        }

        //private int CheckNewSides(Region region, Point location)
        //{
        //    var newSides = 0;

        //    var current = region.Locations.FirstOrDefault(x => x.location == location);
        //    var above = region.Locations.FirstOrDefault(x => x.location == location + _above);
        //    var right = region.Locations.FirstOrDefault(x => x.location == location + _right);
        //    var below = region.Locations.FirstOrDefault(x => x.location == location + _below);
        //    var left = region.Locations.FirstOrDefault(x => x.location == location + _left);

        //    if (current.fences.HasFlag(Fences.Above) && !(left.fences.HasFlag(Fences.Above) || right.fences.HasFlag(Fences.Above)))
        //    {
        //        newSides++;
        //    }

        //    if (current.fences.HasFlag(Fences.Right) && !(above.fences.HasFlag(Fences.Right) || below.fences.HasFlag(Fences.Right)))
        //    {
        //        newSides++;
        //    }

        //    if (current.fences.HasFlag(Fences.Below) && !(left.fences.HasFlag(Fences.Below) || right.fences.HasFlag(Fences.Below)))
        //    {
        //        newSides++;
        //    }

        //    if (region.Locations.Count > 1 && current.fences.HasFlag(Fences.Left) && !(above.fences.HasFlag(Fences.Left) || below.fences.HasFlag(Fences.Left)))
        //    {
        //        newSides++;
        //    }

        //    return newSides;
        //}

        private long CalculateCostByFences()
        {
            long totalCost = 0;
            foreach (var region in _regions)
            {
                var fences = region.Locations.Select(x => x.fences.Count).Sum();
                totalCost += region.Locations.Count * fences;
            }

            return totalCost;
        }

        private long CalculateCostBySides()
        {
            long totalCost = 0;
            foreach (var region in _regions)
            {
                var numberOfSides = CalculateNoOfSides(region);
                var regionCost = region.Locations.Count * numberOfSides;
                totalCost += regionCost;
            }

            return totalCost;
        }

        private int CalculateNoOfSides(Region region)
        {
            File.AppendAllText(@"c:\temp\regions.txt", $"Region {region.Crop}{Environment.NewLine}");

            var sideCount = 0;

            var fencesToCheck = region.Locations.SelectMany(
                    tuple => tuple.fences.Select(fence => (location: tuple.location, fence: fence))
                )
                .ToHashSet();

            //var startPoint = region.Locations.Where(o => o.fences.Contains(_above))
            //    .OrderBy(o => o.location.Value.Y)
            //    .ThenBy(o => o.location.Value.X)
            //    .FirstOrDefault();
            do
            {
                var startPoint = fencesToCheck.Where(o => o.fence == _above)
                    .OrderBy(o => o.location.Value.Y)
                    .ThenBy(o => o.location.Value.X)
                    .FirstOrDefault();

                var currentLocation = region.Locations.First(x => x.location == startPoint.location);
                var direction = _above;
                
                File.AppendAllText(@"c:\temp\regions.txt", $"Start: {currentLocation.location.Value.X},{currentLocation.location.Value.Y}{Environment.NewLine}");
                
                if (currentLocation.fences.Count == 4)
                {
                    //linesToWrite.Add($"{startPoint.location.Value.X},{startPoint.location.Value.Y} {direction.X},{direction.Y}");
                    //linesToWrite.Add($"Sides: {4}");
                    //linesToWrite.Add("");

                    //File.AppendAllLines(@"c:\temp\regions.txt", linesToWrite);
                    foreach (var fence in currentLocation.fences)
                    {
                        sideCount++;
                        fencesToCheck.Remove((currentLocation.location, fence));
                    }

                    goto EndOfPathReached;
                }

                sideCount++;

                do
                {
                    fencesToCheck.Remove((currentLocation.location, direction));

                    //linesToWrite.Add($"{currentLocation.location.Value.X},{currentLocation.location.Value.Y} {direction.X},{direction.Y}");

                    if (currentLocation.fences.Contains(TurnRight(direction)))
                    {
                        do
                        {
                            direction = TurnRight(direction);

                            fencesToCheck.Remove((currentLocation.location, direction));

                            //linesToWrite.Add($"{currentLocation.location.Value.X},{currentLocation.location.Value.Y} {direction.X},{direction.Y}");

                            if (currentLocation.location == startPoint.location && direction == _above)
                            {
                                //linesToWrite.Add($"Sides: {sideCount}");
                                //linesToWrite.Add("");

                                //File.AppendAllLines(@"c:\temp\regions.txt", linesToWrite);

                                goto EndOfPathReached;
                            }

                            sideCount++;
                        } while (currentLocation.fences.Contains(TurnRight(direction)));

                        var locationToCheck = currentLocation.location + direction + TurnLeft(direction);
                        if (region.Locations.FirstOrDefault(x => x.location == (locationToCheck)).fences != null &&
                            region.Locations.FirstOrDefault(x => x.location == (locationToCheck)).fences.Contains(TurnLeft(direction)))
                        {
                            direction = TurnLeft(direction);
                            currentLocation = region.Locations.First(x => x.location == (locationToCheck));
                            sideCount++;
                        }
                        else
                        {
                            currentLocation = region.Locations.First(x => x.location == currentLocation.location + direction);
                        }
                    }
                    else
                    {
                        var locationToCheck = currentLocation.location + direction + TurnLeft(direction);
                        if (region.Locations.FirstOrDefault(x => x.location == (locationToCheck)).fences != null &&
                            region.Locations.FirstOrDefault(x => x.location == (locationToCheck)).fences.Contains(TurnLeft(direction)))
                        {
                            direction = TurnLeft(direction);
                            currentLocation = region.Locations.First(x => x.location == (locationToCheck));
                            if (currentLocation.location == startPoint.location && direction == _above)
                            {
                                goto EndOfPathReached;
                            }
                            sideCount++;
                        }
                        else
                        {
                            currentLocation = region.Locations.First(x => x.location == (currentLocation.location + direction));
                        }
                    }
                } while (!(currentLocation.location == startPoint.location && TurnRight(direction) == _above));
                
                fencesToCheck.Remove((currentLocation.location, direction));

            EndOfPathReached:
                    int blank = 0;
            } while (fencesToCheck.Any());

            File.AppendAllText(@"c:\temp\regions.txt", $"Sides: {sideCount}{Environment.NewLine}{Environment.NewLine}");

            return sideCount;
        }

        private Point TurnLeft(Point direction)
        {
            return new Point(direction.Y, -direction.X);
        }

        private Point TurnRight(Point direction)
        {
            return new Point(-direction.Y, direction.X);
        }
    }
}
