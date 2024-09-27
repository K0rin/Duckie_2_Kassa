using System;
using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Enums;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.DbmsService.Records;
using Microsoft.EntityFrameworkCore;

namespace Duckie2Client.Services.DbmsService;

/// <summary>
/// Contains functionality for working with the "User" database entity.
/// </summary>
public class Users
{
    public void Create(UserRecord user)
    {
        // ReSharper disable once ConvertToUsingDeclaration
        using (var db = new DbmsService())
        {
            // todo: async tasks: Branch getting

            // Branch

            // todo: error: record not found.
            var existingBranches = user.Branch.Select(
                branchRecord => db.Branches.First(
                    branch => branch.Id.Equals(branchRecord.Id))
            ).ToList();

            // Salary Rate

            var r = user.SalaryRate;
            var newSalaryRate = new Rate();
            PropertySetter.SetProperties<RateRecord, Rate>(ref r, ref newSalaryRate);

            // Communication Means

            // todo: error: email already exists.
            // todo: error: phone already exists.

            var newCommunicationMeans = user.Communication.Select(
                communicationMeanRecord => new CommunicationMean
                {
                    Id = Guid.NewGuid(),
                    Email = communicationMeanRecord.Email,
                    Phone = communicationMeanRecord.Phone
                }).ToList();

            // User
            var newUser = new User
            {
                Login = "",
                Password = "",
                FirstName = "",
                LastName = "",
                Branches = existingBranches,
                SalaryRate = [newSalaryRate],
                Communication = newCommunicationMeans
            };

            PropertySetter.SetProperties<UserRecord, User>(ref user, ref newUser);

            db.Users.Add(newUser);
            db.SaveChanges();
        }
    }

    /// <summary>
    /// <para>
    /// Returns a list of users.
    /// </para>
    /// <para>
    /// By default, a selection query result contains only "active" Operators, who not marked as "removed".
    /// </para>
    /// </summary>
    /// <param name="isReadInactiveOnly">
    /// <c>True</c> - read records marked as "removed". <c>False </c> (default) - read "active" records only.
    /// </param>
    /// <param name="readFlags"></param>
    public static List<User> Read(RecordReadFlags readFlags)
    {
        // todo: error: empty flags. None - invalid flag

        var isOperatorActive = readFlags.HasFlag(RecordReadFlags.ActiveRecords);
        var isReadAllRecords = readFlags.HasFlag(RecordReadFlags.ActiveRecords) &
                               readFlags.HasFlag(RecordReadFlags.InactiveRecords);
        List<User> result;

        // todo: settings for the current branch id.
        var currentBranchId = new Guid("6D074317-4514-4444-AF39-0A65F4A4BE05");

        // ReSharper disable once ConvertToUsingDeclaration
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

        return result;
    }

    public void Delete()
    {
        // todo: Delete conditions
        // Operator cannot be deleted if it is referenced in the Wash data model.

        // ReSharper disable once ConvertToUsingDeclaration
        using (var db = new DbmsService())
        {
            var user = db.Users.FirstOrDefault();
            if (user == null) return;
            db.Users.Remove(user);
            db.SaveChanges();
        }
    }

    public void Update()
    {
    }
}