using System;

internal class Player
{
    private readonly Random _random = new Random();

    public int RollDie() => _random.Next(18) + 1;

    public double GenerateSpellStrength() => 100 * _random.NextDouble();
}
