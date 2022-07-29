using System;

internal static class ArmstrongNumbers
{
    public static bool IsArmstrongNumber(int number)
    {
        var numberCopy = number;

        var power = (int)Math.Ceiling(Math.Log10(numberCopy));
        
        var result = 0;
        while (numberCopy > 0)
        {
            result += (int)Math.Pow(numberCopy % 10, power);
            numberCopy /= 10;
        }

        return result == number;
    }
}
