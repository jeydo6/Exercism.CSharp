using System;
using System.Text.RegularExpressions;

internal class LogParser
{
    public bool IsValidLine(string text) =>
        Regex.IsMatch(text, @"^(\[TRC\] | \[DBG\] | \[INF\] | \[ERR\] | \[WRN\] | \[FTL\])", RegexOptions.IgnorePatternWhitespace);

    public string[] SplitLogLine(string text) =>
        Regex.Split(text, "<[*^=-]*>");

    public int CountQuotedPasswords(string lines) =>
        Regex.Matches(lines, @""".*password.*""", RegexOptions.IgnoreCase | RegexOptions.Multiline).Count;

    public string RemoveEndOfLineText(string line) =>
        Regex.Replace(line, @"end-of-Line\d+", string.Empty, RegexOptions.IgnoreCase);

    public string[] ListLinesWithPasswords(string[] lines)
    {
        var outlines = new string[lines.Length];
        var regex = new Regex(@".*\s(?<pw>password\S+).*", RegexOptions.IgnoreCase);
        for (var i = 0; i < lines.Length; i++)
        {
            var matches = regex.Matches(lines[i]);
            outlines[i] = matches.Count > 0 ? $"{matches[0].Groups["pw"].Value}: {lines[i]}" : $"--------: {lines[i]}";
        }

        return outlines;
    }
}
