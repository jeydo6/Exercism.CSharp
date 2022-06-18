internal static class BinarySearch
{
    public static int Find(int[] input, int value)
    {
        var lo = 0;
        var hi = input.Length - 1;
        while (lo <= hi)
        {
            var mid = lo + (hi - lo) / 2;
            if (input[mid] < value)
            {
                lo = mid + 1;
            }
            else if (input[mid] > value)
            {
                hi = mid - 1;
            }
            else
            {
                return mid;
            }
        }
        return -1;
    }
}
