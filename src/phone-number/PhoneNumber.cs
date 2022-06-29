using System;
using System.Text;

internal static class PhoneNumber
{
    public static string Clean(string phoneNumber)
    {
        var cleanPhoneNumber = GetCleanPhoneNumber(phoneNumber);
        
        if (!Validate(cleanPhoneNumber))
        {
            throw new ArgumentException(null, nameof(phoneNumber));
        }

        return cleanPhoneNumber[^10..];
    }

    private static bool Validate(string digits)
    {
        if (digits.Length is not (10 or 11))
        {
            return false;
        }

        if (digits.Length == 11 && digits[0] != '1')
        {
            return false;
        }

        if (!(digits[^10] >= '2' && digits[^10] <= '9') ||
            !(digits[^7] >= '2' && digits[^7] <= '9'))
        {
            return false;
        }

        return true;
    }

    private static string GetCleanPhoneNumber(string phoneNumber)
    {
        var sb = new StringBuilder(phoneNumber.Length);
        foreach (var ch in phoneNumber)
        {
            if (char.IsDigit(ch))
            {
                sb.Append(ch);
            }
        }

        return sb.ToString();
    }
}

