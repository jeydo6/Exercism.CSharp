using System;
using System.Collections.Generic;

internal class RailFenceCipher
{
    private readonly int _rails;
    
    public RailFenceCipher(int rails) => _rails = rails;

    public string Encode(string input)
    {
        var pattern = GetPattern(input.Length, _rails);
        var result = new char[input.Length];
        for (var i = 0; i < input.Length; i++)
        {
            result[i] = input[pattern[i]];
        }

        return new string(result);
    }

    public string Decode(string input)
    {
        var pattern = GetPattern(input.Length, _rails);
        var result = new char[input.Length];
        for (var i = 0; i < input.Length; i++)
        {
            result[pattern[i]] = input[i];
        }

        return new string(result);
    }

    private static int[] GetPattern(int length, int rails)
    {
        var rows = new List<int>[rails];
        var period = 2 * (rails - 1);
        for (var i = 0; i < length; i++)
        {
            var rowIndex = Math.Min(i % period, period - i % period);

            rows[rowIndex] ??= new List<int>();
            rows[rowIndex].Add(i);
        }

        var result = new List<int>(length);
        for (var i = 0; i < rails; i++)
        {
            result.AddRange(rows[i]);
        }

        return result.ToArray();
    }
}
