internal static class RotationalCipher
{
    public static string Rotate(string text, int shiftKey)
    {
        var result = new char[text.Length];
        for (var i = 0; i < text.Length; i++)
        {
            var ch = text[i];
            if (char.IsLetter(ch))
            {
                var current = char.ToLower(ch) - 'a';
                var shift = (current + shiftKey) % 26 - current;
                result[i] = (char)(ch + shift);
            }
            else
            {
                result[i] = ch;
            }
        }

        return new string(result);
    }
}
