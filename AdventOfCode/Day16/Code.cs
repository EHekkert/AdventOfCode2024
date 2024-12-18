using AdventOfCode.Tools;

namespace AdventOfCode.Day16
{
    public class Code
    {
        private int _width;
        private int _height;
        private char[,] _map;
        private Point _endpoint = new Point(-1,-1);
        private long _lowestScore = long.MaxValue;

        private HashSet<(Point location, Point direction, long score)> _visited = new();

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
            
            var lowestScore = FindLowestScoreAsync(startPoint, startDirection);

            return lowestScore;
        }

        public long Part2(string[] lines)
        {
            throw new NotImplementedException();
        }

        private long FindLowestScoreAsync(Point startLocation, Point startDirection)
        {
            long currentScore = 0;

            CheckLocationAsync(startLocation, startDirection, currentScore);

            return _lowestScore;
        }

        private void CheckLocationAsync(Point location, Point direction, long currentScore)
        {
            var visited = _visited.FirstOrDefault(v => v.location == location && v.direction == direction);

            if ((visited.location != new Point(0,0) && visited.score <= currentScore) || currentScore > _lowestScore)
            {
                return;
            }

            if (location == _endpoint)
            {
                UpdateLowestScore(currentScore);
            }

            if (visited.location != new Point(0, 0))
            {
                _visited.Remove(visited);
            }

            _visited.Add((location, direction, currentScore));
            
            HashSet<Point> possibleDirections = GetPossibleDirectionsAsync(location, direction);
            
            foreach (var possibleDirection in possibleDirections)
            {
                CheckDirectionAsync(location, direction, possibleDirection, currentScore);
            }
        }

        private void CheckDirectionAsync(Point location, Point currentDirection, Point directionToCheck, long score)
        {
            if (directionToCheck != currentDirection)
            {
                score += 1000;
            }

            score += 1;

            CheckLocationAsync(location + directionToCheck, directionToCheck, score);
        }

        private HashSet<Point> GetPossibleDirectionsAsync(Point location, Point direction)
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

        private void UpdateLowestScore(long score)
        {
            if (score < _lowestScore)
            {
                _lowestScore = score;
            }
        }
    }
}