using System;

internal static class CollatzConjecture
{
    public static int Steps(int number)
    {
        if (number <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(number));
        }
        
        var result = 0;
        
        while (number != 1)
        {
            result++;
            number = number % 2 == 0 ? number / 2 : 3 * number + 1;
        }

        return result;
    }
}
