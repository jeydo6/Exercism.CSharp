using System;
using System.Collections.Generic;
using System.Linq;

internal static class Change
{
    public static int[] FindFewestCoins(int[] coins, int target)
    {
        if (target < 0)
        {
            throw new ArgumentException(null, nameof(target));
        }
        
        if (target > 0 && target < coins.Min())
        {
            throw new ArgumentException(null, nameof(target));
        }
        
        var minimalCoinsMap = Enumerable.Range(1, target)
            .Aggregate(
                new Dictionary<int, List<int>>
                {
                    [0] = new List<int>(0)
                },
                (current, subTarget) =>
                {
                    var subTargetMinimalCoins = coins
                        .Where(coin => coin <= subTarget)
                        .Select(coin => current.TryGetValue(subTarget - coin, out var subTargetMinimalCoins)
                            ? subTargetMinimalCoins.Append(coin).ToList()
                            : null)
                        .Where(subTargetMinimalCoins => subTargetMinimalCoins != null)
                        .MinBy(subTargetMinimalCoins => subTargetMinimalCoins.Count);

                    if (subTargetMinimalCoins != null)
                    {
                        current.Add(subTarget, subTargetMinimalCoins);
                    }

                    return current;
                }
            );

        if (minimalCoinsMap.TryGetValue(target, out var minimalCoins))
        {
            return minimalCoins.OrderBy(coin => coin).ToArray();
        }

        throw new ArgumentException(null, nameof(target));
    }
}
