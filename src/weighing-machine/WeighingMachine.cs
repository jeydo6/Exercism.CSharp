using System;
using System.Globalization;

internal class WeighingMachine
{
    private double _weight;
    
    public WeighingMachine(int precision)
    {
        Precision = precision;
    }
    
    public int Precision { get; }
    
    public double TareAdjustment { get; set; } = 5;

    public double Weight
    {
        get => _weight;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            _weight = value;
        }
    }

    public string DisplayWeight => (Weight - TareAdjustment)
        .ToString("0." + new string('0', Precision), CultureInfo.InvariantCulture) + " kg";
}
