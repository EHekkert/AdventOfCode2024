using AdventOfCode.Tools;

namespace AdventOfCode.Day10
{
    public class Code
    {
        private int _width;
        private int _height;
        private char[,] _map;
        private HashSet<Point> _startingPoints = new HashSet<Point>();
        private long _trailheadsFound = 0;

        public long Part1(string[] lines)
        {
            _width = lines[0].Length;
            _height = lines.Length;
            _map ??= new char[_width, _height];

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _map[x, y] = lines[y][x];
                    if (_map[x, y] == '0')
                    {
                        //Register starting points
                        _startingPoints.Add(new Point(x, y));
                    }
                }
            }

            object totalPathsFoundLock = new object ();
            var totalPathsFound = 0;
            Parallel.ForEach(_startingPoints, startingPoint =>
            {
                //Find unique endpoints (9) reachable from this startpoint
                var uniqueEndpointsFound = CheckNextStep(startingPoint);
                lock (totalPathsFoundLock)
                {
                    totalPathsFound += uniqueEndpointsFound.Count;
                }
            });

            return totalPathsFound;
        }

        public long Part2(string[] lines)
        {
            _width = lines[0].Length;
            _height = lines.Length;
            _map ??= new char[_width, _height];

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _map[x, y] = lines[y][x];
                    if (_map[x, y] == '0')
                    {
                        _startingPoints.Add(new Point(x, y));
                    }
                }
            }

            object totalUniquePathsFoundLock = new object();
            var totalUniquePathsFound = 0;
            Parallel.ForEach(_startingPoints, startingPoint =>
            {
                //Find number of unique paths that lead to endpoints (9)
                var uniquePathsFound = CheckNextStep2(startingPoint);
                lock (totalUniquePathsFoundLock)
                {
                    totalUniquePathsFound += uniquePathsFound;
                }
            });

            return totalUniquePathsFound;
        }

        private HashSet<Point> CheckNextStep(Point current)
        {
            object endpointsLock = new object();

            HashSet<Point> uniqueEndPoints = new HashSet<Point>();

            Int16 elevation = (Int16)Char.GetNumericValue(_map[current.X, current.Y]);
            if (elevation == 9)
            {
                uniqueEndPoints.Add(current);
            }
            else
            {
                //Find points that are one higher than the current point
                HashSet<Point> nextSteps = new();
                //Check left
                if (current.X > 0 && (Int16)Char.GetNumericValue(_map[current.X - 1, current.Y]) == elevation + 1)
                {
                    nextSteps.Add(new Point(current.X - 1, current.Y));
                }

                //Check right
                if (current.X < _width - 1 && (Int16)Char.GetNumericValue(_map[current.X + 1, current.Y]) == elevation + 1)
                {
                    nextSteps.Add(new Point(current.X + 1, current.Y));
                }

                //Check up
                if (current.Y > 0 && (Int16)Char.GetNumericValue(_map[current.X, current.Y - 1]) == elevation + 1)
                {
                    nextSteps.Add(new Point(current.X, current.Y - 1));
                }

                //Check down
                if (current.Y < _height - 1 && (Int16)Char.GetNumericValue(_map[current.X, current.Y + 1]) == elevation + 1)
                {
                    nextSteps.Add(new Point(current.X, current.Y + 1));
                }

                Parallel.ForEach(nextSteps, nextStep =>
                {
                    //Continue search for endpoints (9) reachable from this point
                    var foundEndPoints = CheckNextStep(nextStep);
                    lock (endpointsLock)
                    {
                        //Store unique endpoints
                        uniqueEndPoints.UnionWith(foundEndPoints);
                    }
                });
            }

            return uniqueEndPoints;
        }

        private int CheckNextStep2(Point current)
        {
            object pathsToEndpointsLock = new object();
            
            Int16 elevation = (Int16)Char.GetNumericValue(_map[current.X, current.Y]);
            if (elevation == 9)
            {
                return 1;
            }
            else
            {
                int pathsToEndpoints = 0;

                //Find points that are one higher than the current point
                HashSet<Point> nextSteps = new();
                //Check left
                if (current.X > 0 && (Int16)Char.GetNumericValue(_map[current.X - 1, current.Y]) == elevation + 1)
                {
                    nextSteps.Add(new Point(current.X - 1, current.Y));
                }

                //Check right
                if (current.X < _width - 1 && (Int16)Char.GetNumericValue(_map[current.X + 1, current.Y]) == elevation + 1)
                {
                    nextSteps.Add(new Point(current.X + 1, current.Y));
                }

                //Check up
                if (current.Y > 0 && (Int16)Char.GetNumericValue(_map[current.X, current.Y - 1]) == elevation + 1)
                {
                    nextSteps.Add(new Point(current.X, current.Y - 1));
                }

                //Check down
                if (current.Y < _height - 1 && (Int16)Char.GetNumericValue(_map[current.X, current.Y + 1]) == elevation + 1)
                {
                    nextSteps.Add(new Point(current.X, current.Y + 1));
                }

                Parallel.ForEach(nextSteps, nextStep =>
                {
                    //Continue search for paths to endpoints (9) reachable from this point
                    var foundPathsToEndPoints = CheckNextStep2(nextStep);
                    lock (pathsToEndpointsLock)
                    {
                        pathsToEndpoints += foundPathsToEndPoints;
                    }
                });

                return pathsToEndpoints;
            }
        }
    }
}
