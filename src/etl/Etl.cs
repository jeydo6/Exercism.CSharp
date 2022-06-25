using System.Collections.Generic;

internal static class Etl
{
    public static Dictionary<string, int> Transform(Dictionary<int, string[]> old)
    {
        var result = new Dictionary<string, int>();
        foreach (var (score, letters) in old)
        {
            foreach (var letter in letters)
            {
                result[letter.ToLower()] = score;
            }
        }

        return result;
    }
}
