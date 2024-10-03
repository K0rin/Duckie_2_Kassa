using System;
using System.Collections.Generic;

namespace Duckie2Client.Services.DbmsService.Records;

public class ClientRecord : RecordBase
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Notes { get; set; }
    public DateTime FirstRegistration { get; set; }

    public List<CommunicationMeanRecord> CommunicationMeans { get; set; }
    public ClientBonusRecord Bonus { get; set; }
    public List<VehicleRecord> Vehicles { get; set; }
}