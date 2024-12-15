using AdventOfCode.Tools;
using System.Text;

namespace AdventOfCode.Day15
{
    public class Code
    {
        private int _width;
        private int _height;
        private char[,] _map;
        private Point _robot;

        public long Part1(string line)
        {
            var lines = line.Split($"{Environment.NewLine}{Environment.NewLine}");
            var mapLines = lines[0].Split(Environment.NewLine);
            var movementArray = lines[1].Replace(Environment.NewLine, "").ToCharArray();

            _width = mapLines[0].Length;
            _height = mapLines.Count();
            _map = new char[_width, _height];

            Point[] movements = new Point[movementArray.Length];
            for (int i = 0; i < movementArray.Length; i++)
            {
                switch(movementArray[i])
                {
                    case '^':
                        movements[i] = new Point(0, -1);
                        break;
                    case '>':
                        movements[i] = new Point(1, 0);
                        break;
                    case 'v':
                        movements[i] = new Point(0, 1);
                        break;
                    case '<':
                        movements[i] = new Point(-1, 0);
                        break;
                    default:
                        throw new Exception("Could not translate movement");
                }
            }

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _map[x, y] = mapLines[y][x];

                    if (mapLines[y][x] == '@')
                    {
                        //Store location
                        _robot = new Point(x, y);
                    }
                }
            }

            MoveRobot(movements);

