using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var sortedLines = File.ReadAllLines("input.txt").ToList();
sortedLines.Sort();
var numberOfDigits = sortedLines[0].Length;

var oxygenGenerator = GenerateRating(OxygenGeneratorBitCriteria);
var co2scrubber = GenerateRating(CO2ScrubberBitCriteria);
Console.WriteLine($"Product: {Convert.ToInt32(oxygenGenerator, 2) * Convert.ToInt32(co2scrubber, 2)}");

string GenerateRating(Func<int, int, bool> bitCriteria)
{
  var lower = 0;
  var upper = sortedLines.Count;

  for (var currentDigit = 0; currentDigit < numberOfDigits; currentDigit++)
  {
    var indexOfFirstMatchingOne = IndexOfFirstMatchingBit(sortedLines, currentDigit, lower, upper, '1');
    if (indexOfFirstMatchingOne <= 0)
    {
      // all bits equal in this digit place, cannot decrease the search area, move on to next digit
      continue;
    }

    var middle = (lower + upper) / 2;
    if (bitCriteria(middle, indexOfFirstMatchingOne))
    {
      // keep lower interval
      upper = indexOfFirstMatchingOne;
    }
    else
    {
      // keep upper interval
      lower = indexOfFirstMatchingOne;
    }

    if (upper - lower > 1) continue;

    Console.WriteLine("Rating generated: " + sortedLines[lower]);
    return sortedLines[lower];
  }

  throw new Exception($"Unable to generate value, lower:{lower} and upper:{upper}");
}

bool OxygenGeneratorBitCriteria(int middle, int indexOfFirstMatchingOne) => indexOfFirstMatchingOne > middle;

bool CO2ScrubberBitCriteria(int middle, int indexOfFirstMatchingOne) => indexOfFirstMatchingOne <= middle;

int IndexOfFirstMatchingBit(IList<string> lines, int digit, int start, int limit, char bit)
{
  for (var i = start; i < limit; i++)
  {
    if (lines[i][digit] == bit)
    {
      return i;
    }
  }

  return -1;
}
