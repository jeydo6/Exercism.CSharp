using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

internal static class PigLatin
{
    public static string Translate(string word)
    {
        var result = new List<string>();

        var words = word.Split(' ');
        for (var i = 0; i < words.Length; i++)
        {
            result.Add(TranslateWord(words[i]));
        }

        return string.Join(" ", result);
    }

    private static string TranslateWord(string word)
    {
        if (WordStartsWithVowelLike(word))
        {
            return word + "ay";
        }

        if (WordStartsWithPrefixes(word, "thr", "sch"))
        {
            return word[3..] + word[..3] + "ay";
        }

        if (WordStartsWithPrefixes(word, "ch", "qu", "th", "rh"))
        {
            return word[2..] + word[..2] + "ay";
        }

        if (WordStartsWithConsonantAndQu(word))
        {
            return word[3..] + word[0] + "quay";
        }

        return word[1..] + word[0] + "ay";
    }

    private static bool WordStartsWithVowelLike(string word) =>
        Regex.IsMatch("[aeiou]", word[0].ToString()) ||
        word.StartsWith("yt") ||
        word.StartsWith("xr");

    private static bool WordStartsWithPrefixes(string word, params string[] prefixes) =>
        Array.Exists(prefixes, word.StartsWith);

    private static bool WordStartsWithConsonantAndQu(string word) =>
        word[1..].StartsWith("qu");
}
