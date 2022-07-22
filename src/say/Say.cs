using System;
using System.Collections.Generic;

internal static class Say
{
    public static string InEnglish(long number) => number switch
    {
        < 0L or >= 1000000000000L => throw new ArgumentOutOfRangeException(nameof(number)),
        0L => "zero",
        _ => string.Join(" ", Parts(number))
    };

    private static string[] Parts(long number)
    {
        var (billionsCount, millionsCount, thousandsCount, remainder) = Counts(number);

        var billions = Billions(billionsCount);
        var millions = Millions(millionsCount);
        var thousands = Thousands(thousandsCount);
        var hundreds = Hundreds(remainder);

        var result = new List<string>();
        foreach (var str in new string[] { billions, millions, thousands, hundreds })
        {
            if (str != null)
            {
                result.Add(str);
            }
        }
        return result.ToArray();
    }
    
    private static string Bases(long number)
    {
        var values = new string[]
        {
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine",
            "ten",
            "eleven",
            "twelve",
            "thirteen",
            "fourteen",
            "fifteen",
            "sixteen",
            "seventeen",
            "eighteen",
            "nineteen"
        };

        return number > 0 && number <= values.Length ? values[number - 1] : null;
    }

    private static string Tens(long number)
    {
        if (number < 20L)
        {
            return Bases(number);
        }

        var values = new string[]
        {
            "twenty",
            "thirty",
            "forty",
            "fifty",
            "sixty",
            "seventy",
            "eighty",
            "ninety"
        };

        var count = number / 10L;
        var remainder = number % 10L;
        var bases = Bases(remainder);

        var countStr = values[count - 2];
        var basesStr = bases == null ? "" : $"-{bases}";

        return $"{countStr}{basesStr}";
    }
    
    private static string Hundreds(long number)
    {
        if (number < 100L)
        {
            return Tens(number);
        }
        
        var count = number / 100L;
        var remainder = number % 100L;
        var bases = Bases(count);
        var tens = Tens(remainder);
        var tensStr = tens == null ? "" : $" {tens}";
        
        return $"{bases} hundred{tensStr}";
    }
    
    private static string Chunk(string str, long number)
    {
        var hundreds = Hundreds(number);
        return hundreds == null ? null : $"{hundreds} {str}";
    }

    private static string Thousands(long number) => Chunk("thousand", number);
    private static string Millions(long number) => Chunk("million", number);
    private static string Billions(long number) => Chunk("billion", number);
    
    private static (long BillionsCount, long MillionsCount, long ThousandsCount, long ThousandsRemainder) Counts(long number)
    {
        var billionsCount = number / 1000000000L;
        var billionsRemainder = number % 1000000000L;

        var millionsCount = billionsRemainder / 1000000L;
        var millionsRemainder = billionsRemainder % 1000000L;

        var thousandsCount = millionsRemainder / 1000L;
        var thousandsRemainder = millionsRemainder % 1000L;

        return (billionsCount, millionsCount, thousandsCount, thousandsRemainder);
    }
}
