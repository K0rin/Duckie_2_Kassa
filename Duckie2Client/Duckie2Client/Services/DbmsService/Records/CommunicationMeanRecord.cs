using System;

namespace Duckie2Client.Services.DbmsService.Records;

public class CommunicationMeanRecord : RecordBase
{
    public string Email { get; set; }
    public string Phone { get; set; }
    public Guid ClientId { get; set; }
}