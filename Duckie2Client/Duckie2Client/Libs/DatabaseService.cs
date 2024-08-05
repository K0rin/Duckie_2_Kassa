using System;
using System.Data.SqlClient;
using System.Security;
using Duckie2Client.Libs.Enums;

namespace Duckie2Client.Libs;

public class DatabaseService
{
    private readonly string _connectionString;
    private readonly CredentialTypes _currentCredentialType;
    private readonly SqlCredential? _sqlCredentials;


    // ReSharper disable once UnusedMember.Global
    // NOTE: Constructor is using in
    // Duckie2Client.Libs.Enums.Extensions.GetDatabaseService
    public DatabaseService(string serverName, string initialCatalog)
    {
        // todo: check for null value of each arguments.
        _connectionString =
            $"Data Source={serverName};" +
            $"Initial Catalog={initialCatalog};" +
            $"Integrated Security=True;";
        _currentCredentialType = CredentialTypes.Windows;
    }

    // ReSharper disable once UnusedMember.Global
    // NOTE: Constructor is using in
    // Duckie2Client.Libs.Enums.Extensions.GetDatabaseService
    public DatabaseService(
        string serverName,
        string initialCatalog,
        string userId,
        string password)
    {
        // todo: check for null value of each arguments.

        //  if (string.IsNullOrEmpty(password))
        // throw new ArgumentException("Password cannot be null or empty", nameof(password));

        // connetionString = @"Data Source=DESKTOP-H1O55SG\SQLEXPRESS;Initial Catalog=dummydatabase;User ID=sa;Password=demol23";
        _connectionString =
            $"Data Source={serverName};" +
            $"Initial Catalog={initialCatalog};";

        var securePwd = new SecureString();
        var passwordChars = password.ToCharArray();

        foreach (var c in passwordChars) securePwd.AppendChar(c);
        securePwd.MakeReadOnly();
        _sqlCredentials = new SqlCredential(userId, securePwd);
        securePwd.Dispose();
        _currentCredentialType = CredentialTypes.SqlServer;
    }


    public SqlConnection GetSqlConnection()
    {
        // throws ArgumentException

        var result = _currentCredentialType switch
        {
            CredentialTypes.Windows => new SqlConnection(_connectionString),
            CredentialTypes.SqlServer => new SqlConnection(
                _connectionString,
                _sqlCredentials),
            _ => throw new Exception("Unknown credential type.")
        };
        return result;
    }
}