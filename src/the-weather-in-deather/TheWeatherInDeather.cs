using System;
using System.Collections.Generic;

internal class WeatherStation
{
    private readonly IList<(DateTime Date, Reading Value)> _readings = new List<(DateTime Date, Reading Value)>();
    
    public void AcceptReading(Reading reading)
    {
        _readings.Add((DateTime.Now, reading));
    }

    public void ClearAll()
    {
        _readings.Clear();
    }

    public decimal LatestTemperature => _readings.Count > 0 ? _readings[^1].Value.Temperature : default;

    public decimal LatestPressure => _readings.Count > 0 ? _readings[^1].Value.Pressure : default;

    public decimal LatestRainfall => _readings.Count > 0 ? _readings[^1].Value.Rainfall : default;

    public bool HasHistory => _readings.Count > 1;

    public Outlook ShortTermOutlook
    {
        get
        {
            if (_readings.Count == 0)
            {
                throw new ArgumentException();
            }
            
            var reading = _readings[^1].Value;
            
            return
                reading.Pressure < 10m && reading.Temperature < 30m ? Outlook.Cool :
                reading.Temperature > 50 ? Outlook.Good :
                Outlook.Warm;
        }
    }

    public Outlook LongTermOutlook
    {
        get
        {
            if (_readings.Count == 0)
            {
                throw new ArgumentException();
            }
            
            var reading = _readings[^1].Value;

            return reading.WindDirection switch
            {
                WindDirection.Southerly => Outlook.Good,
                WindDirection.Northerly => Outlook.Cool,
                WindDirection.Easterly when reading.Temperature <= 20 => Outlook.Warm,
                WindDirection.Easterly => Outlook.Good,
                WindDirection.Westerly => Outlook.Rainy,
                WindDirection.Unknown => throw new ArgumentException(),
                _ => throw new ArgumentException()
            };

        }
    }

    public State RunSelfTest() => _readings.Count == 0 ? State.Bad : State.Good;
}

/*** Please do not modify this struct ***/
internal struct Reading
{
    public decimal Temperature { get; }
    public decimal Pressure { get; }
    public decimal Rainfall { get; }
    public WindDirection WindDirection { get; }

    public Reading(decimal temperature, decimal pressure,
        decimal rainfall, WindDirection windDirection)
    {
        Temperature = temperature;
        Pressure = pressure;
        Rainfall = rainfall;
        WindDirection = windDirection;
    }
}

/*** Please do not modify this enum ***/
internal enum State
{
    Good,
    Bad
}

/*** Please do not modify this enum ***/
internal enum Outlook
{
    Cool,
    Rainy,
    Warm,
    Good
}

/*** Please do not modify this enum ***/
internal enum WindDirection
{
    Unknown, // default
    Northerly,
    Easterly,
    Southerly,
    Westerly
}
