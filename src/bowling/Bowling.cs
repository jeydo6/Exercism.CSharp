using System;
using System.Collections.Generic;

internal class BowlingGame
{
    private const int NumberOfFrames = 10;
    private const int MaximumFrameScore = 10;

    private readonly IList<int> _rolls = new List<int>();

    public void Roll(int pins)
    {
        if (!IsValid(pins))
        {
            throw new ArgumentException(null, nameof(pins));
        }
        _rolls.Add(pins);
    }

    public int? Score()
    {
        var score = 0;
        var frameIndex = 0;

        if (_rolls.Count is < 12 or > 21)
        {
            throw new ArgumentException();
        }

        for (var i = 1; i <= NumberOfFrames; i++)
        {
            if (_rolls.Count <= frameIndex)
            {
                throw new ArgumentException();
            }

            if (IsStrike(frameIndex))
            {
                if (_rolls.Count <= frameIndex + 2)
                {
                    throw new ArgumentException();
                }

                var strikeBonus = StrikeBonus(frameIndex);
                if ((strikeBonus > MaximumFrameScore && !IsStrike(frameIndex + 1)) || strikeBonus > 20)
                {
                    throw new ArgumentException();
                }

                score += 10 + strikeBonus;
                frameIndex += i == NumberOfFrames ? 3 : 1;
            }
            else if (IsSpare(frameIndex))
            {
                if (_rolls.Count <= frameIndex + 2)
                {
                    throw new ArgumentException();
                }

                score += 10 + SpareBonus(frameIndex);
                frameIndex += i == NumberOfFrames ? 3 : 2;
            }
            else
            {
                var frameScore = FrameScore(frameIndex);
                if (frameScore is < 0 or > 10)
                {
                    throw new ArgumentException();
                }

                score += frameScore;
                frameIndex += 2;
            }
        }
        
        return frameIndex == _rolls.Count ? score : default(int?); 
    }
    
    private bool IsStrike(int frameIndex) => _rolls[frameIndex] == MaximumFrameScore;
    
    private bool IsSpare(int frameIndex) => _rolls[frameIndex] + _rolls[frameIndex + 1] == MaximumFrameScore;
    
    private int StrikeBonus(int frameIndex) => _rolls[frameIndex + 1] + _rolls[frameIndex + 2];
    
    private int SpareBonus(int frameIndex) => _rolls[frameIndex + 2];

    private int FrameScore(int frameIndex) => _rolls[frameIndex] + _rolls[frameIndex + 1];

    private bool IsValid(int pins)
    {
        if (
            pins is < 0 or > 10 ||
            _rolls.Count >= 21 ||
            _rolls.Count + 1 % 2 == 0 &&_rolls[^1] + pins > 10
        )
        {
            return false;
        }

        if (
            (_rolls.Count + 1) % 2 == 0 &&
            _rolls[^1] != 10 &&
            _rolls[^1] + pins > 10
        )
        {
            return false;
        }

        if (_rolls.Count != 20)
        {
            return true;
        }

        if (_rolls[18] != 10 && _rolls[18] + _rolls[19] != 10)
        {
            return false;
        }

        if (
            pins == 10 &&
            _rolls[18] + _rolls[19] != 10 &&
            (
                _rolls[18] != 10 ||
                _rolls[19] != 10 ||
                _rolls[19] + pins > 10 && _rolls[19] + pins != 20
            )
        )
        {
            return false;
        }

        return pins == 10 || _rolls[19] + pins <= 10 || _rolls[19] == 10;
    }
}
