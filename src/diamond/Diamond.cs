using System.Linq;

internal static class Diamond
{
    public static string Make(char target)
    {
        var letters = GetLetters(target);

        var diamondLetters = letters.Concat(letters[..^1].Reverse()).ToArray();
        
        return string.Join("\n", diamondLetters.Select(diamondLetter => MakeLine(letters.Length, diamondLetter)));
    }
    
    private static (char Letter, int Row)[] GetLetters(char target) => Enumerable
        .Range('A', target - 'A' + 1)
        .Select((ch, i) => ((char)ch, i))
        .ToArray();
    
    private static string MakeLine(int letterCount, (char Letter, int Row) letterRow)
    {
        var (letter, row) = letterRow;
        var outerSpaces = "".PadRight(letterCount - row - 1);
        var innerSpaces = "".PadRight(row == 0 ? 0 : row * 2 - 1);

        return letter == 'A' ?
            $"{outerSpaces}{letter}{outerSpaces}" :
            $"{outerSpaces}{letter}{innerSpaces}{letter}{outerSpaces}";
    }
}
