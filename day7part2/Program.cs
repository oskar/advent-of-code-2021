using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var crabs = File.ReadAllText("input.txt").Split(",").Select(int.Parse).ToList();

var minFuel = Enumerable.Range(crabs.Min(), crabs.Max()).Min(i => FuelSum(crabs, i));
Console.WriteLine("Min fuel: " + minFuel);

static int FuelSum(IEnumerable<int> crabs, int toPosition) 
    => crabs.Sum(crab => FuelToPosition(crab, toPosition));

static int FuelToPosition(int crab, int toPosition)
{
    var distance = Math.Abs(crab - toPosition);
    return distance * (1 + distance) / 2;
}
