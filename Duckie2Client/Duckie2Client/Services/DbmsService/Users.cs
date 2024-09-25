using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.DbmsService.Records;

namespace Duckie2Client.Services.DbmsService;

/// <summary>
/// Contains functionality for working with the "User" database entity.
/// </summary>
public class Users
{
    private DbmsService db = new();

    public void Create(UserRecord user)
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var newUser = new User
        {
            Login = null,
            Password = null,
            FirstName = null,
            LastName = null,
            Branch = null
        };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        PropertySetter.SetProperties<UserRecord, User>(ref user, ref newUser);

        // Salary Rate

        var newRate = new Rate();
        var r = user.SalaryRate;
        PropertySetter.SetProperties<RateRecord, Rate>(ref r, ref newRate);
        newUser.SalaryRate = newRate;

        // Branch

        var existingBranch = db.Branches.Find(new Guid("6D074317-4514-4444-AF39-0A65F4A4BE05"));
        // todo: error: record not found.
        newUser.Branch = existingBranch;

        // Communication Means

        // todo: error: email already exists.
        // todo: error: phone already exists.

        var newCommunicationMeans = user.Communication.Select(
            communicationMeanRecord => new CommunicationMean
            {
                Id = Guid.NewGuid(),
                Email = communicationMeanRecord.Email,
                Phone = communicationMeanRecord.Phone,
                User = newUser
            }).ToList();
        newUser.Communication = newCommunicationMeans;

        db.Users.Add(newUser);
        db.SaveChanges();
    }

    public void Read()
    {
    }

    public void Delete()
    {
    }

    public void Update()
    {
    }
}