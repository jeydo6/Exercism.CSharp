internal static class DifferenceOfSquares
{
    public static int CalculateSquareOfSum(int max)
    {
        var result = 0;
        for (var number = 1; number <= max; number++)
        {
            result += number;
        }

        return result * result;
    }

    public static int CalculateSumOfSquares(int max)
    {
        var result = 0;
        for (var number = 1; number <= max; number++)
        {
            result += number * number;
        }

        return result;
    }

    public static int CalculateDifferenceOfSquares(int max) => CalculateSquareOfSum(max) - CalculateSumOfSquares(max);
}
