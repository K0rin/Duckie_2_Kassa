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
        if (builder.Build() is not PriceTypeRecord builtPriceType) return DataModelOperationResult.RecordNotFound;

        var newPriceType = new PriceType();
        PropertySetter.SetProperties<PriceTypeRecord, PriceType>(ref builtPriceType, ref newPriceType);

        using var db = new DbmsService();
        return AddAndSave(db.PriceTypes, db, newPriceType);
    }
}