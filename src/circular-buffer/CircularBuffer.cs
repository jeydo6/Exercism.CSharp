using System;
using System.Collections.Generic;

internal class CircularBuffer<T>
{
    private readonly LinkedList<T> _values = new LinkedList<T>();
    private readonly int _capacity;

    public CircularBuffer(int capacity) => _capacity = capacity;

    public T Read()
    {
        if (_values.Count <= 0)
        {
            throw new InvalidOperationException();
        }

        var value = _values.First!.Value;
        _values.RemoveFirst();
        return value;
    }

    public void Write(T value)
    {
        if (_values.Count < _capacity)
        {
            _values.AddLast(value);
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    public void Overwrite(T value)
    {
        if (_values.Count < _capacity)
        {
            _values.AddLast(value);
        }
        else
        {
            Read();
            Write(value);
        }
    }

    public void Clear() => _values.Clear();
}
