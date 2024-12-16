using AdventOfCode.Tools;

namespace AdventOfCode.Day16
{
    public class Code
    {
        public object _lock2 = new();
        public object _lock = new();

        private int _width;
        private int _height;
        private char[,] _map;
        private Point _endpoint = new Point(-1,-1);
        private long _lowestScore = long.MaxValue;

        //private HashSet<Point> _allVisitedLocations = new();

        public long Part1(string[] lines)
        {
            _width = lines[0].Length;
            _height = lines.Count();
            _map = new char[_width, _height];

            Point startPoint = new Point(-1,-1);

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _map[x, y] = lines[y][x];

                    if (lines[y][x] == 'S')
                    {
                        //Store location
                        startPoint = new Point(x, y);
                    }
                    if (lines[y][x] == 'E')
                    {
                        //Store location
                        _endpoint = new Point(x, y);
                    }
                }
            }

            if (startPoint == new Point(-1,-1))
            {
                throw new Exception("Starting point not found");
            }
            if (_endpoint == new Point(-1, -1))
            {
                throw new Exception("End point not found");
            }

            Point startDirection = new Point(1, 0);
            
            var lowestScore = FindLowestScoreAsync(startPoint, startDirection).Result;

            //List<Point> allVisitedLocations = new List<Point>(_allVisitedLocations);
            //allVisitedLocations = allVisitedLocations.OrderBy(o => o.Y).ThenBy(o => o.X).ToList();
            //StringBuilder sb = new StringBuilder();
            //foreach (var visitedLocation in allVisitedLocations)
            //{
            //    sb.AppendLine($"{visitedLocation.X},{visitedLocation.Y}");
            //}

            //File.AppendAllText(@"c:\temp\AoC\VisitedLocations.txt", sb.ToString());

            return lowestScore;
        }

        public long Part2(string[] lines)
        {
            throw new NotImplementedException();
        }

        private async Task<long> FindLowestScoreAsync(Point startLocation, Point startDirection)
        {
            HashSet<Point> visitedLocations = new();
            long currentScore = 0;

            await CheckLocationAsync(startLocation, startDirection, visitedLocations, currentScore);

            return _lowestScore;
        }

        private async Task CheckLocationAsync(Point location, Point direction, HashSet<Point> locationsVisited, long currentScore)
        {
            HashSet<Point> visitedLocations = new HashSet<Point>(locationsVisited);

            if (currentScore >= _lowestScore || visitedLocations.Contains(location))
            {
                return;
            }

            if (location == _endpoint)
            {
                await UpdateLowestScore(currentScore);
                return;
            }

            visitedLocations.Add(location);

            HashSet<Point> possibleDirections = await GetPossibleDirectionsAsync(location, direction);
            if (!possibleDirections.Any())
            {
                //Deadend
                return;
            }

            foreach (var possibleDirection in possibleDirections)
            {
                await CheckDirectionAsync(location, direction, possibleDirection, visitedLocations, currentScore);
            }
        }

        private async Task CheckDirectionAsync(Point location, Point currentDirection, Point directionToCheck, HashSet<Point> visitedLocations, long score)
        {
            if (directionToCheck != currentDirection)
            {
                score += 1000;
            }

            score += 1;

            await CheckLocationAsync(location + directionToCheck, directionToCheck, visitedLocations, score);
        }

        private async Task<HashSet<Point>> GetPossibleDirectionsAsync(Point location, Point direction)
        {
            char[] validFields = new char[2] { '.', 'E' };

            HashSet<Point> possibleDirections = new();
            if (validFields.Contains(WhatIsOnTheLeft(location, direction)))
            {
                possibleDirections.Add(TurnLeft(direction));
            }
            if (validFields.Contains(WhatIsAhead(location, direction)))
            {
                possibleDirections.Add(direction);
            }
            if (validFields.Contains(WhatIsOnTheRight(location, direction)))
            {
                possibleDirections.Add(TurnRight(direction));
            }

            return possibleDirections;
        }

        private Point TurnLeft(Point direction)
        {
            return new Point(direction.Y, direction.X * -1);
        }
        private Point TurnRight(Point direction)
        {
            return new Point(direction.Y * -1, direction.X);
        }
        private char WhatIsOnTheLeft(Point location, Point direction)
        {
            var locationToLeft = location +TurnLeft(direction);
            return _map[locationToLeft.X,locationToLeft.Y];
        }
        private char WhatIsAhead(Point location, Point direction)
        {
            var locationAhead = location + direction;
            return _map[locationAhead.X, locationAhead.Y];
        }
        private char WhatIsOnTheRight(Point location, Point direction)
        {
            var locationToRight = location + TurnRight(direction);
            return _map[locationToRight.X, locationToRight.Y];
        }

        private async Task UpdateLowestScore(long score)
        {
            //lock (_lock)
            //{
                if (score < _lowestScore)
                {
                    _lowestScore = score;
                }
            //}
        }
    }
}
