using System;
using System.Collections.Generic;
using System.Linq;

internal enum Owner
{
    None,
    Black,
    White
}

internal class GoCounting
{
    private readonly Owner[][] _board;
    
    private int Rows => _board.Length;
    private int Cols => _board[0].Length;

    public GoCounting(string input) => _board = input
        .Split('\n')
        .Select(row => row
            .Select(ch => ch switch
            {
                'B' => Owner.Black,
                'W' => Owner.White,
                _ => Owner.None
            })
            .ToArray()
        )
        .ToArray();

    public ValueTuple<Owner, HashSet<(int Col, int Row)>> Territory((int Col, int Row) coordinate)
    {
        if (coordinate.Row < 0 || coordinate.Row >= Rows || coordinate.Col < 0 || coordinate.Col >= Cols)
        {
            throw new ArgumentException(null, nameof(coordinate));
        }
        
        var coordinates = TerritoryHelper(coordinate);
        if (!coordinates.Any())
        {
            return (Owner.None, new HashSet<(int Col, int Row)>());
        }
        
        var owner = TerritoryOwner(coordinates);
        return (owner, new HashSet<(int Col, int Row)>(coordinates.OrderBy(x => x.Col).ThenBy(x => x.Row)));
    }

    public Dictionary<Owner, HashSet<(int Col, int Row)>> Territories()
    {
        var emptyCoordinates = EmptyCoordinates();

        var territories = new Dictionary<Owner, HashSet<(int Col, int Row)>>
        {
            [Owner.Black] = new HashSet<(int Col, int Row)>(),
            [Owner.White] = new HashSet<(int Col, int Row)>(),
            [Owner.None] = new HashSet<(int Col, int Row)>()
        };
        
        return TerritoriesHelper(new HashSet<(int Col, int Row)>(emptyCoordinates), territories);
    }
    
    private Owner GetPlayer((int Col, int Row) coordinate) => _board[coordinate.Row][coordinate.Col];
    
    private bool IsEmpty((int Col, int Row) coordinate) => GetPlayer(coordinate) == Owner.None;
    private bool IsTaken((int Col, int Row) coordinate) => GetPlayer(coordinate) != Owner.None;
    private bool IsValid((int Col, int Row) coordinate) =>
        coordinate.Row >= 0 && coordinate.Row < Rows &&
        coordinate.Col >= 0 && coordinate.Col < Cols;
    
    private IEnumerable<(int Col, int Row)> EmptyCoordinates() =>
        Enumerable.Range(0, Cols).SelectMany(col =>
        Enumerable.Range(0, Rows).Select(row => (col, row)))
            .Where(IsEmpty);
    
    private IEnumerable<(int Col, int Row)> NeighborCoordinates((int Col, int Row) coordinate) => new (int Col, int Row)[]
    {
        (coordinate.Col, coordinate.Row - 1),
        (coordinate.Col - 1, coordinate.Row),
        (coordinate.Col + 1, coordinate.Row),
        (coordinate.Col, coordinate.Row + 1)
    }.Where(IsValid);
    
    private IEnumerable<(int Col, int Row)> TakenNeighborCoordinates((int Col, int Row) coordinate) =>
        NeighborCoordinates(coordinate).Where(IsTaken);

    private IEnumerable<(int Col, int Row)> EmptyNeighborCoordinates((int Col, int Row) coordinate) =>
        NeighborCoordinates(coordinate).Where(IsEmpty);
    
    private Owner TerritoryOwner(IEnumerable<(int Col, int Row)> coordinates)
    {
        var neighborColors = coordinates.SelectMany(TakenNeighborCoordinates).Select(GetPlayer);
        var uniqueNeighborColors = new HashSet<Owner>(neighborColors);

        return uniqueNeighborColors.Count == 1 ? uniqueNeighborColors.First() : Owner.None;
    }
    
    private HashSet<(int Col, int Row)> TerritoryHelper((int Col, int Row) coordinate) => IsValid(coordinate) && IsEmpty(coordinate) ?
        TerritoryHelper(new HashSet<(int Col, int Row)> { coordinate }, new HashSet<(int Col, int Row)> { coordinate }) :
        new HashSet<(int Col, int Row)>();
    
    private HashSet<(int Col, int Row)> TerritoryHelper(HashSet<(int Col, int Row)> remainder, HashSet<(int Col, int Row)> acc)
    {
        if (!remainder.Any())
        {
            return acc;
        }

        var emptyNeighbors = new HashSet<(int Col, int Row)>(remainder.SelectMany(EmptyNeighborCoordinates));
        emptyNeighbors.ExceptWith(acc);

        var newAcc = new HashSet<(int Col, int Row)>(acc);
        newAcc.UnionWith(emptyNeighbors);
        return TerritoryHelper(emptyNeighbors, newAcc);
    }

    private Dictionary<Owner, HashSet<(int Col, int Row)>> TerritoriesHelper(HashSet<(int Col, int Row)> remainder, Dictionary<Owner, HashSet<(int Col, int Row)>> acc)
    {
        if (!remainder.Any())
        {
            return acc;
        }

        var coordinate = remainder.First();
        var coordinates = TerritoryHelper(coordinate);
        var owner = TerritoryOwner(coordinates);

        var newRemainder = new HashSet<(int Col, int Row)>(remainder);
        newRemainder.ExceptWith(coordinates);

        acc[owner].UnionWith(coordinates);
        return TerritoriesHelper(newRemainder, acc);
    }
}
