using System;

internal static class SimpleCalculator
{
    public static string Calculate(int operand1, int operand2, string operation) => operation switch
    {
        null => throw new ArgumentNullException(nameof(operation)),
        "" => throw new ArgumentException(null, nameof(operation)),
        "+" => $"{operand1} + {operand2} = {SimpleOperation.Addition(operand1, operand2)}",
        "*" => $"{operand1} * {operand2} = {SimpleOperation.Multiplication(operand1, operand2)}",
        "/" => operand2 == 0 ? "Division by zero is not allowed." : $"{operand1} / {operand2} = {SimpleOperation.Division(operand1, operand2)}",
        _ => throw new ArgumentOutOfRangeException(nameof(operation))
    };
}
