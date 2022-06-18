using System.Text;

internal static class RunLengthEncoding
{
    public static string Encode(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        
        var sb = new StringBuilder();
        
        var count = 1;
        var letter = input[0];
        for (var i = 1; i < input.Length; i++)
        {
            if (input[i] == letter)
            {
                count++;
            }
            else
            {
                if (count > 1)
                {
                    sb.Append(count);
                }
                sb.Append(letter);

                count = 1;
                letter = input[i];
            }
        }
        
        if (count > 1)
        {
            sb.Append(count);
        }
        sb.Append(letter);

        return sb.ToString();
    }

    public static string Decode(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        
        var sb = new StringBuilder();

        var countIndex = 0;
        for (var i = 0; i < input.Length; i++)
        {
            if (char.IsNumber(input[i]))
            {
                continue;
            }

            sb.Append(new string(
                input[i],
                i > countIndex ? int.Parse(input[countIndex..i]) : 1
            ));

            countIndex = i + 1;
        }

        return sb.ToString();
    }
}
