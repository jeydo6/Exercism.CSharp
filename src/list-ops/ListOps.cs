using System;
using System.Collections.Generic;

internal static class ListOps
{
    public static int Length<T>(List<T> input) =>
        Foldl(input, 0, (acc, _) => acc + 1);

    public static List<T> Reverse<T>(List<T> input) =>
        Foldr(input, new List<T>(), (item, acc) => Append(acc, item));

    public static List<TOut> Map<TIn, TOut>(List<TIn> input, Func<TIn, TOut> map) =>
        Foldl(input, new List<TOut>(), (acc, item) => Append(acc, map(item)));

    public static List<T> Filter<T>(List<T> input, Func<T, bool> predicate) =>
        Foldl(input, new List<T>(), (acc, item) => predicate(item) ? Append(acc, item) : acc);

    public static TOut Foldl<TIn, TOut>(List<TIn> input, TOut start, Func<TOut, TIn, TOut> func)
    {
        var acc = start;

        for (var i = 0; i < input.Count; i++)
        {
            acc = func(acc, input[i]);
        }

        return acc;
    }

    public static TOut Foldr<TIn, TOut>(List<TIn> input, TOut start, Func<TIn, TOut, TOut> func)
    {
        var acc = start;

        for (var i = input.Count - 1; i >= 0; i--)
        {
            acc = func(input[i], acc);
        }

        return acc;
    }

    public static List<T> Concat<T>(List<List<T>> input) =>
        Foldl(input, new List<T>(), Append);

    public static List<T> Append<T>(List<T> left, List<T> right)
    {
        var result = new T[left.Count + right.Count];

        for (var i = 0; i < left.Count; i++)
        {
            result[i] = left[i];
        }

        for (var j = 0; j < right.Count; j++)
        {
            result[left.Count + j] = right[j];
        }

        return new List<T>(result);
    }

    private static List<T> Append<T>(List<T> left, T right) =>
        new List<T>(left) { right };
}
