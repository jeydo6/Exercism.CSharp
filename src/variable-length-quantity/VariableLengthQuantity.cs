using System;
using System.Collections.Generic;

internal static class VariableLengthQuantity
{
    public static uint[] Encode(uint[] numbers)
    {
        var result = new List<uint>();

        for (var i = 0; i < numbers.Length; i++)
        {
            var number = numbers[i];
            if (number == 0)
            {
                result.Add(0u);
            }

            var bytes = new List<uint>(4);
            while (number > 0)
            {
                var tmp = number & 0x7fu;
                number >>= 7;

                if (bytes.Count > 0)
                {
                    tmp |= 0x80u;
                }
                
                bytes.Add(tmp);
            }

            for (var j = bytes.Count - 1; j >= 0; j--)
            {
                result.Add(bytes[j]);
            }
        }

        return result.ToArray();
    }

    public static uint[] Decode(uint[] bytes)
    {
        var numbers = new List<uint>();
        var tmp = 0u;

        for (var i = 0; i < bytes.Length; i++)
        {
            if ((tmp & 0xfe000000u) > 0)
            {
                throw new InvalidOperationException();
            }

            tmp = (tmp << 7) | (bytes[i] & 0x7fu);

            if ((bytes[i] & 0x80) == 0)
            {
                numbers.Add(tmp);
                tmp = 0;
            }
            else if (i == bytes.Length - 1)
            {
                throw new InvalidOperationException();
            }
        }
        
        return numbers.ToArray();
    }
}
