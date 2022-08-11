using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

internal static class Grep
{
    private class Line
    {
        public int Number { get; set; }
        public string Text { get; set; }
        public string File { get; set; }
    }
    
    [Flags]
    private enum Flags
    {
        None = 0,
        PrintLineNumbers = 1,
        PrintFileNames = 2,
        CaseInsensitive = 4,
        Invert = 8,
        MatchEntireLines = 16
    }
    
    public static string Match(string pattern, string flags, string[] files)
    {
        var parsedFlags = ParseFlags(flags);
        
        return parsedFlags.HasFlag(Flags.PrintFileNames) ?
            FormatMatchingFiles(pattern, parsedFlags, files) :
            FormatMatchingLines(pattern, parsedFlags, files);
    }
    
    private static Flags ParseFlag(string flag) => flag switch
    {
        "-n" => Flags.PrintLineNumbers,
        "-l" => Flags.PrintFileNames,
        "-i" => Flags.CaseInsensitive,
        "-v" => Flags.Invert,
        "-x" => Flags.MatchEntireLines,
        _ => Flags.None
    };
    
    private static Flags ParseFlags(string flags) => flags
        .Split(' ')
        .Aggregate(Flags.None, (acc, flag) => acc | ParseFlag(flag));
    
    private static IEnumerable<Line> FindMatchingLines(string pattern, Flags flags, string file)
    {
        var matchPattern = flags.HasFlag(Flags.MatchEntireLines) ? $"^{pattern}$" : pattern;
        var options = flags.HasFlag(Flags.CaseInsensitive) ? RegexOptions.IgnoreCase : RegexOptions.None;
        var regex = new Regex(matchPattern, options);
        
        return File.ReadAllLines(file)
            .Select((line, index) => new Line { File = file, Number = index + 1, Text = line })
            .Where(line => regex.IsMatch(line.Text) != flags.HasFlag(Flags.Invert));
    }
    
    private static string FormatMatchingFile(string file) => file;

    private static string FormatMatchingFiles(string pattern, Flags flags, IEnumerable<string> files)
    {
        var matchingFiles = files
            .Where(file => FindMatchingLines(pattern, flags, file).Any())
            .Select(FormatMatchingFile);

        return string.Join("\n", matchingFiles);
    }
    
    private static string FormatMatchingLine(Flags flags, string[] files, Line line) => flags.HasFlag(Flags.PrintLineNumbers) switch
    {
        true when files.Length > 1 => $"{line.File}:{line.Number}:{line.Text}",
        false when files.Length > 1 => $"{line.File}:{line.Text}",
        true => $"{line.Number}:{line.Text}",
        _ => $"{line.Text}"
    };

    private static string FormatMatchingLines(string pattern, Flags flags, string[] files)
    {
        var matchingFiles = files
            .SelectMany(file => FindMatchingLines(pattern, flags, file))
            .Select(line => FormatMatchingLine(flags, files, line));

        return string.Join("\n", matchingFiles);
    }
}
