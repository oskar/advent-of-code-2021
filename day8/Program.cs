using System;
using System.IO;
using System.Linq;

/*
0 => 6
1 => 2 uniq
2 => 5
3 => 5
4 => 4 uniq
5 => 5
6 => 6
7 => 3 uniq
8 => 7 uniq
9 => 6
 */

var lines = File.ReadAllLines("input.txt");

var sum = lines.Sum(line => line.Split(" | ")[1].Split(" ").Count(o => o.Length is 2 or 3 or 4 or 7));

Console.WriteLine(sum);