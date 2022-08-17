using System.Collections.Generic;
using System.Linq;

internal static class Poker
{
    private struct Card
    {
        public int Rank;
        public int Suit;
    }

    private struct Hand
    {
        public string Input;
        public Score Result;
    }
    
    private struct Score
    {
        public int RegularValue;
        public int TieBreakerValue;
    }
    
    public static IEnumerable<string> BestHands(IEnumerable<string> hands)
    {
        var scoredHands = hands.Select(ParseHand).ToArray();
        var maxScore = scoredHands.Max(h => h.Result.RegularValue);
        var maxHands = scoredHands.Where(h => h.Result.RegularValue == maxScore).ToArray();

        return maxHands
            .Where(h => h.Result.TieBreakerValue == maxHands.Max(m => m.Result.TieBreakerValue))
            .Select(s => s.Input)
            .ToList();
    }

    private static Score ScoreHand(Card[] cards)
    {
        var cardsByRank = cards
            .GroupBy(c => c.Rank)
            .OrderByDescending(c => c.Count())
            .Select(g => g.Max(c => c.Rank))
            .ToArray();

        var rankCounts = cards
            .GroupBy(c => c.Rank)
            .Select(g => g.Count())
            .OrderByDescending(c => c)
            .ToArray();

        var ranks = cards.Select(c => c.Rank).ToArray();
        var suits = cards.Select(c => c.Suit).ToArray();

        if (ranks.SequenceEqual(new[] { 14, 5, 4, 3, 2 }))
        {
            ranks = new[] { 5, 4, 3, 2, 1 };
        }

        var flush = suits.Distinct().Count() == 1;
        var straight = ranks.Distinct().Count() == 5 && ranks[0] - ranks[4] == 4;

        if (straight && flush)
        {
            return new Score
            {
                RegularValue = 800 + ranks.First()
            };
        }

        if (rankCounts.SequenceEqual(new[] { 4, 1 }))
        {
            return new Score
            {
                RegularValue = 700 + cardsByRank[0],
                TieBreakerValue = cardsByRank[1]
            };
        }

        if (rankCounts.SequenceEqual(new[] { 3, 2 }))
        {
            return new Score
            {
                RegularValue = 600 + cardsByRank[0],
                TieBreakerValue = cardsByRank[1]
            };
        }

        if (flush)
        {
            return new Score
            {
                RegularValue = 500 + ranks.First()
            };
        }

        if (straight)
        {
            return new Score
            {
                RegularValue = 400 + ranks.First()
            };
        }

        if (rankCounts.SequenceEqual(new[] { 3, 1, 1 }))
        {
            return new Score
            {
                RegularValue = 300 + cardsByRank[0],
                TieBreakerValue = cardsByRank[1]
            };
        }

        if (rankCounts.SequenceEqual(new[] { 2, 2, 1 }))
        {
            return new Score
            {
                RegularValue = 200 + cardsByRank[0] + cardsByRank[1],
                TieBreakerValue = cardsByRank[2]
            };
        }

        if (rankCounts.SequenceEqual(new[] { 2, 1, 1, 1 }))
        {
            return new Score
            {
                RegularValue = 100 + cardsByRank[0],
                TieBreakerValue = 0
            };
        }

        return new Score
        {
            RegularValue = ranks.Max(),
            TieBreakerValue = cardsByRank[4]
        };
    }
    
    private static Hand ParseHand(string hand) => new Hand { Input = hand, Result = ScoreHand(ParseCards(hand)) };

    private static Card[] ParseCards(string hand) => hand
        .Replace("10", "T")
        .Split(' ')
        .Select(ParseCard)
        .OrderByDescending(c => c.Rank)
        .ToArray();

    private static Card ParseCard(string card) => new Card { Rank = ParseRank(card), Suit = ParseSuit(card) };

    private static int ParseRank(string card) => "..23456789TJQKA".IndexOf(card[0]);

    private static int ParseSuit(string card) => ".HSDC".IndexOf(card[1]);
}
