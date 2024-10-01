using System.Collections.Generic;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Services.DbmsService.Records;
using Duckie2Client.Services.DbmsService.Records.Builders;

namespace Duckie2Client.Services.DbmsService;

public abstract class CrudOperationsBase
{
    public virtual DataModelOperationResult Create<T>(RecordBuilderBase<T> builder) where T : RecordBase, new()
    {
        throw new System.NotSupportedException();
    }

    public virtual object Read<TDataModel>(RecordReadFlags readFlags)
    {
        throw new System.NotSupportedException();
    }
}