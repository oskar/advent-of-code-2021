using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var lines = File.ReadAllLines("input.txt");

var illegalCharacters = new List<char>();

foreach (var line in lines)
{
    var stack = new Stack<char>();
    foreach (var character in line)
    {
        if (IsOpeningCharacter(character))
        {
            stack.Push(character);
        }
        else
        {
            if (stack.Pop() != GetMatchingOpeningCharacter(character))
            {
                illegalCharacters.Add(character);
                break;
            }
        }
    }
}

Console.WriteLine("Illegal characters: " + string.Join(", ", illegalCharacters));
Console.WriteLine("Syntax error score: " + illegalCharacters.Select(GetCharacterScore).Sum());

static bool IsOpeningCharacter(char character) => character is '(' or '[' or '{' or '<';

static char GetMatchingOpeningCharacter(char closingCharacter)
{
    return closingCharacter switch
    {
        ')' => '(',
        ']' => '[',
        '}' => '{',
        '>' => '<',
        _ => throw new Exception("Unexpected closing character: " + closingCharacter)
    };
}

static int GetCharacterScore(char illegalCharacter)
{
    return illegalCharacter switch
    {
        ')' => 3,
        ']' => 57,
        '}' => 1197,
        '>' => 25137,
        _ => throw new Exception("Unexpected illegal character: " + illegalCharacter)
    };
}
