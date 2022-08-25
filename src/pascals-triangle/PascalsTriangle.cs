using System;
using System.Collections.Generic;

internal static class PascalsTriangle
{
    public static IEnumerable<IEnumerable<int>> Calculate(int rows) =>
        rows >= 0 ?
        IterateRows(rows) :
        throw new ArgumentOutOfRangeException(nameof(rows));
    
    private static IEnumerable<IEnumerable<int>> IterateRows(int rows)
    {
        for (var i = 1; i <= rows; i++)
        {
            yield return Row(i);
        }
    }

    private static IEnumerable<int> Row(int row)
    {
        yield return 1;
        
        var column = 1;
        for (var j = 1; j < row; j++)
        {
            column = column * (row - j) / j;
            yield return column;
        }
    }
}
