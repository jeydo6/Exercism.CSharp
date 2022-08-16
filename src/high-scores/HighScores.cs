using System;
using System.Collections.Generic;
using System.Linq;

internal class HighScores
{
    private readonly int[] _scores;

    public HighScores(List<int> list) => _scores = list.ToArray();

    public List<int> Scores() => new List<int>(_scores);

    public int Latest() => _scores[^1];

    public int PersonalBest() => Sort(_scores)[0];

    public List<int> PersonalTopThree() => new List<int>(
        Sort(_scores)[..Math.Min(_scores.Length, 3)]
    );

    private static int[] Sort(int[] source)
    {
        var destination = new int[source.Length];
        Array.Copy(source, destination, destination.Length);
        Array.Sort(destination, (a, b) => b - a);
        return destination;
    }
}
