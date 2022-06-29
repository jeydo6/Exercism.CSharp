using System;

internal static class RnaTranscription
{
    public static string ToRna(string nucleotide)
    {
        var result = new char[nucleotide.Length];

        for (var i = 0; i < nucleotide.Length; i++)
        {
            result[i] = nucleotide[i] switch
            {
                'G' => 'C',
                'C' => 'G',
                'T' => 'A',
                'A' => 'U',
                _ => throw new ArgumentOutOfRangeException(nameof(nucleotide))
            };
        }

        return new string(result);
    }
}
