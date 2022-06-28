using System;

internal static class Hamming
{
    public static int Distance(string firstStrand, string secondStrand)
    {
        if (firstStrand.Length != secondStrand.Length)
        {
            throw new ArgumentException("The length should be equal.");
        }

        var result = 0;

        var length = firstStrand.Length;
        for (var i = 0; i < length; i++)
        {
            if (firstStrand[i] != secondStrand[i])
            {
                result++;
            }
        }

        return result;
    }
}
