using System;
using System.Collections.Generic;
using Avalonia.X11.Interop;

namespace Duckie2Client.Services.DbmsService.Records.Builders;

public class CommunicationMeanBuilder : RecordBuilderBase<CommunicationMeanRecord>
{
    public void AddEmail(string value)
    {
        GetProduct().Email = value;
    }

    public void AddPhone(string value)
    {
        GetProduct().Phone = value;
    }

    public void AddClienId(Guid value)
    {
        GetProduct().ClientId = value;
    }
    
}