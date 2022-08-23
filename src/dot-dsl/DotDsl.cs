using System.Collections;
using System.Collections.Generic;

internal class Attr
{
    private readonly string _key;
    private readonly string _value;
    
    public Attr(string key, string value) => (_key, _value) = (key, value);
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (Attr)obj;
        
        return _key == other._key && _value == other._value;
    }

    public override int GetHashCode() => _key.GetHashCode() ^ _value.GetHashCode();
}

internal class Node : IEnumerable<Attr>
{
    private readonly string _name;
    
    private readonly ICollection<Attr> _attrs = new List<Attr>();
    
    public Node(string name) => _name = name;
    
    public void Add(string key, string value) => _attrs.Add(new Attr(key, value));

    public IEnumerator<Attr> GetEnumerator() => _attrs.GetEnumerator();
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (Node)obj;

        return _name == other._name;
    }

    public override int GetHashCode() => _name.GetHashCode();
}

internal class Edge : IEnumerable<Attr>
{
    private readonly string _node1;
    private readonly string _node2;

    private readonly ICollection<Attr> _attrs = new List<Attr>();
    
    public Edge(string node1, string node2) => (_node1, _node2) = (node1, node2);
    
    public void Add(string key, string value) => _attrs.Add(new Attr(key, value));

    public IEnumerator<Attr> GetEnumerator() => _attrs.GetEnumerator();
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (Edge)obj;

        return _node1 == other._node1 && _node2 == other._node2;
    }

    public override int GetHashCode() => _node1.GetHashCode() ^ _node2.GetHashCode();
}

internal class Graph : IEnumerable<Attr>
{
    private readonly ICollection<Node> _nodes = new List<Node>();
    private readonly ICollection<Edge> _edges = new List<Edge>();
    private readonly ICollection<Attr> _attrs = new List<Attr>();

    public IEnumerable<Node> Nodes => _nodes;
    
    public IEnumerable<Edge> Edges => _edges;
    
    public IEnumerable<Attr> Attrs => _attrs;

    public void Add(Node node) => _nodes.Add(node);
    
    public void Add(Edge edge) => _edges.Add(edge);
    
    public void Add(string key, string value) => _attrs.Add(new Attr(key, value));

    public IEnumerator<Attr> GetEnumerator() => _attrs.GetEnumerator();
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
