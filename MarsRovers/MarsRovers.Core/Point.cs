using System;

namespace MarsRovers.Core
{
    public record Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        protected Point() {}

        public int X { get; init; }
        public int Y { get; init; }

        public static Point Origin() => new Point(0, 0);
    }
}
