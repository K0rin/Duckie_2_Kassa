namespace Duckie2Client.Services;

public class Database
{
    public Database()
    {
        IsDbServiceRun();
    }

    private static void IsDbServiceRun()
    {
        // todo: check DBMS running.
        // throw new DuckieException(ErrorCodes.DbServiceUnavailable);
    }
}