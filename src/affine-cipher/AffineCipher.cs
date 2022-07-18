using System;
using System.Text;

internal static class AffineCipher
{
    public static string Encode(string plainText, int a, int b)
    {
        if (!IsCoPrime(a, 26))
        {
            throw new ArgumentException(null);
        }

        var sb = new StringBuilder(plainText.Length + plainText.Length / 5);
        var i = 0;
        foreach (var ch in plainText)
        {
            if (!char.IsLetterOrDigit(ch))
            {
                continue;
            }
            
            if (i > 0 && i % 5 == 0)
            {
                sb.Append(' ');
            }
            
            sb.Append(Encode(
                char.ToLower(ch),
                a,
                b,
                26
            ));

            i++;
        }

        return sb.ToString();
    }

    public static string Decode(string cipheredText, int a, int b)
    {
        if (!IsCoPrime(a, 26))
        {
            throw new ArgumentException(null);
        }
        
        var sb = new StringBuilder(cipheredText.Length);
        foreach (var ch in cipheredText)
        {
            if (!char.IsLetterOrDigit(ch))
            {
                continue;
            }

            sb.Append(Decode(
                char.ToLower(ch),
                ModularMultiplicativeInverse(a, 26),
                b,
                26
            ));
        }

        return sb.ToString();
    }

    private static char Encode(char ch, int a, int b, int m)
    {
        if (!char.IsLetter(ch))
        {
            return ch;
        }

        var mod = (a * (ch - 'a') + b) % m;
        return (char)('a' + mod);
    }

    private static char Decode(char ch, int a, int b, int m)
    {
        if (!char.IsLetter(ch))
        {
            return ch;
        }

        var mod = (a * (ch - 'a' - b)) % m;
        return (char)('a' + mod + (mod < 0 ? m : 0));
    }

    private static bool IsCoPrime(int a, int b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return (a + b) == 1;
    }

    private static int ModularMultiplicativeInverse(int a, int m)
    {
        var x = 1;
        while (a * x % m != 1)
        {
            x++;
        }

        return x;
    }
}
