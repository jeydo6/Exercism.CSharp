using System;
using System.Collections.Generic;

internal class Anagram
{
    private readonly string _baseWord;
    private readonly int[] _chars;

    public Anagram(string baseWord) => (_baseWord, _chars) = (baseWord, GetChars(baseWord));

    public string[] FindAnagrams(string[] potentialMatches)
    {
        var anagrams = new List<string>();
        for (var i = 0; i < potentialMatches.Length; i++)
        {
            if (string.Compare(_baseWord, potentialMatches[i], StringComparison.OrdinalIgnoreCase) == 0)
            {
                continue;
            }
            
            var chars = GetChars(potentialMatches[i]);
            var isAnagram = true;
            for (var j = 0; j < 26; j++)
            {
                if (chars[j] != _chars[j])
                {
                    isAnagram = false;
                    break;
                }
            }

            if (isAnagram)
            {
                anagrams.Add(potentialMatches[i]);
            }
        }

        return anagrams.ToArray();
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
