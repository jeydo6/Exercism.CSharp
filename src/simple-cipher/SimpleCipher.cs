using System;

internal class SimpleCipher
{
    private static readonly Random _random = new Random();

    public SimpleCipher()
    {
        var key = new char[100];
        for (var i = 0; i < key.Length; i++)
        {
            key[i] = (char)('a' + _random.Next(26));
        }

        Key = new string(key);
    }

    public SimpleCipher(string key) => Key = key;

    public string Key { get; }

    public string Encode(string plaintext)
    {
        var result = new char[plaintext.Length];
        for (var i = 0; i < plaintext.Length; i++)
        {
            var shift = plaintext[i] + Key[i % Key.Length] - 2 * 'a';
            if (shift >= 26)
            {
                shift -= 26;
            }
            result[i] = (char)('a' + shift);
        }

        return new string(result);
    }

    public string Decode(string ciphertext)
    {
        var result = new char[ciphertext.Length];
        for (var i = 0; i < ciphertext.Length; i++)
        {
            var shift = ciphertext[i] - Key[i % Key.Length];
            if (shift < 0)
            {
                shift += 26;
            }
            result[i] = (char)('a' + shift);
        }

        return new string(result);
    }
}
