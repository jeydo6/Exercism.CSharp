using System;
using System.Collections.Generic;
using System.Linq;

internal static class NthPrime
{
    public static int Prime(int nth) =>
        nth >= 1 ?
        Primes().Skip(nth - 1).First() :
        throw new ArgumentOutOfRangeException(nameof(nth));
    
    private static IEnumerable<int> Primes()
    {
        yield return 2;
        yield return 3;

        foreach (var prime in PossiblePrimes().Where(IsPrime))
        {
            yield return prime;
        }
    }

    private static IEnumerable<int> PossiblePrimes()
    {
        var n = 6;
        while (true)
        {
            yield return n - 1;
            yield return n + 1;

            n += 6;
        }
    }

    private static bool IsPrime(int n)
    {
        var r = (int)Math.Floor(Math.Sqrt(n));
        return r < 5 || Enumerable.Range(5, r - 4).All(x => n % x != 0);
    }
}
