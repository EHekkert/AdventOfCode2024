using System.Text.RegularExpressions;

namespace AdventOfCode.Day06
{
    public class Code
    {
        public string _obstructionPattern = "#";
        public string _guardPattern = @"\^";

        public int? Part1(string[] lines)
        {
            var map = ProcessData(lines);

            PatrolMap(map);

            return map?.Visited.Count;
        }

        public int Part2(string[] lines)
        {
            var loopCount = 0;
            var initialMap = ProcessData(lines);

            PatrolMap(initialMap);

            for (int i = 1; i < initialMap.Visited.Count; i++)
            {
                var temporaryObstruction = initialMap.Visited[i];
                var map = ProcessData(lines, temporaryObstruction);

                try
                {
                    PatrolMap(map);
                }
                catch (LoopException)
                {
                    loopCount++;
                }
            }

            return loopCount;
        }

        private Map ProcessData(string[] lines, Coordinate temporaryObstruction = null)
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

            map.SetTemporaryObstruction(temporaryObstruction);

            for(int i = 0; i < lines.Length; i++)
            {
                map.AddObstructions(Regex.Matches(lines[i], _obstructionPattern).Select(m => new Coordinate { X = m.Index + 1, Y = i + 1}).ToList());
                var guard = Regex.Matches(lines[i], _guardPattern).Select(g => new Guard(new Coordinate { X = g.Index + 1, Y = i + 1 }, Direction.North)).FirstOrDefault();
                if (guard != null)
                {
                    map.Guard = guard;
                }
            }

            return map;
        }

        private void PatrolMap(Map map)
        {
            Guard? guard = map.Guard;
            if (guard != null)
            {
                do
                {
                    //Register location 
                    map.RegisterLocation(guard.Position);

                    //Can guard move forward
                    if (!PathBlocked(map))
                    {
                        //Move
                        guard.MoveForward();
                    }
                    else
                    {
                        //Turn
                        guard.ChangeDirection();
                    }

                } while (!IsGuardOffMap(map)); //Stop when guard leaves map
            }
        }

        /// <summary>
        /// Is the guard within the confines of the map
        /// </summary>
        /// <returns></returns>
        private bool IsGuardOffMap(Map map)
        {
            Guard? guard = map.Guard;
            if (guard == null)
            {
                return true;
            }
            
            return guard.Position.X < map.Limits.MinX ||
                   guard.Position.X > map.Limits.MaxX ||
                   guard.Position.Y < map.Limits.MinY ||
                   guard.Position.Y > map.Limits.MaxY;
        }

        private bool PathBlocked(Map map)
        {
            Guard? guard = map.Guard;
            if (guard == null)
            {
                throw new Exception("No guard present");
            }

            switch (guard.Direction)
            {
                case Direction.North:
                    return map.Obstructions.Any(o => o.X == guard.Position.X && o.Y == guard.Position.Y - 1);
                case Direction.East:
                    return map.Obstructions.Any(o => o.X == guard.Position.X + 1 && o.Y == guard.Position.Y);
                case Direction.South:
                    return map.Obstructions.Any(o => o.X == guard.Position.X && o.Y == guard.Position.Y + 1);
                case Direction.West:
                    return map.Obstructions.Any(o => o.X == guard.Position.X - 1 && o.Y == guard.Position.Y);
                default:
                    throw new Exception("Cannot determine if path is blocked");
            }
        }
    }
}
