internal static class RealNumberExtension
{
    public static double Expreal(this int realNumber, RationalNumber r) => r.Expreal(realNumber);
}

internal readonly struct RationalNumber
{
    private readonly int _numerator;
    private readonly int _denominator;

    public RationalNumber(int numerator, int denominator) =>
        (_numerator, _denominator) = (numerator, denominator);

    public static RationalNumber operator +(RationalNumber r1, RationalNumber r2) =>
        new RationalNumber(r1._numerator * r2._denominator + r1._denominator * r2._numerator, r1._denominator * r2._denominator).Reduce();

    public static RationalNumber operator -(RationalNumber r1, RationalNumber r2) =>
        new RationalNumber(r1._numerator * r2._denominator - r1._denominator * r2._numerator, r1._denominator * r2._denominator).Reduce();

    public static RationalNumber operator *(RationalNumber r1, RationalNumber r2) =>
        r1._numerator == 0 ?
        new RationalNumber(0, 1) :
        new RationalNumber(r1._numerator * r2._numerator, r1._denominator * r2._denominator).Reduce();

    public static RationalNumber operator /(RationalNumber r1, RationalNumber r2) =>
        new RationalNumber(r1._numerator * r2._denominator, r1._denominator * r2._numerator).Reduce();

    public RationalNumber Abs() =>
        new RationalNumber(Abs(_numerator), Abs(_denominator));

    public RationalNumber Reduce()
    {
        if (_denominator == 0)
        {
            return new RationalNumber(_numerator, _denominator);
        }

        if (_numerator == 0)
        {
            return new RationalNumber(0, 1);
        }

        var a = Abs(_numerator);
        var b = Abs(_denominator);

        var sign = Sign(_numerator) * Sign(_denominator);

        return new RationalNumber(sign * a / GreatestCommonDenominator(a, b), b / GreatestCommonDenominator(a, b));
    }

    public RationalNumber Exprational(int power) =>
        power == 0 ?
        new RationalNumber(1, 1) :
        new RationalNumber(
            (Sign(_numerator) / Sign(_denominator)) * Abs((int)Pow(_numerator, power)),
            Abs((int)Pow(_denominator, power))
        ).Reduce();

    public double Expreal(int baseNumber) =>
        _numerator == 0 ? 1 : Pow(baseNumber);
    
    private double Pow(int baseNumber) => NthRoot(
        Sign(_numerator) == Sign(_denominator) ? Pow(baseNumber, Abs(_numerator)) : 1d / Pow(baseNumber, Abs(_numerator)),
        Abs(_denominator)
    );

    private static int Abs(int value) => (value > 0) ? value : -value;

    private static double Abs(double value) => (value > 0) ? value : -value;

    private static int Sign(int value) => (value > 0) ? 1 : -1;

    private static int GreatestCommonDenominator(int a, int b)
    {
        var x = Abs(a);
        var y = Abs(b);

        while (x != 0 && y != 0)
            if (x > y) x %= y; else y %= x;
        return x == 0 ? y : x;
    }

    private static double Pow(double baseValue, int exp)
    {
        var result = 1.0;
        while (exp != 0)
        {
            if ((exp & 1) != 0) result *= baseValue;
            baseValue *= baseValue;
            exp >>= 1;
        }
        return result;
    }

    private static double NthRoot(double baseValue, int n)
    {
        if (n == 1)
        {
            return baseValue;
        }
        
        double deltaX;
        var x = 0.1;
        do
        {
            deltaX = (baseValue / Pow(x, (n - 1)) - x) / n;
            x += deltaX;
        }
        while (Abs(deltaX) > 0);
        
        return x;
    }
}
