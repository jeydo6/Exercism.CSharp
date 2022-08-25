using System;

internal struct CurrencyAmount
{
    private readonly decimal _amount;
    private readonly string _currency;

    public CurrencyAmount(decimal amount, string currency) =>
        (_amount, _currency) = (amount, currency);

    public static bool operator ==(CurrencyAmount obj1, CurrencyAmount obj2)
    {
        if (obj1._currency != obj2._currency)
        {
            throw new ArgumentException(null);
        }
        
        return obj1._amount == obj2._amount;
    }
    
    public static bool operator !=(CurrencyAmount obj1, CurrencyAmount obj2)
    {
        if (obj1._currency != obj2._currency)
        {
            throw new ArgumentException(null);
        }

        return obj1._amount != obj2._amount;
    }
    
    public static bool operator >(CurrencyAmount obj1, CurrencyAmount obj2)
    {
        if (obj1._currency != obj2._currency)
        {
            throw new ArgumentException(null);
        }

        return obj1._amount > obj2._amount;
    }

    public static bool operator <(CurrencyAmount obj1, CurrencyAmount obj2)
    {
        if (obj1._currency != obj2._currency)
        {
            throw new ArgumentException(null);
        }

        return obj1._amount < obj2._amount;
    }

    public static CurrencyAmount operator +(CurrencyAmount obj1, CurrencyAmount obj2)
    {
        if (obj1._currency != obj2._currency)
        {
            throw new ArgumentException(null);
        }

        return new CurrencyAmount(obj1._amount + obj2._amount, obj1._currency);
    }

    public static CurrencyAmount operator -(CurrencyAmount obj1, CurrencyAmount obj2)
    {
        if (obj1._currency != obj2._currency)
        {
            throw new ArgumentException(null);
        }

        return new CurrencyAmount(obj1._amount - obj2._amount, obj1._currency);
    }

    public static CurrencyAmount operator *(CurrencyAmount obj, decimal multiplier) => new CurrencyAmount(obj._amount * multiplier, obj._currency);

    public static CurrencyAmount operator *(decimal multiplier, CurrencyAmount obj) => new CurrencyAmount(obj._amount * multiplier, obj._currency);

    public static CurrencyAmount operator /(CurrencyAmount obj, decimal divisor) => new CurrencyAmount(obj._amount / divisor, obj._currency);

    public static explicit operator double(CurrencyAmount obj) => (double)obj._amount;

    public static implicit operator decimal(CurrencyAmount obj) => obj._amount;
}
