using System;

internal static class PlayAnalyzer
{
    public static string AnalyzeOnField(int shirtNum) => shirtNum switch
    {
        1 => "goalie",
        2 => "left back",
        3 or 4 => "center back",
        5 => "right back",
        6 or 7 or 8 => "midfielder",
        9 => "left wing",
        10 => "striker",
        11 => "right wing",
        _ => throw new ArgumentOutOfRangeException(nameof(shirtNum))
    };

    public static string AnalyzeOffField(object report) => report switch
    {
        int supporters => $"There are {supporters} supporters at the match.",
        string announcement => announcement,
        Foul foul => foul.GetDescription(),
        Injury injury => $"Oh no! {injury.GetDescription()} Medics are on the field.",
        Incident incident => incident.GetDescription(),
        Manager manager => manager.Name + (manager.Club != null ? $" ({manager.Club})" : ""),
        _ => throw new ArgumentException(message: null, paramName: nameof(report))
    };
}
