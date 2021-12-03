using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2021
{
    public class Day1
    {
        static readonly List<int> input = File.ReadAllLines("lucka-1.txt").Select(line => Int32.Parse(line)).ToList();

        public static void Part1()
        {
            var increase = 0;

            for (int i = 0; i < input.Count; i++)
            {
                var number = input[i];

                if (i + 1 < input.Count && number < input[i + 1])
                {
                    increase++;
                }
            }

            Console.WriteLine($"Day 1, Part 1: {increase}");
        }

        public static void Part2()
        {
            var increase = 0;

            for (int i = 0; i < input.Count; i++)
            {
                if (i + 3 < input.Count)
                {
                    var sum1 = input[i] + input[i + 1] + input[i + 2];
                    var sum2 = input[i + 1] + input[i + 2] + input[i + 3];

                    if (sum1 < sum2)
                    {
                        increase++;
                    }
                }
            }

            Console.WriteLine($"Day 1, Part 2: {increase}");
        }
    }
}
