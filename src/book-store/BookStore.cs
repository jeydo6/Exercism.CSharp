using System;
using System.Collections.Generic;

internal static class BookStore
{
    private const decimal Price = 8m;
    
    public static decimal Total(IEnumerable<int> books)
    {
        var counts = new int[5];
        foreach (var book in books)
        {
            counts[book - 1]++;
        }

        var minCost = decimal.MaxValue;
        for (var i = -1; i < counts.Length; i++)
        {
            var (group1, group2) = GetGroups(counts, i);

            var cost = GetCost(group1) + GetCost(group2);

            if (cost < minCost)
            {
                minCost = cost;
            }
        }

        return minCost;
    }

    private static (IList<int> Group1, IList<int> Group2) GetGroups(IList<int> counts, int index)
    {
        if (index < 0)
        {
            return (counts, new int[5]);
        }
        
        var group1 = new int[5];
        var group2 = new int[5];

        for (var i = 0; i < counts.Count; i++)
        {
            group1[i] = Math.Min(counts[i], counts[index]);
            group2[i] = Math.Max(counts[i] - counts[index], 0);
        }

        group2[index] += group1[index];
        group1[index] -= group1[index];

        return (group1, group2);
    }
    
    private static decimal GetCost(IEnumerable<int> counts)
    {
        var copy = new List<int>(counts);
        var cost = 0m;
        while (true)
        {
            var size = 0;
            for (var i = 0; i < copy.Count; i++)
            {
                if (copy[i] == 0)
                {
                    continue;
                }
        
                size++;
                copy[i]--;
            }

            if (size == 0)
            {
                break;
            }
        
            cost += Price * size * GetDiscount(size);
        }
        
        return cost;
    }
    
    private static decimal GetDiscount(int size) => size switch
    {
        1 => 1.00m,
        2 => 0.95m,
        3 => 0.90m,
        4 => 0.80m,
        5 => 0.75m,
        _ => 0.00m
    };
}
