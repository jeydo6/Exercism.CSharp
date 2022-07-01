using System;
using System.Collections.Generic;

internal static class Strain
{
    public static IEnumerable<T> Keep<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
    {
        var result = new List<T>();
        foreach (var item in collection)
        {
            if (predicate(item))
            {
                result.Add(item);
            }
        }

        return result;
    }

    public static IEnumerable<T> Discard<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
    {
        var result = new List<T>();
        foreach (var item in collection)
        {
            if (!predicate(item))
            {
                result.Add(item);
            }
        }

        return result;    }
}
