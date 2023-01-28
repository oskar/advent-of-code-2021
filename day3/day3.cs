using System;
using System.IO;

var lines = File.ReadAllLines("input.txt");
var digits = lines[0].Length;

Console.WriteLine("Digits: " + digits);

var sums = new uint[digits];

foreach (var line in lines)
{
  for (var i = 0; i < digits; i++)
  {
    if (line[i] == '1')
    {
      sums[i]++;
    }
  }
}

uint gammaRate = 0;
for (var i = 0; i < digits; i++)
{
  uint bit = sums[i] > lines.Length / 2 ? 1U : 0U;
  gammaRate += bit << (digits - i - 1);
}

var mask = (1U << digits) - 1;
var epsilonRate = ~gammaRate & mask;

PrintBinary("Gamma rate", gammaRate);
PrintBinary("Epsilon rate", epsilonRate);
PrintBinary("Power consumption", gammaRate * epsilonRate);

void PrintBinary(string variableName, uint number) 
  => Console.WriteLine($"{variableName}: {Convert.ToString(number, 2)} ({number})");