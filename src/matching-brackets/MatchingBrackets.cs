using System.Linq;

internal static class MatchingBrackets
{
    public static bool IsPaired(string input)
    {
        var brackets = new string(input.Where(ch => "[]{}()".Contains(ch)).ToArray());
        
        var length = brackets.Length;
        while (brackets.Length > 0)
        {
            brackets = brackets
                .Replace("[]", "")
                .Replace("{}", "")
                .Replace("()", "");

            if (brackets.Length == length)
            {
                return false;
            }

            length = brackets.Length;
        }

        return true;
    }
}
