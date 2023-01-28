using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var lines = File.ReadAllLines("input.txt").ToList();
var numbersToDraw = lines[0].Split(",").Select(n => Convert.ToInt32(n));
var boards = new List<Dictionary<int, bool>>();

for (var i = 2; i < lines.Count; i += 6)
{
  var boardNumbers = new List<int>();
  for (var j = 0; j < 5; j++)
  {
    var row = Regex.Split(lines[i + j].Trim(), " +").Select(s => Convert.ToInt32(s));
    boardNumbers.AddRange(row);
  }

  boards.Add(boardNumbers.ToDictionary(b => b, _ => false));
}

PlayBingo(numbersToDraw, boards);

static void PlayBingo(IEnumerable<int> numbersToDraw, List<Dictionary<int, bool>> boards)
{
  foreach (var numberDrawn in numbersToDraw)
  {
    Console.WriteLine("Number drawn: " + numberDrawn);

    var boardsWithBingo = new List<Dictionary<int, bool>>();
    foreach (var board in boards.Where(board => board.ContainsKey(numberDrawn)))
    {
      board[numberDrawn] = true;
      if (IsBingo(board))
      {
        boardsWithBingo.Add(board);
      }
    }

    foreach (var board in boardsWithBingo)
    {
      boards.Remove(board);
    }
    
    if (boards.Count > 0)
      continue;
    
    Console.WriteLine("Only one board left: " + boardsWithBingo.Count);
    Console.WriteLine("Its score: " + Score(boardsWithBingo[0], numberDrawn));
    return;
  }
}

static bool IsBingo(Dictionary<int, bool> board)
{
  var values = board.Values.ToArray();
  var rowStartIndices = Enumerable.Range(0, 5).Select(i => 5 * i).ToList();

  for (var i = 0; i < 5; i++)
  {
    if (values[(5 * i)..(5 * i + 5)].All(n => n))
    {
      return true;
    }

    if (rowStartIndices.All(ri => values[ri + i]))
    {
      return true;
    }
  }

  return false;
}

static int Score(Dictionary<int, bool> board, int winningNumber)
{
  var sumUnmarkedNumbers = board
    .Where(cell => !cell.Value)
    .Sum(cell => cell.Key);

  return sumUnmarkedNumbers * winningNumber;
}