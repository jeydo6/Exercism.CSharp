internal static class ReverseString
{
    public static string Reverse(string input)
    {
        var chars = input.ToCharArray();
        for (var i = 0; i < chars.Length / 2; i++)
        {
            (chars[i], chars[^(i + 1)]) = (chars[^(i + 1)], chars[i]);
        }

        return new string(chars);
    }
}
