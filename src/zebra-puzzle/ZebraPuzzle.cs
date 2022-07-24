using System;
using System.Collections.Generic;
using System.Linq;

internal enum Color { Red , Green , Ivory , Yellow , Blue }
internal enum Nationality { Englishman , Spaniard , Ukranian , Japanese , Norwegian }
internal enum Pet { Dog , Snails , Fox , Horse , Zebra }
internal enum Drink { Coffee , Tea , Milk , OrangeJuice , Water }
internal enum Smoke { OldGold , Kools , Chesterfields , LuckyStrike , Parliaments }

internal static class ZebraPuzzle
{
    private struct Solution
    {
        internal Color[] Colors;
        internal Nationality[] Nationalities;
        internal Pet[] Pets;
        internal Drink[] Drinks;
        internal Smoke[] Smokes;
    }

    private static readonly Color[] Colors = Enum.GetValues<Color>();
    private static readonly Nationality[] Nationalities = Enum.GetValues<Nationality>();
    private static readonly Pet[] Pets = Enum.GetValues<Pet>();
    private static readonly Drink[] Drinks = Enum.GetValues<Drink>();
    private static readonly Smoke[] Smokes = Enum.GetValues<Smoke>();
    
    public static Nationality DrinksWater()
    {
        var solution = Solve();
        return solution.Nationalities[Array.IndexOf(solution.Drinks, Drink.Water)];
    }

    public static Nationality OwnsZebra()
    {
        var solution = Solve();
        return solution.Nationalities[Array.IndexOf(solution.Pets, Pet.Zebra)];
    }
    
    private static IEnumerable<Solution> Solutions() => 
        from validColors in Colors.Permutations().Where(MatchesColorRules)
        from validNationalities in Nationalities.Permutations().Where(nationalities => MatchesNationalityRules(validColors, nationalities))
        from validPets in Pets.Permutations().Where(pets => MatchesPetRules(validNationalities, pets))
        from validDrinks in Drinks.Permutations().Where(drinks => MatchesDrinkRules(validColors, validNationalities, drinks))
        from validSmokes in Smokes.Permutations().Where(smokes => MatchesSmokeRules(validColors, validNationalities, validDrinks, validPets, smokes))
        select new Solution { Colors = validColors, Nationalities = validNationalities, Pets = validPets, Drinks = validDrinks, Smokes = validSmokes };
    
    private static Solution Solve() => Solutions().First();
    
        private static IEnumerable<T[]> Permutations<T>(this T[] input) => input.Permutations(input.Length);
    
    private static IEnumerable<T[]> Permutations<T>(this T[] input, int length)
    {
        if (length == 1)
        {
            return input.Select(t => new[] { t });
        }

        return input.Permutations(length - 1)
            .SelectMany(t => input.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new[] { t2 }).ToArray());
    }
    
    private static bool MatchesColorRules(Color[] colors)
    {
        var greenRightOfIvoryHouse = Array.IndexOf(colors, Color.Ivory) == Array.IndexOf(colors, Color.Green) - 1;
        return greenRightOfIvoryHouse;
    }
    
    private static bool MatchesNationalityRules(Color[] colors, Nationality[] nationalities)
    {
        var englishManInRedHouse = IsIndexMatch(nationalities, Nationality.Englishman, colors, Color.Red);
        var norwegianInFirstHouse = nationalities[0] == Nationality.Norwegian;
        var norwegianLivesNextToBlueHouse = IsAdjacentMatch(nationalities, Nationality.Norwegian, colors, Color.Blue);

        return englishManInRedHouse && norwegianInFirstHouse && norwegianLivesNextToBlueHouse;
    }
    
    private static bool MatchesPetRules(Nationality[] nationalities, Pet[] pets)
    {
        var spaniardOwnsDog = IsIndexMatch(nationalities, Nationality.Spaniard, pets, Pet.Dog);
        return spaniardOwnsDog;
    }

    private static bool MatchesDrinkRules(Color[] colors, Nationality[] nationalities, Drink[] drinks)
    {
        var coffeeDrunkInGreenHouse = IsIndexMatch(colors, Color.Green, drinks, Drink.Coffee);
        var ukranianDrinksTee = IsIndexMatch(nationalities, Nationality.Ukranian, drinks, Drink.Tea);
        var milkDrunkInMiddleHouse = drinks[2] == Drink.Milk;

        return coffeeDrunkInGreenHouse && ukranianDrinksTee && milkDrunkInMiddleHouse;
    }
    
    private static bool MatchesSmokeRules(Color[] colors, Nationality[] nationalities, Drink[] drinks, Pet[] pets, Smoke[] smokes)
    {
        var oldGoldSmokesOwnsSnails = IsIndexMatch(smokes, Smoke.OldGold, pets, Pet.Snails);
        var koolsSmokedInYellowHouse = IsIndexMatch(colors, Color.Yellow, smokes, Smoke.Kools);
        var chesterfieldsSmokedNextToHouseWithFox = IsAdjacentMatch(smokes, Smoke.Chesterfields, pets, Pet.Fox);
        var koolsSmokedNextToHouseWithHorse = IsAdjacentMatch(smokes, Smoke.Kools, pets, Pet.Horse);

        var luckyStrikeSmokerDrinksOrangeJuice = IsIndexMatch(smokes, Smoke.LuckyStrike, drinks, Drink.OrangeJuice);
        var japaneseSmokesParliaments = IsIndexMatch(nationalities, Nationality.Japanese, smokes, Smoke.Parliaments);

        return
            oldGoldSmokesOwnsSnails &&
            koolsSmokedInYellowHouse &&
            chesterfieldsSmokedNextToHouseWithFox &&
            koolsSmokedNextToHouseWithHorse &&
            luckyStrikeSmokerDrinksOrangeJuice &&
            japaneseSmokesParliaments;
    }

    private static bool IsIndexMatch<T1, T2>(T1[] values1, T1 value1, T2[] values2, T2 value2) => values2[Array.IndexOf(values1, value1)].Equals(value2);

    private static bool IsAdjacentMatch<T1, T2>(T1[] values1, T1 value1, T2[] values2, T2 value2)
    {
        var index = Array.IndexOf(values1, value1);
        return (index > 0 && values2[index - 1].Equals(value2)) || (index < values2.Length - 1 && values2[index + 1].Equals(value2));
    }
}
