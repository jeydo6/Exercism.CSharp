using System;

internal class BankAccount
{
    private static readonly object _obj = new object();
    
    private decimal? _balance;
    
    public void Open()
    {
        lock (_obj)
        {
            _balance = 0m;
        }
    }

    public void Close()
    {
        lock (_obj)
        {
            _balance = null;
        }
    }

    public decimal Balance => _balance ?? throw new InvalidOperationException();

    public void UpdateBalance(decimal change)
    {
        lock (_obj)
        {
            _balance += change;
        }
    }
}
