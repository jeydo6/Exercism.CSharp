internal class RemoteControlCar
{
    private int _battery = 100;
    private int _distance;
    
    public static RemoteControlCar Buy()
    {
        return new RemoteControlCar();
    }

    public string DistanceDisplay() => $"Driven {_distance} meters";

    public string BatteryDisplay() => _battery > 0 ? $"Battery at {_battery}%" : "Battery empty";

    public void Drive()
    {
        if (_battery < 1) return;
        
        _battery -= 1;
        _distance += 20;
    }
}
