using System.Text.RegularExpressions;

namespace AdventOfCode.Day06
{
    public class Code
    {
        public string _obstructionPattern = "#";
        public string _guardPattern = @"\^";
        private Map? _map;

        public int? Part1(string[] lines)
        {
            ProcessData(lines);

            PatrolMap();

            return _map?.Visited.Count;
        }

        public int Part2(string[] lines)
        {
            ProcessData(lines);

            throw new NotImplementedException();
        }

        private void ProcessData(string[] lines)
        {
            Map map = new Map
            {
                Limits = new Limits
                {
                    MinX = 1,
                    MaxX = lines[0].Length,
                    MinY = 1,
                    MaxY = lines.Length
                }
            };

            for(int i = 0; i < lines.Length; i++)
            {
                map.Obstructions.AddRange(Regex.Matches(lines[i], _obstructionPattern).Select(m => new Coordinate { X = m.Index + 1, Y = i + 1}).ToList());
                var guard = Regex.Matches(lines[i], _guardPattern).Select(g => new Guard(new Coordinate { X = g.Index + 1, Y = i + 1 }, Direction.North)).FirstOrDefault();
                if (guard != null)
                {
                    map.Guard = guard;
                }
            }

            _map = map;
        }

        private void PatrolMap()
        {
            Guard? guard = _map.Guard;
            if (guard != null)
            {
                do
                {
                    //Register location 
                    _map.RegisterLocation(guard.Position);

                    //Can guard move forward
                    if (!PathBlocked())
                    {
                        //Move
                        guard.MoveForward();
                    }
                    else
                    {
                        //Turn
                        guard.ChangeDirection();
                    }

                } while (!IsGuardOffMap()); //Stop when guard leaves map
            }
        }

        /// <summary>
        /// Is the guard within the confines of the map
        /// </summary>
        /// <returns></returns>
        private bool IsGuardOffMap()
        {
            Guard? guard = _map.Guard;
            if (guard == null)
            {
                return true;
            }
            
            return guard.Position.X < _map.Limits.MinX ||
                   guard.Position.X > _map.Limits.MaxX ||
                   guard.Position.Y < _map.Limits.MinY ||
                   guard.Position.Y > _map.Limits.MaxY;
        }

        private bool PathBlocked()
        {
            Guard? guard = _map.Guard;
            if (guard == null)
            {
                throw new Exception("No guard present");
            }

            switch (guard.Direction)
            {
                case Direction.North:
                    return _map.Obstructions.Any(o => o.X == guard.Position.X && o.Y == guard.Position.Y - 1);
                case Direction.East:
                    return _map.Obstructions.Any(o => o.X == guard.Position.X + 1 && o.Y == guard.Position.Y);
                case Direction.South:
                    return _map.Obstructions.Any(o => o.X == guard.Position.X && o.Y == guard.Position.Y + 1);
                case Direction.West:
                    return _map.Obstructions.Any(o => o.X == guard.Position.X - 1 && o.Y == guard.Position.Y);
                default:
                    throw new Exception("Cannot determine if path is blocked");
            }
        }
    }
}
