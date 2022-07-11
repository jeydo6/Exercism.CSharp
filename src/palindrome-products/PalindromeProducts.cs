using System;
using System.Collections.Generic;
using System.Linq;

internal static class PalindromeProducts
{
    public static (int, IEnumerable<(int,int)>) Largest(int minFactor, int maxFactor)
    {
        var result = new List<(int, int)>();
        
        var largestProduct = int.MinValue;
        for (var i = maxFactor; i >= minFactor; i--)
        {
            for (var j = i; j >= minFactor; j--)
            {
                var product = i * j;
                if (product < largestProduct || !IsPalindrome(i * j))
                {
                    continue;
                }
                
                if (product > largestProduct)
                {
                    largestProduct = product;
                    result.Clear();
                }
                
                result.Add((j, i));
            }
        }

        if (largestProduct == int.MinValue)
        {
            throw new ArgumentException(nameof(minFactor) + " | " + nameof(maxFactor));
        }
        
        return (largestProduct, result.ToArray());
    }

    public static (int, IEnumerable<(int,int)>) Smallest(int minFactor, int maxFactor)
    {
        var result = new List<(int, int)>();
        
        var smallestProduct = int.MaxValue;
        for (var i = minFactor; i <= maxFactor; i++)
        {
            for (var j = i; j <= maxFactor; j++)
            {
                var product = i * j;
                if (product > smallestProduct || !IsPalindrome(i * j))
                {
                    continue;
                }
                
                if (product < smallestProduct)
                {
                    smallestProduct = product;
                    result.Clear();
                }
                
                result.Add((j, i));
            }
        }

        if (smallestProduct == int.MaxValue)
        {
            throw new ArgumentException(nameof(minFactor) + " | " + nameof(maxFactor));
        }
        
        return (smallestProduct, result.ToArray());
    }

    private static bool IsPalindrome(int product)
    {
        var strProduct = product.ToString();
        for (var i = 0; i < strProduct.Length / 2; i++)
        {
            if (strProduct[i] != strProduct[^(i + 1)])
            {
                return false;
            }
        }

        return true;
    }
}
