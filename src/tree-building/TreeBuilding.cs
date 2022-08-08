using System;
using System.Collections.Generic;

internal class TreeBuildingRecord
{
    public int ParentId { get; set; }
    public int RecordId { get; set; }
}

internal class Tree
{
    public Tree(int id)
    {
        Id = id;
        Children = new List<Tree>();
    }
    
    public int Id { get; }
    public List<Tree> Children { get; }
    public bool IsLeaf => Children.Count == 0;
}

internal static class TreeBuilder
{
    private const int RootRecordId = 0;
    
    public static Tree BuildTree(IEnumerable<TreeBuildingRecord> records)
    {
        var orderedRecords = GetOrderedRecords(records);

        if (orderedRecords.Count == 0)
        {
            throw new ArgumentException(null);
        }
        
        var nodes = new Dictionary<int, Tree>();
        
        var previousRecordId = -1;
        foreach (var record in orderedRecords)
        {
            if (!Validate(record, previousRecordId))
            {
                throw new ArgumentException(null);
            }

            nodes[record.RecordId] = new Tree(record.RecordId);

            if (record.RecordId != RootRecordId)
            {
                nodes[record.ParentId].Children.Add(nodes[record.RecordId]);
            }

            previousRecordId++;
        }

        return nodes[RootRecordId];
    }

    private static IList<TreeBuildingRecord> GetOrderedRecords(IEnumerable<TreeBuildingRecord> records)
    {
        var result = new SortedList<int, TreeBuildingRecord>();
        foreach (var record in records)
        {
            result.Add(record.RecordId, record);
        }

        return result.Values;
    }

    private static bool Validate(TreeBuildingRecord record, int previousRecordId)
    {
        if (record.RecordId == RootRecordId && record.ParentId != RootRecordId)
        {
            return false;
        }

        if (record.RecordId != RootRecordId && record.ParentId >= record.RecordId)
        {
            return false;
        }

        if (record.RecordId != RootRecordId && record.RecordId != previousRecordId + 1)
        {
            return false;
        }

        return true;
    }
}
