using System;

internal static class Grains
{
    private static readonly ulong[] _squares;

    static Grains()
    {
        _squares = new ulong[64];
        _squares[0] = 1;
        for (var i = 1; i < _squares.Length; i++)
        {
            _squares[i] = 2 * _squares[i - 1];
        }
    }

    public static ulong Square(int n) =>
        n > 0 && n <= _squares.Length ?
        _squares[n - 1] : throw new ArgumentOutOfRangeException(nameof(n));

    public static ulong Total() => 2 * _squares[^1] - 1;
}
