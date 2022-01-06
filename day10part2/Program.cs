using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var lines = File.ReadAllLines("input.txt");
var openingToClosing = new Dictionary<char, char>
{
    {'(', ')'},
    {'[', ']'},
    {'{', '}'},
    {'<', '>'},
};
var closingToOpening = openingToClosing.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

var lineScores = new List<long>();

foreach (var line in lines)
{
    var stack = new Stack<char>();
    var discardLine = false;
    foreach (var character in line)
    {
        if (openingToClosing.ContainsKey(character))
        {
            stack.Push(character);
        }
        else
        {
            if (stack.Pop() != closingToOpening[character])
            {
                discardLine = true;
                break;
            }
        }
    }

    if (discardLine)
    {
        continue;
    }

    long lineScore = 0;
    foreach (var character in stack.Select(c => openingToClosing[c]))
    {
        lineScore *= 5;
        lineScore += GetCharacterScore(character);
    }
    
    lineScores.Add(lineScore);
}

lineScores.Sort();
Console.WriteLine("Middle score: " + lineScores[lineScores.Count / 2]);

static int GetCharacterScore(char autoCompleteCharacter)
{
    return autoCompleteCharacter switch
    {
        ')' => 1,
        ']' => 2,
        '}' => 3,
        '>' => 4,
        _ => throw new Exception("Unexpected autocomplete character: " + autoCompleteCharacter)
    };
}