            return CalculateGPSCoordinatesSum('O');
        }

        public long Part2(string line)
        {
            var lines = line.Split($"{Environment.NewLine}{Environment.NewLine}");
            var mapLines = lines[0].Split(Environment.NewLine);
            var movementArray = lines[1].Replace(Environment.NewLine, "").ToCharArray();

            _width = mapLines[0].Length * 2;
            _height = mapLines.Count();
            _map = new char[_width, _height];

            Point[] movements = new Point[movementArray.Length];
            for (int i = 0; i < movementArray.Length; i++)
            {
                switch (movementArray[i])
                {
                    case '^':
                        movements[i] = new Point(0, -1);
                        break;
                    case '>':
                        movements[i] = new Point(1, 0);
                        break;
                    case 'v':
                        movements[i] = new Point(0, 1);
                        break;
                    case '<':
                        movements[i] = new Point(-1, 0);
                        break;
                    default:
                        throw new Exception("Could not translate movement");
                }
            }

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width/2; x++)
                {
                    if (mapLines[y][x] == 'O')
                    {
                        _map[x * 2, y] = '[';
                        _map[x * 2 + 1, y] = ']';
                    }
                    else if (mapLines[y][x] == '@')
                    {
                        //Store location
                        _robot = new Point(x*2, y);

                        _map[x * 2, y] = '@';
                        _map[x*2+1, y] = '.';
                    }
                    else
                    {
                        _map[x * 2, y] = mapLines[y][x];
                        _map[x * 2 + 1, y] = mapLines[y][x];
                    }
                }
            }

            MoveRobot2(movements);
            
            return CalculateGPSCoordinatesSum('[');
        }

        private void MoveRobot(Point[] movements)
        {
#if DEBUG
            int movementCounter = 0;
#endif

            foreach (var movement in movements)
            {
                List<Point> locationsToUpdate = new();

                var locationToCheck = _robot + movement;
                var atLocation = _map[locationToCheck.X,locationToCheck.Y];
                
                locationsToUpdate.Add(_robot);
                locationsToUpdate.Add(locationToCheck);

                while (atLocation != '.' &&  atLocation != '#')
                {
                    locationToCheck += movement;
                    atLocation = _map[locationToCheck.X, locationToCheck.Y];

                    locationsToUpdate.Add(locationToCheck);
                }

                if (atLocation == '.')
                {
                    //Move everything from robot position to the empty position over one place
                    for (int i = locationsToUpdate.Count - 1; i > 0; i--)
                    {
                        //_map[locationsToUpdate[i].X, locationsToUpdate[i].Y] = _map[locationsToUpdate[i-1].X, locationsToUpdate[i-1].Y];
                        var updateWith = new Point(locationsToUpdate[i].X + movement.X * -1, locationsToUpdate[i].Y + movement.Y * -1);
                        _map[locationsToUpdate[i].X, locationsToUpdate[i].Y] = _map[updateWith.X, updateWith.Y];
                    }

                    //Fill empty location with empty field representation
                    _map[locationsToUpdate[0].X, locationsToUpdate[0].Y] = '.';

                    //Update robot location
                    _robot += movement;
                }

#if DEBUG
                movementCounter++;
                DumpMap(movementCounter);
#endif
            }
        }

        private void MoveRobot2(Point[] movements)
        {
            //loop through all movements
            foreach (var movement in movements)
            {
                List<Point> locationsToUpdate = new();

                //Check the next location the robot want to move to
                var locationToCheck = _robot + movement;
                locationsToUpdate.Add(_robot);

                //Horizontally
                if (movement == new Point(-1, 0) || movement == new Point(1, 0))
                {
                    //Check what is at the location the robot wants to go to
                    var atLocation = _map[locationToCheck.X, locationToCheck.Y];

                    locationsToUpdate.Add(locationToCheck);

                    //if the next location isn't empty or a wall
                    while (atLocation != '.' && atLocation != '#')
                    {
                        //check the next location
                        locationToCheck += movement;
                        atLocation = _map[locationToCheck.X, locationToCheck.Y];

                        locationsToUpdate.Add(locationToCheck);
                    }

                    //if an empty spot was found and the robot can move itself and any boxes
                    if (atLocation == '.')
                    {
                        //Move everything one position in the direction of the movement
                        for (int i = locationsToUpdate.Count - 1; i > 0; i--)
                        {
                            //swap content of old en new location
                            var backup = _map[locationsToUpdate[i].X, locationsToUpdate[i].Y];
                            _map[locationsToUpdate[i].X, locationsToUpdate[i].Y] = _map[locationsToUpdate[i - 1].X, locationsToUpdate[i - 1].Y];
                            _map[locationsToUpdate[i - 1].X, locationsToUpdate[i - 1].Y] = backup;
                        }
                        
                        //Update robot location
                        _robot += movement;
                    }
                }
                else
                {
                    //Check if robot can move
                    var check = CheckCanMove(locationToCheck, movement);
                    if (check.CanMove)
                    {
                        locationsToUpdate.AddRange(check.LocationsToUpdate);

                        //remove any duplicate locations
                        locationsToUpdate = locationsToUpdate.Distinct().ToList();

                        //Moving up, sort Y low to high
                        if (movement == new Point(0, -1))
                        {
                            locationsToUpdate = locationsToUpdate.OrderBy(o => o.Y).ToList();
                        }
                        //Moving down, sort Y high to low
                        else
                        {
                            locationsToUpdate = locationsToUpdate.OrderByDescending(o => o.Y).ToList();
                        }

                        for (int i = 0; i < locationsToUpdate.Count - 1; i++)
                        {
                            //swap content of old en new location, but only if both are in the update list.
                            var updateWith = new Point(locationsToUpdate[i].X + movement.X * -1, locationsToUpdate[i].Y + movement.Y * -1);
                            if (locationsToUpdate.Contains(updateWith))
                            {
                                var backup = _map[locationsToUpdate[i].X, locationsToUpdate[i].Y];
                                _map[locationsToUpdate[i].X, locationsToUpdate[i].Y] = _map[updateWith.X, updateWith.Y];
                                _map[updateWith.X, updateWith.Y] = backup;
                            }
                        }
                        
                        //Update robot location
                        _robot += movement;
                    }
                }
            }
        }

        private (bool CanMove, List<Point>? LocationsToUpdate) CheckCanMove(Point locationToCheck, Point movement)
        {
            Point left = new Point(-1, 0);
            Point right = new Point(1, 0);

            var canMove = true;
            var locationsToUpdate = new List<Point>();

            char atLocation = _map[locationToCheck.X, locationToCheck.Y];

            if (atLocation == '.')
            {
                locationsToUpdate.Add(locationToCheck);
                return (true, locationsToUpdate);
            }
            else if (atLocation == '#')
            {
                return (false, null);
            }
            else if (atLocation == ']')
            {
                locationsToUpdate.Add(locationToCheck);
                locationsToUpdate.Add(locationToCheck + left);

                var check1 = CheckCanMove(locationToCheck + movement, movement);
                if (check1.CanMove == false)
                {
                    return (false, null);
                }
                var check2 = CheckCanMove(locationToCheck+ left + movement, movement);
                if (check2.CanMove == false)
                {
                    return (false, null);
                }

                locationsToUpdate.AddRange(check1.LocationsToUpdate);
                locationsToUpdate.AddRange(check2.LocationsToUpdate);
            }
            else if (atLocation == '[')
            {
                locationsToUpdate.Add(locationToCheck);
                locationsToUpdate.Add(locationToCheck + right);

                var check1 = CheckCanMove(locationToCheck + movement, movement);
                if (check1.CanMove == false)
                {
                    return (false, null);
                }
                var check2 = CheckCanMove(locationToCheck + right + movement, movement);
                if (check2.CanMove == false)
                {
                    return (false, null);
                }

                locationsToUpdate.AddRange(check1.LocationsToUpdate);
                locationsToUpdate.AddRange(check2.LocationsToUpdate);
            }

            return (true, locationsToUpdate);
        }

        private long CalculateGPSCoordinatesSum(char symbolToLookFor)
        {
            //Did not come up with this Linq query. Asked AI assistant how to get all coordinates from a char[,] where value is O. 
            var cratePositions = Enumerable.Range(0, _map.GetLength(0))
                .SelectMany(i => Enumerable.Range(0, _map.GetLength(1)), (i, j) => new Point(i, j))
                .Where(pos => _map[pos.X, pos.Y] == symbolToLookFor)
                .ToList();

            long sum = 0;
            foreach (var cratePosition in cratePositions)
            {
                sum += 100 * cratePosition.Y + cratePosition.X;
            }

            return sum;
        }

        private void DumpMap(int movementCounter)
        {
            StringBuilder sb = new StringBuilder();

            var file = @$"c:\temp\AoC\{movementCounter}.txt";
            for (int y = 0; y < _height; y++)
            {
                char[] line = new char[_width];
                for (int x = 0; x < _width; x++)
                {
                    line[x] = _map[x, y];
                }

                sb.AppendLine(new string(line));
            }

            File.AppendAllText(file, sb.ToString());
        }
    }
}