using System;

internal class Orm
{
    private readonly Database _database;

    public Orm(Database database)
    {
        _database = database;
    }

    public void Write(string data)
    {
        try
        {
            _database.BeginTransaction();
            _database.Write(data);
            _database.EndTransaction();
        }
        finally
        {
            _database.Dispose();
        }
    }

    public bool WriteSafely(string data)
    {
        try
        {
            Write(data);
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }
}
