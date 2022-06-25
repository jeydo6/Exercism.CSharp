using System.Text;

internal static class BeerSong
{
    public static string Recite(int startBottles, int takeDown)
    {
        var sb = new StringBuilder();
        while (takeDown > 0 && startBottles >= 0)
        {
            sb.Append(startBottles switch
            {
                > 2 => $"{startBottles} bottles of beer on the wall, {startBottles} bottles of beer.\nTake one down and pass it around, {startBottles - 1} bottles of beer on the wall.\n",
                2 => "2 bottles of beer on the wall, 2 bottles of beer.\nTake one down and pass it around, 1 bottle of beer on the wall.\n",
                1 => "1 bottle of beer on the wall, 1 bottle of beer.\nTake it down and pass it around, no more bottles of beer on the wall.\n",
                0 => "No more bottles of beer on the wall, no more bottles of beer.\nGo to the store and buy some more, 99 bottles of beer on the wall.\n"
            });
            sb.Append('\n');

            startBottles--;
            takeDown--;
        }

        return sb.ToString().TrimEnd('\n');
    }
}
