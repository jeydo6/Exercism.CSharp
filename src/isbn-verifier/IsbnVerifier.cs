using System.Collections.Generic;

internal static class IsbnVerifier
{
    public static bool IsValid(string number)
    {
        var digits = new List<int>(number.Length);
        for (var i = 0; i < number.Length; i++)
        {
            if (char.IsDigit(number[i]))
            {
                digits.Add(number[i] - '0');
            }
            else if (i == number.Length - 1 && number[i] == 'X')
            {
                digits.Add(10);
            }
            else if (number[i] != '-')
            {
                return false;
            }
        }

        if (digits.Count != 10)
        {
            return false;
        }
        
        var result = 0;
        for (var i = 0; i < digits.Count; i++)
        {
            result += digits[^(i + 1)] * (i + 1);
        }

        return result % (digits.Count + 1) == 0;
    }
}
