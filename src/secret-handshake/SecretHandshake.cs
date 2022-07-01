using System.Collections.Generic;

internal static class SecretHandshake
{
    private static readonly string[] _handshakes = new string[]
    {
        "wink",
        "double blink",
        "close your eyes",
        "jump"
    };
    public static string[] Commands(int commandValue)
    {
        var result = new List<string>();
        
        var bits = GetBits(commandValue);
        for (var i = 0; i < bits.Length - 1; i++)
        {
            var position = bits[^1] ? bits.Length - 2 - i : i;
            if (bits[position])
            {
                result.Add(_handshakes[position]);
            }
        }

        return result.ToArray();
    }

    private static bool[] GetBits(int commandValue)
    {
        var result = new bool[5];
        for (var i = 0; i < 5; i++)
        {
            result[i] = commandValue % 2 == 1;
            commandValue /= 2;
        }

        return result;
    }
}
