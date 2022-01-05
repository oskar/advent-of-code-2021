using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var lines = File.ReadAllLines("input.txt");
var points = lines.Select(l => l.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
var rowCount = points.Length;
var columnCount = points[0].Length;

var lowPoints = new List<int>();

for (var row = 0; row < rowCount; row++)
{
    for (var column = 0; column < columnCount; column++)
    {
        var point = points[row][column];
        var neighbours = GetNeighbours(row, column);
        if (neighbours.All(n => n > point))
        {
            lowPoints.Add(point);
        }
    }
}

Console.WriteLine("Low points: " + string.Join(", ", lowPoints));
Console.WriteLine("Risk: " + lowPoints.Sum(p => p + 1));

IEnumerable<int> GetNeighbours(int row, int column)
{
    if (row > 0) yield return points[row - 1][column];
    if (row < rowCount - 1) yield return points[row + 1][column];
    if (column > 0) yield return points[row][column - 1];
    if (column < columnCount - 1) yield return points[row][column + 1];
}
