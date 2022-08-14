using System;
using System.Collections.Generic;

public static class House
{
    private static readonly string[] Subjects =
    {
        "house that Jack built",
        "malt",
        "rat",
        "cat",
        "dog",
        "cow with the crumpled horn",
        "maiden all forlorn",
        "man all tattered and torn",
        "priest all shaven and shorn",
        "rooster that crowed in the morn",
        "farmer sowing his corn",
        "horse and the hound and the horn"
    };

    private static readonly string[] Verbs =
    {
        "lay in",
        "ate",
        "killed",
        "worried",
        "tossed",
        "milked",
        "kissed",
        "married",
        "woke",
        "kept",
        "belonged to",
        ""
    };

    public static string Recite(int verseNumber) => Recite(verseNumber, verseNumber);

    public static string Recite(int startVerse, int endVerse)
    {
        var verses = new List<string>();
        for (var verseNumber = startVerse; verseNumber <= endVerse; verseNumber++)
        {
            verses.Add(Verse(verseNumber));
        }

        return string.Join('\n', verses);
    }
    
    private static string Verse(int verseNumber)
    {
        var lines = new List<string>();
        for (var i = verseNumber; i >= 1; i--)
        {
            lines.Add(Line(verseNumber, i));
        }
        
        return string.Join(" ", lines);
    }

    private static string Line(int verseNumber, int index)
    {
        var subject = Subjects[index - 1];
        var verb = Verbs[index - 1];
        var ending = index == 1 ? "." : "";

        return index == verseNumber ? $"This is the {subject}{ending}" : $"that {verb} the {subject}{ending}";
    }
}
