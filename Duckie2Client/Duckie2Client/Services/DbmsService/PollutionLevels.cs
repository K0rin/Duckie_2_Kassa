using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.DbmsService.Records;
using Duckie2Client.Services.DbmsService.Records.Builders;

namespace Duckie2Client.Services.DbmsService;

public class PollutionLevels : CrudOperationsBase
{
    public override object Read<TDataModel>(RecordReadFlags readFlags)
    {
        object result;

        using (var db = new DbmsService())
        {
            result = db.PollutionLevels.ToList();
        }

        return (List<TDataModel>)result;
    }

    public override DataModelOperationResult Create<T>(RecordBuilderBase<T> builder)
    {
        using var db = new DbmsService();

        var builtPollutionLevel = builder.Build() as PollutionLevelRecord;

        // Rate 

        var newRates = builtPollutionLevel?.Rates.Select(
            r => new Rate
            {
                Id = r.Id,
                EndDate = r.EndDate,
                StartDate = r.StartDate,
                Value = r.Value
            }
        ).ToList();

        // Pollution Level

        var newPollutionLevel = new PollutionLevel
        {
            Name = "",
            Rates = newRates
        };
        PropertySetter.SetProperties<PollutionLevelRecord, PollutionLevel>(
            ref builtPollutionLevel!,
            ref newPollutionLevel);


        db.PollutionLevels.Add(newPollutionLevel);
        // todo: Учитывать количество сделанных изменений. Если их 0, тогда, это ошибка.
        db.SaveChanges();

        return DataModelOperationResult.Successful;
    }
}