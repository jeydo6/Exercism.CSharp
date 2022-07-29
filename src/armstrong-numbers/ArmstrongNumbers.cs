internal static class ArmstrongNumbers
{
    public static bool IsArmstrongNumber(int number)
    {
        var str = number.ToString();
        
        var result = 0;
        for (var i = 0; i < str.Length; i++)
        {
            result += Pow(str[i] - '0', str.Length);
        }

        return result == number;
    }

    private static int Pow(int number, int power)
    {
        var result = 1;
        for (var i = 0; i < power; i++)
        {
            result *= number;
        }

        return result;
    }
}
