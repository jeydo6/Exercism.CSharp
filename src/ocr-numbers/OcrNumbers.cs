using System;
using System.Collections.Generic;
using System.Text;

internal static class OcrNumbers
{
    #region digits
    private const string Zero =
        " _ " +
        "| |" +
        "|_|" +
        "   ";
    private const string One =
        "   " +
        "  |" +
        "  |" +
        "   ";
    private const string Two =
        " _ " +
        " _|" +
        "|_ " +
        "   ";
    private const string Three =
        " _ " +
        " _|" +
        " _|" +
        "   ";
    private const string Four =
        "   " +
        "|_|" +
        "  |" +
        "   ";
    private const string Five =
        " _ " +
        "|_ " +
        " _|" +
        "   ";
    private const string Six =
        " _ " +
        "|_ " +
        "|_|" +
        "   ";
    private const string Seven =
        " _ " +
        "  |" +
        "  |" +
        "   ";
    private const string Eight =
        " _ " +
        "|_|" +
        "|_|" +
        "   ";
    private const string Nine =
        " _ " +
        "|_|" +
        " _|" +
        "   ";
    #endregion
    
    public static string Convert(string input)
    {
        var rows = input.Split('\n');

        var rowsCount = rows.Length;
        var columnsCount = rows[0].Length;
        
        if (rowsCount % 4 != 0 || columnsCount % 3 != 0)
        {
            throw new ArgumentException(null, nameof(input));
        }

        var result = new List<string>();

        for (var i = 0; i < rowsCount; i += 4)
        {
            var sb = new StringBuilder();
            for (var j = 0; j < columnsCount; j += 3)
            {
                sb.Append(Recognize(
                    rows[i][j..(j + 3)] +
                    rows[i + 1][j..(j + 3)] +
                    rows[i + 2][j..(j + 3)] +
                    rows[i + 3][j..(j + 3)]
                ));
            }
            
            result.Add(
                sb.ToString()
            );
        }
        
        return string.Join(',', result);
    }

    private static char Recognize(string input) => input switch
    {
        Zero => '0',
        One => '1',
        Two => '2',
        Three => '3',
        Four => '4',
        Five => '5',
        Six => '6',
        Seven => '7',
        Eight => '8',
        Nine => '9',
        _ => '?'
    };
}
