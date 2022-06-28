using System;
using System.Collections.Generic;

internal static class Sieve
{
    public static int[] Primes(int limit)
    {
        switch (limit)
        {
            case < 0:
                throw new ArgumentOutOfRangeException(nameof(limit));
            case < 2:
                return Array.Empty<int>();
        }

        var numbers = new int[limit - 1];
        for (var i = 0; i < numbers.Length; i++)
        {
            numbers[i] = i + 2;
        }
        
        for (var step = 2; step <= Math.Sqrt(limit); step++)
        {
            for (var i = 2 * step - 2; i < numbers.Length; i += step)
            {
                if (numbers[i] > 0)
                {
                    numbers[i] = 0;
                }
            }
        }

        var result = new List<int>();
        for (var i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] > 0)
            {
                result.Add(numbers[i]);
            }
        }

        return result.ToArray();
    }
}
