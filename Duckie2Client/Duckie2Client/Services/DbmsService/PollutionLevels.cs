using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.DbmsService.Libs;
using Duckie2Client.Services.DbmsService.Records;
using Duckie2Client.Services.DbmsService.Records.Builders;

namespace Duckie2Client.Services.DbmsService;

public class PollutionLevels : CrudOperationsBase
{
    public override DataModelOperationResult Create<T>(RecordBuilderBase<T> builder)
    {
        if (builder.Build() is not PollutionLevelRecord builtPollutionLevel)
            return DataModelOperationResult.RecordNotFound;

        var newRates = RatesHelper.CreateRateList(builtPollutionLevel);
        var newPollutionLevel = CreatePollutionLevel(builtPollutionLevel, newRates);

        var db = new DbmsService();
        return AddAndSave(db.PollutionLevels, db, newPollutionLevel);
    }

    private static PollutionLevel CreatePollutionLevel(PollutionLevelRecord record, List<Rate> newRates)
    {
        var pollutionLevel = new PollutionLevel
        {
            Name = string.Empty,
            Rates = newRates
        };
        PropertySetter.SetProperties<PollutionLevelRecord, PollutionLevel>(ref record, ref pollutionLevel);
        return pollutionLevel;
    }

    public override object Read<TDataModel>(RecordReadFlags readFlags)
    {
        object result;

        using (var db = new DbmsService())
        {
            result = db.PollutionLevels.ToList();
        }

        return (List<TDataModel>)result;
    }
}