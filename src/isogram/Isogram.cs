internal static class Isogram
{
    public static bool IsIsogram(string word)
    {
        var letters = new int[26];
        for (var i = 0; i < word.Length; i++)
        {
            var ch = word[i];
            if (!char.IsLetter(ch))
            {
                continue;
            }

            var letterIndex = char.ToLower(ch) - 'a';
            if (letters[letterIndex] > 0)
            {
                return false;
            }

            letters[letterIndex]++;
        }

        return true;
    }
}
