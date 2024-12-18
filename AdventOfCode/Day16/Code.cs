using AdventOfCode.Tools;
using System.Text;

namespace AdventOfCode.Day16
{
    public class Code
    {
        private int _width;
        private int _height;
        private char[,] _map;
        private Point _endpoint = new Point(-1,-1);
        private long _lowestScore = long.MaxValue;
        private HashSet<(Point location, Point direction)> _lowestScoringRoute = new();

        private HashSet<(Point location, Point direction)> deadEnds = new();

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

            //List<Point> allVisitedLocations = new List<Point>(_allVisitedLocations);
            //allVisitedLocations = allVisitedLocations.OrderBy(o => o.Y).ThenBy(o => o.X).ToList();
            //StringBuilder sb = new StringBuilder();
            //foreach (var visitedLocation in allVisitedLocations)
            //{
            //    sb.AppendLine($"{visitedLocation.X},{visitedLocation.Y}");
            //}

            //File.AppendAllText(@"c:\temp\AoC\VisitedLocations.txt", sb.ToString());

            StringBuilder sb = new();
            foreach (var position in _lowestScoringRoute)
            {
                sb.AppendLine($"{position.location.X},{position.location.Y} - {position.direction.X},{position.direction.Y}");
            }
            File.AppendAllText(@"c:\temp\AoC\Route.txt", sb.ToString());

            return lowestScore;
        }

        public long Part2(string[] lines)
        {
            throw new NotImplementedException();
        }

        private long FindLowestScoreAsync(Point startLocation, Point startDirection)
        {
            HashSet<(Point location, Point direction)> visitedLocations = new();
            long currentScore = 0;

            CheckLocationAsync(startLocation, startDirection, visitedLocations, currentScore);

            return _lowestScore;
        }

        private bool CheckLocationAsync(Point location, Point direction, HashSet<(Point location,Point direction)> locationsVisited, long currentScore)
        {
            if (locationsVisited.Any(l => l.location == location) || deadEnds.Contains((location, direction)))
            {
                //loop detected
                return false;
            }

            if (location == _endpoint)
            {
                UpdateLowestScore(currentScore, locationsVisited);
                
                return true;
            }

            locationsVisited.Add((location, direction));

            //File.AppendAllText(@"c:\temp\AoC\PathsFollowed.txt", $"{location.X},{location.Y}{Environment.NewLine}");

            HashSet<Point> possibleDirections = GetPossibleDirectionsAsync(location, direction);
            if (!possibleDirections.Any())
            {
                return false;
            }
            
            var pathPossible = false;
            foreach (var possibleDirection in possibleDirections)
            {
                if (location == new Point(4, 7) && direction == new Point(0,-1) && possibleDirection == new Point(1, 0))
                {
                    StringBuilder sb = new();
                    foreach (var position in locationsVisited)
                    {
                        sb.AppendLine($"{position.location.X},{position.location.Y} - {position.direction.X},{position.direction.Y}");
                    }
                    File.AppendAllText(@"c:\temp\AoC\visited.txt", sb.ToString());
                }
                HashSet<(Point location, Point direction)> visitedLocations = new HashSet<(Point location, Point direction)>(locationsVisited);

                if (CheckDirectionAsync(location, direction, possibleDirection, visitedLocations, currentScore))
                {
                    pathPossible = true;
                }
                else
                {
                    deadEnds.Add((location + possibleDirection, possibleDirection));
                }
            }

            return pathPossible;
        }

        private bool CheckDirectionAsync(Point location, Point currentDirection, Point directionToCheck, HashSet<(Point location, Point direction)> visitedLocations, long score)
        {
            if (directionToCheck != currentDirection)
            {
                score += 1000;
            }

            score += 1;

            return CheckLocationAsync(location + directionToCheck, directionToCheck, visitedLocations, score);
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

        private void UpdateLowestScore(long score, HashSet<(Point location, Point direction)> visitedLocations)
        {
            if (score < _lowestScore)
            {
                _lowestScore = score;
                _lowestScoringRoute = visitedLocations;
            }
        }
    }
}