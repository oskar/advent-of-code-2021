using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var inputs = File.ReadAllText("input.txt").Split(",").Select(int.Parse);
var fishes = inputs.GroupBy(i => i).ToDictionary(i => i.Key, i => (long)i.Count());

for (var i = 0; i <= 8; i++)
{
    if (!fishes.ContainsKey(i))
    {
        fishes[i] = 0;
    }
}

Print(0, fishes);

for (var iteration = 1; iteration <= 256; iteration++)
{
    var fishesToSpawn = fishes[0];
    for (var age = 1; age <= 8; age++)
    {
        fishes[age - 1] = fishes[age];
    }

    fishes[8] = fishesToSpawn;
    fishes[6] += fishesToSpawn;

    Print(iteration, fishes);
}

Console.WriteLine("Number of fishes: " + fishes.Values.Sum());

static void Print(int iteration, IDictionary<int, long> fishes) 
    => Console.WriteLine($"After {iteration} days: {string.Join(",", fishes.Select(kvp => $"{kvp.Key}:{kvp.Value}"))}");