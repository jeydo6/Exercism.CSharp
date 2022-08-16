using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

internal class LedgerEntry
{
   public LedgerEntry(string date, string description, decimal change)
   {
       Date = DateTime.Parse(date, CultureInfo.InvariantCulture);
       Description = description;
       Change = change;
   }

   public DateTime Date { get; }
   public string Description { get; }
   public decimal Change { get; }
   
   public string ToString(IFormatProvider culture) =>
       $"{FormatDate(culture, Date)} | {FormatDescription(Description),-25} | {FormatChange(culture, Change),13}";

   private static string FormatDate(IFormatProvider culture, DateTime date) =>
       date.ToString("d", culture);

   private static string FormatDescription(string description) =>
       description.Length > 25 ?
           description[..22] + "..." :
           description;

   private static string FormatChange(IFormatProvider culture, decimal change) =>
       change.ToString("C", culture) + (change < 0.0m ? "" : " ");
}

internal static class Ledger
{
    public static LedgerEntry CreateEntry(string date, string description, int change) =>
        new LedgerEntry(date, description, change / 100.0m);
    
    public static string Format(string currency, string locale, IEnumerable<LedgerEntry> entries)
    {
       var culture = GetCultureInfo(currency, locale);

       var lines = entries
           .OrderBy(e => e.Change >= 0)
           .ThenBy(e => e.Date + "@" + e.Description + "@" + e.Change)
           .Select(e => e.ToString(culture))
           .Prepend(GetHeading(locale));

       return string.Join("\n", lines);
    }
    
    private static string GetHeading(string locale) => locale switch
    {
        "en-US" => "Date       | Description               | Change       ",
        "nl-NL" => "Datum      | Omschrijving              | Verandering  ",
        _ => throw new ArgumentException("Invalid locale")
    };

    private static CultureInfo GetCultureInfo(string currency, string locale)
    {
       if (currency != "USD" && currency != "EUR")
       {
           throw new ArgumentException("Invalid currency");
       }
       
       if (locale != "nl-NL" && locale != "en-US")
       {
           throw new ArgumentException("Invalid locale");
       }

       var (currencySymbol, currencyNegativePattern, shortDatePattern) = currency switch
       {
           "USD" when locale == "en-US" => ("$", 0, "MM/dd/yyyy"),
           "USD" when locale == "nl-NL" => ("$", 12, "dd/MM/yyyy"),
           "EUR" when locale == "en-US" => ("€", 0, "dd/MM/yyyy"),
           "EUR" when locale == "nl-NL" => ("€", 12, "dd/MM/yyyy"),
           _ => (null, 0, null)
       };

       return new CultureInfo(locale)
       {
           NumberFormat =
           {
               CurrencySymbol = currencySymbol,
               CurrencyNegativePattern = currencyNegativePattern
           },
           DateTimeFormat =
           {
               ShortDatePattern = shortDatePattern
           }
       };
    }
}
