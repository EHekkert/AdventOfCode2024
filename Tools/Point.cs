namespace AdventOfCode.Tools
{
    public record struct Point(int X, int Y)
    {
        public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
        public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);
        public static Point operator *(Point a, int multiple) => new Point(a.X * multiple, a.Y * multiple);

        public static implicit operator Point((int X, int Y) tuple) => new Point(tuple.X, tuple.Y);

        public Point Normalize() => new Point(X != 0 ? X / Math.Abs(X) : 0, Y != 0? X / Math.Abs(Y) : 0);
        public int ManhattanDistance(Point b) => Math.Abs(X - b.X) + Math.Abs(Y - b.Y);
        public Point AntiNodeLocation(Point b) => new Point(X + ((b.X - X) * 2), Y + ((b.Y - Y) * 2));
        public (int X, int Y) Path(Point b) => (b.X - X, b.Y - Y);

    }
}