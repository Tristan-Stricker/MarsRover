using System;

namespace MarsRovers.Core
{
    public class Rover
    {
        private readonly Bounds bounds;

        public Point Position { get; private set; }

        public Direction Heading { get; private set; }

        public Rover(Bounds bounds, Point startingPoint, Direction heading)
        {
            this.bounds = bounds;
            this.Position = startingPoint;
            this.Heading = heading;
            EnsureValidState();
        }

        public void TurnLeft()
        {
            Direction newDirection = Heading switch
            {
                Direction.North => Direction.West,
                Direction.East => Direction.North,
                Direction.South => Direction.East,
                Direction.West => Direction.South,
                _ => throw new NotImplementedException(),
            };

            Heading = newDirection;
        }

        public void TurnRight()
        {
            Direction newDirection = Heading switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => throw new NotImplementedException(),
            };

            Heading = newDirection;
        }

        public void Move()
        {
            var currentX = Position.X;
            var currentY = Position.Y;

            Point newPosition = Heading switch
            {
                Direction.North => Position with { Y = currentY + 1},
                Direction.East  => Position with { X = currentX + 1 },
                Direction.South => Position with { Y = currentY - 1 },
                Direction.West  => Position with { X = currentX - 1 },
                _ => throw new NotImplementedException(),
            };

            Position = newPosition;
            EnsureValidState();
        }

        private void EnsureValidState()
        {
            if (!bounds.Contains(Position))
            {
                throw new InvalidOperationException("Position is not within exploration boundary");
            }
        }
    }
}
