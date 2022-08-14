using System;

internal static class ResistorColorTrio
{
    private const int KiloOhms = 1_000;
    
    public static string Label(string[] colors)
    {
        var value = GetValue(colors);
        return $"{(value >= KiloOhms ? value / KiloOhms : value)} " + GetUnit(value);
    }

    private static int GetValue(string[] colors) => ((GetValue(colors[0]) * 10) + GetValue(colors[1])) * (int)Math.Pow(10, GetValue(colors[2]));

    private static int GetValue(string color) => color switch
    {
        "black" => 0,
        "brown" => 1,
        "red" => 2,
        "orange" => 3,
        "yellow" => 4,
        "green" => 5,
        "blue" => 6,
        "violet" => 7,
        "grey" => 8,
        "white" => 9,
        _ => 0
    };
    
    private static string GetUnit(int value) => (value >= KiloOhms ? "kilo" : "") + "ohms";
}
