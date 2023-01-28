using System;
using System.IO;

var lines = File.ReadAllLines("input.txt");
var horizontal = 0;
var depth = 0;
var aim = 0;

foreach (var line in lines)
{
  var lineArray = line.Split(" ");
  var command = lineArray[0];
  var movement = Convert.ToInt32(lineArray[1]);
  
  switch (command)
  {
    case "forward": horizontal += movement; depth += movement * aim; break;
    case "down": aim += movement; break;
    case "up": aim -= movement; break;
  }
}

Console.WriteLine("Horizontal position: " + horizontal);
Console.WriteLine("Depth: " + depth);
Console.WriteLine("Aim: " + aim);
Console.WriteLine("Product: " + horizontal * depth);
