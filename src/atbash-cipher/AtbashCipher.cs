using System;
using System.Text;

internal static class AtbashCipher
{
    public static string Encode(string plainValue)
    {
        var result = new StringBuilder(plainValue.Length + plainValue.Length / 5);
        for (var i = 0; i < plainValue.Length; i++)
        {
            var ch = plainValue[i];
            if (char.IsLetter(ch))
            {
                result.Append((char)('z' + 'a' - char.ToLower(ch)));
            }
            else if (char.IsDigit(ch))
            {
                result.Append(ch);
            }
    
            if (result.Length % 6 == 5)
            {
                result.Append(' ');
            }
        }
    
        return result.ToString().Trim();
    }

    public static string Decode(string encodedValue)
    {
        var result = new StringBuilder(encodedValue.Length);
        for (var i = 0; i < encodedValue.Length; i++)
        {
            var ch = encodedValue[i];
            if (char.IsLetter(ch))
            {
                result.Append((char)('z' + 'a' - char.ToLower(ch)));
            }
            else if (char.IsDigit(ch))
            {
                result.Append(ch);
            }
        }
    
        return result.ToString();
    }
}
