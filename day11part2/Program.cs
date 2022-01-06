using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var lines = File.ReadAllLines("input.txt");
var energies = lines.Select(l => l.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
var octopusCount = lines.Sum(l => l.Length);
var rowCount = energies.Length;
var columnCount = energies[0].Length;
var step = 0;

while (true)
{
    step++;
    
    // Increase all energy levels by 1
    for (var row = 0; row < rowCount; row++)
        for (var column = 0; column < columnCount; column++)
            energies[row][column] += 1;
    
    // Find flashing octopuses
    var flashingOctopuses = new HashSet<(int, int)>();
    for (var row = 0; row < rowCount; row++)
    {
        for (var column = 0; column < columnCount; column++)
        {
            if (energies[row][column] > 9)
            {
                Flash(row, column);
            }
        }
    }
    
    void Flash(int row, int column)
    {
        // Bail out if already flashed this step
        if (!flashingOctopuses.Add((row, column)))
            return;
        
        foreach (var (neighbourRow, neighbourColumn) in GetNeighbours(row, column))
        {
            // Increase energy level of neighbour
            energies[neighbourRow][neighbourColumn] += 1;
            
            if (energies[neighbourRow][neighbourColumn] > 9)
            {
                // Neighbour flashes as well
                Flash(neighbourRow, neighbourColumn);
            }
        }
    }

    if (flashingOctopuses.Count >= octopusCount)
    {
        break;
    }

    foreach (var (row, column) in flashingOctopuses)
    {
        energies[row][column] = 0;
    }
}

Console.WriteLine("All octopuses flashed during step: " + step);

List<(int, int)> GetNeighbours(int row, int column)
{
    var neighbours = new List<(int, int)>();
    
    if (row > 0) neighbours.Add((row - 1, column));
    if (row < rowCount - 1) neighbours.Add((row + 1, column));
    if (column > 0) neighbours.Add((row, column - 1));
    if (column < columnCount - 1) neighbours.Add((row, column + 1));
    
    if(row > 0 && column > 0) neighbours.Add((row - 1, column - 1));
    if(row > 0 && column < columnCount - 1) neighbours.Add((row - 1, column + 1));
    if(row < rowCount - 1 && column > 0) neighbours.Add((row + 1, column - 1));
    if(row < rowCount - 1 && column < columnCount - 1) neighbours.Add((row + 1, column + 1));
    
    return neighbours;
}
