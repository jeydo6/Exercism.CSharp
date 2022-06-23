using System;
using System.Collections.Generic;

internal static class PrimeFactors
{
    public static long[] Factors(long number)
    {
        var result = new List<long>();

        var factor = 2L;
        while (factor <= number)
        {
            if (number % factor == 0 && IsPrime(factor))
            {
                result.Add(factor);
                number /= factor;
            }
            else
            {
                factor++;
            }
        }

        return result.ToArray();
    }

    private static bool IsPrime(long factor)
    {
        if (factor < 2)
        {
            return false;
        }

        var i = 2L;
        while (i <= (long)Math.Sqrt(factor))
        {
            if (factor % i == 0)
            {
                return false;
            }

            i++;
        }

        return true;
    }
}
