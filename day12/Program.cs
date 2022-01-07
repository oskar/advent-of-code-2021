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

Visit(ImmutableList<string>.Empty.Add("start"));

Console.WriteLine("Paths found: " + completePathCount);

void Visit(ImmutableList<string> path)
{
    var currentNode = path.Last();

    if (currentNode == "end")
    {
        Console.WriteLine(string.Join("-", path));
        completePathCount++;
        return;
    }
    
    foreach (var connectedNode in connections[currentNode])
    {
        if (path.Contains(connectedNode) && !connectedNode.All(char.IsUpper))
            continue;
        
        Visit(path.Add(connectedNode));
    }
}
