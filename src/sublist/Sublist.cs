using System;
using System.Collections.Generic;

internal enum SublistType
{
    Equal,
    Unequal,
    Superlist,
    Sublist
}

internal static class Sublist
{
    public static SublistType Classify<T>(List<T> list1, List<T> list2)
        where T : IComparable =>
        (list1.Contains(list2), list2.Contains(list1)) switch
        {
            (true, true) => SublistType.Equal,
            (true, false) => SublistType.Superlist,
            (false, true) => SublistType.Sublist,
            (false, false) => SublistType.Unequal
        };

    private static bool Contains<T>(this List<T> list1, List<T> list2)
        where T : IComparable
    {
        if (list2.Count == 0)
        {
            return true;
        }

        if (list2.Count > list1.Count)
        {
            return false;
        }

        for (var i = 0; i < list1.Count; i++)
        {
            var isEqual = true;
            for (var j = 0; j < list2.Count; j++)
            {
                if (
                    list1[Math.Min(i + j, list1.Count - 1)]
                        .CompareTo(list2[j]) != 0
                )
                {
                    isEqual = false;
                    break;
                }
            }

            if (isEqual)
            {
                return true;
            }
        }

        return false;
    }
}
