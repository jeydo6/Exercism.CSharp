using System;
using System.Collections.Generic;

[Flags]
internal enum Allergen
{
    Eggs = 1,
    Peanuts = 2,
    Shellfish = 4,
    Strawberries = 8,
    Tomatoes = 16,
    Chocolate = 32,
    Pollen = 64,
    Cats = 128
}

internal class Allergies
{
    private readonly Allergen _mask;

    public Allergies(int mask) => _mask = (Allergen)(mask % 256);

    public bool IsAllergicTo(Allergen allergen) => _mask.HasFlag(allergen);

    public Allergen[] List()
    {
        var result = new List<Allergen>();
        foreach (var allergen in Enum.GetValues<Allergen>())
        {
            if (IsAllergicTo(allergen))
            {
                result.Add(allergen);
            }
        }

        return result.ToArray();
    }
}
