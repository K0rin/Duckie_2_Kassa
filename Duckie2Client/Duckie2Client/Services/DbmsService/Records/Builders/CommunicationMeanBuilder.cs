using System;

namespace Duckie2Client.Services.DbmsService.Records.Builders;

public class CommunicationMeanBuilder : RecordBuilderBase<CommunicationMeanRecord>
{
    public void AddEmail(string value)
    {
        // todo: Validation

        GetProduct().Email = value;
    }

    public void AddPhone(string value)
    {
        // todo: Validation 

        GetProduct().Phone = value;
    }

    public void AddClienId(Guid value)
    {
        GetProduct().ClientId = value;
    }
}