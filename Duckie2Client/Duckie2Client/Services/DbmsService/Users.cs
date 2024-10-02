using System;
using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.DbmsService.Records;
using Duckie2Client.Services.DbmsService.Records.Builders;
using Microsoft.EntityFrameworkCore;

namespace Duckie2Client.Services.DbmsService;

/// <summary>
/// Contains functionality for working with the "User" database entity.
/// </summary>
public class Users : CrudOperationsBase
{
    public override DataModelOperationResult Create<T>(RecordBuilderBase<T> builder)
    {
        using var db = new DbmsService();
        var builtUser = builder.Build() as UserRecord;

        // Branch
        // todo: error: record not found.
        // todo: async tasks: Branch getting
        var existingBranches = builtUser!.Branch!.Select(
            branchRecord => db.Branches.First(
                branch => branch.Id.Equals(branchRecord.Id))
        ).ToList();

        // Salary Rate

        var r = builtUser.SalaryRate;
        var newSalaryRate = new Rate();
        PropertySetter.SetProperties<RateRecord, Rate>(ref r, ref newSalaryRate);

        // Communication Means

        // todo: error: email already exists.
        // todo: error: phone already exists.

        var newCommunicationMeans = builtUser.Communication!.Select(
            communicationMeanRecord => new CommunicationMean
            {
                Id = Guid.NewGuid(),
                Email = communicationMeanRecord.Email,
                Phone = communicationMeanRecord.Phone
            }).ToList();

        // User
        var newUserRec = new User
        {
            Login = "",
            Password = "",
            FirstName = "",
            LastName = "",
            Branches = existingBranches,
            SalaryRate = [newSalaryRate],
            Communication = newCommunicationMeans
        };

        PropertySetter.SetProperties<UserRecord, User>(ref builtUser, ref newUserRec);

        db.Users.Add(newUserRec);
        db.SaveChanges();

        return DataModelOperationResult.Successful;
    }


    /// <summary>
    /// <para>
    /// Returns a list of users.
    /// </para>
    /// <para>
    /// By default, a selection query result contains only "active" Operators, who not marked as "removed".
    /// </para>
    /// </summary>
    /// <param name="readFlags">
    /// <see cref="RecordReadFlags"/>
    /// </param>
    public override object Read<TDataModel>(RecordReadFlags readFlags)
    {
        // todo: error: empty flags. None - invalid flag

        var isOperatorActive = readFlags.HasFlag(RecordReadFlags.ActiveRecords);
        var isReadAllRecords = readFlags.HasFlag(RecordReadFlags.ActiveRecords) &
                               readFlags.HasFlag(RecordReadFlags.InactiveRecords);

        object result;

        // todo: settings for the current branch id.
        var currentBranchId = new Guid("6D074317-4514-4444-AF39-0A65F4A4BE05");

        using (var db = new DbmsService())
        {
            var query = db.Users
                // Selects only those Operators who work in the current branch.
                .Where(user => user.Branches.Any(branch => branch.Id.Equals(currentBranchId)))
                .Include(user => user.Communication)
                .Select(user => new
                {
                    User = user,
                    // The list of wage rates is filtered so that the most recent rate is included in the sample.
                    LatestSalaryRate = user.SalaryRate!.OrderByDescending(sr => sr.EndDate).First()
                });

            if (!isReadAllRecords)
                query = query.Where(user => user.User.IsActive.Equals(isOperatorActive));

            result = query.ToList()
                .Select(user =>
                {
                    user.User.SalaryRate = new List<Rate> { user.LatestSalaryRate };
                    return user.User;
                })
                .ToList();
        }

        return (List<TDataModel>)result;
    }

    public override DataModelOperationResult Delete<T>(RecordBuilderBase<T> builder)
    {
        // todo: error: user.Id == null

        // todo: Delete conditions
        // Operator cannot be deleted if it is referenced in the Wash data model.

        // todo: If a record cannot be removed then mark it as "deleted".

        using var db = new DbmsService();

        var removeUser = db.Users.Find(builder.Build().Id);

        if (removeUser == null) return DataModelOperationResult.RecordNotFound;

        db.Users.Remove(removeUser);
        db.SaveChanges();

        return DataModelOperationResult.Successful;
    }

    public override DataModelOperationResult DeleteMany<T>(List<RecordBuilderBase<T>> builder)
    {
        var any = builder.Select(Delete).Contains(DataModelOperationResult.RecordNotFound);

        return any
            ? DataModelOperationResult.RecordNotFound
            : DataModelOperationResult.Successful;
    }

    public DataModelOperationResult Update<T>(RecordBuilderBase<T> builder) where T : RecordBase, new()
    {
        // using var db = new DbmsService();
        //
        // // UserRecord record = userRecord.;
        // UserRecord record = builder.Build();
        //
        // var updateUser = db.Users
        //     .Where(u => u.Id.Equals(record.Id))
        //     .Include(user => user.Communication);
        //
        // if (!updateUser.Any()) return DataModelOperationResult.RecordNotFound;
        //
        // // todo: refact: автоматизировать процесс присвоения данных для типа RecordBase.
        //
        // if (userRecord.Communication != null)
        //     for (var i = 0; i < updateUser.First().Communication.Count; i++)
        //     {
        //         var source = userRecord.Communication[i];
        //         var target = db.CommunicationMeans.Find(userRecord.Communication[i].Id);
        //
        //         if (target != null)
        //         {
        //             PropertySetter.SetProperties<CommunicationMeanRecord, CommunicationMean>(
        //                 ref source,
        //                 ref target,
        //                 true);
        //             db.CommunicationMeans.Update(target);
        //         }
        //         else
        //         {
        //             return DataModelOperationResult.RecordNotFound;
        //         }
        //     }
        //
        // if (userRecord.Branch != null)
        //     // No Operator may be transferred to another branch from the current branch.
        //     throw new NotImplementedException();
        //
        // // todo: implement
        // if (userRecord.SalaryRate != null)
        //     // todo: Условия возможности изменить ставку.
        //     // - Изменить можно только текущую ставку. Нельзя менять "старые" ставки.
        //     // - Ставку можно поменять, если Оператор не сделал ни одной мойки в период действия ставки. 
        //     // Если была сделана хотя бы одна мойка, ставку поменять нельзя. Можно добавить новую со сроком начала
        //     // действия с завтрашнего дня.
        //
        //     throw new NotImplementedException();
        //
        // // Updating fields of not UserRecord type.
        // var targetClass = updateUser.First();
        // PropertySetter.SetProperties<UserRecord, User>(ref userRecord, ref targetClass, true);
        //
        // db.Users.Update(targetClass);
        //
        // db.SaveChanges();
        //
        return DataModelOperationResult.Successful;
    }
}