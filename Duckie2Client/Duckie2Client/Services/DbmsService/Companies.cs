using System;
using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.DbmsService.Records;
using Duckie2Client.Services.DbmsService.Records.Builders;

namespace Duckie2Client.Services.DbmsService;

public class Companies : CrudOperationsBase
{
    public override object Read<TDataModel>(RecordReadFlags readFlags)
    {
        object result;

        using (var db = new DbmsService())
        {
            result = db.Companies.ToList();
        }

        return (List<TDataModel>)result;
    }

    public override DataModelOperationResult Create<T>(RecordBuilderBase<T> builder)
    {
        using var db = new DbmsService();

        var builtCompany = builder.Build() as CompanyRecord;

        // vehicle
        // todo: refact: функция добавления транспорта

        var newCompanyVehicles = new List<Vehicle>();

        foreach (var vehicle in builtCompany?.Vehicles!)
        {
            PriceType existingPriceType;

            try
            {
                existingPriceType = db.PriceTypes.First(pt => pt.Id.Equals(vehicle.PriceType.Id));
            }
            catch (Exception e) when (e is ArgumentNullException or InvalidOperationException)
            {
                return DataModelOperationResult.RecordNotFound;
            }

            var newVehicle = new Vehicle
            {
                Licence = null!,
                PriceType = existingPriceType
            };
            var vehicleRecord = vehicle;

            PropertySetter.SetProperties<VehicleRecord, Vehicle>(ref vehicleRecord, ref newVehicle);
            newCompanyVehicles.Add(newVehicle);
        }

        var newCompany = new Company
        {
            Name = "",
            Address = "",
            Vehicles = newCompanyVehicles
        };

        PropertySetter.SetProperties<CompanyRecord, Company>(ref builtCompany!, ref newCompany);

        db.Companies.Add(newCompany);
        // todo: Учитывать количество сделанных изменений. Если их 0, тогда, это ошибка.
        db.SaveChanges();

        return DataModelOperationResult.Successful;
    }
}