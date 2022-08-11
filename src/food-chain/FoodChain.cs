using System;
using System.Collections.Generic;

internal static class FoodChain
{
    private static readonly string[] Subjects =
    {
        "spider",
        "bird",
        "cat",
        "dog",
        "goat",
        "cow"
    };
    
    private static readonly string[] FollowUps =
    {
        "It wriggled and jiggled and tickled inside her.",
        "How absurd to swallow a bird!",
        "Imagine that, to swallow a cat!",
        "What a hog, to swallow a dog!",
        "Just opened her throat and swallowed a goat!",
        "I don't know how she swallowed a cow!"
    };
    
    private static readonly string[] Histories =
    {
        "I don't know how she swallowed a cow!",
        "She swallowed the cow to catch the goat.",
        "She swallowed the goat to catch the dog.",
        "She swallowed the dog to catch the cat.",
        "She swallowed the cat to catch the bird.",
        "She swallowed the bird to catch the spider that wriggled and jiggled and tickled inside her.",
        "She swallowed the spider to catch the fly.",
        "I don't know why she swallowed the fly. Perhaps she'll die."
    };

    public static string Recite(int verseNumber) => Recite(verseNumber, verseNumber);

    public static string Recite(int startVerse, int endVerse)
    {
        var verses = new List<string>();
        for (var verseNumber = startVerse; verseNumber <= endVerse; verseNumber++)
        {
            verses.Add($"{Start(verseNumber)}\n{End(verseNumber)}");
        }

        return string.Join("\n\n", verses);
    }
    
    private static string Start(int verseNumber) => verseNumber switch
    {
        1 => "I know an old lady who swallowed a fly.",
        8 => "I know an old lady who swallowed a horse.",
        _ => $"I know an old lady who swallowed a {Subjects[verseNumber - 2]}.\n{FollowUps[verseNumber - 2]}"
    };
    
    private static string End(int verseNumber) => verseNumber == 8 ?
        "She's dead, of course!" :
        string.Join("\n", Histories[^verseNumber..]);
}
