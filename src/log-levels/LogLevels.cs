using System;

internal static class LogLine
{
    public static string Message(string logLine) => logLine.SubstringAfter(": ").Trim();

    public static string LogLevel(string logLine) => logLine.SubstringBetween("[", "]").ToLower();

    public static string Reformat(string logLine) => $"{Message(logLine)} ({LogLevel(logLine)})";
    
    private static string SubstringAfter(this string str, string delimiter)
    {
        var startIndex = str.IndexOf(delimiter, StringComparison.Ordinal) + delimiter.Length;
        return str[startIndex..];
    }
    
    private static string SubstringBetween(this string str, string startDelimiter, string endDelimiter)
    {
        var startIndex = str.IndexOf(startDelimiter, StringComparison.Ordinal) + startDelimiter.Length;
        var endIndex = str.IndexOf(endDelimiter, StringComparison.Ordinal);
        return str[startIndex..endIndex];
    }
}
