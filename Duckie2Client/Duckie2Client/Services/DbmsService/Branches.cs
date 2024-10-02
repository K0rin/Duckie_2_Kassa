using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.DbmsService.Records;
using Duckie2Client.Services.DbmsService.Records.Builders;

namespace Duckie2Client.Services.DbmsService;

public class Branches : CrudOperationsBase
{
    public override DataModelOperationResult Create<T>(RecordBuilderBase<T> builder)
    {
        var branchRecord = builder.Build() as BranchRecord;
        using var db = new DbmsService();

        var newBranch = new Branch
        {
            Name = null!,
            Address = null!
        };

        PropertySetter.SetProperties<BranchRecord, Branch>(ref branchRecord!, ref newBranch);

        db.Branches.Add(newBranch);
        db.SaveChanges();

        return DataModelOperationResult.Successful;
    }

    public override object Read<TDataModel>(RecordReadFlags readFlags)
    {
        object result;

        using (var db = new DbmsService())
        {
            result = db.Branches.ToList();
        }

        return (List<TDataModel>)result;
    }
}