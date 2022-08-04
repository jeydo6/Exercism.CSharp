internal class Matrix
{
    private readonly int[][] _matrix;

    public Matrix(string input) => _matrix = BuildMatrix(input);

    public int[] Row(int row)
    {
        var result = new int[_matrix[0].Length];

        var i = row - 1;
        for (var j = 0; j < result.Length; j++)
        {
            result[j] = _matrix[i][j];
        }

        return result;
    }

    public int[] Column(int col)
    {
        var result = new int[_matrix.Length];

        var j = col - 1;
        for (var i = 0; i < result.Length; i++)
        {
            result[i] = _matrix[i][j];
        }

        return result;
    }

    private static int[][] BuildMatrix(string input)
    {
        var rows = input.Split('\n');
        
        var matrix = new int[rows.Length][];
        for (var i = 0; i < matrix.Length; i++)
        {
            var cols = rows[i].Split(' ');
            matrix[i] = new int[cols.Length];
            for (var j = 0; j < matrix[i].Length; j++)
            {
                matrix[i][j] = int.Parse(cols[j]);
            }
        }

        return matrix;
    }
}
