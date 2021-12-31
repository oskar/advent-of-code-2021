using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var fishes = File.ReadAllText("input.txt").Split(",").Select(int.Parse).ToList();
Print(0, fishes);

for (var iteration = 1; iteration <= 80; iteration++)
{
    var count = fishes.Count;
    for (var i = 0; i < count; i++)
    {
        if (fishes[i] < 1)
        {
            fishes[i] = 6;
            fishes.Add(8);
        }
        else
        {
            fishes[i]--;            
        }
    }
    Console.WriteLine($"Iteration {iteration}, fish count {fishes.Count}");
}

Console.WriteLine("Number of fishes: " + fishes.Count);

static void Print(int iteration, IEnumerable<int> fishes) 
    => Console.WriteLine($"After {iteration} days: {string.Join(",", fishes)}");