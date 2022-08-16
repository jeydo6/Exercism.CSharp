using System;
using System.Collections.Generic;

internal interface IRemoteControlCar
{
    int DistanceTravelled { get; }
    
    void Drive();
}

internal class ProductionRemoteControlCar : IRemoteControlCar, IComparable<ProductionRemoteControlCar>
{
    public int DistanceTravelled { get; private set; }
    public int NumberOfVictories { get; set; }

    public void Drive()
    {
        DistanceTravelled += 10;
    }
    
    public int CompareTo(ProductionRemoteControlCar other) => NumberOfVictories.CompareTo(other.NumberOfVictories);
}

internal class ExperimentalRemoteControlCar : IRemoteControlCar
{
    public int DistanceTravelled { get; private set; }

    public void Drive()
    {
        DistanceTravelled += 20;
    }
}

internal static class TestTrack
{
    public static void Race(IRemoteControlCar car) => car.Drive();

    public static List<ProductionRemoteControlCar> GetRankedCars(ProductionRemoteControlCar prc1,
        ProductionRemoteControlCar prc2) => prc2.CompareTo(prc1) < 0 ?
        new List<ProductionRemoteControlCar> { prc2, prc1 } :
        new List<ProductionRemoteControlCar> { prc1, prc2 };
}
