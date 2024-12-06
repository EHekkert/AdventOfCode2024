namespace AdventOfCode.Day06
{
    public class Guard
    {
        public Coordinate Position { get; set; }
        public Direction Direction { get; set; }

        public Guard(Coordinate position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }

        public void ChangeDirection()
        {
            switch (Direction)
            {
                case Direction.North:
                    Direction = Direction.East;
                    break;
                case Direction.East:
                    Direction = Direction.South;
                    break;
                case Direction.South:
                    Direction = Direction.West;
                    break;
                case Direction.West:
                    Direction = Direction.North;
                    break;
            }
        }

        public void MoveForward()
        {
            switch (Direction)
            {
                case Direction.North:
                    Position.Y = Position.Y - 1;
                    break;
                case Direction.East:
                    Position.X = Position.X + 1;
                    break;
                case Direction.South:
                    Position.Y = Position.Y + 1;
                    break;
                case Direction.West:
                    Position.X = Position.X - 1;
                    break;
            }
        }
    }
}
