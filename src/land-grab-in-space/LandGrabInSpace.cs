using System.Collections.Generic;

internal struct Coord
{
    public Coord(ushort x, ushort y)
    {
        X = x;
        Y = y;
    }

    public ushort X { get; }
    public ushort Y { get; }
}

internal readonly struct Plot
{
    public Plot(Coord topLeft, Coord topRight, Coord bottomLeft, Coord bottomRight) =>
        (TopLeft, TopRight, BottomLeft, BottomRight) = (topLeft, topRight, bottomLeft, bottomRight);

    private Coord TopLeft { get; }
    private Coord TopRight { get; }
    private Coord BottomLeft { get; }
    private Coord BottomRight { get; }

    public ushort GetLongestSide()
    {
        var sides = new ushort[]
        {
            (ushort)(TopRight.X - TopLeft.X),
            (ushort)(BottomRight.X - BottomLeft.X),
            (ushort)(BottomRight.Y - TopRight.Y),
            (ushort)(BottomLeft.Y - TopLeft.Y)
        };
        
        var longest = ushort.MinValue;
        foreach (var side in sides)
        {
            if (side > longest)
            {
                longest = side;
            }
        }

        return longest;
    }
}

internal class ClaimsHandler
{
    private readonly ISet<Plot> _plots = new HashSet<Plot>();
    
    private Plot _lastClaim;
    
    public void StakeClaim(Plot plot)
    {
        _lastClaim = plot;
        _plots.Add(plot);
    }

    public bool IsClaimStaked(Plot plot) => _plots.Contains(plot);

    public bool IsLastClaim(Plot plot) => _lastClaim.Equals(plot);

    public Plot GetClaimWithLongestSide()
    {
        var longest = new Plot();
        foreach (var plot in _plots)
        {
            if (plot.GetLongestSide() > longest.GetLongestSide())
            {
                longest = plot;
            }
        }

        return longest;
    }
}
