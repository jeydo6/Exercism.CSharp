using System;

internal static class Wordy
{
    public static int Answer(string question)
    {
        if (!question.StartsWith("What is ") || question.Length <= "What is ".Length)
        {
            throw new ArgumentException(null, nameof(question));
        }

        var parts = question[8..^1]
            .Replace("plus", "+")
            .Replace("minus", "-")
            .Replace("multiplied by", "*")
            .Replace("divided by", "/")
            .Split();

        if (parts.Length % 2 == 0)
        {
            throw new ArgumentException(null, nameof(question));
        }

        if (!int.TryParse(parts[0], out var result))
        {
            throw new ArgumentException(null, nameof(question));
        }
        
        for (var i = 1; i < parts.Length; i += 2)
        {
            var op = parts[i];
            if (!int.TryParse(parts[i + 1], out var value))
            {
                throw new ArgumentException(null, nameof(question));
            }
            
            result = op switch
            {
                "+" => result + value,
                "-" => result - value,
                "*" => result * value,
                "/" => result / value,
                _ => throw new ArgumentException(null, nameof(question))
            };
        }

        return result;
    }
}
