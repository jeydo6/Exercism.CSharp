using System.Collections.Generic;
using System.Linq;

internal static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max)
    {
        var numbers = Enumerable
            .Range(1, max - 1)
            .ToArray();
    
        for (var i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] == 0 || multiples.Any(multiple => multiple > 0 && numbers[i] % multiple == 0))
            {
                continue;
            }
            
            numbers[i] = 0;
        }
    
        return numbers.Sum();
    }
}
