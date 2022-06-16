using System.Linq;

internal static class Bob
{
    public static string Response(string statement)
    {
        statement = statement.Trim();
        
        if (IsEmpty(statement))
        {
            return "Fine. Be that way!";
        }
        
        if (statement.IsUpper())
        {
            return statement.IsQuestion() ? "Calm down, I know what I'm doing!" : "Whoa, chill out!";
        }
        
        return statement.IsQuestion() ? "Sure." : "Whatever.";
    }

    private static bool IsUpper(this string statement) => statement.Any(char.IsUpper) && !statement.Any(char.IsLower);

    private static bool IsQuestion(this string statement) => statement[^1] == '?';

    private static bool IsEmpty(this string statement) => statement == "";
}
