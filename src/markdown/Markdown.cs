using System;
using System.Linq;
using System.Text.RegularExpressions;

internal static class Markdown
{
    private const string HeaderMarkdown = "#";
    private const string BoldMarkdown = "__";
    private const string ItalicMarkdown = "_";
    private const string ListItemMarkdown = "*";

    private const string BoldTag = "strong";
    private const string ItalicTag = "em";
    private const string ParagraphTag = "p";
    private const string ListTag = "ul";
    private const string ListItemTag = "li";
    
    private static string OpeningTag(string tag) => $"<{tag}>";
    private static string ClosingTag(string tag) => $"</{tag}>";

    private static string Wrap(this string text, string tag) => OpeningTag(tag) + text + ClosingTag(tag);
    
    private static string ParseDelimited(this string markdown, string delimiter, string tag)
    {
        var pattern = $"{delimiter}(.+){delimiter}";
        var replacement = "$1".Wrap(tag);
        
        return Regex.Replace(markdown, pattern, replacement);
    }
    
    private static string ParseBold(this string markdown) => markdown.ParseDelimited(BoldMarkdown, BoldTag);
    
    private static string ParseItalic(this string markdown) => markdown.ParseDelimited(ItalicMarkdown, ItalicTag);

    private static string ParseText(this string markdown, bool list)
    {
        var textHtml = markdown
            .ParseBold()
            .ParseItalic();

        return list ? textHtml : textHtml.Wrap(ParagraphTag);
    }

    private static (bool List, string Html)? ParseHeader(this string markdown, bool list)
    {
        var headerNumber = markdown
            .TakeWhile(c => c == HeaderMarkdown[0])
            .Count();

        if (headerNumber == 0)
        {
            return null;
        }

        var headerTag = $"h{headerNumber}";
        var headerHtml = markdown[(headerNumber + 1)..].Wrap(headerTag);
        var html = list ? ClosingTag(ListTag) + headerHtml : headerHtml;

        return (false, html);
    }

    private static (bool List, string Html)? ParseLineItem(this string markdown, bool list)
    {
        if (!markdown.StartsWith(ListItemMarkdown))
        {
            return null;
        }

        var innerHtml = markdown[2..]
            .ParseText(true)
            .Wrap(ListItemTag);

        var html = list ? innerHtml : OpeningTag(ListTag) + innerHtml;
        return (true, html);
    }

    private static (bool List, string Html)? ParseParagraph(this string markdown, bool list)
    {
        var html = list ? ClosingTag(ListTag) + markdown.ParseText(false) : markdown.ParseText(false);
        
        return (false, html);
    }

    private static (bool List, string Html) ParseLine(this string markdown, (bool List, string Html) accumulator)
    {
        var (list, html) = accumulator;

        var result =
            markdown.ParseHeader(list) ??
            markdown.ParseLineItem(list) ??
            markdown.ParseParagraph(list);

        if (result == null)
        {
            throw new ArgumentException("Invalid markdown");
        }

        return (result.Value.List, html + result.Value.Html);
    }

    public static string Parse(this string markdown)
    {
        var (list, html) = markdown
            .Split('\n')
            .Aggregate((false, ""), (acc, m) => m.ParseLine(acc));

        return list ? html + ClosingTag(ListTag) : html;
    }
}
