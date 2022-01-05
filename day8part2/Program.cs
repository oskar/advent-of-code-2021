using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var lines = File.ReadAllLines("input.txt");
var sum = 0;

foreach (var line in lines)
{
    var split = line.Split(" | ");
    var signalPatterns = split[0]
        .Split(" ")
        .Select(p => p.ToHashSet())
        .GroupBy(h => h.Count)
        .ToDictionary(g => g.Key, g => g.ToList());
    var outputValues = split[1].Split(" ");
    
    var numberOne = signalPatterns[2].Single();
    var numberSeven = signalPatterns[3].Single();
    var numberFour = signalPatterns[4].Single();
    var numberEight = signalPatterns[7].Single();

    var intersectionOfNumberTwoThreeAndFive = IntersectAll(signalPatterns[5]);
    var intersectionOfNumberZeroSixAndNine = IntersectAll(signalPatterns[6]);

    var numberThree = Union(numberOne, intersectionOfNumberTwoThreeAndFive);
    var numberNine = Union(numberFour, intersectionOfNumberZeroSixAndNine);
    var numberFive = Union(intersectionOfNumberTwoThreeAndFive, intersectionOfNumberZeroSixAndNine);
    var numberTwo = Union(Except(numberEight, numberFive), intersectionOfNumberTwoThreeAndFive);
    var numberZero = Union(Except(numberEight, intersectionOfNumberTwoThreeAndFive), intersectionOfNumberZeroSixAndNine);
    var numberSix = Union(Except(numberEight, numberOne), intersectionOfNumberZeroSixAndNine);

    sum += int.Parse(string.Join("", outputValues.Select(OutputValueToNumber)));
    
    string OutputValueToNumber(string outputValue)
    {
        var h = outputValue.ToHashSet();
        if (h.SetEquals(numberZero)) return "0";
        if (h.SetEquals(numberOne)) return "1";
        if (h.SetEquals(numberTwo)) return "2";
        if (h.SetEquals(numberThree)) return "3";
        if (h.SetEquals(numberFour)) return "4";
        if (h.SetEquals(numberFive)) return "5";
        if (h.SetEquals(numberSix)) return "6";
        if (h.SetEquals(numberSeven)) return "7";
        if (h.SetEquals(numberEight)) return "8";
        if (h.SetEquals(numberNine)) return "9";
        
        throw new Exception("Unexpected unmapped output value: " + outputValue);
    }
}

Console.WriteLine("Sum of output values: " + sum);

static HashSet<T> IntersectAll<T>(IList<HashSet<T>> hashSets)
{
    var intersection = new HashSet<T>(hashSets.First());
    foreach (var h in hashSets.Skip(1))
    {
        intersection.IntersectWith(h);
    }
    return intersection;
}

static HashSet<T> Except<T>(HashSet<T> a, HashSet<T> b)
{
    var hashSet = new HashSet<T>(a);
    hashSet.ExceptWith(b);
    return hashSet;
}

static HashSet<T> Union<T>(HashSet<T> a, HashSet<T> b)
{
    var hashSet = new HashSet<T>(a);
    hashSet.UnionWith(b);
    return hashSet;
}
