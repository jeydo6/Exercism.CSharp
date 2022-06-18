internal static class ResistorColorDuo
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

    public static int Value(string[] colors)
    {
        var colorCodes = new int[2];

        var colorPosition = 0;
        foreach (var color in colors)
        {
            var colorCode = ColorCode(color);
            if (colorCode >= 0 && colorPosition < colorCodes.Length)
            {
                colorCodes[colorPosition++] = colorCode;
            }
        }

        return 10 * colorCodes[0] + colorCodes[1];
    }
    
    private static int ColorCode(string color)
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
}
