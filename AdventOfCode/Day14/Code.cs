using AdventOfCode.Tools;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day14
{
    public class Code
    {
        private HashSet<Robot> _robots = new();
        private int _roomWidth = 101;
        private int _roomHeight = 103;

        public int Part1(string[] lines, int roomWidth, int roomHeight, int seconds)
        {
            _robots = new();
            _roomWidth = roomWidth;
            _roomHeight = roomHeight;

            string pattern = @"\-*\d+";

            for (int i = 0; i < lines.Count(); i++)
            {
                var matches = Regex.Matches(lines[i], pattern);
                _robots.Add(new Robot
                {
                    Id = i,
                    Position = new Point(Convert.ToInt32(matches[0].Value), Convert.ToInt32(matches[1].Value)),
                    Velocity = new Point(Convert.ToInt32(matches[2].Value), Convert.ToInt32(matches[3].Value)),
                });
            }

            MoveRobots(seconds);

            return CalculateSafetyFactor();
        }

        public void Part2(string[] lines, int roomWidth, int roomHeight, int seconds)
        {
            _robots = new();
            _roomWidth = roomWidth;
            _roomHeight = roomHeight;

            string pattern = @"\-*\d+";

            for (int i = 0; i < lines.Count(); i++)
            {
                var matches = Regex.Matches(lines[i], pattern);
                _robots.Add(new Robot
                {
                    Id = i,
                    Position = new Point(Convert.ToInt32(matches[0].Value), Convert.ToInt32(matches[1].Value)),
                    Velocity = new Point(Convert.ToInt32(matches[2].Value), Convert.ToInt32(matches[3].Value)),
                });
            }

            MoveRobots(seconds);

            DumpGrid(seconds);
        }

        private void MoveRobots(int seconds)
        {
            foreach (var robot in _robots)
            {
                Point distanceMoved = robot.Velocity * seconds;
                int quotientX = Math.Abs(distanceMoved.X / _roomWidth);
                int quotientY = Math.Abs(distanceMoved.Y / _roomHeight);
                var relativeMovementX = Math.Sign(distanceMoved.X) * (Math.Abs(distanceMoved.X) - (_roomWidth * quotientX));
                var relativeMovementY = Math.Sign(distanceMoved.Y) * (Math.Abs(distanceMoved.Y) - (_roomHeight * quotientY));

                var positionX = robot.Position.X + relativeMovementX;
                var positionY = robot.Position.Y + relativeMovementY;

                positionX = positionX < 0 ? positionX + _roomWidth : positionX;
                positionX = positionX >= _roomWidth ? positionX - _roomWidth : positionX;
                positionY = positionY < 0 ? positionY + _roomHeight : positionY;
                positionY = positionY >= _roomHeight ? positionY - _roomHeight : positionY;

                robot.Position = new Point(positionX, positionY);
            }
        }

        private int CalculateSafetyFactor()
        {
            var quadrantWidth = _roomWidth / 2;
            var quadrantHeight = _roomHeight / 2;

            var robotsInQuadrant1 = RobotsInGrid(0, 0, quadrantWidth, quadrantHeight);
            var robotsInQuadrant2 = RobotsInGrid(quadrantWidth + 1, 0, quadrantWidth, quadrantHeight);
            var robotsInQuadrant3 = RobotsInGrid(0, quadrantHeight + 1, quadrantWidth, quadrantHeight);
            var robotsInQuadrant4 = RobotsInGrid(quadrantWidth + 1, quadrantHeight + 1, quadrantWidth, quadrantHeight);

            return robotsInQuadrant1 * robotsInQuadrant2 * robotsInQuadrant3 * robotsInQuadrant4;
        }

        private int RobotsInGrid(int topLeftX, int topLeftY, int width, int height)
        {
            var robotsInQuadrant = 0;
            for (int y = topLeftY; y < topLeftY + height; y++)
            {
                for (int x = topLeftX; x < topLeftX + width; x++)
                {
                    var robotsAtPosition = _robots.Where(r => r.Position == new Point(x, y)).Count();
                    if (robotsAtPosition > 0)
                    {
                        robotsInQuadrant += robotsAtPosition;
                    }
                }
            }

            return robotsInQuadrant;
        }

        private void DumpGrid(int seconds)
        {
            StringBuilder sb = new StringBuilder();

            var file = @$"c:\temp\AoC\103\{seconds}.txt";
            for (int y = 0; y < _roomHeight; y++)
            {
                char[] line = new char[_roomWidth];
                for (int x = 0; x < _roomWidth; x++)
                {
                    line[x] = _robots.Any(r => r.Position == new Point(x, y)) ? 'X' : '.';
                }

                sb.AppendLine(new string(line));
            }

            File.AppendAllText(file, sb.ToString());
        }
    }
}
