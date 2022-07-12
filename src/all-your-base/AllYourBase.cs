using System;
using System.Collections.Generic;

internal static class AllYourBase
{
    public static int[] Rebase(int inputBase, int[] inputDigits, int outputBase) =>
        GetDigits(
            outputBase,
            GetNumber(inputBase, inputDigits)
        );

    private static int GetNumber(int @base, IReadOnlyList<int> digits)
    {
        if (@base < 2)
        {
            throw new ArgumentException(null, nameof(@base));
        }

        if (digits == null || digits.Count == 0)
        {
            return 0;
        }
        
        var result = 0;
        
        var power = 1;
        for (var i = 0; i < digits.Count; i++)
        {
            if (digits[^(i + 1)] < 0 || digits[^(i + 1)] >= @base)
            {
                throw new ArgumentException(null, nameof(digits));
            }
            
            result += digits[^(i + 1)] * power;
            power *= @base;
        }

        return result;
    }

    private static int[] GetDigits(int @base, int number)
    {
        if (@base < 2)
        {
            throw new ArgumentException(null, nameof(@base));
        }

        if (number == 0)
        {
            return new int[] { 0 };
        }
        
        var result = new List<int>();

        while (number > 0)
        {
            result.Add(number % @base);
            number /= @base;
        }

        result.Reverse();
        return result.ToArray();
    }
}
