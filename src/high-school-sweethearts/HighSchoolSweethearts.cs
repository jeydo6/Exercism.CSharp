using System;
using System.Globalization;

internal static class HighSchoolSweethearts
{
    private const string Banner =
@"
     ******       ******
   **      **   **      **
 **         ** **         **
**            *            **
**                         **
**     {0} +  {1}    **
 **                       **
   **                   **
     **               **
       **           **
         **       **
           **   **
             ***
              *
";

    public static string DisplaySingleLine(string studentA, string studentB) =>
        $"{studentA,29} ♡ {studentB,-29}";

    public static string DisplayBanner(string studentA, string studentB) => string.Format(Banner, studentA, studentB);

    public static string DisplayGermanExchangeStudents(string studentA, string studentB, DateTime start, float hours)
    {
        FormattableString fs = $"{studentA} and {studentB} have been dating since {start:d} - that's {hours:n2} hours";
        return fs.ToString(new CultureInfo("de-DE"));
    }
}
