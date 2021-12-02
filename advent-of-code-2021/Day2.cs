using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace advent_of_code_2021
{
    public class Day2
    {
        const string FORWARD = "forward";
        const string DOWN = "down";
        const string UP = "up";

        static readonly List<string> input = File.ReadAllLines("lucka-2.txt").ToList();

        public static void Part1()
        {
            
            var horizontalPosition = 0;
            var depth = 0;

            foreach(var command in input)
            {
                var direction = command.Split(" ")[0];
                var numberOfUnits = int.Parse(command.Split(" ")[1]);

                switch (direction)
                {
                    case FORWARD:
                        horizontalPosition += numberOfUnits;
                        break;
                    case UP:
                        depth -= numberOfUnits;
                        break;
                    case DOWN:
                        depth += numberOfUnits;
                        break;
                }
            }

            Console.WriteLine($"Day 2, Part 1: {horizontalPosition * depth}");
        }

        public static void Part2()
        {
            var horizontalPosition = 0;
            var depth = 0;
            var aim = 0;

            foreach (var command in input)
            {
                var direction = command.Split(" ")[0];
                var numberOfUnits = int.Parse(command.Split(" ")[1]);

                switch (direction)
                {
                    case FORWARD:
                        horizontalPosition += numberOfUnits;
                        depth += aim * numberOfUnits;
                        break;
                    case UP:
                        aim -= numberOfUnits;
                        break;
                    case DOWN:
                        aim += numberOfUnits;
                        break;
                }
            }

            Console.WriteLine($"Day 2, Part 2: {horizontalPosition * depth}");
        }
    }
}
