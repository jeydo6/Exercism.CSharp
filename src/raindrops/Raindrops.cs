using System.Text;

internal static class Raindrops
{
    public static string Convert(int number)
    {
        var sb = new StringBuilder();
        if (number % 3 == 0)
        {
            sb.Append("Pling");
        }
        
        if (number % 5 == 0)
        {
            sb.Append("Plang");
        }
        
        if (number % 7 == 0)
        {
            sb.Append("Plong");
        }

        return sb.Length > 0 ? sb.ToString() : number.ToString();
    }
}
