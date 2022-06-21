public static class Rectangles
{
    public static int Count(string[] rows)
    {
        if (rows.Length == 0 || rows[0].Length == 0)
        {
            return 0;
        }
        
        var n = rows.Length;
        var m = rows[0].Length;
        
        var count = 0;

        var i = 0;
        var j = 0;
        while (i < n)
        {
            var y = i;
            var x = j;
            if (rows[i][j] == '+')
            {
                var x1 = j;
                while (++j < m)
                {
                    if (rows[i][j] == '+')
                    {
                        while (++i < n)
                        {
                            if (rows[i][x1] == '+' && rows[i][j] == '+')
                            {
                                count++;
                            }
                            else if (
                                rows[i][x1] == '-' || rows[i][j] == '-' ||
                                rows[i][x1] == ' ' || rows[i][j] == ' '
                            )
                            {
                                break;
                            }
                        }

                        i = y;
                    }
                    else if (rows[i][j] != '-')
                    {
                        break;
                    }
                }
            }

            j = x;
            if (j + 1 < m)
            {
                j++;
            }
            else
            {
                j = 0;
                i++;
            }
        }

        return count;
    }
}
