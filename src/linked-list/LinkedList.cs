using System;

internal class Deque<T>
{
    private class Node
    {
        public Node() { }

        public Node(T value) => Value = value;

        public T Value { get; }
        
        public Node Prev { get; set; }
        
        public Node Next { get; set; }
    }

    private readonly Node _head;
    private readonly Node _tail;

    public Deque()
    {
        _head = new Node();
        _tail = new Node();

        _head.Next = _tail;
        _tail.Prev = _head;
    }
    
    public void Push(T value)
    {
        var node = new Node(value)
        {
            Prev = _tail.Prev,
            Next = _tail
        };

        _tail.Prev.Next = node;
        _tail.Prev = node;
    }

    public T Pop()
    {
        var node = _tail.Prev;

        _tail.Prev = _tail.Prev.Prev;
        _tail.Prev.Next = _tail;

        return node.Value;
    }

    public void Unshift(T value)
    {
        var node = new Node(value)
        {
            Next = _head.Next,
            Prev = _head
        };

        _head.Next.Prev = node;
        _head.Next = node;
    }

    public T Shift()
    {
        var node = _head.Next;

        _head.Next = _head.Next.Next;
        _head.Next.Prev = _head;

        return node.Value;
    }
}
