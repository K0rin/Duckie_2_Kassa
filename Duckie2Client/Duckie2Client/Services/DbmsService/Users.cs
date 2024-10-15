using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Libs.SecretStrings;
using Duckie2Client.Models;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.DbmsService.Libs;
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
        if (builder.Build() is not UserRecord builtUser) return DataModelOperationResult.RecordNotFound;

        var newSalaryRate = RatesHelper.CreateRateList(builtUser);
        var newCommunicationMeans = CreateCommunicationMeanList(builtUser!);

        using var db = new DbmsService();

        var existingBranches = GetExistingBranchList(builtUser, db);
        var newUserRec = CreateUser(builtUser, existingBranches, newSalaryRate, newCommunicationMeans);

        return AddAndSave(db.Users, db, newUserRec);
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

        // todo: settings for the current branch id.
        // зависимость. аргумент метода.
        var currentBranchId = new Guid("7EFF09D9-D11E-4B21-A7A6-AC294F29C382");

        using var db = new DbmsService();
        var query = db.Users
            // Selects only those Operators who work in the current branch.
            .Where(user => user.Branches.Any(branch => branch.Id.Equals(currentBranchId)))
            .Include(user => user.Communication)
            .Select(user => new
            {
                User = user,
                // The list of wage rates is filtered so that the most recent rate is included in the sample.
                LatestSalaryRate = user.SalaryRates!.OrderByDescending(sr => sr.EndDate).First()
            });

        if (!isReadAllRecords)
            query = query.Where(user => user.User.IsActive.Equals(isOperatorActive));

        object result = query.ToList()
            .Select(user =>
            {
                user.User.SalaryRates = new List<Rate> { user.LatestSalaryRate };
                return user.User;
            })
            .ToList();

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

    // todo: refact: generic
    private static List<CommunicationMean> CreateCommunicationMeanList(UserRecord record)
    {
        // todo: error: email already exists.
        // todo: error: phone already exists.

        var newCommunicationMeans = new List<CommunicationMean>();

        foreach (var communicationMean in record.CommunicationMeans!)
        {
            var newCommunicationMean = new CommunicationMean();
            var communicationMeanRecord = communicationMean;
            PropertySetter.SetProperties<CommunicationMeanRecord, CommunicationMean>(
                ref communicationMeanRecord,
                ref newCommunicationMean);
            newCommunicationMeans.Add(newCommunicationMean);
        }

        return newCommunicationMeans;
    }

    // ReSharper disable once MemberCanBeMadeStatic.Local
#pragma warning disable CA1822
    private List<Branch> GetExistingBranchList(UserRecord? builtUser, DbmsService db)
#pragma warning restore CA1822
    {
        // todo: error: record not found.
        // todo: async tasks: Branch getting

        var existingBranches = new List<Branch>();
        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (var branchRecord in builtUser!.Branch!)
            existingBranches.Add(db.Branches.First(branch => branch.Id.Equals(branchRecord.Id)));
        return existingBranches;
    }

    private static User CreateUser(
        UserRecord? record,
        List<Branch> existingBranches,
        List<Rate> rates,
        List<CommunicationMean> communicationMeans)
    {
        var newUserRec = new User
        {
            Login = "",
            Password = "",
            FirstName = "",
            LastName = "",
            Branches = existingBranches,
            SalaryRates = rates,
            Communication = communicationMeans
        };

        PropertySetter.SetProperties<UserRecord, User>(ref record!, ref newUserRec);
        return newUserRec;
    }

    public static LoginUserRecord? CheckLoginandPassword(string login, string inputtedPassword)
    {
        LoginUserRecord? returnResult = null;

        using var db = new DbmsService();

        var foundUser = db.Users
            .Select(p => new User
            {
                Id = p.Id,
                Login = p.Login,
                Password = p.Password,
                LastName = p.LastName,
                FirstName = p.FirstName
            })
            .FirstOrDefault(u => u.Login == login);

        if (foundUser == null) return returnResult;

        var securePassword = new SecureString();
        foreach (var c in inputtedPassword) securePassword.AppendChar(c);
        securePassword.MakeReadOnly();

        using var sha256 = SHA256.Create();

        var hashedPassword = SecretStrings.GetHash(securePassword, sha256);
        var isEquals = SecretStrings.VerifyHash(inputtedPassword, foundUser.Password, sha256);

        // Если пользователь авторизован успешно - присвоить переменной его запись.
        if (isEquals) returnResult = new LoginUserRecord(foundUser.Id, GetUserInitials(foundUser));

        return returnResult;
    }

    private static string GetUserInitials(User user)
    {
        var firstName = user.FirstName.ToCharArray()[0].ToString().ToUpper();
        var lastName = user.LastName.ToCharArray()[0].ToString().ToUpper();
        return $"{firstName}{lastName}";
    }
}