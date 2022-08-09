internal enum Direction
{
    North,
    East,
    South,
    West
}

internal class RobotSimulator
{
    public RobotSimulator(Direction direction, int x, int y) => (Direction, X, Y) = (direction, x, y);

    public Direction Direction { get; private set; }

    public int X { get; private set; }

    public int Y { get; private set; }

    public void Move(string instructions)
    {
        foreach (var instruction in instructions)
        {
            switch (instruction)
            {
                case 'R':
                    TurnRight();
                    break;
                case 'L':
                    TurnLeft();
                    break;
                case 'A':
                    Advance();
                    break;
            }
        }
    }

    private void TurnRight() => Direction = Direction == Direction.West ? Direction.North : Direction + 1;

    private void TurnLeft() => Direction = Direction == Direction.North ? Direction.West : Direction - 1;

    private void Advance() => (X, Y) = Direction switch
    {
        Direction.North => (X, Y + 1),
        Direction.South => (X, Y - 1),
        Direction.East => (X + 1, Y),
        Direction.West => (X - 1, Y),
        _ => (X, Y)
    };
}
