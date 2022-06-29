using System;

internal class Queen
{
    public Queen(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public int Row { get; }
    public int Column { get; }
}

internal static class QueenAttack
{
    public static bool CanAttack(Queen white, Queen black) =>
        white.Row == black.Row ||
        white.Column == black.Column ||
        Math.Abs(white.Column - black.Column) == Math.Abs(white.Row - black.Row);

    public static Queen Create(int row, int column)
    {
        if (row is not (>= 0 and < 8))
        {
            throw new ArgumentOutOfRangeException(nameof(row));
        }
        
        if (column is not (>= 0 and < 8))
        {
            throw new ArgumentOutOfRangeException(nameof(column));
        }
        
        return new Queen(row, column);
    }
}
