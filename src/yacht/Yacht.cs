using System;
using System.Collections.Generic;

internal enum YachtCategory
{
    Ones = 1,
    Twos = 2,
    Threes = 3,
    Fours = 4,
    Fives = 5,
    Sixes = 6,
    FullHouse = 7,
    FourOfAKind = 8,
    LittleStraight = 9,
    BigStraight = 10,
    Choice = 11,
    Yacht = 12,
}

internal static class YachtGame
{
    public static int Score(int[] dice, YachtCategory category) => category switch
    {
        YachtCategory.Ones => dice.ScoreSingles(1),
        YachtCategory.Twos => dice.ScoreSingles(2),
        YachtCategory.Threes => dice.ScoreSingles(3),
        YachtCategory.Fours => dice.ScoreSingles(4),
        YachtCategory.Fives => dice.ScoreSingles(5),
        YachtCategory.Sixes => dice.ScoreSingles(6),
        YachtCategory.FullHouse => dice.ScoreFullHouse(),
        YachtCategory.FourOfAKind => dice.ScoreFourOfAKind(),
        YachtCategory.LittleStraight => dice.ScoreLittleStraight(),
        YachtCategory.BigStraight => dice.ScoreBigStraight(),
        YachtCategory.Choice => dice.ScoreChoice(),
        YachtCategory.Yacht => dice.ScoreYacht(),
        _ => throw new ArgumentOutOfRangeException(nameof(category))
    };

    private static int ScoreSingles(this IEnumerable<int> dice, int single)
    {
        var result = 0;
        foreach (var item in dice)
        {
            if (item == single)
            {
                result += item;
            }
        }

        return result;
    }

    private static int ScoreFullHouse(this IEnumerable<int> dice)
    {
        var groups = dice.GetGroups();

        var result = 0;
        var count = 5;
        for (var i = 0; i < groups.Length; i++)
        {
            if (groups[i] is not (2 or 3))
            {
                continue;
            }

            result += (i + 1) * groups[i];
            count -= groups[i];
        }

        return count == 0 ? result : 0;
    }

    private static int ScoreFourOfAKind(this IEnumerable<int> dice)
    {
        var groups = dice.GetGroups();
        
        for (var i = 0; i < groups.Length; i++)
        {
            if (groups[i] >= 4)
            {
                return (i + 1) * 4;
            }
        }

        return 0;
    }

    private static int ScoreLittleStraight(this IEnumerable<int> dice)
    {
        var groups = dice.GetGroups();
        
        if (groups[5] != 0)
        {
            return 0;
        }

        for (var i = 0; i < 5; i++)
        {
            if (groups[i] != 1)
            {
                return 0;
            }
        }

        return 30;
    }
    
    private static int ScoreBigStraight(this IEnumerable<int> dice)
    {
        var groups = dice.GetGroups();
        
        if (groups[0] != 0)
        {
            return 0;
        }

        for (var i = 1; i < 6; i++)
        {
            if (groups[i] != 1)
            {
                return 0;
            }
        }

        return 30;
    }

    private static int ScoreChoice(this IEnumerable<int> dice)
    {
        var groups = dice.GetGroups();
        
        var result = 0;
        for (var i = 0; i < groups.Length; i++)
        {
            result += (i + 1) * groups[i];
        }

        return result;
    }
    
    private static int ScoreYacht(this IEnumerable<int> dice)
    {
        var groups = dice.GetGroups();
        
        for (var i = 0; i < groups.Length; i++)
        {
            if (groups[i] == 5)
            {
                return 50;
            }
        }

        return 0;
    }

    private static int[] GetGroups(this IEnumerable<int> dice)
    {
        var groups = new int[6];
        foreach (var item in dice)
        {
            groups[item - 1]++;
        }

        return groups;
    }
}

