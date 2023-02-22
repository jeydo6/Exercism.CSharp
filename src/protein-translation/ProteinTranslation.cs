using System;
using System.Collections.Generic;
using System.Linq;

internal static class ProteinTranslation
{
    public static string[] Proteins(string strand)
    {
        var terminatingCodons = new HashSet<string> { "UAA", "UAG", "UGA" };
        return strand.Chunk(3).Select(x => new string(x))
            .TakeWhile(i => !terminatingCodons.Contains(i))
            .Select(i => i switch
            {
                "AUG" => "Methionine",
                "UUU" or "UUC" => "Phenylalanine",
                "UUA" or "UUG" => "Leucine",
                "UCU" or "UCC" or "UCA" or "UCG" => "Serine",
                "UAU" or "UAC" => "Tyrosine",
                "UGU" or "UGC" => "Cysteine",
                "UGG" => "Tryptophan",
                _ => throw new ArgumentOutOfRangeException()
            }).ToArray();
    }
}
