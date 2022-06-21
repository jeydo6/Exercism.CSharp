using System.Text;

internal static class Transpose
{
    public static string String(string input)
    {
        var matrix = GetMatrix(input);

        var n = matrix.Length;
        var m = matrix[0].Length;
        
        var transposed = new StringBuilder[m];
        for (var j = 0; j < m; j++)
        {
            transposed[j] = new StringBuilder(n);
        }
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < matrix[i].Length; j++)
            {
                transposed[j].Append(matrix[i][j]);
            }
        }

        var result = new string[m];
        for (var j = 0; j < m; j++)
        {
            result[j] = transposed[j].ToString();
        }

        return string.Join('\n', result);
    }

    private static string[] GetMatrix(string input)
    {
        var lines = input.Split('\n');
        var result = new string[lines.Length];

        var maxLength = lines[^1].Length;
        for (var i = lines.Length - 1; i >= 0; i--)
        {
            if (lines[i].Length > maxLength)
            {
                maxLength = lines[i].Length;
            }

            result[i] = lines[i] + new string(' ', maxLength - lines[i].Length);
        }

        return result;
    }
}
