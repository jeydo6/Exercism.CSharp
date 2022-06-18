internal static class ResistorColor
{
    private static readonly string[] _colors = {
        "black",
        "brown",
        "red",
        "orange",
        "yellow",
        "green",
        "blue",
        "violet",
        "grey",
        "white"
    };
    
    public static int ColorCode(string color)
    {
        for (var i = 0; i < _colors.Length; i++)
        {
            if (_colors[i] == color)
            {
                return i;
            }
        }

        return -1;
    }

    public static string[] Colors() => _colors;
}
