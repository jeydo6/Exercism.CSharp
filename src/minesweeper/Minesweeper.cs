using System.Collections.Generic;
using System.Text;

internal static class Minesweeper
{
    public static string[] Annotate(string[] board)
    {
        var results = new List<string>();

        for (var i = 0; i < board.Length; i++)
        {
            var sb = new StringBuilder();

            for (var j = 0; j < board[0].Length; j++)
            {
                if (board[i][j] == '*')
                {
                    sb.Append('*');
                }
                else
                {
                    var count = GetCount(board, i, j);
                    sb.Append(count == 0 ? " " : count.ToString());
                }
            }

            results.Add(sb.ToString());
        }

        return results.ToArray();
    }
    
    private static int GetCount(string[] board, int x, int y)
    {
        var result = 0;

        for (var i = -1; i <= 1; i++)
        {
            for (var j = -1; j <= 1; j++)
            {
                if (
                    x + i >= 0 && x + i <= board.Length - 1 &&
                    y + j >= 0 && y + j <= board[0].Length - 1 &&
                    board[x + i][y + j] == '*'
                )
                {
                    result++;
                }
            }
        }

        return result;
    }
}
