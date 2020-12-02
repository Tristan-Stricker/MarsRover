using System;
using System.Collections.Generic;
using Xunit;

namespace MarsRovers.Core.Tests
{
    public class RoverSpec
    {
        [Fact]
        public void Should_Throw_When_Start_Point_Is_Out_Of_Bounds()
        {
            // arrange
            var plateau = new Bounds(new Point(0, 0), new Point(5, 5));
            var outOfBounds = new Point(-1, -2);
            var heading = Direction.North;

            // act
            void constuctor() => new Rover(plateau, outOfBounds, heading);

            // assert
            Assert.Throws<InvalidOperationException>(constuctor);
        }

        [Theory]
        [InlineData(Direction.North, Direction.West)]
        [InlineData(Direction.West,  Direction.South)]
        [InlineData(Direction.South, Direction.East)]
        [InlineData(Direction.East,  Direction.North)]
        public void Turning_Left_Should_Change_Direction(Direction start, Direction after)
        {
            // arrange
            var rover = MakeRover(direction: start);

            // act
            rover.TurnLeft();

            // assert
            Assert.Equal(after, rover.Heading);
        }

        [Theory]
        [InlineData(Direction.North, Direction.East)]
        [InlineData(Direction.West, Direction.North)]
        [InlineData(Direction.South, Direction.West)]
        [InlineData(Direction.East, Direction.South)]
        public void Turning_Right_Should_Change_Direction(Direction start, Direction after)
        {
            // arrange
            var rover = MakeRover(direction: start);

            // act
            rover.TurnRight();

            // assert
            Assert.Equal(after, rover.Heading);
        }

        public static IEnumerable<object[]> MoveTestCases => new List<object[]>
        {
            new object[] { Direction.North, Point.Origin(), new Point(0,  1) }, 
            new object[] { Direction.South, Point.Origin(), new Point(0, -1) },
            new object[] { Direction.East,  Point.Origin(), new Point(1,  0) },
            new object[] { Direction.West,  Point.Origin(), new Point(-1, 0) },
        };

        [Theory]
        [MemberData(nameof(MoveTestCases))]
        public void Moving_Rover_Changes_Position(Direction direction, Point start, Point expectedEndPosition)
        {
            // arrange
            var rover = MakeRover(start, direction);

            // act
            rover.Move();

            // assert
            Assert.Equal(expectedEndPosition, rover.Position);
        }

        [Fact]
        public void Moving_Out_Of_The_Boundary_Throws()
        {
            // arrange
            var bounds = new Bounds(Point.Origin(), new Point(1, 1));
            var rover = new Rover(bounds, Point.Origin(), Direction.South);

            // act
            void move() => rover.Move();

            // assert
            Assert.Throws<InvalidOperationException>(move);
        }

        // Convenience methods
        private static Rover MakeRover(Direction direction = Direction.North)
        {
            return MakeRover(Point.Origin(), direction);
        }

        private static Rover MakeRover(Point start, Direction direction = Direction.North)
        {
            var plateau = new Bounds(new Point(int.MinValue, int.MinValue), new Point(int.MaxValue, int.MaxValue));
            return new Rover(plateau, start, direction);
        }
    }
}
