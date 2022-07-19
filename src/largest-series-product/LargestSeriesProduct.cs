using System;

internal static class LargestSeriesProduct
{
    public static long GetLargestProduct(string digits, int span) 
    {
        if (span == 0)
        {
            return 1;
        }

        if (span < 0 || span > digits.Length)
        {
            throw new ArgumentException(null, nameof(span));
        }

        if (digits.Length == 0)
        {
            throw new ArgumentException(null, nameof(digits));
        }
        
        foreach (var digit in digits)
        {
            if (!char.IsDigit(digit))
            {
                throw new ArgumentException(null, nameof(digits));
            }
        }
        
        var maxProduct = int.MinValue;
        for (var i = 0; i < digits.Length - span + 1; i++)
        {
            var product = 1;
            for (var j = 0; j < span; j++)
            {
                product *= digits[i + j] - '0';
            }

            if (product > maxProduct)
            {
                maxProduct = product;
            }
        }

        return maxProduct;
    }
}
