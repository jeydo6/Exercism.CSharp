using System;
using System.Collections.Generic;

internal enum Plant
{
    Violets,
    Radishes,
    Clover,
    Grass
}

internal class KindergartenGarden
{
    private readonly IDictionary<int, Plant[]> _plants = new Dictionary<int, Plant[]>();

    public KindergartenGarden(string diagram)
    {
        var lines = diagram.Split('\n');

        for (var studentIndex = 0; studentIndex < lines[0].Length / 2; studentIndex++)
        {
            _plants[studentIndex] = new Plant[]
            {
                GetPlant(lines[0][studentIndex * 2]),
                GetPlant(lines[0][studentIndex * 2 + 1]),
                GetPlant(lines[1][studentIndex * 2]),
                GetPlant(lines[1][studentIndex * 2 + 1])
            };
        }
    }

    public IEnumerable<Plant> Plants(string student) => _plants[student[0] - 'A'];

    private static Plant GetPlant(char plantCode) => plantCode switch
    {
        'V' => Plant.Violets,
        'R' => Plant.Radishes,
        'C' => Plant.Clover,
        'G' => Plant.Grass,
        _ => throw new ArgumentOutOfRangeException(nameof(plantCode))
    };
}
