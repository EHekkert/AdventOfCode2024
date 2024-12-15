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

                        ////Do not show robot on map
                        //_map[x, y] = '.';
                    }
                }
            }

            MoveRobot(movements);

            return CalculateGPSCoordinatesSum();
        }

        public long Part2(string line)
        {
            string cleanedLine = line.Replace(Environment.NewLine, "");

            throw new NotImplementedException();
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
                        _map[locationsToUpdate[i].X, locationsToUpdate[i].Y] = _map[locationsToUpdate[i-1].X, locationsToUpdate[i-1].Y];
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

        private long CalculateGPSCoordinatesSum()
        {
            //Did not come up with this Linq query. Asked AI assistant how to get all coordinates from a char[,] where value is O. 
            var cratePositions = Enumerable.Range(0, _map.GetLength(0))
                .SelectMany(i => Enumerable.Range(0, _map.GetLength(1)), (i, j) => new Point(i, j))
                .Where(pos => _map[pos.X, pos.Y] == 'O')
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
