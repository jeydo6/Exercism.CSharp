using System;
using System.Text;

internal static class Acronym
{
    public static string Abbreviate(string phrase)
    {
        var words = phrase.Split(
            new char[] { ' ', '-', '_' },
            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries
        );

        var acronym = new StringBuilder(words.Length);
        foreach (var word in words)
        {
            var ch = char.ToUpper(word[0]);
            acronym.Append(ch);
        }

        return acronym.ToString();
    }
}
