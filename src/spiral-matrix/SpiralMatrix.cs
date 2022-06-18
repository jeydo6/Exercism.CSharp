internal static class SpiralMatrix
{
    public static int[,] GetMatrix(int size)
    {
        var matrix = new int[size, size];

        var count = 1;
        var left = 0;
        var right = matrix.GetUpperBound(0);
        var top = 0;
        var bottom = matrix.GetUpperBound(1);
        while (count <= matrix.Length)
        {
            for (var j = left; j <= right; j++)
            {
                matrix[top, j] = count++;
            }
            top++;
            
            for (var i = top; i <= bottom; i++)
            {
                matrix[i, right] = count++;
            }
            right--;
            
            for (var j = right; j >= left; j--)
            {
                matrix[bottom, j] = count++;
            }
            bottom--;
            
            for (var i = bottom; i >= top; i--)
            {
                matrix[i, left] = count++;
            }
            left++;
        }

        return matrix;
    }
}
