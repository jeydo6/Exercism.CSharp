using System;
using System.Collections.Generic;

internal class Robot
{
    private static readonly HashSet<string> _usedNames = new HashSet<string>();
    private static readonly Random _random = new Random();

    private string _name;
    
    public string Name
    {
        get
        {
            if (_name != null)
            {
                return _name;
            }

            var nameCandidate = GetRandomName();
            while (_usedNames.Contains(nameCandidate))
            {
                nameCandidate = GetRandomName();
            }
            
            _name = nameCandidate;
            _usedNames.Add(nameCandidate);

            return _name;
        }
    }

    public void Reset() => _name = null;
    
    private static string GetRandomName()
    {
        var number = _random.Next(100, 675 * 1000 + 1000);

        var result = new char[5];

        for (var i = result.Length - 1; i >= 2; i--)
        {
            result[i] = (char)('0' + number % 10);
            number /= 10;
        }

        for (var i = 1; i >= 0; i--)
        {
            result[i] = (char)('A' + number % 26);
            number /= 26;
        }
        
        return new string(result);
    }
}
