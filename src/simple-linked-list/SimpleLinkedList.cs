using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

internal class SimpleLinkedList<T> : IEnumerable<T>
{
    public SimpleLinkedList(T value) => Value = value;

    public SimpleLinkedList(IEnumerable<T> values)
    {
        var index = 0;
        foreach (var value in values)
        {
            if (index == 0)
            {
                Value = value;
            }
            else
            {
                Add(value);
            }

            index++;
        }
    }

    public T Value { get; }

    public SimpleLinkedList<T> Next  { get; private set; }

    public SimpleLinkedList<T> Add(T value)
    {
        var last = this;

        while (last.Next != null)
        {
            last = last.Next;
        }

        last.Next = new SimpleLinkedList<T>(value);

        return this;
    }

    public IEnumerator<T> GetEnumerator()
    {
        yield return Value;

        if (Next is null)
        {
            yield break;
        }

        foreach (var next in Next)
        {
            yield return next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
