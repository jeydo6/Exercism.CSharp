using System;

internal static class Series
{
    public static string[] Slices(string numbers, int sliceLength)
    {
        if (sliceLength <= 0 || sliceLength > numbers.Length)
        {
            throw new ArgumentException(null, nameof(sliceLength));
        }

        var result = new string[numbers.Length - sliceLength + 1];
        for (var i = 0; i < result.Length; i++)
        {
            result[i] = numbers[i..(i + sliceLength)];
        }

        return result;
    }
}
