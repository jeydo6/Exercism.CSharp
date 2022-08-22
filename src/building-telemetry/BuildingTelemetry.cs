using System;

internal class RemoteControlCar
{
    private int _batteryPercentage = 100;
    private int _distanceDrivenInMeters;
    private int _latestSerialNum;
    
    private string[] _sponsors = Array.Empty<string>();

    public void Drive()
    {
        if (_batteryPercentage > 0)
        {
            _batteryPercentage -= 10;
            _distanceDrivenInMeters += 2;
        }
    }

    public void SetSponsors(params string[] sponsors) => _sponsors = sponsors;

    public string DisplaySponsor(int sponsorNum) => _sponsors[sponsorNum];

    public bool GetTelemetryData(ref int serialNum, out int batteryPercentage, out int distanceDrivenInMeters)
    {
        if (serialNum < _latestSerialNum)
        {
            serialNum = _latestSerialNum;
            batteryPercentage = -1;
            distanceDrivenInMeters = -1;
            return false;
        }

        _latestSerialNum = serialNum;
        batteryPercentage = _batteryPercentage;
        distanceDrivenInMeters = _distanceDrivenInMeters;
        return true;
    }

    public static RemoteControlCar Buy()
    {
        return new RemoteControlCar();
    }
}

internal class TelemetryClient
{
    private readonly RemoteControlCar _car;

    public TelemetryClient(RemoteControlCar car) => _car = car;

    public string GetBatteryUsagePerMeter(int serialNum) =>
        _car.GetTelemetryData(ref serialNum, out var batteryPercentage, out var distanceDrivenInMeters) && batteryPercentage < 100
            ? $"usage-per-meter={(100 - batteryPercentage) / distanceDrivenInMeters}"
            : "no data";
}
