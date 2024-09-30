using System;

namespace Duckie2Client.Services.DbmsService.Records.Builders;

public class UserRecordBuilder : RecordBuilderBase<UserRecord>
{
    public void AddBranch(Guid existingBranchId)
    {
        var branch = new BranchRecord
        {
            Id = existingBranchId
        };
        GetProduct().Branch = [branch];
    }

    public void AddCommunication(CommunicationMeanBuilder value)
    {
        value.AddClienId(GetProduct().Id);
        GetProduct().Communication = [value.Build()];
    }

    public void AddSalaryRate(SalaryRateBuilder value)
    {
        GetProduct().SalaryRate = value.Build();
    }

    public void AddIsStaff(bool value)
    {
        GetProduct().IsStaff = value;
    }

    public void AddLogin(string value)
    {
        // todo: Validate

        GetProduct().Login = value;
    }

    public void AddFirstName(string value)
    {
        // todo: Validate 

        GetProduct().FirstName = value;
    }

    public void AddLastName(string value)
    {
        // todo: Validate 

        GetProduct().LastName = value;
    }

    public void AddRegistrationDate(DateOnly value)
    {
        // todo: check: must be today day. Not tomorrow or yesterday.

        GetProduct().RegistrationDate = value;
    }

    public void AddPassword(string? value)
    {
        // todo: check for null
        GetProduct().Password = value;
    }
}