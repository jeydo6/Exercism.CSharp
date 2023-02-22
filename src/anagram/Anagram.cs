using System;
using System.Collections.Generic;
using System.Linq;

internal class Anagram
{
    private readonly string _baseWord;
    private readonly int[] _chars;

    public Anagram(string baseWord) => (_baseWord, _chars) = (baseWord, GetChars(baseWord));

    public string[] FindAnagrams(string[] potentialMatches) => potentialMatches.Where(IsAnagram).ToArray();

    private bool IsAnagram(string potentialMatch)
    {
        if (string.Compare(_baseWord, potentialMatch, StringComparison.OrdinalIgnoreCase) == 0)
            return false;
        
        var chars = GetChars(potentialMatch);
        for (var j = 0; j < 26; j++)
        {
            if (chars[j] != _chars[j])
                return false;
        }

        return true;
    }

    private static int[] GetChars(string word)
    {
        var chars = new int[26];
        for (var i = 0; i < word.Length; i++)
        {
            if (!char.IsLetter(word[i]))
            {
                continue;
            }
            
            chars[char.ToLower(word[i]) - 'a']++;
        }

        return chars;
    }
}
