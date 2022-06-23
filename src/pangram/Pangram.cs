internal static class Pangram
{
    public static bool IsPangram(string input)
    {
        var letters = new bool[26];
        var count = 0;

        for (var i = 0; i < input.Length; i++)
        {
            var ch = input[i];
            if (!char.IsLetter(ch))
            {
                continue;
            }

            var letterIndex = char.ToLower(ch) - 'a';
            if (letters[letterIndex])
            {
                continue;
            }

            letters[letterIndex] = true;
            count++;
        }

        return count == letters.Length;
    }
}
