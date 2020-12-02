namespace MarsRovers.Core
{
    public record Bounds
    {
        public Bounds(Point origin, Point extent)
        {
            Origin = origin;
            Extent = extent;
        }

        public bool Contains(Point point)
        {
            return Origin.X <= point.X && Origin.Y <= point.Y &&
                   Extent.X >= point.X && Extent.Y >= point.Y;
        }

        public Point Origin { get; init; }

        public Point Extent { get; init; }
    }
}
