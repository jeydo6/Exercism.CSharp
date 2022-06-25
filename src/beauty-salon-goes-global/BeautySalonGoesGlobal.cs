using System;
using System.Globalization;


internal enum Location
{
    NewYork,
    London,
    Paris
}

internal enum AlertLevel
{
    Early,
    Standard,
    Late
}

internal static class Appointment
{
    private static readonly bool _isWindows = OperatingSystem.IsWindows();

    public static DateTime ShowLocalTime(DateTime dtUtc) =>
        dtUtc + GetTimeZoneInfo().GetUtcOffset(dtUtc);

    public static DateTime Schedule(string appointmentDateDescription, Location location)
    {
        var dt = DateTime.Parse(appointmentDateDescription, CultureInfo.InvariantCulture);
        var tzi = GetTimeZoneInfo(location);

        // dtUtc
        return dt - tzi.GetUtcOffset(dt);
    }

    public static DateTime GetAlertTime(DateTime appointment, AlertLevel alertLevel) => appointment - alertLevel switch
    {
        AlertLevel.Early => TimeSpan.FromDays(1),
        AlertLevel.Standard => TimeSpan.FromMinutes(105),
        AlertLevel.Late => TimeSpan.FromMinutes(30),
        _ => throw new ArgumentOutOfRangeException(nameof(alertLevel))
    };

    public static bool HasDaylightSavingChanged(DateTime dt, Location location)
    {
        var tzi = GetTimeZoneInfo(location);

        return tzi.IsDaylightSavingTime(dt) != tzi.IsDaylightSavingTime(dt.AddDays(-7));
    }

    public static DateTime NormalizeDateTime(string dtStr, Location location) =>
        DateTime.TryParse(dtStr, GetCultureInfo(location), DateTimeStyles.None, out var dt)
            ? dt
            : new DateTime(1, 1, 1);

    private static TimeZoneInfo GetTimeZoneInfo(Location? location = null)
    {
        if (location is null)
        {
            return TimeZoneInfo.Local;
        }
        
        var timeZoneId = location switch
        {
            Location.NewYork => _isWindows ? "Eastern Standard Time" : "America/New_York",
            Location.London => _isWindows ? "GMT Standard Time" : "Europe/London",
            Location.Paris => _isWindows ? "W. Europe Standard Time" : "Europe/Paris",
            _ => throw new ArgumentOutOfRangeException(nameof(location))
        };

        return TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
    }

    private static CultureInfo GetCultureInfo(Location? location = null)
    {
        if (location is null)
        {
            return CultureInfo.CurrentCulture;
        }

        var culture = location switch
        {
            Location.NewYork => "en-US",
            Location.London => "en-GB",
            Location.Paris => "fr-FR",
            _ => throw new ArgumentOutOfRangeException(nameof(location))
        };

        return CultureInfo.GetCultureInfo(culture);
    }
}
