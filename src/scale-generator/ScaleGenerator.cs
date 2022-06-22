using System;
using System.Collections.Generic;

internal static class ScaleGenerator
{
    private static readonly string[] _sharps = new string[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
    private static readonly string[] _flats = new string[] { "F", "Gb", "G", "Ab", "A", "Bb", "B", "C", "Db", "D", "Eb", "E" };
    private static readonly string[] _usesFlats = new string[] { "F", "Bb", "Eb", "Ab", "Db", "Gb", "d", "g", "c", "f", "bb", "eb" };

    private static readonly IDictionary<char, int> _steps = new Dictionary<char, int>
    {
        ['m'] = 1,
        ['M'] = 2,
        ['A'] = 3
    };

    public static string[] Chromatic(string tonic) => Array.IndexOf(_usesFlats, tonic) >= 0 ? _flats : _sharps;
    
    public static string[] Interval(string tonic, string pattern)
    {
        var result = new string[pattern.Length];
        
        var chromatic = Chromatic(tonic);
        
        var current = char.ToUpper(tonic[0]) + tonic[1..];
        var position = Array.IndexOf(chromatic, current);
        for (var i = 0; i < pattern.Length; i++)
        {
            result[i] = chromatic[position];
            position = (position + _steps[pattern[i]]) % chromatic.Length;
        }

        return result;
    }
}
