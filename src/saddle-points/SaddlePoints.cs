using System.Collections.Generic;

internal static class SaddlePoints
{
    public static IEnumerable<(int, int)> Calculate(int[,] matrix)
    {
        var result = new List<(int r, int c)>();
        
        var rows = matrix.GetUpperBound(0) + 1;
        var cols = matrix.GetUpperBound(1) + 1;

        var saddles = new HashSet<(int, int)>();
        
        for (var i = 0; i < rows; i++)
        {
            var col = 0;
            for (var j = 0; j < cols; j++)
            {
                if (matrix[i, j] > matrix[i, col])
                {
                    col = j;
                }
            }
        
            for (var j = 0; j < cols; j++)
            {
                if (matrix[i, j] != matrix[i, col])
                {
                    continue;
                }
                
                var pair = (i + 1, j + 1);
                saddles.Add(pair);
            }
        }
        
        for (var j = 0; j < cols; j++)
        {
            var row = 0;
            for (var i = 0; i < rows; i++)
            {
                if (matrix[i, j] < matrix[row, j])
                {
                    row = i;
                }
            }
            
            for (var i = 0; i < rows; i++)
            {
                if (matrix[i, j] != matrix[row, j])
                {
                    continue;
                }
                
                var pair = (i + 1, j + 1);
                if (saddles.Contains(pair))
                {
                    result.Add(pair);
                }
            }
        }

        return result.ToArray();
    }
}
