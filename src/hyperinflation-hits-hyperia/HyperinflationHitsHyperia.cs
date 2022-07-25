using System;
using System.Globalization;

internal static class CentralBank
{
    public static string DisplayDenomination(long @base, long multiplier)
    {
        try
        {
            return checked(@base * multiplier).ToString(CultureInfo.InvariantCulture);
        }
        catch (OverflowException)
        {
            return "*** Too Big ***";
        }
    }

    public static string DisplayGDP(float @base, float multiplier)
    {
        var result = @base * multiplier;
        
        return !float.IsInfinity(result) ?
            result.ToString(CultureInfo.InvariantCulture) :
            "*** Too Big ***";
    }

    public static string DisplayChiefEconomistSalary(decimal salaryBase, decimal multiplier)
    {
        try
        {
            return (salaryBase * multiplier).ToString(CultureInfo.InvariantCulture);
        }
        catch (Exception)
        {
            return "*** Much Too Big ***";
        }
    }
}
