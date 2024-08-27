namespace Duckie2Client.Libs.DatabaseManager;

public static class Common
{
    public const string DefaultReturnValueParameterName = "@ReturnValue";

    public struct ServerDatabaseNames
    {
        public string ServerName;
        public string DatabaseName;

        public void Deconstruct(out string servername, out string initialCatalog)
        {
            servername = ServerName;
            initialCatalog = DatabaseName;
        }
    }
}