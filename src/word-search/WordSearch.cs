using System;
using System.Collections.Generic;
using System.Linq;

internal class WordSearch
{
    private static readonly (int X, int Y)[] _directions = new (int X, int Y)[]
    {
        (1, 0),
        (1, -1),
        (0, -1),
        (-1, -1),
        (-1, 0),
        (-1, 1),
        (0, 1),
        (1, 1)
    };
    
    private readonly string[] _grid;

    public WordSearch(string grid) => _grid = grid.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

    public Dictionary<string, ((int X, int Y) Start, (int X, int Y) End)?> Search(string[] wordsToSearchFor)
    {
        return null;
    }

    private ((int X, int Y) Start, (int X, int Y) End)? Search(string word, (int X, int Y) start)
    {
        foreach (var direction in _directions)
        {
            var length = 0;
            var end = start;
            while (
                length < word.Length &
                end.Y >= 1 && end.Y <= _grid.Length &&
                end.X >= 1 && end.X <= _grid[0].Length &&
                _grid[end.Y - 1][end.X - 1] == word[length++]
            )
            {
                if (length == word.Length)
                {
                    return (start, end);
                }
                
                end = (end.X + direction.X, end.Y + direction.Y);
            }
        }

        return null;
    }
}
