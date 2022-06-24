using System.Collections.Generic;

internal static class Luhn
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
            else if (number[i] != ' ')
            {
                return false;
            }
        }

        if (digits.Count < 2)
        {
            return false;
        }

        var result = 0;
        for (var i = 0; i < digits.Count; i++)
        {
            var digit = digits[^(i + 1)];
            result += i % 2 == 1 ? digit * 2 > 9 ?
                digit * 2 - 9 :
                digit * 2 :
                digit;
        }

        return result % 10 == 0;
    }
}
