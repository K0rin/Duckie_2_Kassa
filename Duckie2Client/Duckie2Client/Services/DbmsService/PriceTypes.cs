using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.DbmsService.Records;
using Duckie2Client.Services.DbmsService.Records.Builders;

namespace Duckie2Client.Services.DbmsService;

public class PriceTypes : CrudOperationsBase
{
    public override object Read<TDataModel>(RecordReadFlags readFlags)
    {
        object result;

        using (var db = new DbmsService())
        {
            result = db.PriceTypes.ToList();
        }

        return (List<TDataModel>)result;
    }

    public override DataModelOperationResult Create<T>(RecordBuilderBase<T> builder)
    {
        using var db = new DbmsService();
        var builtPriceType = builder.Build() as PriceTypeRecord;

        var newPriceType = new PriceType();
        PropertySetter.SetProperties<PriceTypeRecord, PriceType>(ref builtPriceType!, ref newPriceType);

        db.PriceTypes.Add(newPriceType);
        db.SaveChanges();

        return DataModelOperationResult.Successful;
    }
}