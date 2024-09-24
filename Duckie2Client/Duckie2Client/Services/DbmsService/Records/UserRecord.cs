using System;
using System.Collections.Generic;

namespace Duckie2Client.Services.DbmsService.Records;

public class UserRecord : RecordBase
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<CommunicationMeanRecord> Communication { get; set; }
    public bool IsStaff { get; set; }
    public DateOnly RegistrationDate { get; set; }
    public RateRecord SalaryRate { get; set; }
    public BranchRecord Branch { get; set; }
}