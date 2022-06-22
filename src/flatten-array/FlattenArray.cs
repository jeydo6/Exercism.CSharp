using System.Collections;

internal static class FlattenArray
{
    public static IEnumerable Flatten(IEnumerable input)
    {
        var result = new ArrayList();
        foreach (var inputItem in input)
        {
            if (inputItem is IEnumerable enumerable)
            {
                foreach (var flattenedItem in Flatten(enumerable))
                {
                    result.Add(flattenedItem);
                }
            }
            else if (inputItem is not null)
            {
                result.Add(inputItem);
            }
        }
        return result.ToArray();
    }
}
