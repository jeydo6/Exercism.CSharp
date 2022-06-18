using System;
using System.Collections.Generic;

internal static class Proverb
{
    public static string[] Recite(string[] subjects)
    {
        if (subjects.Length == 0)
        {
            return Array.Empty<string>();
        }
        
        var result = new List<string>();
        for (var i = 0; i < subjects.Length - 1; i++)
        {
            result.Add($"For want of a {subjects[i]} the {subjects[i + 1]} was lost.");
        }
        result.Add($"And all for the want of a {subjects[0]}.");

        return result.ToArray();
    }
}
