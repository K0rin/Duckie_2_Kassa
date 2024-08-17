using System.Data.SqlClient;

namespace Duckie2Client.Services.Commands;

public interface ISqlDuckieCommand : IDuckieCommand
{
    public void ExeccuteWithConnection(SqlConnection connection);
}