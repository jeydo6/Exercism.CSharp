internal class Manager
{
    public string Name { get; }

    public string? Club { get; }

    public Manager(string name, string? club)
    {
        Name = name;
        Club = club;
    }
}

internal class Incident
{
    public virtual string GetDescription() => "An incident happened.";
}

internal class Foul : Incident
{
    public override string GetDescription() => "The referee deemed a foul.";
}

internal class Injury : Incident
{
    private readonly int _player;

    public Injury(int player)
    {
        _player = player;
    }

    public override string GetDescription() => $"Player {_player} is injured.";
}
