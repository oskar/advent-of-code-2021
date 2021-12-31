using System;
using System.IO;
using System.Linq;

var crabs = File.ReadAllText("input.txt").Split(",").Select(int.Parse).ToList();

var minFuel = crabs.Min(i => crabs.Sum(j => Math.Abs(j - i)));
Console.WriteLine("Min fuel: " + minFuel);
