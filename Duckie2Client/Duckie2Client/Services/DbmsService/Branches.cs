using System;
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
        if (builder.Build() is not BranchRecord branchRecord) return DataModelOperationResult.RecordNotFound;
        var newBranch = CreateBranch(branchRecord);
        using var db = new DbmsService();
        return AddAndSave(db.Branches, db, newBranch);
    }

    private static Branch CreateBranch(BranchRecord? branchRecord)
    {
        var newBranch = new Branch
        {
            Name = null!,
            Address = null!
        };
        PropertySetter.SetProperties<BranchRecord, Branch>(ref branchRecord!, ref newBranch);
        return newBranch;
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

    public static Guid FindBranchId(Guid userId)
    {

        using var db = new DbmsService();

        var foundBranch = db.Branches
            .Where(branch => branch.Users.Any(user => user.Id == userId))
            .FirstOrDefault();

        return foundBranch.Id;
    }
}