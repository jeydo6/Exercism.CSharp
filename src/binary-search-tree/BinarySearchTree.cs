using System;
using System.Collections;
using System.Collections.Generic;

internal class BinarySearchTree : IEnumerable<int>
{
    public BinarySearchTree(int value) => Value = value;

    public BinarySearchTree(IEnumerable<int> values)
    {
        var count = 0;
        foreach (var value in values)
        {
            if (count == 0)
            {
                Value = value;
            }
            else
            {
                Add(value);
            }
            
            count++;
        }

        if (count == 0)
        {
            throw new ArgumentException(null, nameof(values));
        }
    }

    public int Value { get; }

    public BinarySearchTree Left { get; private set; }

    public BinarySearchTree Right { get; private set; }
    
    public IEnumerator<int> GetEnumerator()
    {
        if (Left is not null)
        {
            foreach (var left in Left)
            {
                yield return left;
            }
        }

        yield return Value;

        if (Right is not null)
        {
            foreach (var right in Right)
            {
                yield return right;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    private void Add(int value)
    {
        if (value <= Value)
        {
            if (Left is null)
            {
                Left = new BinarySearchTree(value);
            }
            else
            {
                Left.Add(value);
            }
        }
        else
        {
            if (Right is null)
            {
                Right = new BinarySearchTree(value);
            }
            else
            {
                Right.Add(value);
            }
        }
    }
}
