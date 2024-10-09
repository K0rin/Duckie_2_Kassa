using System;
using System.Collections.Generic;

namespace Duckie2Client.Services.DbmsService.Records;

// todo: info: имена атрибутов должны совпадать с именами атрибутов соответствующей модели данных.
public class UserRecord : RecordBase
{
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public List<CommunicationMeanRecord>? CommunicationMeans { get; set; }
    public bool? IsStaff { get; set; }
    public DateOnly? RegistrationDate { get; set; }
    public List<RateRecord>? SalaryRates { get; set; }
    public List<BranchRecord>? Branch { get; set; }
}