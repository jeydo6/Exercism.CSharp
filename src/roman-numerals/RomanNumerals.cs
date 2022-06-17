using System.Collections.Generic;
using System.Text;

internal static class RomanNumeralExtension
{
    private static readonly IDictionary<string, int> _romanInts = new Dictionary<string, int>
    {
        ["M"] = 1000,
        ["CM"] = 900,
        ["D"] = 500,
        ["CD"] = 400,
        ["C"] = 100,
        ["XC"] = 90,
        ["L"] = 50,
        ["XL"] = 40,
        ["X"] = 10,
        ["IX"] = 9,
        ["V"] = 5,
        ["IV"] = 4,
        ["I"] = 1
    };
    
    public static string ToRoman(this int value)
    {
        var sb = new StringBuilder();
        foreach (var pair in _romanInts)
        {
            if (value <= 0)
            {
                break;
            }

            while (pair.Value <= value)
            {
                value -= pair.Value;
                sb.Append(pair.Key);
            }
        }
        return sb.ToString();
    }
}
