using System.Collections.Generic;
using System.Threading.Tasks;

internal static class ParallelLetterFrequency
{
    public static Dictionary<char, int> Calculate(IEnumerable<string> texts)
    {
        var frequencies = new Dictionary<char, int>();
        Parallel.ForEach(texts, text =>
        {
            foreach (var ch in text)
            {
                if (!char.IsLetter(ch))
                {
                    continue;
                }

                lock (frequencies)
                {
                    var letter = char.ToLower(ch);
                    if (!frequencies.ContainsKey(letter))
                    {
                        frequencies[letter] = 0;
                    }

                    frequencies[letter]++;
                }
            }
        });
        
        return frequencies;
    }
}
