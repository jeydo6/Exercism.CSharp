using System;
using System.Collections.Generic;

internal class BinTree : IEquatable<BinTree>
{
    public BinTree(int value, BinTree left, BinTree right) =>
        (Value, Left, Right) = (value, left, right);
    
    public BinTree(BinTree tree) : this(tree.Value, tree.Left, tree.Right) { }

    public int Value { get; }
    
    public BinTree Left { get; }
    
    public BinTree Right { get; }
    
    public bool Equals(BinTree other)
    {
        if (other == null || !Equals(Value, other.Value))
            return false;

        if (!ReferenceEquals(Left, other.Left) && (!Left?.Equals(other.Left) ?? false))
            return false;

        if (!ReferenceEquals(Right, other.Right) && (!Right?.Equals(other.Right) ?? false))
            return false;

        return true;
    }
    
    public override int GetHashCode() => HashCode.Combine(Value, Left, Right);
}

internal class Zipper : IEquatable<Zipper>
{
    private Zipper(int value, BinTree left, BinTree right, List<BinTreeCrumb> crumbs) =>
        (_value, _left, _right, _crumbs) = (value, left, right, crumbs);
    
    private abstract class BinTreeCrumb
    {
        protected BinTreeCrumb(int value, BinTree tree) =>
            (Value, Tree) = (value, tree);

        public int Value { get; }
        public BinTree Tree { get; }
    }

    private class BinTreeLeftCrumb : BinTreeCrumb
    {
        public BinTreeLeftCrumb(int value, BinTree tree) : base(value, tree)
        {
        }
    }

    private class BinTreeRightCrumb : BinTreeCrumb
    {
        public BinTreeRightCrumb(int value, BinTree tree) : base(value, tree)
        {
        }
    }

    private readonly int _value;

    private readonly BinTree _left;

    private readonly BinTree _right;

    private readonly List<BinTreeCrumb> _crumbs;

    public int Value() => _value;

    public Zipper SetValue(int newValue) => new Zipper(newValue, _left, _right, _crumbs);

    public Zipper SetLeft(BinTree binTree) => new Zipper(_value, binTree, _right, _crumbs);

    public Zipper SetRight(BinTree binTree) => new Zipper(_value, _left, binTree, _crumbs);

    public Zipper Left()
    {
        if (_left == null)
        {
            return null;
        }

        var newCrumbs = new List<BinTreeCrumb> { new BinTreeLeftCrumb(_value, _right) };
        newCrumbs.AddRange(_crumbs);

        return new Zipper(_left.Value, _left.Left, _left.Right, newCrumbs);
    }

    public Zipper Right()
    {
        if (_right == null)
        {
            return null;
        }

        var newCrumbs = new List<BinTreeCrumb> { new BinTreeRightCrumb(_value, _left) };
        newCrumbs.AddRange(_crumbs);

        return new Zipper(_right.Value, _right.Left, _right.Right, newCrumbs);
    }

    public Zipper Up()
    {
        if (_crumbs.Count == 0)
        {
            return null;
        }

        var firstCrumb = _crumbs[0];
        var remainingCrumbs = _crumbs.GetRange(1, _crumbs.Count - 1);

        return firstCrumb switch
        {
            BinTreeLeftCrumb => new Zipper(firstCrumb.Value, new BinTree(_value, _left, _right), firstCrumb.Tree, remainingCrumbs),
            BinTreeRightCrumb => new Zipper(firstCrumb.Value, firstCrumb.Tree, new BinTree(_value, _left, _right), remainingCrumbs),
            _ => null
        };
    }

    public BinTree ToTree()
    {
        var tree = new BinTree(_value, _left, _right);

        foreach (var crumb in _crumbs)
        {
            tree = crumb switch
            {
                BinTreeLeftCrumb => new BinTree(crumb.Value, new BinTree(tree), crumb.Tree),
                BinTreeRightCrumb => new BinTree(crumb.Value, crumb.Tree, new BinTree(tree)),
                _ => tree
            };
        }

        return tree;
    }

    public static Zipper FromTree(BinTree tree) => new Zipper(tree.Value, tree.Left, tree.Right, new List<BinTreeCrumb>());

    public bool Equals(Zipper other) =>
        other != null && ToTree().Equals(other.ToTree());

    public override int GetHashCode() => HashCode.Combine(_value, _left, _right, _crumbs);
}
