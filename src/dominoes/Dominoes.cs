using System.Collections.Generic;

internal static class Dominoes
{
    public static bool CanChain(IEnumerable<(int, int)> dominoes)
    {
        foreach (var combination in GetCombinations(new List<(int, int)>(dominoes)))
        {
            var canChain = CanChain(combination);
            if (canChain)
            {
                return true;
            }
        }
        return false;
    }
    
    private static bool CanChain(IList<(int, int)> dominoes)
    {
        if (dominoes.Count == 0)
        {
            return true;
        }
        
        for (var i = 0; i < dominoes.Count - 1; i++)
        {
            if (dominoes[i].Item2 == dominoes[i + 1].Item2)
            {
                dominoes[i + 1] = (dominoes[i + 1].Item2, dominoes[i + 1].Item1);
            }
            
            if (dominoes[i].Item2 != dominoes[i + 1].Item1)
            {
                return false;
            }
        }

        return dominoes[0].Item1 == dominoes[^1].Item2;
    }

    private static IEnumerable<IList<(int, int)>> GetCombinations(IList<(int, int)> dominoes)
    {
        var counter = new Dictionary<(int, int), int>();
        foreach (var pair in dominoes)
        {
            if (!counter.ContainsKey(pair))
            {
                counter[pair] = 0;
            }
    
            counter[pair]++;
        }
    
        return GetCombinations(counter, dominoes.Count, new Stack<(int, int)>());
    }
    
    private static IEnumerable<IList<(int, int)>> GetCombinations(IDictionary<(int, int), int> counter, int size, Stack<(int, int)> combination)
    {
        var result = new List<IList<(int, int)>>();
        if (combination.Count == size)
        {
            result.Add(
                new List<(int, int)>(combination)
            );

            return result;
        }
        
        foreach (var (pair, count) in counter)
        {
            if (count == 0)
            {
                continue;
            }
            
            combination.Push(pair);
            counter[pair]--;

            result.AddRange(
                GetCombinations(counter, size, combination)
            );
            
            combination.Pop();
            counter[pair] = count;
        }

        return result;
    }
}
