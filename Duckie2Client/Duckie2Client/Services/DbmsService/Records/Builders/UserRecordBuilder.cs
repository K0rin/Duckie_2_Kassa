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
        GetProduct().Login = value;
    }

    public void AddFirstName(string value)
    {
        GetProduct().FirstName = value;
    }

    public void AddLastName(string value)
    {
        GetProduct().LastName = value;
    }

    public void AddRegistrationDate(DateOnly value)
    {
        GetProduct().RegistrationDate = value;
    }

    public void AddPassword(string? value)
    {
        // todo: check for null
        GetProduct().Password = value;
    }
}