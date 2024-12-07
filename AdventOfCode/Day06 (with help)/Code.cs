namespace AdventOfCode.Day06.WithHelp
{
    public class Code
    {
        private object _lock = new object();
        private int _width;
        private int _height;
        private char[,] _map;
        private Point _startingPoint;

        public int Part1(string[] lines)
        {
            _width = lines[0].Length;
            _height = lines.Length;
            _map ??= new char[_width,_height];

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _map[x, y] = lines[y][x];
                    if (_map[x, y] == '^')
                    {
                        _startingPoint = new Point(x, y);
                    }
                }
            }

            return CountSteps(_startingPoint);
        }

        public int Part2(string[] lines)
        {
            _width = lines[0].Length;
            _height = lines.Length;
            _map ??= new char[_width, _height];

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _map[x, y] = lines[y][x];
                    if (_map[x, y] == '^')
                    {
                        _startingPoint = new Point(x, y);
                    }
                }
            }

            var potentialObstructionPositions = GetPotentialObstructionPositions(_startingPoint).Except([_startingPoint]);

            var obstructionCount = 0;

            Parallel.ForEach(potentialObstructionPositions, newObstruction =>
            {
                //new obstruction cannot be at the guard's starting point
                if ((newObstruction.X != _startingPoint.X || newObstruction.Y != _startingPoint.Y) &&
                    DoesGuardLoop(_startingPoint, newObstruction))
                {
                    lock (_lock)
                    {
                        obstructionCount++;
                    }
                }
            });

            return obstructionCount;
        }

        private int CountSteps(Point startPoint)
        {
            //HashSet cannot contain duplicates which is why there is no check if point has already been visited.
            //When adding to a HashSet it returns a boolean indicating if the item was added.
            HashSet<Point> visited = new();

            var currentDirection = new Point(0, -1); //Direction on X-axis is 0 and on Y-axis is -1 making it point north
            var currentPoint = startPoint;
            while (true)
            {
                visited.Add(currentPoint);
                var nextPosition = currentPoint + currentDirection;
                if (IsOutOfBounds(nextPosition))
                {
                    break;
                }

                //Is there an obstruction in the next position
                if (_map[nextPosition.X, nextPosition.Y] == '#')
                {
                    //Turn right
                    currentDirection = new Point(-currentDirection.Y, currentDirection.X); //X gets the negative value of Y and Y gets the value of X
                    nextPosition = currentPoint;
                }

                if (IsOutOfBounds(nextPosition))
                {
                    break;
                }

                //Move is possible, update position
                currentPoint = nextPosition;
            }

            return visited.Count;
        }

        private HashSet<Point> GetPotentialObstructionPositions(Point startPoint)
        {
            //HashSet cannot contain duplicates which is why there is no check if point has already been visited.
            //When adding to a HashSet it returns a boolean indicating if the item was added.
            HashSet<Point> visited = new();

            var currentDirection = new Point(0, -1); //Direction on X-axis is 0 and on Y-axis is -1 making it point north
            var currentPoint = startPoint;
            while (true)
            {
                visited.Add(currentPoint);
                var nextPosition = currentPoint + currentDirection;
                if (IsOutOfBounds(nextPosition))
                {
                    break;
                }

                //Is there an obstruction in the next position
                if (_map[nextPosition.X, nextPosition.Y] == '#')
                {
                    //Turn right
                    currentDirection = new Point(-currentDirection.Y, currentDirection.X); //X gets the negative value of Y and Y gets the value of X
                    nextPosition = currentPoint;
                }

                if (IsOutOfBounds(nextPosition))
                {
                    break;
                }

                //Move is possible, update position
                currentPoint = nextPosition;
            }

            return visited;
        }

        private bool DoesGuardLoop(Point startPoint, Point newObstruction)
        {
            HashSet<(Point point, Point direction)> visited = new();

            var currentDirection = new Point(0, -1); //Direction on X-axis is 0 and on Y-axis is -1 making it point north
            var currentPoint = startPoint;
            while (true)
            {
                if (visited.Contains((currentPoint, currentDirection)))
                {
                    //Stuck in loop
                    return true;
                }
                visited.Add((currentPoint, currentDirection));
                var nextPosition = currentPoint + currentDirection;
                if (IsOutOfBounds(nextPosition))
                {
                    break;
                }

                //Is there an obstruction in the next position
                if (_map[nextPosition.X, nextPosition.Y] == '#' ||
                    (nextPosition.X == newObstruction.X && nextPosition.Y == newObstruction.Y))
                {
                    //Turn right
                    currentDirection = new Point(-currentDirection.Y, currentDirection.X); //X gets the negative value of Y and Y gets the value of X
                    nextPosition = currentPoint;
                }

                if (IsOutOfBounds(nextPosition))
                {
                    break;
                }

                //Move is possible, update position
                currentPoint = nextPosition;
            }

            return false;
        }

        private bool IsOutOfBounds(Point nextPosition)
        {
            return nextPosition.X < 0 || nextPosition.X >= _width || nextPosition.Y < 0 || nextPosition.Y >= _height;
        }
    }
}
