internal class RemoteControlCar
{
    private Speed _currentSpeed;

    public RemoteControlCar() => Telemetry = new Telemetry(this);
    
    public Telemetry Telemetry { get; }

    public string CurrentSponsor { get; private set; }

    public void SetSponsor(string sponsorName)
    {
        CurrentSponsor = sponsorName;
    }
    
    public string GetSpeed() => _currentSpeed.ToString();

    public void SetSpeed(Speed speed) => _currentSpeed = speed;
}

internal enum SpeedUnits
{
    MetersPerSecond,
    CentimetersPerSecond
}

internal readonly struct Speed
{
    private readonly decimal _amount;
    private readonly SpeedUnits _speedUnits;

    public Speed(decimal amount, SpeedUnits speedUnits) =>
        (_amount, _speedUnits) = (amount, speedUnits);

    public override string ToString() =>
        _amount + 
        (_speedUnits == SpeedUnits.CentimetersPerSecond ? " centimeters per second" : " meters per second");
}

internal class Telemetry
{
    private readonly RemoteControlCar _car;

    public Telemetry(RemoteControlCar car) => _car = car;
    
    public void Calibrate() { }

    public bool SelfTest() => true;

    public void ShowSponsor(string sponsorName) =>_car.SetSponsor(sponsorName);

    public void SetSpeed(decimal amount, string unitsString) => _car.SetSpeed(
        new Speed(amount, unitsString == "cps" ? SpeedUnits.CentimetersPerSecond : SpeedUnits.MetersPerSecond)
    );
}
