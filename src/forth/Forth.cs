using System;
using System.Collections.Generic;
using System.Linq;

using Sprache;

internal static class Forth
{
    public static string Evaluate(IEnumerable<string> instructions)
    {
        var state = new ForthState();

        foreach (var instruction in instructions)
        {
            var expression = Expression.Parse(instruction);

            foreach (var definition in expression)
            {
                definition.Evaluate(state);
            }
        }

        return state.ToString();
    }
    
    private static readonly Parser<Constant> Constant =
        from number in Parse.Number
        from whitespace in Parse.WhiteSpace.Optional()
        select new Constant(int.Parse(number));

    private static readonly Parser<string> TermIdentifier =
        from term in Parse.Regex(@"[^\s;]+")
        from whitespace in Parse.WhiteSpace.Optional()
        select term.ToLowerInvariant();

    private static readonly Parser<ForthDefinition> Term =
        TermIdentifier.Select<string, ForthDefinition>(term => term switch
        {
            "+" => new Plus(),
            "-" => new Minus(),
            "*" => new Multiply(),
            "/" => new Division(),
            "dup" => new Dup(),
            "drop" => new Drop(),
            "swap" => new Swap(),
            "over" => new Over(),
            _ => new Word(term)
        });

    private static readonly Parser<ForthDefinition> CustomTerm =
        from a in Parse.Char(':')
        from b in Parse.WhiteSpace
        from customTerm in TermIdentifier
        from definitions in Constant.XOr(Term).AtLeastOnce()            
        from d in Parse.Char(';')
        from e in Parse.WhiteSpace.Optional()
        select new CustomTerm(customTerm, definitions);

    private static readonly Parser<IEnumerable<ForthDefinition>> Expression =
        (CustomTerm.XOr(Constant).XOr(Term)).Many();
}

internal class ForthState
{
    public Stack<int> Stack { get; } = new Stack<int>();
    public Dictionary<string, IEnumerable<ForthDefinition>> Mapping { get; } = new Dictionary<string, IEnumerable<ForthDefinition>>();
    public override string ToString() => string.Join(" ", Stack.Reverse());
}

internal abstract class ForthDefinition
{
    public abstract void Evaluate(ForthState state);
}

internal abstract class TermDefinition : ForthDefinition
{
    protected TermDefinition(string term) => Term = term;

    public string Term { get; }

    public override void Evaluate(ForthState state)
    {
        if (IsUserTerm(state))
            EvaluateUserTerm(state);
        else
            EvaluateDefaultTerm(state);
    }

    private bool IsUserTerm(ForthState state) => state.Mapping.ContainsKey(Term);

    protected virtual void EvaluateUserTerm(ForthState state)
    {
        foreach (var definition in state.Mapping[Term])
        {
            definition.Evaluate(state);
        }
    }

    protected abstract void EvaluateDefaultTerm(ForthState state);
}

internal abstract class UnaryOperation: TermDefinition
{
    protected UnaryOperation(string term) : base(term) { }

    protected override void EvaluateDefaultTerm(ForthState state)
    {
        if (state.Stack.Count < 1)
        {
            throw new InvalidOperationException();
        }

        var operand = state.Stack.Pop();

        foreach (var value in Operation(operand))
        {
            state.Stack.Push(value);
        }
    }

    protected abstract List<int> Operation(int x);
}

internal abstract class BinaryOperation : TermDefinition
{
    protected BinaryOperation(string term) : base(term) { }

    protected override void EvaluateDefaultTerm(ForthState state)
    {
        if (state.Stack.Count <= 1)
        {
            throw new InvalidOperationException();
        }

        var operand2 = state.Stack.Pop();
        var operand1 = state.Stack.Pop();

        foreach (var value in Operation(operand1, operand2))
        {
            state.Stack.Push(value);
        }
    }

    protected abstract List<int> Operation(int x, int y);
}

internal class Constant : ForthDefinition
{
    private readonly int _n;

    public Constant(int n) => _n = n;

    public override void Evaluate(ForthState state) => state.Stack.Push(_n);
}

internal class Word : TermDefinition
{
    public Word(string str) : base(str) { }

    protected override void EvaluateDefaultTerm(ForthState state)
    {
        if (!state.Mapping.ContainsKey(Term))
        {
            throw new InvalidOperationException();
        }   
    }
}

internal class Dup: UnaryOperation
{
    public Dup() : base("dup") { }
    protected override List<int> Operation(int x) => new List<int> { x, x };
}

internal class Drop : UnaryOperation
{
    public Drop() : base("drop") { }
    protected override List<int> Operation(int x) => new List<int>();
}

internal class Swap : BinaryOperation
{
    public Swap() : base("swap") { }
    protected override List<int> Operation(int x, int y) => new List<int> { y, x };
}

internal class Over : BinaryOperation
{
    public Over() : base("over") { }
    protected override List<int> Operation(int x, int y) => new List<int> { x, y, x };
}

internal class Plus : BinaryOperation
{
    public Plus() : base("+") { }
    protected override List<int> Operation(int x, int y) => new List<int> { x + y };
}

internal class Minus : BinaryOperation
{
    public Minus() : base("-") { }
    protected override List<int> Operation(int x, int y) => new List<int> { x - y };
}

internal class Multiply : BinaryOperation
{
    public Multiply() : base("*") { }
    protected override List<int> Operation(int x, int y) => new List<int> { x * y };
}

internal class Division : BinaryOperation
{
    public Division() : base("/") { }

    protected override List<int> Operation(int x, int y) =>
        y == 0 ? throw new DivideByZeroException() : new List<int> { x / y };
}

internal class CustomTerm : ForthDefinition
{
    private readonly string _term;
    private readonly ForthDefinition[] _actions;

    public CustomTerm(string term, IEnumerable<ForthDefinition> actions)
    {
        _term = term;
        _actions = actions.ToArray();
    }

    public override void Evaluate(ForthState state)
    {
        if (int.TryParse(_term, out _))
        {
            throw new InvalidOperationException();
        }

        var normalizedActions = new List<ForthDefinition>();

        foreach (var action in _actions)
        {
            if (action is Word word)
            {
                normalizedActions.AddRange(state.Mapping[word.Term]);
            }
            else
            {
                normalizedActions.Add(action);
            }
        }
        
        state.Mapping[_term] = normalizedActions;
    }
}
