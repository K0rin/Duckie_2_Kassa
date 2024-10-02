using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Enums.Flags;

namespace Duckie2Client.Services.DbmsService;

public class PriceTypes : CrudOperationsBase
{
    public override object Read<TDataModel>(RecordReadFlags readFlags)
    {
        object result;

        using (var db = new DbmsService())
        {
            result = db.PriceTypes.ToList();
        }

        return (List<TDataModel>)result;
    }
}