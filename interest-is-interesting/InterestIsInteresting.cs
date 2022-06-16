internal static class SavingsAccount
{
    public static float InterestRate(decimal balance) => balance switch
    {
        < 0m => 3.213f,
        < 1000m => 0.5f,
        < 5000m => 1.621f,
        _ => 2.475f
    };

    public static decimal Interest(decimal balance) => balance * new decimal(InterestRate(balance)) / 100m;

    public static decimal AnnualBalanceUpdate(decimal balance) => balance + Interest(balance);

    public static int YearsBeforeDesiredBalance(decimal balance, decimal targetBalance)
    {
        var years = 0;
        
        while (balance < targetBalance)
        {
            balance += Interest(balance);
            years++;
        }

        return years;
    }
}
