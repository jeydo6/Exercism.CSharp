using System.Collections.Generic;
using System.Text;

internal static class WordCount
{
    private static readonly ISet<char> _allowed = new HashSet<char>
    {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        '\''
    };
    
    public static IDictionary<string, int> CountWords(string phrase)
    {
        var result = new Dictionary<string, int>();
        foreach (var word in GetWords(phrase))
        {
            if (!result.ContainsKey(word))
            {
                result[word] = 0;
            }

            result[word]++;
        }

        return result;
    }

    private static IEnumerable<string> GetWords(string phrase)
    {
        var result = new List<string>();

        var sb = new StringBuilder();
        foreach (var ch in phrase)
        {
            if (_allowed.Contains(char.ToLower(ch)))
            {
                sb.Append(char.ToLower(ch));
            }
            else
            {
                if (sb.Length > 0)
                {
                    var word = sb
                        .ToString()
                        .Trim('\'');
                
                    result.Add(word);
                    sb.Clear();
                }
            }
        }

        if (sb.Length > 0)
        {
            var word = sb
                .ToString()
                .Trim('\'');
                
            result.Add(word);
        }

        return result;
    }
}
