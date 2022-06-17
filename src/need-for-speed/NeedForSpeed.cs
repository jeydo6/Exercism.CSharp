internal class RemoteControlCar
{
    private readonly int _speed;
    private readonly int _batteryDrain;

    private int _distance;
    private int _battery = 100;

    public RemoteControlCar(int speed, int batteryDrain) => (_speed, _batteryDrain) = (speed, batteryDrain);

    public bool BatteryDrained() => _battery < _batteryDrain;

    public int DistanceDriven() => _distance;

    public void Drive()
    {
        if (BatteryDrained()) return;
        
        _distance += _speed;
        _battery -= _batteryDrain;
    }

    public static RemoteControlCar Nitro() => new RemoteControlCar(50, 4);
}

internal class RaceTrack
{
    private readonly int _distance;
    
    public RaceTrack(int distance) => _distance = distance;

    public bool TryFinishTrack(RemoteControlCar car)
    {
        while (!car.BatteryDrained())
        {
            car.Drive();
        }

        return car.DistanceDriven() >= _distance;
    }
}
