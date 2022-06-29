using System;
using System.Collections.Generic;

internal static class NucleotideCount
{
    public static IDictionary<char, int> Count(string sequence)
    {
        var result = new Dictionary<char, int>
        {
            ['A'] = 0,
            ['C'] = 0,
            ['T'] = 0,
            ['G'] = 0
        };

        for (var i = 0; i < sequence.Length; i++)
        {
            if (!result.ContainsKey(sequence[i]))
            {
                throw new ArgumentException(null, nameof(sequence));
            }

            result[sequence[i]]++;
        }

        return result;
    }
}
