using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

internal class Reactor
{
    private readonly IDictionary<int, Cell> _cells = new Dictionary<int, Cell>();
    
    public InputCell CreateInputCell(int value)
    {
        var inputCell = new InputCell(_cells.Count, value);
        inputCell.Changed += CellChanged;

        _cells[inputCell.Id] = inputCell;        

        return inputCell;
    }

    public ComputeCell CreateComputeCell(IEnumerable<Cell> producers, Func<int[], int> compute)
    {
        var computeCell = new ComputeCell(_cells.Count, producers, compute);
        _cells[computeCell.Id] = computeCell;

        return computeCell;
    }
    
    private void CellChanged(object sender, int value)
    {
        var cell = (Cell)sender;
        var consumers = new BitArray(_cells.Count);

        foreach (var consumer in cell.Consumers)
        {
            consumers.Set(consumer.Id, true);
        }

        for (var id = cell.Id + 1; id < _cells.Count; id++)
        {
            if (!consumers.Get(id))
            {
                continue;
            }

            var consumer = (ComputeCell)_cells[id];
            consumer.Recompute();

            foreach (var transitiveConsumer in consumer.Consumers)
            {
                consumers.Set(transitiveConsumer.Id, true);
            }
        }
    }
}

internal abstract class Cell
{
    protected Cell(int id)
    {
        Id = id;
        Consumers = new List<Cell>();
    }

    public int Id { get; }
    public List<Cell> Consumers { get; }
    
    public abstract int Value { get; set; }
    public abstract event EventHandler<int> Changed;
}

internal class InputCell : Cell
{
    private int _value;

    public InputCell(int id, int value) : base(id) => _value = value;

    public override event EventHandler<int> Changed;

    public override int Value
    {
        get => _value;
        set
        {
            if (_value == value)
            {
                return;
            }

            _value = value;
            Changed?.Invoke(this, _value);
        }
    }  
}

internal class ComputeCell : Cell
{
    private readonly Cell[] _producers;
    private readonly Func<int[], int> _compute;
    
    public ComputeCell(int id, IEnumerable<Cell> producers, Func<int[], int> compute) : base(id)
    {
        _producers = producers.ToArray();
        _compute = compute;

        foreach (var producer in _producers)
        {
            producer.Consumers.Add(this);
        }

        Recompute();
    }

    public override int Value { get; set; }
    public override event EventHandler<int> Changed;

    public void Recompute()
    {
        var updatedValue = _compute(_producers.Select(producer => producer.Value).ToArray());

        if (updatedValue == Value)
        {
            return;
        }
        
        Value = updatedValue;
        Changed?.Invoke(this, updatedValue);
    }
}
