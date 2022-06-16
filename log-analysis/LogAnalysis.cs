using System;

internal static class LogAnalysis 
{
    public static string SubstringAfter(this string str, string delimiter)
    {
        var startIndex = str.IndexOf(delimiter, StringComparison.Ordinal) + delimiter.Length;
        return str[startIndex..];
    }
    
    public static string SubstringBetween(this string str, string startDelimiter, string endDelimiter)
    {
        var startIndex = str.IndexOf(startDelimiter, StringComparison.Ordinal) + startDelimiter.Length;
        var endIndex = str.IndexOf(endDelimiter, StringComparison.Ordinal);
        return str[startIndex..endIndex];
    }

    public static string Message(this string logLine) => logLine.SubstringAfter(": ").Trim();

    public static string LogLevel(this string logLine) => logLine.SubstringBetween("[", "]");
}
