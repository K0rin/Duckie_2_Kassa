using System.Collections.Generic;

namespace Duckie2Client.Services.DbmsService.Records;

public class CompanyRecord : RecordBase
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string? RegistrationNumber { get; set; }

    public List<VehicleRecord>? Vehicles { get; set; }
}