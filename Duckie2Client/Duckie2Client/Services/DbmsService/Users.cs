using System;
using System.Collections.Generic;
using System.Linq;
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
            //
            // var newSalaryRate = new UserSalaryRate
            // {
            //     Id = Guid.NewGuid(),
            //     Rate = newRate,
            //     User = newUser
            // };

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
    /// Read data from the User data model.
    /// </para>
    /// <para>
    /// By default, a selection query result contains only "active" Operators, who not marked as "removed".
    /// </para>
    /// </summary>
    /// <param name="users">The list of users fetched from the data model.</param>
    /// <param name="isReadInactiveOnly">
    /// <c>True</c> - read records marked as "removed". <c>False </c> (default) - read "active" records only.
    /// </param>
    public static void Read(out List<User> users, bool isReadInactiveOnly = false)
    {
        // ReSharper disable once ConvertToUsingDeclaration
        using (var db = new DbmsService())
        {
            users = db.Users
                .Where(user => user.IsActive.Equals(!isReadInactiveOnly))
                .Include(user => user.Communication)
                .Include(user => user.Branches)
                .ToList();
        }
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