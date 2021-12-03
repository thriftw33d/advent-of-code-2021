using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2021
{
    public class Day3
    {
        static readonly List<string> input = File.ReadAllLines("lucka-3.txt").ToList();

        public static void Part1()
        {
            string gammaRate = string.Empty;
            string epsylonRate = string.Empty;

            for (int i = 0; i < input.First().Length; i++)
            {
                var numbersAtPosition = input.Select(inp => inp[i]);

                var result = numbersAtPosition.GroupBy(i => i).OrderBy(g => g.Count()).Select(g => g.Key).ToList();

                var mostCommon = result.Last();
                var leastCommon = result.First();

                gammaRate += mostCommon;
                epsylonRate += leastCommon;
            }

            var gammaDecimal = Convert.ToInt32(gammaRate, 2);
            var epsylonDecimal = Convert.ToInt32(epsylonRate, 2);

            Console.WriteLine("gamma: " + gammaRate + " " + epsylonRate);
            Console.WriteLine("gamma: " + gammaDecimal + " * " + epsylonDecimal + " = " + gammaDecimal * epsylonDecimal);
            
        }

        public static void Part2()
        {
        }
    }
}
