using System;

internal enum Classification
{
    Perfect,
    Abundant,
    Deficient
}

internal static class PerfectNumbers
{
    public static Classification Classify(int number)
    {
        if (number <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(number));
        }
        
        var sum = 0;
        for (var factor = 1; factor < number; factor++)
        {
            if (number % factor == 0)
            {
                sum += factor;
            }
        }

        return sum == number ? Classification.Perfect :
            sum < number ? Classification.Deficient :
            Classification.Abundant;
    }
}
