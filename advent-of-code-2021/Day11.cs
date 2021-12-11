using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace advent_of_code_2021
{
    public class Day11
    {
        static readonly List<string> input = File.ReadAllLines("lucka-11.txt").ToList();
        static int numberOfFlashes = 0;

        private static List<List<int>> GetRows()
        {
            var rows = new List<List<int>>();

            foreach (var line in input)
            {
                var row = line.ToCharArray().Select(l => Int32.Parse(l.ToString())).ToList();

                rows.Add(row);
            }

            return rows;
        }

        private static void Flash(List<List<int>> rows, List<int> row, int indexOfFlashingOctopus)
        {
            var indexOfRow = rows.IndexOf(row);

            var rowAbove = indexOfRow - 1 >= 0 ? rows[indexOfRow - 1] : new List<int>();
            var rowBelow = indexOfRow + 1 < rows.Count() ? rows[indexOfRow + 1] : new List<int>();

            var indexBelowWithinRange = indexOfFlashingOctopus - 1 >= 0;
            var indexAboveWithinRange = indexOfFlashingOctopus + 1 < row.Count;
            var rowAboveWithinRange = rowAbove.Any();
            var rowBelowWithinRange = rowBelow.Any();

            List<int> nextFlashingRow = new List<int>();
            var nextFlashingIndex = -1;

            //Flashing Octopus gets energy level of -1 to indicate that it flashed this step
            row[indexOfFlashingOctopus] = -1;

            int index;

            //If there is a row above, check adjacent octopuses on that row
            if (rowAboveWithinRange)
            {
                //Top left adjacent of flashing octopus
                index = indexOfFlashingOctopus - 1;

                if (indexBelowWithinRange && rowAbove[index] != -1)
                {
                    rowAbove[index]++;

                    if (!nextFlashingRow.Any() && rowAbove[index] > 9)
                    {
                        nextFlashingRow = rowAbove;
                        nextFlashingIndex = index;
                    }
                }

                //Top adjacent of flashing octopus
                index = indexOfFlashingOctopus;

                if (rowAbove[index] != -1)
                {
                    rowAbove[index]++;

                    if (!nextFlashingRow.Any() && rowAbove[index] > 9)
                    {
                        nextFlashingRow = rowAbove;
                        nextFlashingIndex = index;
                    }
                }

                //Top right adjacent of flashing octopus
                index = indexOfFlashingOctopus + 1;

                if (indexAboveWithinRange && rowAbove[index] != -1)
                {
                    rowAbove[index]++;

                    if (!nextFlashingRow.Any() && rowAbove[index] > 9)
                    {
                        nextFlashingRow = rowAbove;
                        nextFlashingIndex = index;
                    }
                }
            }

            //Left adjacent of flashing octopus
            index = indexOfFlashingOctopus - 1;

            if (indexBelowWithinRange && row[index] != -1)
            {
                row[index]++;

                if (!nextFlashingRow.Any() && row[index] > 9)
                {
                    nextFlashingRow = row;
                    nextFlashingIndex = index;
                }
            }

            //Right adjacent of flashing octopus
            index = indexOfFlashingOctopus + 1;

            if (indexAboveWithinRange && row[index] != -1)
            {
                row[index]++;

                if (!nextFlashingRow.Any() && row[index] > 9)
                {
                    nextFlashingRow = row;
                    nextFlashingIndex = index;
                }
            }

            //If there is a row below, check adjacent octopuses on that row
            if (rowBelowWithinRange)
            {
                //Bottom left adjacent of flashing octopus
                index = indexOfFlashingOctopus - 1;

                if (indexBelowWithinRange && rowBelow[index] != -1)
                {
                    rowBelow[index]++;

                    if (!nextFlashingRow.Any() && rowBelow[index] > 9)
                    {
                        nextFlashingRow = rowBelow;
                        nextFlashingIndex = index;
                    }
                }

                //Bottom adjacent of flashing octopus
                index = indexOfFlashingOctopus;

                if (rowBelow[index] != -1)
                {
                    rowBelow[index]++;

                    if (!nextFlashingRow.Any() && rowBelow[index] > 9)
                    {
                        nextFlashingRow = rowBelow;
                        nextFlashingIndex = index;
                    }
                }

                //Bottom right adjacent of flashing octopus
                index = indexOfFlashingOctopus + 1;

                if (indexAboveWithinRange && rowBelow[index] != -1)
                {
                    rowBelow[index]++;

                    if (!nextFlashingRow.Any() && rowBelow[index] > 9)
                    {
                        nextFlashingRow = rowBelow;
                        nextFlashingIndex = index;
                    }
                }                
            }

            //Check if this flash caused any other octopus to flash 
            if (nextFlashingRow.Any() && nextFlashingIndex > -1)
            {
                Flash(rows, nextFlashingRow, nextFlashingIndex);
                numberOfFlashes++;
            }
        }
        private static void ResetOctopusEnergyLevel(List<List<int>> rows)
        {
            //Resetting energy level for octopuses with a current energt level of -1 (i.e. octopuses that flashed during this step)
            foreach (var row in rows)
            {
                for(int i = 0; i < row.Count(); i++)
                {
                    var octopusEnergyLevel = row[i];

                    if (octopusEnergyLevel == -1)
                        row[i] = 0;
                }
            }
        }

        public static void Part1()
        {
            var rows = GetRows();

            var numberOfSteps = 100;

            for (int step = 0; step < numberOfSteps; step++)
            {
                //Increase energy level of each octopus
                foreach (var row in rows)
                {
                    for(int i = 0; i < row.Count(); i++)
                    {
                        if (row[i] != -1)
                        {
                            row[i]++;
                        }
                    }
                }

                //Check if any octopus flashed
                foreach (var row in rows)
                {
                    for (int i = 0; i < row.Count(); i++)
                    {
                        var octopusEnergyLevel = row[i];


                        if (octopusEnergyLevel > 9)
                        {
                            Flash(rows, row, i);
                            numberOfFlashes++;
                        }
                    }
                }

                //finally, all flashing octopuses has its energy level set to 0
                ResetOctopusEnergyLevel(rows);
            }

            Console.WriteLine($"Number of flashes = {numberOfFlashes}");
        }
    }

    //private static Dictionary<string, List<int>> CreateGrid()
    //{
    //    var grid = new Dictionary<string, List<int>>();

    //    foreach (var line in input)
    //    {
    //        var row = line.ToCharArray().Select(l => Int32.Parse(l.ToString())).ToList();

    //        grid.Add($"R{input.IndexOf(line) + 1}", row);
    //    }

    //    var numberOfColumns = 10;

    //    for (int i = 0; i < numberOfColumns; i++)
    //    {
    //        var rows = grid.Values;

    //        var column = rows.Select(row => row[i]).ToList();

    //        grid.Add($"C{i + 1}", column);
    //    }

    //    return grid;
    //}

    //class Day11Constants
    //{
    //    protected static string Column1 = "C1";
    //    protected static string Column2 = "C2";
    //    protected static string Column3 = "C3";
    //    protected static string Column4 = "C4";
    //    protected static string Column5 = "C5";
    //    protected static string Column6 = "C6";
    //    protected static string Column7 = "C7";
    //    protected static string Column8 = "C8";
    //    protected static string Column9 = "C9";
    //    protected static string Column10 = "C10";

    //    protected static string Row1 = "R1";
    //    protected static string Row2 = "R2";
    //    protected static string Row3 = "R3";
    //    protected static string Row4 = "R4";
    //    protected static string Row5 = "R5";
    //    protected static string Row6 = "R6";
    //    protected static string Row7 = "R7";
    //    protected static string Row8 = "R8";
    //    protected static string Row9 = "R9";
    //    protected static string Row10 = "R10";
    //} 
}
