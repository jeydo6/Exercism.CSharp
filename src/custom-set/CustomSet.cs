using System.Collections.Generic;

internal class CustomSet
{
    private readonly IDictionary<int, int> _items = new Dictionary<int, int>();
    
    public CustomSet(params int[] values)
    {
        foreach (var value in values)
        {
            if (_items.ContainsKey(value.GetHashCode()))
            {
                continue;
            }
            
            _items.Add(value.GetHashCode(), value);
        }
    }

    public CustomSet Add(int value)
    {
        var values = new List<int>(_items.Values)
        {
            value
        };
        
        return new CustomSet(values.ToArray());
    }

    public bool Empty() => _items.Count == 0;

    public bool Contains(int value) => _items.ContainsKey(value.GetHashCode());

    public bool Subset(CustomSet right)
    {
        foreach (var key in _items.Keys)
        {
            if (!right._items.ContainsKey(key))
            {
                return false;
            }
        }

        return true;
    }

    public bool Disjoint(CustomSet right)
    {
        foreach (var key in _items.Keys)
        {
            if (right._items.ContainsKey(key))
            {
                return false;
            }
        }

        return true;
    }

    public CustomSet Intersection(CustomSet right)
    {
        var values = new List<int>();
        foreach (var key in _items.Keys)
        {
            if (right._items.ContainsKey(key))
            {
                values.Add(right._items[key]);
            }
        }

        return new CustomSet(values.ToArray());
    }

    public CustomSet Difference(CustomSet right)
    {
        var values = new List<int>();
        foreach (var key in _items.Keys)
        {
            if (!right._items.ContainsKey(key))
            {
                values.Add(_items[key]);
            }
        }

        return new CustomSet(values.ToArray());
    }

    public CustomSet Union(CustomSet right)
    {
        var values = new List<int>(right._items.Values);
        
        foreach (var key in _items.Keys)
        {
            if (!right._items.ContainsKey(key))
            {
                values.Add(_items[key]);
            }
        }
        
        return new CustomSet(values.ToArray());
    }
    
    public override bool Equals(object obj)
    {
        if (obj is not CustomSet other)
        {
            return false;
        }

        if (_items.Count != other._items.Count)
        {
            return false;
        }

        foreach (var key in _items.Keys)
        {
            if (!other._items.ContainsKey(key))
            {
                return false;
            }
        }

        return true;
    }

    public override int GetHashCode() => _items.GetHashCode();
}
