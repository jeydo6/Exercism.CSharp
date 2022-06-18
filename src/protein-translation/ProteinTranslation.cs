using System.Collections.Generic;

internal static class ProteinTranslation
{
    private static readonly IDictionary<string, string> _translations = new Dictionary<string, string>
    {
        ["AUG"] = "Methionine",
        ["UUU"] = "Phenylalanine",
        ["UUC"] = "Phenylalanine",
        ["UUA"] = "Leucine",
        ["UUG"] = "Leucine",
        ["UCU"] = "Serine",
        ["UCC"] = "Serine",
        ["UCA"] = "Serine",
        ["UCG"] = "Serine",
        ["UAU"] = "Tyrosine",
        ["UAC"] = "Tyrosine",
        ["UGU"] = "Cysteine",
        ["UGC"] = "Cysteine",
        ["UGG"] = "Tryptophan",
        ["UAA"] = "STOP",
        ["UAG"] = "STOP",
        ["UGA"] = "STOP"
    };
    
    public static string[] Proteins(string strand)
    {
        var proteins = new List<string>();
        
        var i = 0;
        while (i < strand.Length && _translations[strand.Substring(i, 3)] != "STOP")
        {
            var codon = strand.Substring(i, 3);
            proteins.Add(_translations[codon]);
            i += codon.Length;
        }

        return proteins.ToArray();
    }
}
