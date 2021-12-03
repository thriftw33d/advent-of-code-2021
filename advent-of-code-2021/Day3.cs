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

            Console.WriteLine($"{gammaDecimal} * {epsylonDecimal} = {gammaDecimal * epsylonDecimal}");  
        }

        public static void Part2()
        {
            var oxygenGeneratorRating = new List<string>(input);
            var CO2ScrubberRating = new List<string>(input);

            for (int i = 0; i < input.First().Length; i++)
            {
                if (oxygenGeneratorRating.Count > 1)
                {
                    var numbersAtPositionForOxygen = oxygenGeneratorRating.Select(inp => inp[i]);

                    var numberOfZerosForOxygen = numbersAtPositionForOxygen.Where(n => n == '0').Count();
                    var numberOfOnesForOxygen = numbersAtPositionForOxygen.Where(n => n == '1').Count();

                    var mostCommon = numberOfOnesForOxygen >= numberOfZerosForOxygen ? '1' : '0';

                    oxygenGeneratorRating = oxygenGeneratorRating.Where(inp => inp[i] == mostCommon).ToList();
                }

                if (CO2ScrubberRating.Count > 1)
                {
                    var numbersAtPositionForCO2 = CO2ScrubberRating.Select(inp => inp[i]);

                    var numberOfZerosForCO2 = numbersAtPositionForCO2.Where(n => n == '0').Count();
                    var numberOfOnesForCO2 = numbersAtPositionForCO2.Where(n => n == '1').Count();

                    var leastCommon = numberOfZerosForCO2 <= numberOfOnesForCO2 ? '0' : '1';

                    CO2ScrubberRating = CO2ScrubberRating.Where(inp => inp[i] == leastCommon).ToList();

                }
            }

            var oxygenDecimal = Convert.ToInt32(oxygenGeneratorRating.Single(), 2);
            var CO2Decimal = Convert.ToInt32(CO2ScrubberRating.Single(), 2);

            Console.WriteLine($"{oxygenDecimal} * {CO2Decimal} = {oxygenDecimal * CO2Decimal}");
        }
    }
}
