using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var regexPattern = @"^(\d+),(\d+) -> (\d+),(\d+)$";
var lines = File.ReadAllLines("input.txt");

var diagram = new int[1000, 1000];

foreach (var line in lines)
{
  var match = Regex.Match(line, regexPattern);
  var x1 = int.Parse(match.Groups[1].Value);
  var y1 = int.Parse(match.Groups[2].Value);
  var x2 = int.Parse(match.Groups[3].Value);
  var y2 = int.Parse(match.Groups[4].Value);

  if (x1 == x2)
  {
    for (var y = Math.Min(y1, y2); y <= Math.Max(y1, y2); y++)
    {
      diagram[y, x1] += 1;
    }
  }
  else if (y1 == y2)
  {
    for (var x = Math.Min(x1, x2); x <= Math.Max(x1, x2); x++)
    {
      diagram[y1, x] += 1;
    }
  }
  else
  {
    // diagonal line, 
    var length = Math.Abs(x2 - x1);
    var xDirection = x1 < x2 ? 1 : -1;
    var yDirection = y1 < y2 ? 1 : -1;

    for (var i = 0; i <= length; i++)
    {
      var x = x1 + xDirection * i;
      var y = y1 + yDirection * i;
      diagram[y, x] += 1;
    }
  }
}

PrintDiagram(diagram);

Console.WriteLine("Count: " + diagram.Cast<int>().Count(i => i > 1));

static void PrintDiagram(int[,] diagram)
{
  for (var i = 0; i < 10; i++)
  {
    for (var j = 0; j < 10; j++)
    {
      Console.Write(diagram[i, j]);
    }

    Console.WriteLine();
  }
}
