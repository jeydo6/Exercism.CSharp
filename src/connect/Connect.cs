using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

internal enum ConnectWinner
{
    White,
    Black,
    None
}

internal class Connect
{
    private enum Cell
    {
        White,
        Black,
        None
    }
    
    private readonly Cell[][] _board;
    
    public Connect(IEnumerable<string> input) => _board = input
        .Select(str => Regex
            .Replace(str, @"\s+", "")
            .Select(ch => ch switch
            {
                'O' => Cell.White,
                'X' => Cell.Black,
                _ => Cell.None
            })
            .ToArray())
        .ToArray();
    
    private int Rows => _board.Length;
    private int Cols => _board[0].Length;

    public ConnectWinner Result()
    {
        if (WhiteWins())
        {
            return ConnectWinner.White;
        }

        if (BlackWins())
        {
            return ConnectWinner.Black;
        }

        return ConnectWinner.None;
    }
    
    private ISet<(int Row, int Col)> EqualsNeighborCoordinates(Cell cell, (int Row, int Col) coordinate)
    {
        var coordinates = new[]
        {
            (coordinate.Row + 1, coordinate.Col - 1),
            (coordinate.Row, coordinate.Col - 1),
            (coordinate.Row - 1, coordinate.Col),
            (coordinate.Row + 1, coordinate.Col),
            (coordinate.Row - 1, coordinate.Col + 1),
            (coordinate.Row, coordinate.Col + 1)
        };

        return new HashSet<(int Row, int Col)>(coordinates.Where(coord => IsValid(coord) && IsEqual(cell, coord)));
    }

    private bool IsValid((int Row, int Col) coordinate) =>
        coordinate.Row >= 0 && coordinate.Row < Rows &&
        coordinate.Col >= 0 && coordinate.Col < Cols;
    
    private bool IsEqual(Cell cell, (int Row, int Col) coordinate) =>
        _board[coordinate.Row][coordinate.Col] == cell;
    
    private bool IsValidPath(Cell cell, Func<(int Row, int Col), bool> stop, ISet<(int Row, int Col)> processed, (int Row, int Col) coordinate)
    {
        if (stop(coordinate))
        {
            return true;
        }

        var next = EqualsNeighborCoordinates(cell, coordinate);
        next.ExceptWith(processed);

        return next.Any(nextCoordinate => IsValidPath(cell, stop, new HashSet<(int Row, int Col)>(processed) { nextCoordinate }, nextCoordinate));
    }
    
    private bool IsWhiteStop((int Row, int Col) coordinate) => coordinate.Row == Rows - 1;
    
    private bool IsBlackStop((int Row, int Col) coordinate) => coordinate.Col == Cols - 1;
    
    private ISet<(int Row, int Col)> WhiteStart() => new HashSet<(int Row, int Col)>(
        Enumerable.Range(0, Cols)
            .Select(col => (0, col))
            .Where(coordinate => IsEqual(Cell.White, coordinate))
    );
        
    private ISet<(int Row, int Col)> BlackStart() => new HashSet<(int Row, int Col)>(
        Enumerable.Range(0, Rows)
            .Select(row => (row, 0))
            .Where(coordinate => IsEqual(Cell.Black, coordinate))
    );

    private bool ColorWins(Cell cell, Func<(int Row, int Col), bool> stop, Func<ISet<(int Row, int Col)>> start) => start()
        .Any(coordinate => IsValidPath(cell, stop, new HashSet<(int Row, int Col)>(), coordinate));
    
    private bool WhiteWins() => ColorWins(Cell.White, IsWhiteStop, WhiteStart);
    
    private bool BlackWins() => ColorWins(Cell.Black, IsBlackStop, BlackStart);    
}
