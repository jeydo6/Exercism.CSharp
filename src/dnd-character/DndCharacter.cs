using System;

internal class DndCharacter
{
    private static readonly Random _random = new Random();

    private DndCharacter(int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
    {
        Strength = strength;
        Dexterity = dexterity;
        Constitution = constitution;
        Intelligence = intelligence;
        Wisdom = wisdom;
        Charisma = charisma;
        Hitpoints = 10 + Modifier(constitution);
    }
    
    public int Strength { get; }
    public int Dexterity { get; }
    public int Constitution { get; }
    public int Intelligence { get; }
    public int Wisdom { get; }
    public int Charisma { get; }
    public int Hitpoints { get; }

    public static int Modifier(int score) => (int)Math.Floor((score - 10) / 2.0);

    public static int Ability()
    {
        var rolls = new int[4];
        for (var i = 0; i < rolls.Length; i++)
        {
            rolls[i] = _random.Next(1, 7);
        }
        
        Array.Sort(rolls);

        return rolls[^1] + rolls[^2] + rolls[^3];
    }

    public static DndCharacter Generate() =>
        new DndCharacter(Ability(), Ability(), Ability(), Ability(), Ability(), Ability());
}
