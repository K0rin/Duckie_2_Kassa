using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Enums.Flags;

namespace Duckie2Client.Services.DbmsService;

public class PollutionLevels : CrudOperationsBase
{
    public override object Read<TDataModel>(RecordReadFlags readFlags)
    {
        object result;

        using (var db = new DbmsService())
        {
            result = db.PollutionLevels.ToList();
        }

        return (List<TDataModel>)result;
    }
}