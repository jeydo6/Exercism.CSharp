using System;

internal static class TelemetryBuffer
{
    public static byte[] ToBuffer(long reading) => (reading switch
    {
        >= 4_294_967_296 and <= 9_223_372_036_854_775_807 or >= -9_223_372_036_854_775_808 and <= -2_147_483_649 =>
            BitConverter.GetBytes((long)reading).Prepend(256 - 8),
        >= 2_147_483_648 and <= 4_294_967_295 =>
            BitConverter.GetBytes((uint)reading).Prepend(4),
        >= 65_536 and <= 2_147_483_647 or >= -2_147_483_648 and <= -32_769 =>
            BitConverter.GetBytes((int)reading).Prepend(256 - 4),
        >= 0 and <= 65_535 =>
            BitConverter.GetBytes((ushort)reading).Prepend(2),
        <= -1 and >= -32_768 =>
            BitConverter.GetBytes((short)reading).Prepend(256 - 2)
    }).ToBuffer();

    public static long FromBuffer(byte[] buffer) => buffer[0] switch
    {
        256 - 8 => BitConverter.ToInt64(buffer[1..]),
        256 - 4 => BitConverter.ToInt32(buffer[1..]),
        256 - 2 => BitConverter.ToInt16(buffer[1..]),
        4 => BitConverter.ToUInt32(buffer[1..]),
        2 => BitConverter.ToUInt16(buffer[1..]),
        _ => 0
    };

    private static byte[] Prepend(this byte[] array, byte value)
    {
        var result = new byte[array.Length + 1];
        result[0] = value;
        
        Array.Copy(array, 0, result, 1, array.Length);

        return result;
    }

    private static byte[] ToBuffer(this byte[] array)
    {
        var result = new byte[9];
        
        Array.Copy(array, 0, result, 0, array.Length);

        return result;
    }
}
