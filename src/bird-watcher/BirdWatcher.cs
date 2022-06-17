using System;

internal class BirdCount
{
    private readonly int[] _birdsPerDay;

    public BirdCount(int[] birdsPerDay) => _birdsPerDay = birdsPerDay;

    public static int[] LastWeek() => new int[] { 0, 2, 5, 3, 7, 8, 4 };

    public int Today() => _birdsPerDay[^1];

    public void IncrementTodaysCount() => _birdsPerDay[^1]++;

    public bool HasDayWithoutBirds()
    {
        for (var i = 0; i < _birdsPerDay.Length; i++)
        {
            if (_birdsPerDay[i] == 0)
            {
                return true;
            }
        }

        return false;
    }

    public int CountForFirstDays(int numberOfDays)
    {
        var count = 0;
        for (var i = 0; i < numberOfDays; i++)
        {
            count += _birdsPerDay[i];
        }

        return count;
    }

    public int BusyDays()
    {
        var count = 0;
        for (var i = 0; i < _birdsPerDay.Length; i++)
        {
            count += _birdsPerDay[i] >= 5 ? 1 : 0;
        }

        return count;
    }
}
