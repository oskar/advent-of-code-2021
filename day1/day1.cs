using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var lines = File.ReadAllLines("input.txt")
    .Select(l => Convert.ToInt32(l))
    .ToList();

var measurements = new List<int>();

for (var i = 2; i < lines.Count; i++)
{
    measurements.Add(lines[i] + lines[i - 1] + lines[i - 2]);
}

var count = 0;

for (var i = 1; i < measurements.Count; i++)
{
    var currentLine = measurements[i];
    var previousLine = measurements[i - 1];
    if (currentLine > previousLine)
    {
        count++;
    }
}

Console.WriteLine(count);
