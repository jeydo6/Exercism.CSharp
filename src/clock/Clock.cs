internal readonly struct Clock
{
    private readonly int _hours;
    private readonly int _minutes;

    public Clock(int hours, int minutes)
    {
        _hours = Mod((hours * 60 + minutes) / 60.0, 24);
        _minutes = Mod(minutes, 60);
    }

    public Clock Add(int minutesToAdd) =>
        new Clock(_hours, _minutes + minutesToAdd);

    public Clock Subtract(int minutesToSubtract) =>
        new Clock(_hours, _minutes - minutesToSubtract);
    
    public override string ToString() =>
        $"{_hours:00}:{_minutes:00}";

    private static int Mod(double value, int modulo) =>
        (int)((value % modulo + modulo) % modulo);
}
