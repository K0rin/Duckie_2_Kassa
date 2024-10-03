using System;

namespace Duckie2Client.Services.DbmsService.Records.Builders;

public class ClientBonusRecordBuilder : RecordBuilderBase<ClientBonusRecord>
{
    public void AddSumma(decimal value)
    {
        GetProduct().Summa = value;
    }

    public void AddEndDateTime(DateTime value)
    {
        GetProduct().EndDateTime = value;
    }
}