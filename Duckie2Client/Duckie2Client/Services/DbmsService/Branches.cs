using Duckie2Client.Enums.Flags;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.DbmsService.Records;
using Duckie2Client.Services.DbmsService.Records.Builders;

namespace Duckie2Client.Services.DbmsService;

public class Branches : CrudOperationsBase
{
    /*
     User example:

     var branchRecordBuilder = new BranchRecordBuilder();
     branchRecordBuilder.AddName("Branch 3");
     branchRecordBuilder.AddAddress("Branch 3 Address");
     new Branches().Create(branchRecordBuilder);
     */

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
}