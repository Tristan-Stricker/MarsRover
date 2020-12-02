using MarsRovers.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarsRovers.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("Input.txt");
            var boundsLine = input[0];
            var xyBounds = boundsLine.Split();
            if(xyBounds.Length != 2)
            {
                Console.WriteLine("Invalid boundary");
                return;
            }              
            
            var bounds = new Bounds(Point.Origin(), new Point(int.Parse(xyBounds[0]), int.Parse(xyBounds[1])));

            var roversWithCommands = new List<(Rover rover, string commands)>();
            
            for(var i = 1; i < input.Length; i+=2)
            {
                var startPositionLine = input[i].Split(); //TODO - validate this input better
                var commands = input[i + 1];

                var startPoint = new Point(int.Parse(startPositionLine[0]), int.Parse(startPositionLine[1]));

                Direction direction =  startPositionLine[2] switch { 
                    "N" => Direction.North,
                    "S" => Direction.South,
                    "E" => Direction.East,
                    "W" => Direction.West,
                };

                var rover = new Rover(bounds, startPoint, direction);
                roversWithCommands.Add((rover, commands));
            }

            foreach(var roverWithCommand in roversWithCommands)
            {
                var rover = roverWithCommand.rover;
                var commands = roverWithCommand.commands;

                foreach (var command in commands)
                {
                    if (command == 'L')
                    {
                        rover.TurnLeft();
                    }
                    else if (command == 'R')
                    {
                        rover.TurnRight();
                    }
                    else if (command == 'M')
                    {
                        rover.Move();
                    }
                }

                Console.WriteLine($"{rover.Position.X} {rover.Position.Y} {rover.Heading}");
            }
        }
    }
}
