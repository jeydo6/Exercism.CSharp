using System.Collections.Generic;

internal static class PythagoreanTriplet
{
    public static IEnumerable<(int a, int b, int c)> TripletsWithSum(int sum)
    {
        var result = new List<(int a, int b, int c)>();
        
        for (var a = 0; a <= sum; a++)
        {
            for (var b = 0; b <= sum; b++)
            {
                var c = sum - (a + b);

                if (a < b && b < c && a * a + b * b == c * c)
                {
                    result.Add((a, b, c));
                }
            }
        }

        return result.ToArray();
    }
}
