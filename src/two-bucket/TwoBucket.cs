using System;
using System.Collections.Generic;

internal enum Bucket
{
    One,
    Two
}

internal class TwoBucketResult
{
    public int Moves { get; set; }
    public Bucket GoalBucket { get; set; }
    public int OtherBucket { get; set; }
}

internal class TwoBucket
{
    private readonly int[] _buckets;
    private readonly int _startBucket;

    public TwoBucket(int bucketOne, int bucketTwo, Bucket startBucket) =>
        (_buckets, _startBucket) = (new int[] { bucketOne, bucketTwo }, (int)startBucket);

    public TwoBucketResult Measure(int goal)
    {
        var invalid = new[] { 0, 0 };
        invalid[1 - _startBucket] = _buckets[1 - _startBucket];
        var invalidStr = string.Join(",", invalid);
        
        var buckets = new[] { 0, 0 };
        buckets[_startBucket] = _buckets[_startBucket];
        var goalBucket = Array.IndexOf(buckets, goal);
        
        var moves = 1;
        var toVisit = new Queue<(int[], int)>();
        var visited = new HashSet<string>();
        
        while (goalBucket < 0)
        {
            var key = string.Join(",", buckets);
            if (!visited.Contains(key) && !key.Equals(invalidStr))
            {
                visited.Add(key);
                var next = moves + 1;
                for (int i=0;i<2;i++)
                {
                    if (buckets[i] != 0)
                    {
                        toVisit.Enqueue((Empty(buckets, i), next));
                    }

                    if (buckets[i] == _buckets[i])
                    {
                        continue;
                    }

                    toVisit.Enqueue((Fill(buckets, i), next));
                    toVisit.Enqueue((Consolidate(buckets, i), next));
                }
            }

            if (toVisit.Count == 0)
            {
                throw new ArgumentException(null);
            }
                
            (buckets, moves) = toVisit.Dequeue();
            goalBucket = Array.IndexOf(buckets, goal);
        }
        
        return new TwoBucketResult
        {
            Moves = moves,
            GoalBucket = (Bucket)goalBucket,
            OtherBucket = buckets[1 - goalBucket]
        };
    }

    private int[] Empty(int[] buckets, int i) =>
        i == 0 ? new int[] { 0, buckets[1] } : new int[] { buckets[0], 0 };
    
    private int[] Fill(int[] buckets, int i) =>
        i == 0 ? new int[] { _buckets[0], buckets[1] } : new int[] { buckets[0], _buckets[1] };
    
    private int[] Consolidate(int[] buckets, int i)
    {
        var amount = Math.Min(buckets[1 - i], _buckets[i] - buckets[i]);
        var target = buckets[i] + amount;
        var src = buckets[1 - i] - amount;
        return i == 0 ? new[] { target, src } : new[] { src, target };
    }
}
