using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var lines = File.ReadAllLines("input.txt");
var points = lines.Select(l => l.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
var rowCount = points.Length;
var columnCount = points[0].Length;
var basinSizes = new List<int>();

for (var row = 0; row < rowCount; row++)
{
    for (var column = 0; column < columnCount; column++)
    {
        if (points[row][column] >= 9)
            continue;
        
        // Find whole basin
        var basin = new HashSet<(int, int)> {(row, column)};
        GetNeighbours(basin, row, column);
            
        // Clear out whole basin
        foreach (var (basinRow, basinColumn) in basin)
        {
            points[basinRow][basinColumn] = 9;
        }
            
        basinSizes.Add(basin.Count);
    }
}

Console.WriteLine("Basin sizes: " + string.Join(", ", basinSizes));
Console.WriteLine("Product of three largest basins: " +
                  basinSizes.OrderByDescending(b => b).Take(3).Aggregate((acc, value) => acc * value));

void GetNeighbours(HashSet<(int, int)> basin, int row, int column)
{
    // Collect valid new neighbours
    var neighbours = new HashSet<(int, int)>();
    if (row > 0) neighbours.Add((row - 1, column));
    if (row < rowCount - 1) neighbours.Add((row + 1, column));
    if (column > 0) neighbours.Add((row, column - 1));
    if (column < columnCount - 1) neighbours.Add((row, column + 1));
    
    // Remove those with value 9 (not in basin)
    neighbours.RemoveWhere(p => points[p.Item1][p.Item2] == 9);
    
    // Remove those already in basin
    neighbours.ExceptWith(basin);

    // Add to basin
    basin.UnionWith(neighbours);
    
    // Continue the search for each new neighbour
    foreach (var (neighbourRow, neighbourColumn) in neighbours)
    {
        GetNeighbours(basin, neighbourRow, neighbourColumn);
    }
}
