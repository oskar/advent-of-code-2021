using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

var lines = File.ReadAllLines("input.txt");
var connections = new Dictionary<string, List<string>>();

foreach (var line in lines)
{
    var split = line.Split("-");
    
    if (!connections.ContainsKey(split[0]))
    {
        connections[split[0]] = new List<string>();
    }

    connections[split[0]].Add(split[1]);
    
    if (!connections.ContainsKey(split[1]))
    {
        connections[split[1]] = new List<string>();
    }

    connections[split[1]].Add(split[0]);
}

var completePathCount = 0;

VisitCave(ImmutableList.Create("start"));

Console.WriteLine("Paths found: " + completePathCount);

void VisitCave(ImmutableList<string> path)
{
    var cave = path.Last();

    if (cave == "end")
    {
        completePathCount++;
        return;
    }
    
    foreach (var neighbourCave in connections[cave])
    {
        if (neighbourCave != "start" &&
            (!path.Contains(neighbourCave) || IsLargeCave(neighbourCave) || !TwoSmallCavesVisited(path)))
        {
            VisitCave(path.Add(neighbourCave));
        }
    }
}

static bool TwoSmallCavesVisited(IEnumerable<string> path)
{
    var hashSet = new HashSet<string>();
    return path.Where(IsSmallCave).Any(e => !hashSet.Add(e));
}

static bool IsSmallCave(string cave) => cave.All(char.IsLower);
static bool IsLargeCave(string cave) => cave.All(char.IsUpper);
