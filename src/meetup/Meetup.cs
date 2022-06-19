using System;

internal enum Schedule
{
    Teenth,
    First,
    Second,
    Third,
    Fourth,
    Last
}

internal class Meetup
{
    private readonly DateTime _date;

    public Meetup(int month, int year) => _date = new DateTime(year, month, 1);

    public DateTime Day(DayOfWeek dayOfWeek, Schedule schedule) => schedule switch
    {
        Schedule.First => _date
            .GetNextWeekday(dayOfWeek),
        
        Schedule.Second => _date
            .AddDays(7)
            .GetNextWeekday(dayOfWeek),
        
        Schedule.Third => _date
            .AddDays(14)
            .GetNextWeekday(dayOfWeek),
        
        Schedule.Fourth => _date
            .AddDays(21)
            .GetNextWeekday(dayOfWeek),
        
        Schedule.Last => _date
            .AddMonths(1)
            .AddDays(-7)
            .GetNextWeekday(dayOfWeek),
        
        Schedule.Teenth => _date
            .AddDays(12)
            .GetNextWeekday(dayOfWeek),
        
        _ => _date
    };
}

internal static class DateTimeExtension
{
    public static DateTime GetNextWeekday(this DateTime date, DayOfWeek dayOfWeek)
    {
        var daysToAdd = (dayOfWeek - date.DayOfWeek + 7) % 7;
        return date.AddDays(daysToAdd);
    }
}
